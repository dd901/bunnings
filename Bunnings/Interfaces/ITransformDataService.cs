using System.Collections.Generic;
using Bunnings.Entities;

namespace Bunnings.Interfaces
{
    public interface ITransformDataService
    {
        SupplierProductBarcode[] GetCompanyBSKUWhichHaveDuplicateBarcodesInCompanyA(Company barcodesA, Company barcodesB);
        IEnumerable<SupplierProductBarcode> GetDuplicateSupplierProductBarcodesFromCompanyB(Company barcodesA, Company barcodesB);
        IEnumerable<CommonCatalog> CreateCommonCatalog(Company companyA, Company companyB, IEnumerable<SupplierProductBarcode> duplicateSku);
    }
}