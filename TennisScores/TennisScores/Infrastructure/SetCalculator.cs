using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisScores.Models;

namespace TennisScores.Infrastructure
{
    public class SetCalculator : ISetCalculator
    {
        public Task<SetResult> Calculate(TennisGame game)
        {
            var finalGame = new TennisGame(game.Points);

            var serverChar = finalGame.Points.FirstOrDefault();

            var set = new List<char>();

            while (finalGame.Points.Count > 0)
            {
                var setWon = false;

                var player = game.Points.First();
                set.Add(player);
                finalGame.Points.RemoveAt(0);

                var currentScores = set.GroupBy(x => x)
                    .Select(s => new { Player = s.Key, Score = s.Count() })
                    .OrderBy(s => s.Score);

                var topPlayerChar = ' ';
                var serverScore = currentScores.Where(s => s.Player == serverChar).Select(s => s.Score).FirstOrDefault();
                var receiverScore = currentScores.Where(s => s.Player != serverChar).Select(s => s.Score).FirstOrDefault();

                //Could move this out into something like an ISetCompleteCalculator but left it here since it's just as testable, though messier.
                if ((serverScore >= 4 && receiverScore < 3) ||
                    (receiverScore >= 4 && serverScore < 3) ||
                    (serverScore >= 3 && receiverScore >= 3 && Math.Abs(serverScore - receiverScore) >= 2))
                {
                    setWon = true;
                    topPlayerChar = currentScores.Where(s => s.Score >= 4).OrderByDescending(s => s.Score).Select(s => s.Player).FirstOrDefault();
                }

                if (setWon || finalGame.Points.Count == 0)
                {
                    var advantagePoint = ' ';
                    if (serverScore >= 3 && receiverScore >= 3)
                    {
                        advantagePoint = currentScores.OrderByDescending(s => s.Score).Select(s => s.Player).FirstOrDefault();
                    }

                    serverScore = serverScore.ScoreToTennisScore();
                    receiverScore = receiverScore.ScoreToTennisScore();
                    return Task.FromResult(
                        new SetResult(new TennisSet(setWon, topPlayerChar, serverScore, receiverScore, advantagePoint), 
                            finalGame));
                }
            }

            return Task.FromResult(
                new SetResult(
                    new TennisSet(false, ' ', 0, 0), 
                    game));
        }
    }
}