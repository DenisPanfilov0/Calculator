using System;
using System.Collections.Generic;

namespace Calc
{
    public class ReversePolishNotation
    {
        public string Input
        {
            get { return _input; }
            set { _input = value; }
        }
        public string output
        {
            get { return _output; }
            set { _output = value; }
        }
        private string _input;
        private string _output = "";

        public void LineInRPN ()
        {
            string[] tokens = _input.Split();
            Stack<string> stack = new Stack<string>();


            foreach (string token in tokens)
            {
                // If token is a number, add it to the output
                double number;
                bool isNumber = double.TryParse(token, out number);
                if (isNumber)
                {
                    _output += token + " ";
                }

                // If token is an operator, pop previous operators from the stack and add them to the output
                else if (IsOperator(token))
                {
                    while (stack.Count > 0 && IsOperator(stack.Peek()) && ComparePrecedence(token, stack.Peek()) <= 0)
                    {
                        _output += stack.Pop() + " ";
                    }
                    stack.Push(token);
                }

                // If token is a left parenthesis, push it onto the stack
                else if (token == "(")
                {
                    stack.Push(token);
                }

                // If token is a right parenthesis, pop operators from the stack and add them to the output until a matching left parenthesis is found
                else if (token == ")")
                {
                    while (stack.Count > 0 && stack.Peek() != "(")
                    {
                        _output += stack.Pop() + " ";
                    }
                    stack.Pop();
                }
            }

            // Pop any remaining operators from the stack and add them to the output
            while (stack.Count > 0)
            {
                _output += stack.Pop() + " ";
            }

            //Console.WriteLine("RPN: " + output.Trim());
        }

        // Method to check if a token is an operator
        public bool IsOperator(string token)
        {
            return token == "+" || token == "-" || token == "*" || token == "/" || token == "^";
        }

        // Method to compare the precedence of two operators
        static int ComparePrecedence(string op1, string op2)
        {
            Dictionary<string, int> precedence = new Dictionary<string, int>()
            {
                {"+", 1},
                {"-", 1},
                {"*", 2},
                {"/", 2},
                {"^", 3}
            };
            int op1Precedence = precedence[op1];
            int op2Precedence = precedence[op2];
            return op1Precedence.CompareTo(op2Precedence);
        }

        public void CalculateRPN(string _output)
        {
            string[] tokens = _output.Split(' ');
            Stack<double> stack = new Stack<double>();

            foreach (string token in tokens)
            {
                if (double.TryParse(token, out double number))
                {
                    stack.Push(number);
                }
                else if (token != "")
                {
                    double operand2 = stack.Pop();
                    double operand1 = stack.Pop();

                    switch (token)
                    {
                        case "+":
                            stack.Push(operand1 + operand2);
                            break;
                        case "-":
                            stack.Push(operand1 - operand2);
                            break;
                        case "*":
                            stack.Push(operand1 * operand2);
                            break;
                        case "/":
                            stack.Push(operand1 / operand2);
                            break;
                        case "^":
                            stack.Push(Math.Pow(operand1, operand2));
                            break;
                        default:
                            throw new ArgumentException("Invalid operator: " + token);
                    }
                }
                //else return stack.Pop();
            }
            //return stack.Pop();
            output = stack.Pop().ToString();
        }
    }
}