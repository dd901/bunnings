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

            var companyAUniqueSKUs = getGroupedSKU(companyA);
            var commonCatalogsForCompanyA = CreateCommonCatalogs(companyA, companyAUniqueSKUs);
            combined.AddRange(commonCatalogsForCompanyA);
   
            var companyBUniqueSkus = getGroupedSKU(companyB);
            var duplicateSKU = duplicateSku.ToList();
            
            foreach (var companyBUniqueSku in companyBUniqueSkus)
                if (duplicateSKU.All(x => x.SKU != companyBUniqueSku))
                {
                    var commonCatalogForCompanyBsku = CreateCommonCatalogs(companyB, new string[]{companyBUniqueSku});
                    combined.AddRange(commonCatalogForCompanyBsku);
                }

            return combined;
        }

        private static IEnumerable<CommonCatalog> CreateCommonCatalogs(Company companyA, IEnumerable<string> companyAUniqueSKUs)
        {
            return companyAUniqueSKUs.Select(x => new CommonCatalog(x, companyA.Catalogs.First(y => y.SKU == x).Description, companyA.Name));
        }

        private static IEnumerable<string> getGroupedSKU(Company companyA)
        {
            return companyA.SupplierProductBarcodes.GroupBy(x => x.SKU).Select(x => x.Key);
        }
    }
}