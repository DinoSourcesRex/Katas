using NUnit.Framework;
using RoverExercise.Models;

namespace RoverExercise.Tests.Models
{
    public class PositionTests
    {
        private Coordinates _coordinates;
        private Position _sut;

        [SetUp]
        public void SetUp()
        {
            _coordinates = new Coordinates(1, 1);
        }

        [TestCase(CardinalDirection.North, 1, -49)]
        [TestCase(CardinalDirection.East, 51, 1)]
        [TestCase(CardinalDirection.South, 1, 51)]
        [TestCase(CardinalDirection.West, -49, 1)]
        public void ProcessMovement(CardinalDirection startDirection, int expectedColumn, int expectedRow)
        {
            _sut = new Position(startDirection, _coordinates);
            var result = _sut.ProcessMovement(50);

            Assert.AreEqual(expectedColumn, result.X);
            Assert.AreEqual(expectedRow, result.Y);
        }

        [TestCase(CardinalDirection.North, CardinalDirection.East)]
        [TestCase(CardinalDirection.East, CardinalDirection.South)]
        [TestCase(CardinalDirection.South, CardinalDirection.West)]
        [TestCase(CardinalDirection.West, CardinalDirection.North)]
        public void ExecuteTurn_TurnRight(CardinalDirection startDirection, CardinalDirection expectedDir)
        {
            _sut = new Position(startDirection, _coordinates);
            _sut.ExecuteTurn(new Command(RelativeDirection.Right, 0));

            Assert.AreEqual(expectedDir, _sut.CardinalDirection);
        }

        [TestCase(CardinalDirection.West, CardinalDirection.South)]
        [TestCase(CardinalDirection.South, CardinalDirection.East)]
        [TestCase(CardinalDirection.East, CardinalDirection.North)]
        [TestCase(CardinalDirection.North, CardinalDirection.West)]
        public void ExecuteTurn_TurnLeft(CardinalDirection startDirection, CardinalDirection expectedDir)
        {
            _sut = new Position(startDirection, _coordinates);
            _sut.ExecuteTurn(new Command(RelativeDirection.Left, 0));

            Assert.AreEqual(expectedDir, _sut.CardinalDirection);
        }
    }
}