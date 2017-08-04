using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ca_testTask.Services;

namespace ca_testTask.Handlers
{
    public sealed class Reversed1FileSearcher : FileSearcherBase, IFileSearcher
    {
        public Reversed1FileSearcher(ISurfaceScanner surfaceScanner) : base(surfaceScanner)
        {
        }

        public async Task<IEnumerable<string>> GetFilePaths(string startPath)
        {
            var fileInfos = await GetFileNamesAsync(startPath);
            return fileInfos.Select(f =>
            {
                var cuttedPath = CutPath(f, startPath);
                var pathItems = cuttedPath.Split('\\');
                StringBuilder sb = new StringBuilder();
                foreach (var item in pathItems.Reverse())
                {
                    sb.AppendFormat("{0}{1}", sb.Length > 0 ? "\\" : "", item);
                }
                return sb.ToString();
            });
        }
    }
}