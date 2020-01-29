using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopmilerProject
{
    class ProgramBlock
    {

        private Dictionary<string, float> variables = new Dictionary<string, float>();
        private List<string> ops = new List<string>(); 
        private List<string> addrs1 = new List<string>(); 
        private List<string> addrs2 = new List<string>(); 
        private List<string> addrs3 = new List<string>(); 
        private int index = 0;
        private int temp_counter = 0;

        public int append(string op, string addr1, string addr2, string addr3)
        {
            defineVars(addr1, addr2, addr3);

            ops.Add(op);
            addrs1.Add(addr1);
            addrs2.Add(addr2);
            addrs3.Add(addr3);
            index++;
            return index - 1;
        }



        public void set(int ind, string op, string addr1, string addr2, string addr3)
        {
            defineVars(addr1, addr2, addr3);

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
                if (ops[i] == "") continue;
                str += i.ToString() + ": (" + ops[i] + ", " + addrs1[i] + ", " + addrs2[i] + ", " + addrs3[i] + ")\n";
            }

            return str;
        }





        private void defineVars(string addr1, string addr2, string addr3)
        {
            if (!variables.ContainsKey(addr1) && addr1 != "") variables.Add(addr1, 0);
            if (!variables.ContainsKey(addr2) && addr2 != "") variables.Add(addr2, 0);
            if (!variables.ContainsKey(addr3) && addr3 != "") variables.Add(addr3, 0);
        }


        private void setVar(string name, float value)
        {
            //variables[name] = value;
            variables.Remove(name);
            variables.Add(name, value);
        }

        private float getVar(string name)
        {
            if (name.Contains("#"))
            {
                return float.Parse(name.Replace("#", ""));
            }

            return variables[name];
        }


        public string run()
        {
            bool is_exit = false;
            int p = 0; //code pointer
            string output = "";
            while (!is_exit)
            {
                if (p > (ops.Count - 1) ) break;

                switch (ops[p])
                {
                    case "=":
                        setVar(addrs3[p], getVar(addrs1[p]));
                        p++;
                        break;

                    case "+":
                        setVar(addrs3[p], getVar(addrs1[p]) + getVar(addrs2[p]));
                        p++;
                        break;

                    case "-":
                        if(addrs2[p] != "")  setVar(addrs3[p], getVar(addrs1[p]) - getVar(addrs2[p]));
                        else setVar(addrs3[p], getVar(addrs1[p]) * -1); //negative
                        p++;
                        break;

                    case "*":
                        setVar(addrs3[p], getVar(addrs1[p]) * getVar(addrs2[p]));
                        p++;
                        break;

                    case "/":
                        setVar(addrs3[p], getVar(addrs1[p]) / getVar(addrs2[p]));
                        p++;
                        break;

                    case "^":
                        setVar(addrs3[p], (float)Math.Pow(getVar(addrs1[p]) , getVar(addrs2[p])) );
                        p++;
                        break;

                    case "#":
                        if (getVar(addrs1[p]) == 1 && getVar(addrs2[p]) == 1) setVar(addrs3[p], 1);
                        else setVar(addrs3[p], 0);
                        p++;
                        break;

                    case "%":
                        if (getVar(addrs1[p]) == 1 || getVar(addrs2[p]) == 1) setVar(addrs3[p], 1);
                        else setVar(addrs3[p], 0);
                        p++;
                        break;

                    case "!":
                        if (getVar(addrs1[p]) == 1) setVar(addrs3[p], 0);
                        else setVar(addrs3[p], 1);
                        p++;
                        break;

                    case "==":
                        if (getVar(addrs1[p]) == getVar(addrs2[p])) setVar(addrs3[p], 1);
                        else setVar(addrs3[p], 0);
                        p++;
                        break;

                    case "<=":
                        if (getVar(addrs1[p]) <= getVar(addrs2[p])) setVar(addrs3[p], 1);
                        else setVar(addrs3[p], 0);
                        p++;
                        break;

                    case ">=": 
                        if (getVar(addrs1[p]) >= getVar(addrs2[p])) setVar(addrs3[p], 1);
                        else setVar(addrs3[p], 0);
                        p++;
                        break;

                    case ">":
                        if (getVar(addrs1[p]) > getVar(addrs2[p])) setVar(addrs3[p], 1);
                        else setVar(addrs3[p], 0);
                        p++;
                        break;

                    case "<":
                        if (getVar(addrs1[p]) < getVar(addrs2[p])) setVar(addrs3[p], 1);
                        else setVar(addrs3[p], 0);
                        p++;
                        break;

                    case "<>":
                        if (getVar(addrs1[p]) != getVar(addrs2[p])) setVar(addrs3[p], 1);
                        else setVar(addrs3[p], 0);
                        p++;
                        break;

                    case "jmp":
                        p = int.Parse(addrs3[p]);
                        break;

                    case "jmpf":
                        if (getVar(addrs1[p]) == 0)
                        {
                            p = int.Parse(addrs3[p]);
                        }
                        else p++;
                        break;

                    case "print":
                        output += getVar(addrs1[p]) + "\n";
                        p++;
                        break;

                    case "halt":
                        is_exit = true;
                        break;


                    case "":
                        p++;
                        break;

                    default:
                        is_exit = true;
                        break;

                }
            }

            return output;
        }





       

    }
}
