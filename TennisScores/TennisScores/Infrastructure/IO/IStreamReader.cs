using System.Collections.Generic;
using System.Threading.Tasks;

namespace TennisScores.Infrastructure.IO
{
    public interface IStreamReader
    {
        Task<List<string>> ReadLineAsync(string filePath);
    }
}