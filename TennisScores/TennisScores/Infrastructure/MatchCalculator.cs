using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisScores.Models;

namespace TennisScores.Infrastructure
{
    public class MatchCalculator : IMatchCalculator
    {
        private readonly ISetCalculator _setCalculator;

        public MatchCalculator(ISetCalculator setCalculator)
        {
            _setCalculator = setCalculator;
        }

        public async Task<TennisMatch> Calculate(TennisGame game)
        {
            var tennisMatch = new TennisMatch(new List<TennisSet>());

            bool completed = false;

            while (!completed)
            {
                var setResult = await _setCalculator.Calculate(game);

                tennisMatch.Sets.Add(setResult.Set);
                game = setResult.Remaining;

                if (!setResult.Remaining.Points.Any())
                {
                    completed = true;
                }
            }

            return tennisMatch;
        }
    }
}