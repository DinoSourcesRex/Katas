using System.Collections.Generic;
using System.Threading.Tasks;
using TennisScores.Models;

namespace TennisScores.Infrastructure.IO
{
    public interface IScoreReader
    {
        Task<List<TennisGame>> ReadFile(string filePath);
    }
}