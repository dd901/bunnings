using System.Collections.Generic;

namespace Bunnings.Entities
{
    public interface ICompany
    {
        string Name { get; }
        IEnumerable<Catalog> Catalogs { get; }
        IEnumerable<SupplierProductBarcode> SupplierProductBarcodes { get; }
    }
}