using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2024_C_.Day3
{
    class Day3
    {
        private string _input;

        public Day3(string input)
        {
            _input = input; // Copilot is pretty coolio
        }

        public int Solve()
        {
            int res = 0;
            int i = 0;
            string stack = "";

            bool Do = true;
            while (i < _input.Length) 
            {
                if (stack.Equals("don't()")) { Do = false;}
                if (stack.Length>=4 && stack.Substring(stack.Length-4).Equals("do()")){ Do = true; }

                if (stack.Length>=4 && stack.Substring(stack.Length-4).Equals("mul(") && Do)
                {
                    string firstNum = "";
                    string secondNum = "";
                    // Loop to find the first num
                    while (Char.IsDigit(_input[i]) && firstNum.Length <= 3)
                    {
                        firstNum += _input[i];
                        i++;
                    }

                    if (_input[i] == ',') { i++; }

                    while (Char.IsDigit(_input[i]) && secondNum.Length <= 3)
                    {
                        secondNum += _input[i];
                        i++;
                    }
                    if (_input[i].Equals(')') && int.TryParse(firstNum,out _) && int.TryParse(secondNum, out _))
                    {
                        // i++; // Gotta reset that i?;
                        res += (int.Parse(firstNum) * int.Parse(secondNum));
                    }
                    // I think we need to reset the stack here??
                    stack = String.Empty;
                }
                stack += _input[i];
                if (stack.Length > 7) { stack = stack.Substring(1); }
                i++; // Teehee
            }
            return res;
        }
    }
}
