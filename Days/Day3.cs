using System.Text.RegularExpressions;
using Days.Common;

namespace Days;

public static partial class Day3
{
    public static void Run()
    {
        PartOne("./Days/Data/3.1.S.txt");
        PartOne("./Days/Data/3.txt");
        PartTwo("./Days/Data/3.2.S.txt");
        PartTwo("./Days/Data/3.txt");
    }

    public static void PartTwo(string filename)
    {
        var add = true;
        var total = 0;
        foreach (var line in AdventDay.ReadFromFile(filename))
        {
            foreach (Match match in GetMulsDosDontsRegex().Matches(line))
            {
                switch (match.Groups[1].Value)
                {
                    case "do":
                        add = true;
                    break;
                    
                    case "don't":
                        add = false;
                        break;

                    case "mul":
                            if (!add) {
                                continue;
                            }
                            total += int.Parse(match.Groups[2].Value) *
                                int.Parse(match.Groups[3].Value);
                        break;

                    default:
                        throw new Exception($"How did we get here? {match.Groups[1].Value}");
                }
            }
        }
        Console.WriteLine(total);
    }

    public static void PartOne(string filename)
    {
        var total = 0;
        foreach (var line in AdventDay.ReadFromFile(filename))
        {
            total += GetMuls(line);
        }
        Console.WriteLine(total);
    }

    public static int GetMuls(string line)
    {
        var subTotal = 0;
        foreach (Match match in GetMulsRegex().Matches(line))
        {
             subTotal += int.Parse(match.Groups[1].Value) *
                int.Parse(match.Groups[2].Value);
        }
        return subTotal;
    }


    [GeneratedRegex("mul\\(([0-9]+),([0-9]+)\\)", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex GetMulsRegex();

    [GeneratedRegex("(do|don\'t|mul)\\((?:([0-9]+),([0-9]+))?\\)", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex GetMulsDosDontsRegex();


}