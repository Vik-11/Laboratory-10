using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Задание00prob
{
    internal class InputOutputModule
    {
        public struct TextPos
        {
            public uint LineNum;
            public byte CharNum;

            public TextPos(uint lineNum = 0, byte charNum = 0)
            {
                LineNum = lineNum;
                CharNum = charNum;
            }
        }

        struct Err
        {
            public TextPos Position;
            public byte Code;

            public Err(TextPos position, byte code)
            {
                Position = position;
                Code = code;
            }
        }

        const int ERRMAX = 20;
        static List<Err> errList = new List<Err>();

        static readonly Dictionary<byte, string> errorDescriptions = new Dictionary<byte, string>
        {
            { 1, "Symbol ';' expected" },
            { 2, "Invalid use of method" },
            { 3, "Syntax error" },
            { 4, "Unidentified command" },
            { 5, ")' expected" },
            { 6, "(' expected" }
        };

        static int errCount = 0;
        public static void Error(byte errorCode, TextPos position)
        {
            if (errList.Count < ERRMAX)
            {
                errList.Add(new Err(position, errorCode));
                errCount++;
            }
        }

        public static void DisplayErrs(string[] lines)
        {
            for (uint ln = 0; ln < lines.Length; ln++)
            {
                string line = lines[ln];
                Console.WriteLine(line);

                var err = errList.FirstOrDefault(e => e.Position.LineNum == ln);

                if (errList.Contains(err))
                {
                    Console.WriteLine(new string(' ', Math.Min(err.Position.CharNum, line.Length)) + "^");
                    string description = errorDescriptions.ContainsKey(err.Code) ? errorDescriptions[err.Code] : "Unknown error";
                    Console.WriteLine(new string(' ', Math.Min(err.Position.CharNum, line.Length)) + $"***Error ({err.Code}): {description}***");
                }
            }
            
        }

        public static void NextCh(string[] lines)
        {
            var rand = new Random();
            for (uint ln = 0; ln < lines.Length && errList.Count < ERRMAX; ln++)
            {
                string line = lines[ln];
                for (byte cn = 0; cn < line.Length && errList.Count < ERRMAX; cn++)
                {
                    if (rand.NextDouble() < 0.05)
                    {
                        Error((byte)rand.Next(errorDescriptions.Count + 1), new TextPos(ln, cn));
                        break;
                    }
                }
            }
        }
        public static void Run(string[] lines)
        {
            NextCh(lines);
            DisplayErrs(lines);
            Console.WriteLine($"\nCompilation finished. Errors encountered -- {errCount}\n");
        }
    }
}
