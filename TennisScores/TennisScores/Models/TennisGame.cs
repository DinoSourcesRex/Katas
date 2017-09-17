using System.Collections.Generic;

namespace TennisScores.Models
{
    public class TennisGame
    {
        public List<char> Points { get; }

        public TennisGame(List<char> points)
        {
            Points = points;
        }
    }
}