using System.Collections.Generic;
using System.Linq;
using Bunnings.Entities;
using Bunnings.Interfaces;

namespace Bunnings
{
    public class TransformDataService : ITransformDataService
    {
        public SupplierProductBarcode[] GetCompanyBSKUWhichHaveDuplicateBarcodesInCompanyA(Company barcodesA, Company barcodesB)
        {
            var duplicateSupplierProductBarcodes = GetDuplicateSupplierProductBarcodesFromCompanyB(barcodesA, barcodesB);

            var duplicateSupplierProductBarcodeGroupBySku = duplicateSupplierProductBarcodes.GroupBy(duplicateSupplierProductBarcode => duplicateSupplierProductBarcode.SKU);

            return duplicateSupplierProductBarcodeGroupBySku.Select(x => new SupplierProductBarcode
            {
                SKU = x.Key
            }).ToArray();
        }

        public IEnumerable<SupplierProductBarcode> GetDuplicateSupplierProductBarcodesFromCompanyB(Company barcodesA, Company barcodesB)
        {
            return
                from supplierProductBarcodeFromA in barcodesA.SupplierProductBarcodes
                join supplierProductBarcodeFromB in barcodesB.SupplierProductBarcodes on supplierProductBarcodeFromA.Barcode equals supplierProductBarcodeFromB.Barcode
                select supplierProductBarcodeFromB;
        }


        public IEnumerable<CommonCatalog> CreateCommonCatalog(Company companyA, Company companyB, IEnumerable<SupplierProductBarcode> duplicateSku)
        {
            var combined = new List<CommonCatalog>();

            var barcodesAGrouped = companyA.SupplierProductBarcodes.GroupBy(x => x.SKU).Select(x => x.Key);
            var commonCatalogs = barcodesAGrouped.Select(x => new CommonCatalog(x, companyA.Catalogs.Where(y => y.SKU == x).First().Description, companyA.Name));
            combined.AddRange(commonCatalogs);


            var barcodesBGrouped = companyB.SupplierProductBarcodes.GroupBy(x => x.SKU).Select(x => x.Key);

            foreach (var barcodesBsku in barcodesBGrouped)
                if (duplicateSku.All(x => x.SKU != barcodesBsku))
                    combined.Add(new CommonCatalog(barcodesBsku, companyB.Catalogs.Where(y => y.SKU == barcodesBsku).First().Description, companyB.Name));


            return combined;
        }
    }
}