using System;
using System.Collections.Generic;
using Bunnings;
using Bunnings.Entities;
using Bunnings.Interfaces;
using Moq;
using NUnit.Framework;

namespace BunningsTest.AppTest
{
    public class AppTest
    {
        public class Run
        {
            [Test]
            public void Given_An_InValid_Input_Then_A_Exception_Is_Thrown()
            {
                //arrange
                var verificationServiceMock = new Mock<IVerificationService>();
                var csvImportExportServicesMock = new Mock<ICsvImportExportService>();
                var transformDataServicesMock = new Mock<ITransformDataService>();

                verificationServiceMock.Setup(x => x.ValidInputs(It.IsAny<List<string>>())).Returns(false);

                //act
                var app = new App(verificationServiceMock.Object, csvImportExportServicesMock.Object, transformDataServicesMock.Object);
                
                //assert
                Assert.Throws<Exception>(() => app.Run(new string[] { }));
            }

            [Test]
            public void Given_An_File_That_Cannot_Be_Imported_Then_A_Exception_Is_Thrown()
            {
                //arrange
                var verificationServiceMock = new Mock<IVerificationService>();
                var csvImportExportServicesMock = new Mock<ICsvImportExportService>();
                var transformDataServicesMock = new Mock<ITransformDataService>();

                verificationServiceMock.Setup(x => x.ValidInputs(It.IsAny<List<string>>())).Returns(true);

                csvImportExportServicesMock.Setup(x => x.Import<SupplierProductBarcode>(It.IsAny<string>())).Throws<Exception>();

                //act
                var app = new App(verificationServiceMock.Object, csvImportExportServicesMock.Object, transformDataServicesMock.Object);
                
                //assert
                Assert.Throws<Exception>(() => app.Run(new string[] { }));
            }
        }
    }
}