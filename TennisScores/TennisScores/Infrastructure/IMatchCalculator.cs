using System.Threading.Tasks;
using TennisScores.Models;

namespace TennisScores.Infrastructure
{
    public interface IMatchCalculator
    {
        Task<TennisMatch> Calculate(TennisGame game);
    }
}