namespace TennisScores.Models
{
    public class TennisSet
    {
        public char Winner { get; }
        public char AdvantagePoint { get; set; }
        public bool GameCompleted { get; }

        public int ServerScore { get; }
        public int ReceiverScore { get; }

        public TennisSet(bool gameCompleted, char winner, int serverScore, int receiverScore, char advantagePoint = ' ')
        {
            GameCompleted = gameCompleted;
            Winner = winner;
            ServerScore = serverScore;
            ReceiverScore = receiverScore;

            AdvantagePoint = advantagePoint;
        }
    }
}