using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Bunnings.Interfaces;
using CsvHelper;

namespace Bunnings
{
    public class CsvImportExportService : ICsvImportExportService
    {
        public IEnumerable<T> Import<T>(string fileToImport)
        {
            try
            {
                using (var reader = new StreamReader(fileToImport))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    return csv.GetRecords<T>().ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Export<T>(string fileName, IEnumerable<T> toExport)
        {
            try
            {
                using (var writer = new StreamWriter(fileName))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(toExport);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}