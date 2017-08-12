namespace RoverExercise.Models
{
    public class Command
    {
        public RelativeDirection Direction { get; }
        public int Distance { get; }

        public Command(RelativeDirection direction, int distance)
        {
            Direction = direction;
            Distance = distance;
        }
    }
}