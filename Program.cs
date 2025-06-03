using Задание00prob;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Enter path to program");
        string filePath1 = Console.ReadLine();
        if (!File.Exists(filePath1))
        {
            Console.WriteLine("File not found");
            return;
        }

        Console.WriteLine("Enter path to output file");
        string filePath2 = Console.ReadLine();
        if (!File.Exists(filePath2)) 
        {
            Console.WriteLine("File not found");
            return;
        }

        LexicalAnalyzer.Output(filePath1, filePath2);

    }
}