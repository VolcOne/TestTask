using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ca_testTask.Services;

namespace ca_testTask.Handlers
{
    public sealed class CppFileSearcher : FileSearcherBase, IFileSearcher
    {
        protected override string SearchPattern => "*.cpp";

        public CppFileSearcher(ISurfaceScanner surfaceScanner):base(surfaceScanner)
        {
        }

        public async Task<IEnumerable<string>> GetFilePaths(string startPath)
        {
            var fileInfos = await GetFileNamesAsync(startPath);
            return fileInfos.Select(f => CutPath(f, startPath) + " /");
        }
    }
}