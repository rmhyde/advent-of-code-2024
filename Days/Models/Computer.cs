namespace Days.Models;

public class Computer
{
    public int RegisterA;
    public int RegisterB;
    public int RegisterC;
    public int Pointer = -2;

    public List<int> Run(List<int> program)
    {
        Pointer = -2;
        var output = new List<int>();
        do
        {
            Pointer += 2;
            var length = program.Count;
            if (Pointer > length || Pointer + 1 > length)
            {
                return output;
            }

            var instruction = program[Pointer];
            var operand = program[Pointer + 1];

            switch (instruction)
            {
                case 0:
                    Adv(operand);
                    break;

                case 1:
                    Bxl(operand);
                    break;
                    
                case 2:
                    Bst(operand);
                    break;
                    
                case 3:
                    Jnz(operand);
                    break;
                
                case 4:
                    Bxc();
                    break;

                case 5:
                    output.Add(Out(operand));
                    break;

                case 6:
                    Bdv(operand);
                    break;

                case 7:
                    Cdv(operand);
                    break;

                default:
                    throw new ArgumentException("instruction is out of range");
            }
        } while (true);
    }

    public void Adv(int combo)
    {
        RegisterA /= (int)Math.Pow(2,GetCombo(combo));
    }

    public void Bdv(int combo)
    {
        RegisterB = RegisterA / (int)Math.Pow(2,GetCombo(combo));
    }

    public void Cdv(int combo)
    {
        RegisterC = RegisterA / (int)Math.Pow(2,GetCombo(combo));
    }

    public void Bxl(int literal)
    {
        RegisterB ^= literal;
    }

    public void Bst(int combo)
    {
        RegisterB = GetCombo(combo) % 8;
    }

    public void Jnz(int literal)
    {
        Pointer = RegisterA == 0 ? Pointer : literal - 2;
    }

    public void Bxc()
    {
        RegisterB ^= RegisterC;
    }

    public int Out(int combo)
    {
        return GetCombo(combo) % 8;
    }

    public int GetCombo(int combo)
        => combo switch
        {
            0 => 0,
            1 => 1,
            2 => 2,
            3 => 3,
            4 => RegisterA,
            5 => RegisterB,
            6 => RegisterC,
            _ => throw new ArgumentException("Invalid Combo Code!"),
        };
}