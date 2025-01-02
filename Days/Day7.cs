using System.Text;
using Days.Common;

namespace Days;

public class Day7
{
    public static void Run()
    {
        Run("./Days/Data/7.S.txt", 2);
        Run("./Days/Data/7.txt", 2);
        Run("./Days/Data/7.S.txt", 3);
        Run("./Days/Data/7.txt", 3);
    }

    public static void Run(string filename, int operationsCount)
    {
        double total = 0;
        foreach (var line in AdventDay.ReadFromFile(filename))
        {
            var split = line.Split(':', StringSplitOptions.RemoveEmptyEntries);
            var subTotal = double.Parse(split[0]);
            var numbers = split[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(double.Parse).ToList();
            if (IsAbleToProduceValue(subTotal, numbers, operationsCount))
            {
                total += subTotal;
            }
        }
        Console.WriteLine(total);
    }

    private static bool IsAbleToProduceValue(double total, List<double> numbers, int operationsCount)
    {
        for(var i = 0; i < Math.Pow(operationsCount, numbers.Count - 1); i++)
        {
            var sub = numbers.First();
            var operations = ConvertToBase(i, operationsCount).PadLeft(numbers.Count - 1, '0');
            for(var op = 0; op < operations.Length; op++)
            {
                switch(operations[op])
                {
                    case '0':
                        sub += numbers[op + 1];
                        break;

                    case '1':
                        sub *= numbers[op + 1];
                        break;

                    case '2':
                        sub = double.Parse($"{sub}{numbers[op + 1]}");
                        break;

                    default:
                        throw new ArgumentException("Not sure what this operation is");
                }

                if (sub > total)
                {
                    sub = -1;
                    break;
                }
            }
            if (sub == total)
            {
                return true;
            }
        }
        return false;
    }

    private static string ConvertToBase(int number, int b)
    {
        var sb = new StringBuilder();
        do
        {
            var mod = number % b;
            sb.Insert(0, mod);
            number /= b;
        } while (number != 0);
        return sb.ToString();
    }
}