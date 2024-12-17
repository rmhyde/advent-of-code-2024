namespace Days.Models;

public class Robot
{
    public string Id = Guid.NewGuid().ToString();
    public Coordinate Position = new (0, 0);
    public Coordinate Velocity = new(0, 0);

}