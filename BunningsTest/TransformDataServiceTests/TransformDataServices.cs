using System.Collections.Generic;
using System.Linq;
using Bunnings;
using Bunnings.Entities;
using NUnit.Framework;

namespace BunningsTest.TransformDataServiceTests
{
    public class TransFormDataServicesTest
    {
        public class GetDuplicateSupplierProductBarcodesFromCompanyB
        {
            [Test]
            public void Given_No_Duplicate_Barcodes_Then_Then_Return_Empty_Result()
            {
                //arrange
                var companyA = new TestHarness_Company("A")
                    .With_SupplierProductBarcode(1, "a", "1")
                    .Build();

                var companyB = new TestHarness_Company("B")
                    .With_SupplierProductBarcode(2, "b", "2")
                    .Build();

                //act
                var duplicateSupplierProductBarcodesFromCompanyB = new TransformDataService().GetDuplicateSupplierProductBarcodesFromCompanyB(companyA, companyB);

                //arrange
                Assert.IsEmpty(duplicateSupplierProductBarcodesFromCompanyB);
            }

            [Test]
            public void Given_Multiple_No_Duplicate_Barcodes_Then_Then_Return_Empty_Result()
            {
                //arrange
                var companyA = new TestHarness_Company("A")
                    .With_SupplierProductBarcode(1, "a", "1")
                    .With_SupplierProductBarcode(1, "a", "2")
                    .With_SupplierProductBarcode(1, "a", "3")
                    .Build();

                var companyB = new TestHarness_Company("B")
                    .With_SupplierProductBarcode(1, "b", "4")
                    .With_SupplierProductBarcode(1, "b", "5")
                    .With_SupplierProductBarcode(1, "b", "6")
                    .Build();

                //act
                var duplicateSPBromCompanyB = new TransformDataService().GetDuplicateSupplierProductBarcodesFromCompanyB(companyA, companyB);

                //arrange
                Assert.IsEmpty(duplicateSPBromCompanyB);
            }

            [Test]
            public void Given_One_Duplicate_Barcodes_Then_Return_The_Matching_SPB_In_CompanyB()
            {
                //arrange
                var barcodeDuplicate = "1";
                var skuForCompanyB = "b";
                var companyA = new TestHarness_Company("A")
                    .With_SupplierProductBarcode(1, "a", barcodeDuplicate)
                    .Build();

                var companyB = new TestHarness_Company("B")
                    .With_SupplierProductBarcode(2, skuForCompanyB, barcodeDuplicate)
                    .Build();

                //act
                var duplicateSPBFromCompanyB = new TransformDataService().GetDuplicateSupplierProductBarcodesFromCompanyB(companyA, companyB);

                //arrange
                Assert.IsTrue(duplicateSPBFromCompanyB.Count() == 1);
                Assert.IsTrue(duplicateSPBFromCompanyB.All(x => x.Barcode == barcodeDuplicate && x.SupplierID == 2 && x.SKU == skuForCompanyB));
            }

            [Test]
            public void Given_Two_Duplicate_Barcodes_Then_Return_The_Matching_SPB_In_CompanyB()
            {
                //arrange
                var barcodeDuplicateFirst = "1";
                var barcodeDuplicateSecond = "2";
                var skuForCompanyB = "b";
                var companyA = new TestHarness_Company("A")
                    .With_SupplierProductBarcode(1, "a", barcodeDuplicateFirst)
                    .With_SupplierProductBarcode(1, "a", barcodeDuplicateSecond)
                    .Build();

                var companyB = new TestHarness_Company("B")
                    .With_SupplierProductBarcode(2, skuForCompanyB, barcodeDuplicateFirst)
                    .With_SupplierProductBarcode(2, skuForCompanyB, barcodeDuplicateSecond)
                    .Build();

                //act
                var duplicateSPBFromCompanyB = new TransformDataService().GetDuplicateSupplierProductBarcodesFromCompanyB(companyA, companyB);

                //arrange
                Assert.IsTrue(duplicateSPBFromCompanyB.Count() == 2);
                Assert.IsTrue(duplicateSPBFromCompanyB.Any(x => x.Barcode == barcodeDuplicateFirst && x.SupplierID == 2 && x.SKU == skuForCompanyB));
                Assert.IsTrue(duplicateSPBFromCompanyB.Any(x => x.Barcode == barcodeDuplicateSecond && x.SupplierID == 2 && x.SKU == skuForCompanyB));
            }

