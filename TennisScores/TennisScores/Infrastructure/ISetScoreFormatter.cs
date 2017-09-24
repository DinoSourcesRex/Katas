using System.Threading.Tasks;
using TennisScores.Models;

namespace TennisScores.Infrastructure
{
    public interface ISetScoreFormatter
    {
        Task<string> Format(TennisMatch matches);
    }
}