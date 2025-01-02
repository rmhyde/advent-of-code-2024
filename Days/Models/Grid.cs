namespace Days.Models;

public class Grid<T>() where T : new ()
{
    public T? DefaultValue { get; set; }

    public Dictionary<string, T> Storage = [];

    public Coordinate TopLeft = new(double.MaxValue, double.MaxValue);
    public Coordinate BottomRight = new(double.MinValue, double.MinValue);


    public T Get(Coordinate coord)
    {
        if (coord.X < TopLeft.X || coord.X > BottomRight.X)
        {
            return DefaultValue ?? new T();
        }

        if (coord.Y < TopLeft.Y || coord.Y > BottomRight.Y)
        {
            return DefaultValue ?? new T();
        }

        if (Storage.TryGetValue(coord.ToString(), out var value))
        {
            return value;
        }
        return DefaultValue ?? new T();
    }

    public T? Get(double X, double Y)
    {
        return Get(new Coordinate(X, Y));
    }

    public T? Get(string coord)
    {
        return Get(new Coordinate(coord));
    }

    public void Set(string coord, T value)
    {
        Set(new Coordinate(coord), value);
    }

    public void Set(Coordinate coord, T value)
    {
        BottomRight.Y = coord.Y > BottomRight.Y ? coord.Y : BottomRight.Y;
        BottomRight.X = coord.X > BottomRight.X ? coord.X : BottomRight.X;
        TopLeft.X = coord.X < TopLeft.X ? coord.X : TopLeft.X;
        TopLeft.Y = coord.Y < TopLeft.Y ? coord.Y : TopLeft.Y;
        if (value != null)
        {
            Storage[coord.ToString()] = value;
        }
    }

    public void Remove(string coord)
    {
        Remove(new Coordinate(coord));
    }

    public void Remove(Coordinate coord)
    {
        Storage.Remove(coord.ToString());
    }

    public void Set(double X, double Y, T value)
    {
        var coord = new Coordinate(X, Y);
        Set(coord, value);
    }
}