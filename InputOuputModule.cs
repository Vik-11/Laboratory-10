using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Задание00prob
{
    internal struct TextPos
    {
        internal uint LineNum;
        internal byte CharNum;

        internal TextPos(uint lineNum = 0, byte charNum = 0)
        {
            LineNum = lineNum;
            CharNum = charNum;
        }
    }

    internal struct Err
    {
        internal TextPos Position;
        internal byte Code;

        internal Err(TextPos position, byte code)
        {
            Position = position;
            Code = code;
        }
    }
    internal class InputOutputModule
    {
        const int ERRMAX = 20;
        static int errCount = 0;
        static string line = "";
        public static TextPos positionNow = new TextPos();
        static byte lastInLine = 0;
        public static List<Err> err = new List<Err>();
        static StreamReader File;
        static Dictionary<byte, string> errDesc = new Dictionary<byte, string>
        {
            { 1, "Symbol ';' expected" },
            { 2, "Invalid use of method" },
            { 3, "Syntax error" },
            { 4, "Unidentified command" },
            { 5, ")' expected" },
            { 6, "(' expected" },
            { 201, "Unknown symbol(s)" },
            { 203, "Argument out of range" }
        };

        internal static char Ch { get; set; }
        internal StreamReader Reader { get; set; }
     
        public static void Open(string path)
        {
            File = new StreamReader(path);
            ReadNextLine();
            NextCh();
        }

        internal static void NextCh()
        {
            if (positionNow.CharNum >= lastInLine)
            {
                if (err.Count > 0)
                {
                    ListErrors();
                }
                ReadNextLine();
                if (line == null)
                {
                    Ch = '\0';
                    return;
                }
                positionNow.LineNum++;
                positionNow.CharNum = 0;
            }
            else
            {
                positionNow.CharNum++;
            }
            if (positionNow.CharNum < line.Length)
            {
                Ch = line[positionNow.CharNum];
            }
            else
            {
                Ch = '\0'; 
            }
        }

        private static void ReadNextLine()
        {
            if (!File.EndOfStream)
            {
                line = File.ReadLine();
                lastInLine = (byte)(line.Length - 1);
                Console.WriteLine(line);
                err = new List<Err>();
            }
            else
            {
                End();
                line = null;
            }
        }

        static void End()
        {
            Console.WriteLine($"Compilation completed: errors — {errCount}!");
        }

        static void ListErrors()
        {
            foreach (Err e in err)
            {
                ++errCount;
                string caret = new string(' ', e.Position.CharNum) + "^";
                string message = errDesc.ContainsKey(e.Code) ? errDesc[e.Code] : "***Unknown error***";
                Console.WriteLine($"{caret} ***Error {e.Code}: {message}***");
            }
        }

        public static void Error(byte errorCode, TextPos position)
        {
            if (err.Count < ERRMAX)
            {
                err.Add(new Err(position, errorCode));
            }
        }
    }
}
