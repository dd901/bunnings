using System.Collections.Generic;
using Bunnings.Entities;

namespace BunningsTest.TransformDataServiceTests
{
    public class TestHarnessCompany
    {
        private readonly Company _company;

        public TestHarnessCompany(string companyName)
        {
            _company = new Company(companyName, new List<Catalog>(), new List<SupplierProductBarcode>());
        }

        public TestHarnessCompany WithSupplierProductBarcode(int supplierId, string sku, string barcode)
        {
            _company.AddSupplierProductBarcode(supplierId, sku, barcode);
            return this;
        }

        public TestHarnessCompany WithCatalog(string sku, string description)
        {
            _company.AddCatalog(sku, description);
            return this;
        }

        public Company Build()
        {
            return _company;
        }
    }
}