            [Test]
            public void Given_Two_Unique_And_Two_Duplicate_Barcodes_Then_Return_The_Matching_SPB_In_CompanyB()
            {
                //arrange
                var barcodeDuplicateFirst = "1";
                var barcodeDuplicateSecond = "2";
                var skuForCompanyB = "b";
                var companyA = new TestHarness_Company("A")
                    .With_SupplierProductBarcode(1, "a", barcodeDuplicateFirst)
                    .With_SupplierProductBarcode(1, "a", barcodeDuplicateSecond)
                    .With_SupplierProductBarcode(1, "a", "3")
                    .With_SupplierProductBarcode(1, "a", "4")
                    .Build();

                var companyB = new TestHarness_Company("B")
                    .With_SupplierProductBarcode(2, skuForCompanyB, barcodeDuplicateFirst)
                    .With_SupplierProductBarcode(2, skuForCompanyB, barcodeDuplicateSecond)
                    .With_SupplierProductBarcode(2, skuForCompanyB, "6")
                    .With_SupplierProductBarcode(2, skuForCompanyB, "5")
                    .Build();

                //act
                var duplicateSPBFromCompanyB = new TransformDataService().GetDuplicateSupplierProductBarcodesFromCompanyB(companyA, companyB);

                //arrange
                Assert.IsTrue(duplicateSPBFromCompanyB.Count() == 2);
                Assert.IsTrue(duplicateSPBFromCompanyB.Any(x => x.Barcode == barcodeDuplicateFirst && x.SupplierID == 2 && x.SKU == skuForCompanyB));
                Assert.IsTrue(duplicateSPBFromCompanyB.Any(x => x.Barcode == barcodeDuplicateSecond && x.SupplierID == 2 && x.SKU == skuForCompanyB));
            }
        }

        public class GetCompanyBSKUWhichHaveDuplicateBarcodesInCompanyA
        {
            [Test]
            public void Given_No_Duplicate_Barcodes_Then_Then_Return_Empty_Result()
            {
                //arrange
                var companyA = new TestHarness_Company("A")
                    .With_SupplierProductBarcode(1, "a", "1")
                    .Build();

                var companyB = new TestHarness_Company("B")
                    .With_SupplierProductBarcode(2, "b", "2")
                    .Build();

                //act
                var companyBskuWhichHaveDuplicateBarcodesInCompanyA = new TransformDataService().GetCompanyBSKUWhichHaveDuplicateBarcodesInCompanyA(companyA, companyB);

                //arrange
                Assert.IsEmpty(companyBskuWhichHaveDuplicateBarcodesInCompanyA);
            }

            [Test]
            public void Given_Two_Duplicate_Barcodes_From_same_SKU_Then_Return_That_SKU()
            {
                //arrange
                var barcodeDuplicateFirst = "1";
                var barcodeDuplicateSecond = "2";
                var skuForCompanyB = "aa-bb";
                var companyA = new TestHarness_Company("A")
                    .With_SupplierProductBarcode(1, "a", barcodeDuplicateFirst)
                    .With_SupplierProductBarcode(1, "a", barcodeDuplicateSecond)
                    .Build();

                var companyB = new TestHarness_Company("B")
                    .With_SupplierProductBarcode(2, skuForCompanyB, barcodeDuplicateFirst)
                    .With_SupplierProductBarcode(2, skuForCompanyB, barcodeDuplicateSecond)
                    .Build();

                //act
                var companyBskuWhichHaveDuplicateBarcodesInCompanyA = new TransformDataService().GetCompanyBSKUWhichHaveDuplicateBarcodesInCompanyA(companyA, companyB);

                //arrange
                Assert.IsTrue(companyBskuWhichHaveDuplicateBarcodesInCompanyA.Count() == 1);
                Assert.IsTrue(companyBskuWhichHaveDuplicateBarcodesInCompanyA.All(x => x.SKU == skuForCompanyB));
            }

