using System.Collections.Generic;

namespace TennisScores.Models
{
    public class TennisMatch
    {
        public List<TennisSet> Sets { get; }

        public TennisMatch(List<TennisSet> sets)
        {
            Sets = sets;
        }
    }
}