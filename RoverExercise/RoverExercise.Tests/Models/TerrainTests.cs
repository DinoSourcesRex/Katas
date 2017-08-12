using NUnit.Framework;
using RoverExercise.Models;

namespace RoverExercise.Tests.Models
{
    public class TerrainTests
    {
        private Terrain _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new Terrain(100, 100, 100);
        }

        [TestCase(1, 0)]
        [TestCase(1, -1)]
        public void VehicleTooFarNorth(int x, int y)
        {
            Assert.IsFalse(_sut.WithinPerimeter(new Coordinates(x, y)));
        }

        [TestCase(0, 1)]
        [TestCase(-1, 1)]
        public void VehicleTooFarEast(int x, int y)
        {
            Assert.IsFalse(_sut.WithinPerimeter(new Coordinates(x, y)));
        }

        [TestCase(1, 101)]
        [TestCase(1, 102)]
        public void VehicleTooFarSouth(int x, int y)
        {
            Assert.IsFalse(_sut.WithinPerimeter(new Coordinates(x, y)));
        }

        [TestCase(101, 1)]
        [TestCase(102, 1)]
        public void VehicleTooFarWest(int x, int y)
        {
            Assert.IsFalse(_sut.WithinPerimeter(new Coordinates(x, y)));
        }

        [TestCase(1, 1)]
        [TestCase(1, 100)]
        [TestCase(100, 1)]
        [TestCase(100, 100)]
        public void VehicleWithinBounds(int x, int y)
        {
            Assert.IsTrue(_sut.WithinPerimeter(new Coordinates(x, y)));
        }

        [TestCase(24,47, 4624)]
        [TestCase(11, 85, 8411)]
        [TestCase(48, 78, 7748)]
        public void GetSector(int col, int row, int expectedSector)
        {
            Assert.AreEqual(expectedSector, _sut.GetSector(new Coordinates(col, row)));
        }
    }
}