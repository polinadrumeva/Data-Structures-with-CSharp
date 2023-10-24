namespace Problem04.BalancedParentheses
{
    using System;
    using System.Collections.Generic;

    public class BalancedParenthesesSolve : ISolvable
    {
        public bool AreBalanced(string parentheses)
        {
            if (parentheses.Length % 2 != 0)
            {
                return false;
            }

            var stack = new Stack<char>(parentheses.Length / 2);

            foreach (var current in parentheses)
            {
                char expected = default;
                switch (current)
                {
                    case ')':
                        expected = '(';
                        break;
                    case '}':
                        expected = '{';
                        break;
                    case ']':
                        expected = '[';
                        break;
                    default:
                        stack.Push(current);
                        break;
                }

                if (expected == default)
                {
                    continue;
                }

                if (stack.Pop() != expected)
                {
                    return false; 
                }
            }

            
            return stack.Count == 0;

        }
    }
}
