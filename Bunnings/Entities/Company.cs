using System.Collections.Generic;
using System.Linq;

namespace Bunnings.Entities
{
    public class Company : ICompany
    {
        public string Name { get; }

        public IEnumerable<Catalog> Catalogs { get; private set; }
        public IEnumerable<SupplierProductBarcode> SupplierProductBarcodes { get; private set; }
        
        public Company(string name, IEnumerable<Catalog> catalogs,
            IEnumerable<SupplierProductBarcode> supplierProductBarcodes)
        {
            //ValidateSupplierProductBarcodes(catalogs, supplierProductBarcodes, suppliers);

            Name = name;
            Catalogs = catalogs;
            SupplierProductBarcodes = supplierProductBarcodes;
        }

    

        public void AddSupplierProductBarcode(int supplierId, string sku, string barcode)
        {
            SupplierProductBarcodes = SupplierProductBarcodes.Append(new SupplierProductBarcode
            {
                SupplierID = supplierId,
                SKU = sku,
                Barcode = barcode
            });
        }

        public void AddCatalog(string sku, string description)
        {
            Catalogs = Catalogs.Append(new Catalog
            {
                SKU = sku,
                Description = description
            });
        }
    }
}