            [Test]
            public void Given_Two_Duplicate_Barcodes_From_Two_SKU_Then_Return_Only_THe_SKU_That_Have_Duplicates()
            {
                //arrange
                var barcodeDuplicateFirst = "1";
                var barcodeDuplicateSecond = "2";
                var skuInCompanyB_Second = "d";
                var skuInCompanyB_First = "c";
                var companyA = new TestHarness_Company("A")
                    .With_SupplierProductBarcode(1, "a", barcodeDuplicateFirst)
                    .With_SupplierProductBarcode(1, "b", barcodeDuplicateSecond)
                    .Build();


                var companyB = new TestHarness_Company("B")
                    .With_SupplierProductBarcode(2, skuInCompanyB_First, barcodeDuplicateFirst)
                    .With_SupplierProductBarcode(2, skuInCompanyB_Second, barcodeDuplicateSecond)
                    .Build();

                //act
                var companyBskuWhichHaveDuplicateBarcodesInCompanyA = new TransformDataService().GetCompanyBSKUWhichHaveDuplicateBarcodesInCompanyA(companyA, companyB);

                //arrange
                Assert.IsTrue(companyBskuWhichHaveDuplicateBarcodesInCompanyA.Count() == 2);
                Assert.IsTrue(companyBskuWhichHaveDuplicateBarcodesInCompanyA.Any(x => x.SKU == skuInCompanyB_First));
                Assert.IsTrue(companyBskuWhichHaveDuplicateBarcodesInCompanyA.Any(x => x.SKU == skuInCompanyB_Second));
            }
        }

        public class CreateCommonCatalog
        {
            [Test]
            public void Given_Two_Companies_With_No_Duplicate_Barcodes_Then_Return_Both_SPB()
            {
                //arrange
                var firstSKU = "aa";
                var descriptionOfFirstSKU = "description of aa";
                var secondSKU = "bb";
                var descriptionOfSecondSKU = "description of bb";
                var companyBName = "B";
                var companyAName = "A";

                var companyA = new TestHarness_Company(companyAName)
                    .With_SupplierProductBarcode(1, firstSKU, "1")
                    .With_Catalog(firstSKU, descriptionOfFirstSKU)
                    .Build();


                var companyB = new TestHarness_Company(companyBName)
                    .With_SupplierProductBarcode(2, secondSKU, "2")
                    .With_Catalog(secondSKU, descriptionOfSecondSKU)
                    .Build();

                IEnumerable<SupplierProductBarcode> duplicateSku = new List<SupplierProductBarcode>();
                //act
                var commonCatalog = new TransformDataService().CreateCommonCatalog(companyA, companyB, duplicateSku);

                //arrange
                Assert.IsTrue(commonCatalog.Count() == 2);
                Assert.IsTrue(commonCatalog.Any(x => x.Description == descriptionOfFirstSKU && x.Source == companyAName && x.SKU == firstSKU));
                Assert.IsTrue(commonCatalog.Any(x => x.Description == descriptionOfSecondSKU && x.Source == companyBName && x.SKU == secondSKU));
            }

            [Test]
            public void Given_Two_Companies_With_One_Duplicate_Barcodes_Then_The_Returned_CommonCatalog_Should_Contain_Descriptions_And_SKUs_And_CompanyName_Of_CompanyA()
            {
                //arrange
                var firstSKU = "aa";
                var descriptionOfFirstSKU = "description of aa";
                var secondSKU = "bb";
                var companyAName = "A";
                var duplicateBarcode = "1";

                var companyA = new TestHarness_Company(companyAName)
                    .With_SupplierProductBarcode(1, firstSKU, duplicateBarcode)
                    .With_Catalog(firstSKU, descriptionOfFirstSKU)
                    .Build();


                var companyB = new TestHarness_Company("B")
                    .With_SupplierProductBarcode(2, secondSKU, duplicateBarcode)
                    .With_Catalog(secondSKU, "description of bb")
                    .Build();

                //act
                IEnumerable<SupplierProductBarcode> duplicateSku = new List<SupplierProductBarcode>
                {
                    new()
                    {
                        Barcode = duplicateBarcode,
                        SKU = secondSKU,
                        SupplierID = 2
                    }
                };
                var commonCatalog = new TransformDataService().CreateCommonCatalog(companyA, companyB, duplicateSku);

                //arrange
                Assert.IsTrue(commonCatalog.Count() == 1);
                Assert.IsTrue(commonCatalog.Any(x => x.Description == descriptionOfFirstSKU && x.Source == companyAName && x.SKU == firstSKU));
            }

