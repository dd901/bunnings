using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bunnings;
using Bunnings.Entities;
using CsvHelper.TypeConversion;
using NUnit.Framework;

namespace BunningsTest.CsvImportExportServiceTests
{
    public class CsvImportExportServiceTests
    {
        [Test]
        public void Given_Valid_Input_File_With_One_Row_When_I_Import_Then_Return_One_SupplierProductBarcode()
        {
            //arrange
            var fileToImport = @".\CsvImportExportServiceTests\Inputs\one_valid_supplierproductbarcode\barcodes.csv";

            //act
            var importedSupplierProductBarcodes = new CsvImportExportService().Import<SupplierProductBarcode>(fileToImport);

            //assert
            var importedSPB = importedSupplierProductBarcodes.ToList();
            Assert.IsTrue(importedSPB.Count() == 1);
            Assert.IsTrue(importedSPB.First().Barcode == "abc");
            Assert.IsTrue(importedSPB.First().SKU == "b");
            Assert.IsTrue(importedSPB.First().SupplierID == 1);
        }

        [Test]
        public void Given_Valid_Input_File_With_Four_Rows_When_I_Import_Then_Return_Four_SupplierProductBarcodes()
        {
            //arrange
            var fileToImport = @".\CsvImportExportServiceTests\Inputs\many_valid_supplierproductbarcode\barcodes.csv";

            //act
            var importedSupplierProductBarcodes = new CsvImportExportService().Import<SupplierProductBarcode>(fileToImport);

            //assert
            var importedSPB = importedSupplierProductBarcodes.ToList();
            Assert.IsTrue(importedSPB.Count() == 4);
            Assert.IsTrue(importedSPB.Any(x => x.Barcode == "abc" && x.SKU == "b" && x.SupplierID == 1));
            Assert.IsTrue(importedSPB.Any(x => x.Barcode == "def" && x.SKU == "bb" && x.SupplierID == 11));
            Assert.IsTrue(importedSPB.Any(x => x.Barcode == "ghi" && x.SKU == "bbb" && x.SupplierID == 111));
            Assert.IsTrue(importedSPB.Any(x => x.Barcode == "jkl" && x.SKU == "bbbb" && x.SupplierID == 1111));
        }

        [Test]
        public void Given_Invalid_Input_File_When_I_Import_Then_An_Exception_Should_Be_Thrown()
        {
            //arrange
            var fileToImport = @".\CsvImportExportServiceTests\Inputs\one_invalid_supplierproductbarcode\barcodes.csv";

            Assert.Throws<TypeConverterException>(() => new CsvImportExportService().Import<SupplierProductBarcode>(fileToImport));
        }

        [Test]
        public void Given_A_File_To_Export_When_Export_Then_Ensure_A_File_Has_Been_Created()
        {
            //arrange
            var commonCatalogs = new List<CommonCatalog>
            {
                new("1", "pots", "A"),
                new("2", "pans", "A"),
                new("3", "paper", "B"),
                new("4", "knife", "B"),
                new("5", "spoon", "B")
            };
            //act
            new CsvImportExportService().Export("export.csv", commonCatalogs);

            //assert
            Assert.IsTrue(File.Exists("export.csv"));
        }
    }
}