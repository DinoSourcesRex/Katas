using NUnit.Framework;
using Rhino.Mocks;
using RoverExercise.Infrastructure;
using RoverExercise.Models;

namespace RoverExercise.Tests.Infrastructure
{
    public class NavigationModuleTests
    {
        private ITerrain _mockTerrain;
        private IPosition _mockPosition;
        private ICommandQueue<Command> _mockCommandQueue;

        private NavigationModule _sut;

        [SetUp]
        public void SetUp()
        {
            _mockTerrain = MockRepository.GenerateMock<ITerrain>();
            _mockPosition = MockRepository.GenerateMock<IPosition>();
            _mockCommandQueue = MockRepository.GenerateMock<ICommandQueue<Command>>();

            _sut = new NavigationModule(_mockTerrain, _mockPosition, _mockCommandQueue);
        }

        [Test]
        public void ProcessMoves_MovementExecuted()
        {
            var command = new Command(RelativeDirection.Forward, 5);
            var coordinates = new Coordinates(10, 10);

            _mockCommandQueue.Stub(c => c.Count()).Return(1).Repeat.Once();
            _mockCommandQueue.Stub(c => c.Count()).Return(0);

            _mockCommandQueue.Stub(c => c.Dequeue()).Return(command);
            _mockPosition.Stub(p => p.ProcessMovement(command.Distance)).Return(coordinates);

            _mockTerrain.Stub(t => t.WithinPerimeter(coordinates)).Return(true);
            _sut.ProcessMoves();

            _mockPosition.AssertWasCalled(p => p.ExecuteMovement(coordinates));
        }

        [Test]
        public void ProcessMoves_OutsidePerimeter_QueueCleared()
        {
            var command = new Command(RelativeDirection.Forward, 5);
            var coordinates = new Coordinates(10, 10);

            _mockCommandQueue.Stub(c => c.Count()).Return(1).Repeat.Once();
            _mockCommandQueue.Stub(c => c.Count()).Return(0);

            _mockCommandQueue.Stub(c => c.Dequeue()).Return(command);
            _mockPosition.Stub(p => p.ProcessMovement(command.Distance)).Return(coordinates);

            _mockTerrain.Stub(t => t.WithinPerimeter(coordinates)).Return(false);
            _sut.ProcessMoves();

            _mockCommandQueue.AssertWasCalled(c => c.Clear());
        }

        [Test]
        public void ProcessMoves_TurnExecuted()
        {
            var command = new Command(RelativeDirection.Left, 5);

            _mockCommandQueue.Stub(c => c.Count()).Return(1).Repeat.Once();
            _mockCommandQueue.Stub(c => c.Count()).Return(0);

            _mockCommandQueue.Stub(c => c.Dequeue()).Return(command);

            _sut.ProcessMoves();

            _mockPosition.AssertWasCalled(p => p.ExecuteTurn(command));
        }
    }
}