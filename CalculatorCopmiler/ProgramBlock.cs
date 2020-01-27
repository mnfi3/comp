using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopmilerProject
{
    class ProgramBlock
    {

        private List<string> variables = new List<string>();
        private List<string> ops = new List<string>(); 
        private List<string> addrs1 = new List<string>(); 
        private List<string> addrs2 = new List<string>(); 
        private List<string> addrs3 = new List<string>(); 
        private int index = 0;
        private int temp_counter = 0;

        public int append(string op, string addr1, string addr2, string addr3)
        {
            ops.Add(op);
            addrs1.Add(addr1);
            addrs2.Add(addr2);
            addrs3.Add(addr3);
            index++;
            return index - 1;
        }



        public void set(int ind, string op, string addr1, string addr2, string addr3)
        {
            ops[ind] = op;
            addrs1[ind] = addr1;
            addrs2[ind] = addr2;
            addrs3[ind] = addr3;
        }

        public int skip()
        {
            ops.Add("");
            addrs1.Add("");
            addrs2.Add("");
            addrs3.Add("");
            index++;
            return index - 1;
        }

        public string getTemp()
        {
            temp_counter++;
            return "t" + (temp_counter - 1).ToString();
        }

        public int getI()
        {
            return index;
        }


       

        //public int findAddress(string lexeme)
        //{
        //    if (lexeme[0] == '#') return int.Parse(lexeme.Substring(1));

        //    for (int i = 0; i < variables.Count; i++)
        //    {
        //        if (variables[i] == lexeme) return i;
        //    }

        //    variables.Add(lexeme);

        //    return variables.Count - 1;
        //}


        public string toString()
        {
            string str = "";
            for (int i = 0; i < ops.Count; i++)
            {
                str += i.ToString() + ": (" + ops[i] + ", " + addrs1[i] + ", " + addrs2[i] + ", " + addrs3[i] + ")\n";
            }

            return str;
        }





        //private void defineVars(string addr1, string addr2, string addr3)
        //{
        //    if (!variables.ContainsKey(addr1)) variables.Add(addr1, "");
        //    if (!variables.ContainsKey(addr2)) variables.Add(addr2, "");
        //    if (!variables.ContainsKey(addr3)) variables.Add(addr3, "");
        //}

    }
}
