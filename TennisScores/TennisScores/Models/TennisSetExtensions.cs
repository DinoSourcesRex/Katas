namespace TennisScores.Models
{
    public static class TennisSetExtensions
    {
        public static int ScoreToTennisScore(this int score)
        {
            switch (score)
            {
                case 0:
                    return 0;
                case 1:
                    return 15;
                case 2:
                    return 30;
                default:
                    return 40;
            }
        }
    }
}