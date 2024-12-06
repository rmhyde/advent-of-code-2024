// See https://aka.ms/new-console-template for more information
using Days;

namespace AdventCli;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine($"Starting Day {args[0]}");
        Day2.Run();
        
    }
}