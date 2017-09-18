using System.Threading.Tasks;
using TennisScores.Models;

namespace TennisScores.Infrastructure
{
    public class MatchCalculator : IMatchCalculator
    {
        public Task<TennisMatch> Calculate(TennisGame game)
        {
            throw new System.NotImplementedException();
        }
    }
}