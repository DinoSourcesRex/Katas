namespace RoverExercise.Models
{
    public interface ITerrain
    {
        int TerrainColumns { get; }
        int TerrainRows { get; }

        int GetSector(Coordinates coordinates);
        bool WithinPerimeter(Coordinates coordinates);
    }
}