using System.Collections.Generic;

namespace Bunnings.Interfaces
{
    public interface ICsvImportExportService
    {
        IEnumerable<T> Import<T>(string fileToImport);
        void Export<T>(string fileName, IEnumerable<T> toExport);
    }
}