using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ca_testTask.Services;

namespace ca_testTask.Handlers
{
    public sealed class Reversed2FileSearcher : FileSearcherBase, IFileSearcher
    {
        public Reversed2FileSearcher(ISurfaceScanner surfaceScanner) : base(surfaceScanner)
        {
        }

        public async Task<IEnumerable<string>> GetFilePaths(string startPath)
        {
            var fileInfos = await GetFileNamesAsync(startPath);
            return fileInfos.Select(f => new string(CutPath(f, startPath).Reverse().ToArray()));
        }
    }
}