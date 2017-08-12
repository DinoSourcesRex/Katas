namespace RoverExercise.Models
{
    public class Terrain : ITerrain
    {
        public int TerrainColumns { get; }
        public int TerrainRows { get; }

        private readonly int _totalSectorsPerRow;

        public Terrain(int terrainColumns, int terrainRows, int totalSectorsPerRow)
        {
            TerrainColumns = terrainColumns;
            TerrainRows = terrainRows;
            _totalSectorsPerRow = totalSectorsPerRow;
        }

        public bool WithinPerimeter(Coordinates coordinates)
        {
            return coordinates.X <= TerrainColumns && coordinates.X > 0 &&
                coordinates.Y <= TerrainRows && coordinates.Y > 0;
        }

        public int GetSector(Coordinates coordinates)
        {
            return _totalSectorsPerRow*(coordinates.Y - 1) + coordinates.X;
        }
    }
}