            [Test]
            public void Given_Two_Companies_With_Many_Duplicate_Barcodes_Then_The_Returned_CommonCatalog_Should_Contain_Descriptions_And_SKUs_And_CompanyName_Of_CompanyA()
            {
                //arrange
                var firstSKU = "aa";
                var secondSKU = "bb";
                var thirdSKU = "cc";
                var fourthSKU = "dd";
                var descriptionOfFirstSKU = "description of aa";
                var descriptionOfSecondSKU = "description of bb";
                var descriptionOfThirdSKU = "description of cc";
                var descriptionOfFourthSKU = "description of ddd";

                var companyAName = "A";
                var FirstDuplicateBarcode = "1";
                var SecondDuplicateBarcode = "2";

                var companyA = new TestHarness_Company(companyAName)
                    .With_SupplierProductBarcode(1, firstSKU, FirstDuplicateBarcode)
                    .With_Catalog(firstSKU, descriptionOfFirstSKU)
                    .With_SupplierProductBarcode(1, secondSKU, SecondDuplicateBarcode)
                    .With_Catalog(secondSKU, descriptionOfSecondSKU)
                    .Build();


                var companyB = new TestHarness_Company("B")
                    .With_SupplierProductBarcode(2, thirdSKU, FirstDuplicateBarcode)
                    .With_Catalog(thirdSKU, descriptionOfThirdSKU)
                    .With_SupplierProductBarcode(2, fourthSKU, SecondDuplicateBarcode)
                    .With_Catalog(fourthSKU, descriptionOfFourthSKU)
                    .Build();

                //act
                IEnumerable<SupplierProductBarcode> duplicateSku = new List<SupplierProductBarcode>
                {
                    new()
                    {
                        Barcode = FirstDuplicateBarcode,
                        SKU = thirdSKU,
                        SupplierID = 2
                    },
                    new()
                    {
                        Barcode = FirstDuplicateBarcode,
                        SKU = fourthSKU,
                        SupplierID = 2
                    }
                };
                var commonCatalog = new TransformDataService().CreateCommonCatalog(companyA, companyB, duplicateSku);

                //arrange
                Assert.IsTrue(commonCatalog.Count() == 2);
                Assert.IsTrue(commonCatalog.Any(x => x.Description == descriptionOfFirstSKU && x.Source == companyAName && x.SKU == firstSKU));
                Assert.IsTrue(commonCatalog.Any(x => x.Description == descriptionOfSecondSKU && x.Source == companyAName && x.SKU == secondSKU));
            }

            [Test]
            public void Given_Two_Companies_With_Duplicate_and_Unique_Barcodes_The_Returned_CommonCatalog_Should_Contain_Descriptions_And_SKUs_And_CompanyName_Of_CompanyA_For_The_Duplicates_And_Unique_Barcodes_Should_Be_Represented_By_Their_Respective_Company()
            {
                //arrange
                var firstSKU = "aa";
                var secondSKU = "bb";
                var thirdSKU = "cc";
                var fourthSKU = "dd";
                var descriptionOfFirstSKU = "description of aa";
                var descriptionOfSecondSKU = "description of bb";
                var descriptionOfThirdSKU = "description of cc";
                var descriptionOfFourthSKU = "description of ddd";

                var companyBName = "B";
                var companyAName = "A";
                var FirstDuplicateBarcode = "1";
                var SecondUniqueBarcode = "2";
                var ThirdUniqueBarcode = "2";

                var companyA = new TestHarness_Company(companyAName)
                    .With_SupplierProductBarcode(1, firstSKU, FirstDuplicateBarcode)
                    .With_Catalog(firstSKU, descriptionOfFirstSKU)
                    .With_SupplierProductBarcode(1, secondSKU, SecondUniqueBarcode)
                    .With_Catalog(secondSKU, descriptionOfSecondSKU)
                    .Build();


                var companyB = new TestHarness_Company(companyBName)
                    .With_SupplierProductBarcode(2, thirdSKU, FirstDuplicateBarcode)
                    .With_Catalog(thirdSKU, descriptionOfThirdSKU)
                    .With_SupplierProductBarcode(2, fourthSKU, ThirdUniqueBarcode)
                    .With_Catalog(fourthSKU, descriptionOfFourthSKU)
                    .Build();

                //act
                IEnumerable<SupplierProductBarcode> duplicateSku = new List<SupplierProductBarcode>
                {
                    new()
                    {
                        Barcode = FirstDuplicateBarcode,
                        SKU = thirdSKU,
                        SupplierID = 2
                    }
                };
                var commonCatalog = new TransformDataService().CreateCommonCatalog(companyA, companyB, duplicateSku);

                //arrange
                Assert.IsTrue(commonCatalog.Count() == 3);
                Assert.IsTrue(commonCatalog.Any(x => x.Description == descriptionOfFirstSKU && x.Source == companyAName && x.SKU == firstSKU));
                Assert.IsTrue(commonCatalog.Any(x => x.Description == descriptionOfSecondSKU && x.Source == companyAName && x.SKU == secondSKU));
                Assert.IsTrue(commonCatalog.Any(x => x.Description == descriptionOfFourthSKU && x.Source == companyBName && x.SKU == fourthSKU));
            }
        }
    }
}