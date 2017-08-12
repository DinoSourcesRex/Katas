using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoverExercise.Infrastructure;
using RoverExercise.Models;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace RoverExercise.Tests.Acceptance.Steps
{
    [Binding]
    public sealed class NavigateMarsSteps
    {
        private Terrain _marsTerrain;
        private Position _roverPosition;
        private ICommandQueue<Command> _commandQueue;

        private NavigationModule _roverNavModule;

        [Given(@"Mars has an area of x:(.*) by y:(.*) split into (.*) sectors")]
        public void GivenMarsHasAnAreaOfXbyYSplitIntoSectors(int numCols, int numRows, int numSectors)
        {
            _marsTerrain = new Terrain(numCols, numRows, numSectors);
        }

        [Given(@"The Rover starts at position x:(.*) y:(.*) facing (.*)")]
        public void GivenTheRoverStartsAtPositionXyFacing(int x, int y, CardinalDirection direction)
        {
            _roverPosition = new Position(direction, new Coordinates(x, y));
        }

        [Given(@"The following commands")]
        public void GivenTheFollowingCommands(Table table)
        {
            _commandQueue = new CommandQueue<Command>(5);
            _roverNavModule = new NavigationModule(_marsTerrain, _roverPosition, _commandQueue);

            var commandsTable = table.CreateSet<TableModels.Command>();

            foreach (var command in commandsTable)
            {
                _roverNavModule.AddCommand(new Command(command.Direction, command.Distance));
            }
        }

        [When(@"I execute the commands")]
        public void WhenIExecuteTheCommands()
        {
            _roverNavModule.ProcessMoves();
        }

        [Then(@"the rover should arrive at sector (.*) with coordinates x:(.*) y:(.*) facing (.*)")]
        public void ThenTheRoverShouldArriveAtXyFacing(int sector, int x, int y, CardinalDirection direction)
        {
            Assert.AreEqual(sector, _marsTerrain.GetSector(new Coordinates(x, y)));

            Assert.AreEqual(x, _roverPosition.Coordinates.X);
            Assert.AreEqual(y, _roverPosition.Coordinates.Y);

            Assert.AreEqual(direction, _roverPosition.CardinalDirection);
        }
    }
}