namespace Days.Models;

public class Coord
{
    public Coord(string index)
    {
        var coordinates = index.Split(":");
        X = double.Parse(coordinates[0]);
        Y = double.Parse(coordinates[1]);
    }

    public Coord(Coord coord)
    {
        X = coord.X;
        Y = coord.Y;
    }
    public double X { get; set; }
    public double Y { get; set; }

    public static string Index(Coord coord) => $"{coord.X}:{coord.Y}";
}