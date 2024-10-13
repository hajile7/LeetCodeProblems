using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode_Problems.Classes
{
    //Problem: Create the MinStack class, which implements the following functions:
    internal class MinStack
    {
        private Stack<int> stack;
        private Stack<int> minStack;

        public MinStack()
        {
            stack = new Stack<int>();
            minStack = new Stack<int>();
        }

        //Push is really the only noteworthy function to talk about; it essentially ditches typical stack
        //functionality on minStack and will only ever push the smallest value seen to the top of minStack.
        //As a result, we're able to mostly just use stack for the function calls and minStack when we need
        //to call the GetMin() function.
        public void Push(int val)
        {
            stack.Push(val);
            val = Math.Min(val, minStack.Count == 0 ? val : minStack.Peek());
            minStack.Push(val);
        }

        public void Pop()
        {
            stack.Pop();
            minStack.Pop();
        }

        public int Top()
        {
            return stack.Peek();
        }

        public int GetMin()
        {
            return minStack.Peek();
        }
    }

    //The real key to this problem was instantiating two stacks for the class: stack and minStack. In doing so,
    //we're able to use stack for all our normal stack behavior and use minStack for very specifically keeping
    //track of the smallest value seen.

}
