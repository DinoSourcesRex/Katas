using System.Collections.Generic;
using System.Threading.Tasks;

namespace TennisScores.Infrastructure.IO
{
    public interface IScoreWriter
    {
        Task<bool> WriteFile(string filePath, List<string> matches);
    }
}