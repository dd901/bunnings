using System.Collections.Generic;
using System.Linq;
using Bunnings;
using NUnit.Framework;

namespace BunningsTest.ValidationServiceTests
{
    public class VerificationServiceTest
    {

        public class ValidInputs
        {
            [Test]
            public void Given_Four_Arguments_That_Are_Valid_Files_Then_Should_Return_True()
            {
                //arrange
                var rawInput = new List<string> {"barcodesA.csv", "barcodesB.csv", "catalogA.csv", "catalogB.csv"};
                var input = rawInput.Select(x => $"ValidationServiceTests/Inputs/{x}").ToList();

                //Act
                var verificationService = new VerificationService();
                var isValidInput = verificationService.ValidInputs(input);

                //Assert
                Assert.IsTrue(isValidInput);
                Assert.IsNull(verificationService.Message);
            }

            [Test]
            public void Given_Four_Arguments_That_Are_Not_Valid_Files_Then_ValidInputs_Should_Return_False()
            {
                //arrange
                var rawInput = new List<string> {"doesNotExist.csv", "doesNotExist.csv", "doesNotExist.csv", "doesNotExist.csv"};
                var input = rawInput.Select(x => $"ValidationServiceTests/Inputs/{x}").ToList();

                //Act
                var verificationService = new VerificationService();
                var isValidInput = verificationService.ValidInputs(input);

                //Assert
                Assert.IsFalse(isValidInput);
                Assert.IsNotEmpty(verificationService.Message);
            }

            [Test]
            public void Given_Less_Than_Four_Arguments_Should_Return_False()
            {
                //arrange
                var input = new List<string> {"a", "b", "c"};

                //Act
                var verificationService = new VerificationService();
                var isValidInput = verificationService.ValidInputs(input);

                //Assert
                Assert.IsFalse(isValidInput);
                Assert.IsNotEmpty(verificationService.Message);
            }

            [Test]
            public void Given_More_Than_Four_Arguments_Should_Return_False()
            {
                //arrange
                var input = new List<string> {"a", "b", "c", "d", "e"};

                //Act
                var verificationService = new VerificationService();
                var isValidInput = verificationService.ValidInputs(input);

                //Assert
                Assert.IsFalse(isValidInput);
                Assert.IsNotEmpty(verificationService.Message);
            }
        }
    }
}