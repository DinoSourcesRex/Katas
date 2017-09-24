using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisScores.Models;

namespace TennisScores.Infrastructure
{
    public class SetSetScoreFormatter : ISetScoreFormatter
    {
        private readonly char _server;
        private readonly char _receiver;

        public SetSetScoreFormatter(char server, char receiver)
        {
            _server = server;
            _receiver = receiver;
        }

        public Task<string> Format(TennisMatch matches)
        {
            var result = "0-0";

            if (matches.Sets.Count > 0)
            {
                var items = new List<string>
                {
                    CalculateCompletedGames(matches.Sets),
                    CalculateGameScores(matches.Sets)
                };

                result = string.Join(" ", items.Where(i => !string.IsNullOrWhiteSpace(i)));
            }

            return Task.FromResult(result);
        }

        private string CalculateGameScores(IEnumerable<TennisSet> sets)
        {
            var last = sets.Last();

            if (!last.GameCompleted)
            {
                int serverScore = last.ServerScore;
                int receiverScore = last.ReceiverScore;

                if (serverScore == 40 && receiverScore == 40)
                {
                    string sScore = $"{(last.AdvantagePoint == _server ? "A": serverScore.ToString())}";
                    string rScore = $"{(last.AdvantagePoint == _receiver ? "A" : receiverScore.ToString())}";
                    return $"{sScore}-{rScore}";
                }

                return $"{serverScore}-{receiverScore}";
            }

            return "";
        }

        //Really needs to live in its own dependency.
        //It's actually doing 2 jobs. Calculating played games and remaining games.
        private string CalculateCompletedGames(List<TennisSet> sets)
        {
            var final = new List<string>();
            var appendCurrentPlayedSets = true;

            if (sets.First().GameCompleted)
            {
                var completedSets = new Dictionary<char, int>
                {
                    {_server, 0},
                    {_receiver, 0}
                };

                for(int i = 0; i < sets.Count; i++)
                {
                    if (sets[i].GameCompleted)
                    {
                        completedSets[sets[i].Winner]++;

                        int aWins = completedSets[_server];
                        int bWins = completedSets[_receiver];

                        if ((aWins >= 6 || bWins >= 6) && Math.Abs(aWins - bWins) >= 2)
                        {
                            final.Add($"{completedSets[_server]}-{completedSets[_receiver]}");

                            completedSets[_server] = 0;
                            completedSets[_receiver] = 0;
                        }
                    }
                }

                if (!final.Any())
                {
                    final.Add($"{completedSets[_server]}-{completedSets[_receiver]}");
                }
                else
                {
                    final.Add("0-0");
                }

                appendCurrentPlayedSets = false;
            }

            if (appendCurrentPlayedSets)
            {
                final.Add("0-0");
            }

            return string.Join(" ", final);
        }
    }
}