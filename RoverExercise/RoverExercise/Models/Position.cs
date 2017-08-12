using System.Diagnostics;

namespace RoverExercise.Models
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Position : IPosition
    {
        public CardinalDirection CardinalDirection { get; private set; }
        public Coordinates Coordinates { get; private set; }

        private string DebuggerDisplay => $"X{Coordinates.X} : Y{Coordinates.Y} : Facing {CardinalDirection}";

        public Position(CardinalDirection cardinalDirection, Coordinates coordinates)
        {
            CardinalDirection = cardinalDirection;
            Coordinates = coordinates;
        }

        public Coordinates ProcessMovement(int distance)
        {
            switch (CardinalDirection)
            {
                case CardinalDirection.North:
                    return new Coordinates(Coordinates.X, Coordinates.Y - distance);
                case CardinalDirection.East:
                    return new Coordinates(Coordinates.X + distance, Coordinates.Y);
                case CardinalDirection.South:
                    return new Coordinates(Coordinates.X, Coordinates.Y + distance);
                case CardinalDirection.West:
                    return new Coordinates(Coordinates.X - distance, Coordinates.Y);
                default:
                    return Coordinates;
            }
        }

        public void ExecuteMovement(Coordinates newCoordinates)
        {
            Coordinates = newCoordinates;
        }

        public void ExecuteTurn(Command command)
        {
            switch (command.Direction)
            {
                case RelativeDirection.Left:
                    if (CardinalDirection == CardinalDirection.North)
                    {
                        CardinalDirection = CardinalDirection.West;
                    }
                    else
                    {
                        CardinalDirection--;
                    }
                    break;
                case RelativeDirection.Right:
                    if (CardinalDirection == CardinalDirection.West)
                    {
                        CardinalDirection = CardinalDirection.North;
                    }
                    else
                    {
                        CardinalDirection++;
                    }
                    break;
            }
        }
    }
}