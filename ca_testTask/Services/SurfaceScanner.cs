using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ca_testTask.Services
{
    public class SurfaceScanner : ISurfaceScanner
    {
        public async Task<IEnumerable<string>> ScanFilesAsync(string startPath, string filter = null)
        {
            var files = new List<string>();
            DirectoryInfo directory = new DirectoryInfo(startPath);
            files.AddRange(directory.GetFiles(filter ?? "*.*").Select(f => f.FullName));

            var directories = directory.GetDirectories();

            var tasks = directories.AsParallel().Select(
                async directoryInfo =>
                {
                    var fls = await ScanFilesAsync(directoryInfo.FullName, filter);
                    return fls;
                }).ToList();

            var tasksResult = await Task.WhenAll(tasks);
            foreach (var t in tasksResult)
            {
                files.AddRange(t);
            }
            return files;
        }

    }

    public interface ISurfaceScanner
    {
        Task<IEnumerable<string>> ScanFilesAsync(string startPath, string filter = null);
    }
}
