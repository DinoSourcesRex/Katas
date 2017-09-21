using System.Collections.Generic;
using System.Threading.Tasks;

namespace TennisScores.Infrastructure.IO
{
    public interface IStreamWriter
    {
        Task WriteLineAsync(string filePath, List<string> list);
    }
}
