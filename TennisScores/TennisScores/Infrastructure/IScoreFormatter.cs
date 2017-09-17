using System.Threading.Tasks;
using TennisScores.Models;

namespace TennisScores.Infrastructure
{
    public interface IScoreFormatter
    {
        Task<string> Format(TennisMatch matches);
    }
}