using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Boolean_Algebra_Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfVariables = 0;

            Console.WindowWidth = 138;
            Console.WindowHeight = 30;
            Console.BufferWidth = 138;
            Console.BufferHeight = 30;

            Console.WriteLine(new string('-', 40));
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\t" + "Boolean algebra calculator");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(new string('-', 40) + "\n");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Enter amount of boolean variables: ");
            Console.ForegroundColor = ConsoleColor.White;
            numberOfVariables = Int32.Parse(Console.ReadLine());
            Console.WriteLine(new string('-', 20));

            int[] sets = new int[numberOfVariables];

            for (int i = 0; i < numberOfVariables; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Enter #" + (i + 1) + " variable value (0/1): ");
                Console.ForegroundColor = ConsoleColor.White;
                string enteredValue;
                do
                {
                    enteredValue = Console.ReadLine();

                    if (enteredValue != "0" && enteredValue != "1")
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("\t" + "Error! Only 0 and 1 values are available.");
                        Console.WriteLine("Enter again:");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                } while (enteredValue != "0" && enteredValue != "1");
                sets[i] = Int32.Parse(enteredValue);
            }

            Console.Clear();

            Console.WriteLine(new string('-', 20));

            int index = 0;
            foreach (var item in sets) // Show sets state (True or False)
            {
                switch (item)
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write("Set #" + ++index + " | ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("False");
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write("Set #" + ++index + " | ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("True");
                        break;
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(new string('-', 20));

            string userInput = null;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\t" + "* - Conjunction (AND)");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(" | ");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("+ - Disjunction (OR)");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(" | ");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("' - Negation (NOT)");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(" | ");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("~ - Equivalence (EQUALS)");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(" | ");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(">");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(" / ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("< - Implication (IF X THEN Y)" + "\n\n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\t" + "An example of an expression: ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("1+2'*(3+(1*2)') > (1 ~ 2)" + "\n");
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(new string('-', 20));
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n" + "Enter boolean formula: ");
            Console.ForegroundColor = ConsoleColor.White;


            userInput = Console.ReadLine();

            if (!string.IsNullOrEmpty(userInput))
            {
                List<string> convertedString = new List<string>();
                List<string> rpn = new List<string>();

                convertedString = ConvertToList(userInput);
                rpn = GetRPN(convertedString);

                bool answer = GetAnswer(rpn, sets);
                Console.WriteLine();
                Console.Write("Answer: ");
                switch (answer)
                {
                    case true:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("True");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case false:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("False");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }

                Console.WriteLine();
                Console.WriteLine(" " + new string('-', 40));
                Console.Write("|");
                Console.Write("\t" + "Developed by ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("Bluvband Kirill");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\t " + "|");
                Console.Write("|");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("\t\t" + "bloowb0und");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\t\t " + "|");
                Console.WriteLine(" " + new string('-', 40) + "\n");

                Console.WriteLine("Press any button to exit...");
                Console.ReadKey();
            }
        }

        public static List<string> ConvertToList(string input) // Convert string to List
        {
            List<string> ConversionResult = new List<string>();
            input = Regex.Replace(input, @"\s+", ""); // delete spaces
            string numberString = "";

            foreach (char c in input) // parse string to List with inserting "" in between characters
            {
                ConversionResult.Add(numberString);
                ConversionResult.Add(c.ToString());
            }

            return ConversionResult;
        }

        static List<string> GetRPN(List<string> convertedString) // Get Reversed Polish Notation List
        {
            Stack<string> s = new Stack<string>();
            List<string> PostFix = new List<string>();

            try
            {
                ToPostFix(convertedString, s, ref PostFix); // Transoform parsed string to postfix form
                return PostFix;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        static int GetPriority(string c) // get operation priority
        {
            if (c == "'")
            {
                return 5;
            }
            else if (c == "*")
            {
                return 4;
            }
            else if (c == "+")
            {
                return 3;
            }
            else if (c == ">" || c == "<")
            {
                return 2;
            }
            else if (c == "~")
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        static bool IsOperator(string c) // check if operator
        {
            if (c == "*" || c == "+" || c == "~" || c == "'" || c == ">" || c == "<")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static void ToPostFix(List<string> symbols, Stack<string> s, ref List<string> PostFix) // Transform formula to Postfix form
        {
            int n;
            foreach (string c in symbols)
            {
                if (int.TryParse(c.ToString(), out n))
                {
                    PostFix.Add(c);
                }
                if (c == "(") s.Push(c);
                if (c == ")")
                {
                    while (s.Count != 0 && s.Peek() != "(")
                    {
                        PostFix.Add(s.Pop());
                    }
                    s.Pop();
                }
                if (IsOperator(c))
                {
                    while (s.Count != 0 && GetPriority(s.Peek()) >= GetPriority(c))
                    {
                        PostFix.Add(s.Pop());
                    }
                    s.Push(c);
                }
            }
            while (s.Count != 0)
            {
                PostFix.Add(s.Pop());
            }
        }

        public static bool GetAnswer(List<string> token, int[] sets) // returns answer
        {
            Stack<bool> boolStack = new Stack<bool>();

            for (int i = 0; i < token.Count; i++)
            {
                if (int.TryParse(token[i], out int n))
                {
                    boolStack.Push(Convert.ToBoolean(sets[Int32.Parse(token[i]) - 1]));
                }
                else if (token[i] == "^" || token[i] == "*" || token[i] == "+" || token[i] == "'")
                {
                    switch (token[i])
                    {
                        case "+":
                            boolStack.Push(boolStack.Pop() | boolStack.Pop());
                            break;
                        case "~":
                            boolStack.Push(boolStack.Pop() == boolStack.Pop());
                            break;
                        case "*":
                            boolStack.Push(boolStack.Pop() & boolStack.Pop());
                            break;
                        case "'":
                            boolStack.Push(!boolStack.Pop());
                            break;
                        case ">":
                            if (!(boolStack.Pop() == true && boolStack.Pop() == false))
                                boolStack.Push(true);
                            break;
                        case "<":
                            if (!(boolStack.Pop() == false && boolStack.Pop() == true))
                                boolStack.Push(true);
                            break;
                        default:
                            throw new Exception("Invalid Operation");
                    }
                }
            }

            return boolStack.Pop();

        }
    }
}
