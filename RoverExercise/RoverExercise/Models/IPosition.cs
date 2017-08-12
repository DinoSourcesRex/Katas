namespace RoverExercise.Models
{
    public interface IPosition
    {
        CardinalDirection CardinalDirection { get; }
        Coordinates Coordinates { get; }

        void ExecuteMovement(Coordinates newCoordinates);
        void ExecuteTurn(Command command);
        Coordinates ProcessMovement(int distance);
    }
}