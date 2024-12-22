using Days.Common;
using Days.Models;

namespace Days;

public class Day17
{
    public static void Run()
    {
        //PartOne("./Days/Data/17.S.txt");
        PartOne("./Days/Data/17.txt");
    }

    public static void PartOne(string filename)
    {
        var computer = new Computer();
        var program = new List<int>();
        foreach (var line in AdventDay.ReadFromFile(filename))
        {
            if (line.StartsWith("Register A:"))
            {
                computer.RegisterA = int.Parse(line[11..]);
            }
            if (line.StartsWith("Register B:"))
            {
                computer.RegisterB = int.Parse(line[11..]);
            }
            if (line.StartsWith("Register C:"))
            {
                computer.RegisterC = int.Parse(line[11..]);
            }
            if (line.StartsWith("Program:"))
            {
                program = line[8..].Split(',').Select(int.Parse).ToList();
            }
        }

        var output = string.Join(",", computer.Run(program));
        Console.WriteLine(output);
    }
}