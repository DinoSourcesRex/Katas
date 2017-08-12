using System.Diagnostics;

namespace RoverExercise.Models
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Coordinates
    {
        public int X { get; }
        public int Y { get; }

        private string DebuggerDisplay => $"X{X} : Y{Y}";

        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}