using System.Data;
using Days.Enums;

namespace Days.Models;

public class Coordinate
{
    public Coordinate(string index)
    {
        var coordinates = index.Split(":");
        X = double.Parse(coordinates[0]);
        Y = double.Parse(coordinates[1]);
    }

    public Coordinate(double x, double y)
    {
        X = x;
        Y = y;
    }

    public Coordinate(Coordinate coord)
    {
        X = coord.X;
        Y = coord.Y;
    }

    public Coordinate(Coordinate coord, Direction direction)
    {
        X = coord.X;
        Y = coord.Y;
        Move(this, direction);
    }

    public double X { get; set; }

    public double Y { get; set; }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            throw new NoNullAllowedException("compared card cannot be null");
        }
        var sameType = string.Compare(GetType().Name, obj.GetType().Name, StringComparison.Ordinal) == 0;
        if (!sameType)
        {
            throw new InvalidCastException($"{obj.GetType().Name} is not {GetType().Name}");
        }
        var secondCoord = (Coordinate)obj;

        return X == secondCoord.X &&
                Y == secondCoord.Y;

    }
    
    public static Direction GetDirection(Coordinate startPoint, Coordinate endPoint)
    {
        var x = startPoint.X - endPoint.X;
        var y = startPoint.Y - endPoint.Y;
        if (x == 0 && y == 0) { return Direction.DoNot; }
        if (x == 0 && y <  0) { return Direction.Down; }
        if (x == 0 && y >  0) { return Direction.Up; }
        if (x <  0 && y == 0) { return Direction.Right; }
        if (x <  0 && y >  0) { return Direction.UpRight; }
        if (x <  0 && y <  0) { return Direction.DownRight; }
        if (x >  0 && y == 0) { return Direction.Left; }
        if (x >  0 && y >  0) { return Direction.UpLeft; }
        if (x >  0 && y <  0) { return Direction.DownLeft; }
        throw new ArgumentOutOfRangeException("Not sure how you got here but welcome, have an exception");
    }

    public static Coordinate Move(Coordinate coord, Direction direction)
    {
        switch(direction)
        {
            case Direction.Up:
                coord.Y += -1;
                return coord;
                
            case Direction.UpRight:
                coord.X += 1;
                coord.Y += -1;
                return coord;

            case Direction.Right:
                coord.X += 1;
                return coord;

            case Direction.DownRight:
                coord.X += 1;
                coord.Y += 1;
                return coord;

            case Direction.Down:
                coord.Y += 1;
                return coord;

            case Direction.DownLeft:
                coord.X += -1;
                coord.Y += 1;
                return coord;
                
            case Direction.Left:
                coord.X += -1;
                return coord;
                
            case Direction.UpLeft:
                coord.X += -1;
                coord.Y += -1;
                return coord;

            default:
                return coord;
        }
    }

    internal static double GetDistance(string firstGalaxy, string secondGalaxy)
    {
        var first = new Coordinate(firstGalaxy);
        var second = new Coordinate(secondGalaxy);
        var xDiff = first.X > second.X ? first.X - second.X : second.X - first.X;
        var yDiff = first.Y > second.Y ? first.Y - second.Y : second.Y - first.Y;
        return xDiff + yDiff;
    }
}