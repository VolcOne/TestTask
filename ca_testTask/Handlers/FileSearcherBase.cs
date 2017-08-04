using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ca_testTask.Services;

namespace ca_testTask.Handlers
{
    public abstract class FileSearcherBase
    {
        private readonly ISurfaceScanner _surfaceScanner;
        protected virtual string SearchPattern => "*.*";

        protected FileSearcherBase(ISurfaceScanner surfaceScanner)
        {
            _surfaceScanner = surfaceScanner;
        }

        protected string CutPath(string fileName, string path)
        {
            var indexOf = fileName.IndexOf(path, StringComparison.InvariantCultureIgnoreCase);
            if (indexOf < 0)
            {
                return fileName;
            }

            var remainingLength = fileName.Length - path.Length + indexOf;
            return fileName.Substring(path.Length + indexOf, remainingLength).TrimStart('\\');
        }

        protected async Task<IEnumerable<string>> GetFileNamesAsync(string startPath)
        {
            return await _surfaceScanner.ScanFilesAsync(startPath, SearchPattern);
        }
    }
}