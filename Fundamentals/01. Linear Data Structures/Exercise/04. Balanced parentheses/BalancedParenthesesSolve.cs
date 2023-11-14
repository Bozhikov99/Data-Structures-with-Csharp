
namespace Problem04.BalancedParentheses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BalancedParenthesesSolve : ISolvable
    {
        public bool AreBalanced(string parentheses)
        {
            //({[]}) ()[]{}

            if (parentheses.Length % 2 != 0)
            {
                return false;
            }

            Stack<char> symbols = new Stack<char>();

            for (int i = 0; i < parentheses.Length; i++)
            {
                char expected = default;
                char current = parentheses[i];

                switch (current)
                {
                    case ')':
                        expected = '(';
                        break;
                    case ']':
                        expected = '[';
                        break;
                    case '}':
                        expected = '{';
                        break;
                    default:
                        symbols.Push(current);
                        break;
                }

                if (expected == default(char))
                {
                    continue;
                }

                if (symbols.Pop() != expected)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
