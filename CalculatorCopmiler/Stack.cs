using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopmilerProject
{
    class Stack
    {
        private List<string> values = new List<string>();
        private int top = -1;


        public void push(string value){
            top++;
            values.Add(value.ToString());
        }
        public void push(float value)
        {
            top++;
            values.Add(value.ToString());
        }
        public void push(int value)
        {
            top++;
            values.Add(value.ToString());
        }




        public string pop()
        {
            if (top == -1) return "stack is empty";

            string value = values[top];
            values.RemoveAt(top);
            top--;
            return value;
        }

       
    }
}
