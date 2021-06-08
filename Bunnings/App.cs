using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bunnings.Entities;
using Bunnings.Interfaces;

namespace Bunnings
{
    public class App : IApp
    {
        private readonly ICsvImportExportService _csvImportExportService;
        private readonly ITransformDataService _transformDataService;
        private readonly IVerificationService _verificationService;


        public App(IVerificationService verificationService, ICsvImportExportService csvImportExportService, ITransformDataService transformDataService)
        {
            _transformDataService = transformDataService;
            _csvImportExportService = csvImportExportService;
            _verificationService = verificationService;
        }

        public void Run(string[] args)
        {
            var argsInInputFolder = args.Select(x => $"Input/{x}").ToList();
            if (!_verificationService.ValidInputs(argsInInputFolder)) throw new Exception($"imports are invalid. Error Message: {_verificationService.Message}");

            IEnumerable<SupplierProductBarcode> barcodesA;
            IEnumerable<SupplierProductBarcode> barcodesB;
            IEnumerable<Catalog> catalogA;
            IEnumerable<Catalog> catalogB;
            try
            {
                barcodesA = _csvImportExportService.Import<SupplierProductBarcode>(argsInInputFolder[0]);
                barcodesB = _csvImportExportService.Import<SupplierProductBarcode>(argsInInputFolder[1]);
                catalogA = _csvImportExportService.Import<Catalog>(argsInInputFolder[2]);
                catalogB = _csvImportExportService.Import<Catalog>(argsInInputFolder[3]);
            }
            catch (Exception e)
            {
                throw new Exception($"could not import files. error {e.Message}");
            }

            var companyA = new Company("A", catalogA, barcodesA);
            var companyB = new Company("B", catalogB, barcodesB);

            var companyBSKUWhichHaveDuplicateBarcodesInCompanyA = _transformDataService.GetCompanyBSKUWhichHaveDuplicateBarcodesInCompanyA(companyA, companyB);

            var commonCatalog = _transformDataService.CreateCommonCatalog(companyA, companyB, companyBSKUWhichHaveDuplicateBarcodesInCompanyA);

            var outputFile = "output.csv";
            _csvImportExportService.Export(outputFile, commonCatalog);

            Console.WriteLine($"Success! created merged file '{Directory.GetCurrentDirectory()}\\{outputFile}' with inputs '{string.Join(",", args)}'");
        }
    }
}