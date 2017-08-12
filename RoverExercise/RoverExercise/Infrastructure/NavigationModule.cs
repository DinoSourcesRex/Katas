using RoverExercise.Models;

namespace RoverExercise.Infrastructure
{
    public class NavigationModule
    {
        private readonly ITerrain _terrain;
        private readonly IPosition _position;
        private readonly ICommandQueue<Command> _commandsQueue;

        public NavigationModule(ITerrain terrain, IPosition startPosition, ICommandQueue<Command> commandQueue)
        {
            _terrain = terrain;
            _position = startPosition;

            _commandsQueue = commandQueue;
        }

        public void ProcessMoves()
        {
            while (_commandsQueue.Count() != 0)
            {
                var command = _commandsQueue.Dequeue();

                if (command.Direction == RelativeDirection.Forward)
                {
                    var newCoordinates = _position.ProcessMovement(command.Distance);

                    if (!_terrain.WithinPerimeter(newCoordinates))
                    {
                        _commandsQueue.Clear();
                    }
                    else
                    {
                        _position.ExecuteMovement(newCoordinates);
                    }
                }
                else
                {
                    _position.ExecuteTurn(command);
                }
            }
        }

        public void AddCommand(Command command)
        {
            _commandsQueue.Queue(command);
        }
    }
}