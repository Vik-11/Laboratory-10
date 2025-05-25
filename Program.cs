using Задание00prob;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Enter path to program");
        string filePath = Console.ReadLine();

        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found.");
            return;
        }

        string[] lines = File.ReadAllLines(filePath);

        Console.WriteLine("Compiled program:\n");
        InputOutputModule.Run(lines);
    }
}