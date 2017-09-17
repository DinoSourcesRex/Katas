namespace TennisScores.Models
{
    public class TennisSet
    {
        public char Winner { get; }
        public bool GameCompleted { get; }

        public int ServerScore { get; }
        public int ReceiverScore { get; }

        public TennisSet(bool gameCompleted, char winner, int serverScore, int receiverScore)
        {
            GameCompleted = gameCompleted;
            Winner = winner;
            ServerScore = serverScore;
            ReceiverScore = receiverScore;
        }
    }
}