public record Info(int x, int y, int n);

public static class ConsoleInput {
    public static Info AskForInput()
    {
        var line = Console.ReadLine();

        if (line == "undo")
        {
            return new Info(-1, -1, -1);
        }

        string[]? values = line.Split(' ');

        if (values.Length != 3)
        {
            Console.WriteLine("I need 3 values");
            throw new Exception();
        }

        try {
            
            int x, y, n;

            x = int.Parse(values[0]);
            y = int.Parse(values[1]);
            n = int.Parse(values[2]);

            return new Info(x, y, n);

        } catch (FormatException e) {
            Console.WriteLine($"They need to be integers.\n{e}");
            throw new Exception();
        }
    }
    public static int AskForOneNumInput()
    {
        var line = Console.ReadLine();

        if (line == "") {
            return 0;
        }

        if (line == "undo")
        {
            return -1;
        }

        try {
            
            return int.Parse(line);

        } catch (FormatException e) {
            Console.WriteLine($"It needs to be an integer.\n{e}");
            throw new Exception();
        }
    }
}