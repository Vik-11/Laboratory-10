using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Задание00prob
{
    internal class LexicalAnalyzer
    {
        public const byte
            intc = 15,
            ident = 2,
            semicolon = 14,
            colon = 5,
            assign = 51,
            point = 61,
            twopoints = 74,
            later = 65,
            lessequal = 67,
            lessgreater = 69,
            greater = 86,       
            greaterequal = 87,
            plus = 41,
            minus = 42,
            multiply = 43,
            slash = 44,
            percent = 45,
            lparen = 46,
            rparen = 47,
            comma = 48,
            vbar = 49,
            apostrophe = 50,
            equal = 52,
            dollar = 75,    
            at = 76,         
            hash = 77,       
            ampersand = 78,  
            caret = 79,      
            underscore = 80, 
            tilde = 81,      
            lbrace = 82,     
            rbrace = 83,     
            lbracket = 84,   
            rbracket = 85; 

        internal byte symbol;
        internal TextPos token;
        internal string addrName;
        internal int nmb_int;

        internal static Dictionary<string, byte> keywords = new Dictionary<string, byte>
        {
            { "and", 107 },
            { "array", 115 },
            { "asm", 125 },
            { "begin", 113 },
            { "break", 126 },
            { "case", 31 },
            { "const", 116 },
            { "constructor", 127 },
            { "continue", 128 },
            { "destructor", 129 },
            { "div", 106 },
            { "do", 54 },
            { "downto", 118 },
            { "else", 32 },
            { "end", 104 },
            { "false", 130 },
            { "file", 57 },
            { "for", 109 },
            { "function", 123 },
            { "goto", 33 },
            { "if", 56 },
            { "implementation", 131 },
            { "in", 100 },
            { "inline", 132 },
            { "interface", 133 },
            { "label", 117 },
            { "mod", 110 },
            { "nil", 111 },
            { "not", 108 },
            { "object", 134 },
            { "of", 101 },
            { "on", 135 },
            { "operator", 136 },
            { "or", 102 },
            { "packed", 119 },
            { "procedure", 124 },
            { "program", 122 },
            { "record", 120 },
            { "repeat", 121 },
            { "set", 112 },
            { "shl", 137 },
            { "shr", 138 },
            { "string", 139 },
            { "then", 52 },
            { "to", 103 },
            { "true", 140 },
            { "type", 34 },
            { "unit", 141 },
            { "until", 53 },
            { "uses", 142 },
            { "var", 105 },
            { "while", 114 },
            { "with", 37 },
            { "xor", 143 }
        };


        internal byte NextSym()
        {
            while (InputOutputModule.Ch == ' ')
            {
                InputOutputModule.NextCh();
            }

            token.LineNum = InputOutputModule.positionNow.LineNum;
            token.CharNum = InputOutputModule.positionNow.CharNum;

            char ch = InputOutputModule.Ch;

            if (char.IsLetter(ch))
            {
                string name = "";
                while (char.IsLetterOrDigit(InputOutputModule.Ch))
                {
                    name += InputOutputModule.Ch;
                    InputOutputModule.NextCh();
                }

                addrName = name.ToLower();
                symbol = keywords.ContainsKey(addrName) ? keywords[addrName] : ident;
                return symbol;
            }

            if (char.IsDigit(ch))
            {
                nmb_int = 0;
                bool overflow = false;
                while (char.IsDigit(InputOutputModule.Ch))
                {
                    int digit = InputOutputModule.Ch - '0';
                    if (nmb_int <= (Int16.MaxValue - digit) / 10)
                    {
                        nmb_int = nmb_int * 10 + digit;
                    }
                    else
                    {
                        InputOutputModule.Error(203, InputOutputModule.positionNow);
                        overflow = true;
                    }
                    InputOutputModule.NextCh();
                }
                symbol = intc;
                if (overflow)
                {
                    nmb_int = 0;
                }
                return symbol;
            }

            switch (ch)
            {
                case ';':
                    symbol = semicolon;
                    InputOutputModule.NextCh();
                    break;
                case ',':
                    symbol = comma;
                    InputOutputModule.NextCh(); 
                    break;
                case '|':
                    symbol = vbar;
                    InputOutputModule.NextCh();
                    break;
                case '\'':
                    symbol = apostrophe;
                    InputOutputModule.NextCh();
                    break;
                case '=':
                    symbol = equal;
                    InputOutputModule.NextCh();
                    break;
                case '$':
                    symbol = dollar;
                    InputOutputModule.NextCh();
                    break;
                case '@':
                    symbol = at;
                    InputOutputModule.NextCh();
                    break;
                case '#':
                    symbol = hash;
                    InputOutputModule.NextCh();
                    break;
                case '&':
                    symbol = ampersand;
                    InputOutputModule.NextCh();
                    break;
                case '^':
                    symbol = caret;
                    InputOutputModule.NextCh();
                    break;
                case '_':
                    symbol = underscore;
                    InputOutputModule.NextCh();
                    break;
                case '~':
                    symbol = tilde;
                    InputOutputModule.NextCh();
                    break;
                case '{':
                    symbol = lbrace;
                    InputOutputModule.NextCh();
                    break;
                case '}':
                    symbol = rbrace;
                    InputOutputModule.NextCh();
                    break;
                case '[':
                    symbol = lbracket;
                    InputOutputModule.NextCh();
                    break;
                case ']':
                    symbol = rbracket;
                    InputOutputModule.NextCh();
                    break;
                case ':':
                    InputOutputModule.NextCh();
                    symbol = InputOutputModule.Ch == '=' ? assign : colon;
                    if (symbol == assign)
                    {
                        InputOutputModule.NextCh();
                    }
                    break;
                case '.':
                    InputOutputModule.NextCh();
                    symbol = InputOutputModule.Ch == '.' ? twopoints : point;
                    if (symbol == twopoints)
                    {
                        InputOutputModule.NextCh();
                    }
                    break;
                case '<':
                    InputOutputModule.NextCh();
                    if (InputOutputModule.Ch == '=') 
                    { 
                        symbol = lessequal; 
                        InputOutputModule.NextCh(); 
                    }
                    else if (InputOutputModule.Ch == '>') 
                    { 
                        symbol = lessgreater; 
                        InputOutputModule.NextCh(); 
                    }
                    else symbol = later;
                    break;
                case '>':
                    InputOutputModule.NextCh();
                    if (InputOutputModule.Ch == '=')
                    {
                        symbol = greaterequal; 
                        InputOutputModule.NextCh();
                    }
                    else
                    {
                        symbol = greater;       
                    }
                    break;
                case '+':
                    symbol = plus;
                    InputOutputModule.NextCh();
                    break;
                case '-':
                    symbol = minus;
                    InputOutputModule.NextCh();
                    break;
                case '*':
                    symbol = multiply;
                    InputOutputModule.NextCh();
                    break;
                case '/':
                    symbol = slash;
                    InputOutputModule.NextCh();
                    break;
                case '%':
                    symbol = percent;
                    InputOutputModule.NextCh();
                    break;
                case '(':
                    symbol = lparen;
                    InputOutputModule.NextCh();
                    break;
                case ')':
                    symbol = rparen;
                    InputOutputModule.NextCh();
                    break;

                default:
                    InputOutputModule.Error(201, InputOutputModule.positionNow);
                    InputOutputModule.NextCh();
                    return 0;
            }
            return symbol;
        }

        internal static void Output(string file1, string file2)
        {
            InputOutputModule.Open(file1);
            LexicalAnalyzer analyzer = new LexicalAnalyzer();

            using (StreamWriter writer = new StreamWriter(file2))
            {
                while (InputOutputModule.Ch != '\0') 
                { 
                    byte sym = analyzer.NextSym();
                    if (sym != 0)
                    {
                        writer.Write(sym + " ");
                    }
                }
            }
            Console.WriteLine($"Lexical analysis completed: file output { file2 }");
        }
    }
}
