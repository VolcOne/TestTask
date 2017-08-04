using System.Collections.Generic;
using System.Threading.Tasks;

namespace ca_testTask.Handlers
{
    public interface IFileSearcher
    {
        Task<IEnumerable<string>> GetFilePaths(string startPath);
    }
}