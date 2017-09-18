namespace TennisScores.Models
{
    public class SetResult
    {
        public TennisSet Set { get; }
        public TennisGame Remaining { get; }

        public SetResult(TennisSet set, TennisGame remaining)
        {
            Set = set;
            Remaining = remaining;
        }
    }
}