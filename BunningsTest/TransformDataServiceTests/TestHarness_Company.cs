using System.Collections.Generic;
using Bunnings.Entities;

namespace BunningsTest.TransformDataServiceTests
{
    public class TestHarness_Company
    {
        private readonly Company company;

        public TestHarness_Company(string companyName)
        {
            company = new Company(companyName, new List<Catalog>(), new List<SupplierProductBarcode>());
        }

        public TestHarness_Company With_SupplierProductBarcode(int SupplierID, string SKU, string Barcode)
        {
            company.AddSupplierProductBarcode(SupplierID, SKU, Barcode);
            return this;
        }

        public TestHarness_Company With_Catalog(string sku, string description)
        {
            company.AddCatalog(sku, description);
            return this;
        }

        public Company Build()
        {
            return company;
        }
    }
}