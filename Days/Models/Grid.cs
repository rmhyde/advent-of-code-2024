namespace Days.Models;

public class Grid<T>(T defaultValue)
{
    private readonly T DefaultValue = defaultValue;

    public Dictionary<Coordinate, T> Storage = [];

    public Coordinate TopLeft = new(double.MaxValue, double.MaxValue);
    public Coordinate BottomRight = new(double.MinValue, double.MinValue);


    public T? Get(Coordinate coord)
    {
        if (coord.X < TopLeft.X || coord.X > BottomRight.X)
        {
            return DefaultValue;
        }

        if (coord.Y < TopLeft.Y || coord.Y > BottomRight.Y)
        {
            return DefaultValue;
        }

        if (Storage.TryGetValue(coord, out var value))
        {
            return value;
        }
        return DefaultValue;
    }

    public T? Get(double X, double Y)
    {
        return Get(new Coordinate(X, Y));
    }

    public void Add(Coordinate coord, T value)
    {
        BottomRight.Y = coord.Y > BottomRight.Y ? coord.Y : BottomRight.Y;
        BottomRight.X = coord.X > BottomRight.X ? coord.X : BottomRight.X;
        TopLeft.X = coord.X < TopLeft.X ? coord.X : TopLeft.X;
        TopLeft.Y = coord.Y < TopLeft.Y ? coord.Y : TopLeft.Y;
        if (value != null)
        {
            Storage.Add(coord, value);
        }
    }

    public void Add(double X, double Y, T value)
    {
        var coord = new Coordinate(X, Y);
        Add(coord, value);
    }
}