using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopmilerProject
{
    class Syntax
    {
        private Lexical lexicalAnalyzer;
        Token LA;
        Token last_token;
        List<Error> errors = new List<Error>();
        Stack stack = new Stack();
        ProgramBlock pb = new ProgramBlock();



        public Syntax(Lexical lexical)
        {
            lexicalAnalyzer = lexical;
            if (lexicalAnalyzer.errorReporter().Count > 0)
            {
                Error error = new Error();
                error.analayzer = "SyntaxAnalyzer";
                error.word = "LexicalAnalyzerError";
                error.description = "lexical analyzer reported error . first check lexical errors";
                errors.Add(error);
            }
            else
            {
                LA = lexicalAnalyzer.nextToken();
            }
        }



        public void start()
        {
            if (lexicalAnalyzer.errorReporter().Count > 0)
            {
                return;
            }
            else
            {
                stmts();
                pb.append("halt", "", "", "");
                MessageBox.Show(pb.toString());
            }

        }


        private void stmts()
        {
            stmts1();
        }

        private void stmts1()
        {
            if (LA.type != "T_$" && LA.type != "T_}")
            {
                stmt();
                stmts1();
            }
        }


        private void stmt()
        {
            //generate tha code
            if (LA.type == "T_print")
            {
                match("print");
                match("(");
                expr();
                match(")");
                match(";");
                action_print();
            }
            else if (LA.type == "T_id")
            {
                action_pid();
                match("id");
                match("=");
                expr();
                match(";");
                action_assign();
            }
            else if (LA.type == "T_if")
            {
                match("if");
                match("(");
                _bool();
                match(")");
                action_save();
                stmt();
                action_fill_if_jmpf();
                action_save();
                stmt1();
            }
            else if (LA.type == "T_while" )
            {
                match("while");
                match("(");
                _bool();
                match(")");
                action_save();
                stmt();
                action_fill_while_jmpf();
            }
            else if (LA.type == "T_do")
            {
                match("do");
                action_match_do();
                stmt();
                match("while");
                match("(");
                _bool();
                match(")");
                match(";");
                action_fill_do_while_jmpf();
            }
            
            else if (LA.type == "T_{")
            {
                match("{");
                stmts1();
                match("}");
            }

            //else if (LA.type == "T_}")
            //{
            //    match("}");
            //}
            
        }


        private void stmt1()
        {
            if (LA.type != "T_$" && LA.type != "T_}")
            {
                if (LA.type == "T_else")
                {
                    match("else");
                    stmt();
                    action_fill_if_jmp();
                }
            }
        }

        private void expr()
        {
            term();
            expr1();
        }

        private void expr1()
        {
            if (LA.type != "T_$")
            {
                if (LA.type == "T_+")
                {
                    match("+");
                    term();
                    action_plus();
                    expr1();
                }
                else if (LA.type == "T_-")
                {
                    match("-");
                    term();
                    action_sub();
                    expr1();
                }
            }
        }

        

        private void term()
        {
            unary();
            term1();
        }

        private void term1()
        {
            if (LA.type != "T_$")
            {
                if (LA.type == "T_*")
                {
                    match("*");
                    unary();
                    action_mul();
                    term1();
                }
                else if (LA.type == "T_/")
                {
                    match("/");
                    unary();
                    action_div();
                    term1();
                }
            }
        }


        private void unary()
        {
            if (LA.type == "T_-")
            {
                match("-");
                unary();
                action_neg();
            }
            else
            {
                pow();
            }
        }

        private void _bool()
        {
            if (LA.type == "T_(")
            {
                match("(");
                _bool();
                match(")");
            }
            else if (LA.type == "T_!")
            {
                match("!");
                _bool();
                action_not();
            }
            //else
            //{
                rel();
                _bool1();
            //}
        }

        private void _bool1()
        {
            if (LA.type != "T_$")
            {
                if (LA.type == "T_#")
                {
                    match("#");
                    _bool();
                    action_and();
                    _bool1();
                }
                else if (LA.type == "T_%")
                {
                    match("%");
                    _bool();
                    action_or();
                    _bool1();
                }
            }
        }

        private void rel()
        {
            expr();
            rel1();
        }

        private void rel1()
        {
            if (LA.type == "T_==")
            {
                match("==");
                expr();
                action_eq();
            }
            else if (LA.type == "T_<>")
            {
                match("<>");
                expr();
                action_ne();
            }
            else if (LA.type == "T_<=")
            {
                match("<=");
                expr();
                action_le();
            }
            else if (LA.type == "T_>=")
            {
                match(">=");
                expr();
                action_ge();
            }
            else if (LA.type == "T_>")
            {
                match(">");
                expr();
                action_gt();
            }
            else if (LA.type == "T_<")
            {
                match("<");
                expr();
                action_lt();
            }
        }


        private void pow()
        {
            factor();
            pow1();
        }

        private void pow1()
        {
            if (LA.type != "T_$")
            {
                if (LA.type == "T_^")
                {
                    match("^");
                    pow();
                    action_pow();
                }
            }
        }

        private void factor()
        {
            if (LA.type == "T_num")
            {
                action_num();
                match("num");
            }
            else if (LA.type == "T_(")
            {
                match("(");
                expr();
                match(")");
            }
            else if (LA.type == "T_id")
            {
                action_pid();
                match("id");
            }

        }


 





        private void match(string word)
        {
           
            if (LA.type == ("T_" + word))
            {
                last_token = LA;
                LA = lexicalAnalyzer.nextToken();
            }
            else
            {
                error(LA.lexeme);
            }
        }







        //////////////////////////////////actions//////////////////////////////////

        private void action_save()
        {
            int ind = pb.skip();
            stack.push(ind);
        }

        private void action_fill_if_jmpf()
        {
            int ind = int.Parse(stack.pop());
            string t = stack.pop();
            int jmp_adr = pb.getI() + 1;
            pb.set(ind, "jmpf", t, "", jmp_adr.ToString());
        }

        private void action_fill_if_jmp()
        {
            int ind = int.Parse(stack.pop());
            int jmp_adr = pb.getI();
            pb.set(ind, "jmp", "", "", jmp_adr.ToString());
        }

        

        

        private void action_fill_while_jmpf()
        {
            int ind = int.Parse(stack.pop());
            string t = stack.pop();
            pb.append("jmp", "", "", ind.ToString());
            int jmp_adr = pb.getI();
            pb.set(ind, "jmpf", t, "", jmp_adr.ToString());
        }



        private void action_match_do()
        {
            int jmp_adr = pb.getI();
            stack.push(jmp_adr);
        }

        private void action_fill_do_while_jmpf()
        {
            string t = stack.pop();
            int ind = int.Parse(stack.pop());
            int jmp_adr = pb.getI();
            pb.append("jmpf", t, "", (jmp_adr + 2).ToString());
            pb.append("jmp", "", "", ind.ToString());
        }




        private void action_pid()
        {
            Token t = LA;
            //int ind = pb.findAddress(t.lexeme);
            //stack.push(ind);
            stack.push(t.lexeme);
        }

        private void action_num()
        {
            Token t = LA;
            stack.push("#" + t.lexeme);
        }

        private void action_assign()
        {
            string right = stack.pop();
            string left = stack.pop();
            pb.append("=", right, "", left);
        }

        private void action_plus()
        {
            string right = stack.pop();
            string left = stack.pop();
            string temp = pb.getTemp();
            pb.append("+", left, right, temp);
            stack.push(temp);
        }
        private void action_sub()
        {
            string right = stack.pop();
            string left = stack.pop();
            string temp = pb.getTemp();
            pb.append("-", left, right, temp);
            stack.push(temp);
        }
        private void action_mul()
        {
            string right = stack.pop();
            string left = stack.pop();
            string temp = pb.getTemp();
            pb.append("*", left, right, temp);
            stack.push(temp);
        }
        private void action_div()
        {
            string right = stack.pop();
            string left = stack.pop();
            string temp = pb.getTemp();
            pb.append("/", left, right, temp);
            stack.push(temp);
        }
        private void action_neg()
        {
            string right = stack.pop();
            string temp = pb.getTemp();
            pb.append("-", right, "", temp);
            stack.push(temp);
        }
        private void action_pow()
        {
            string right = stack.pop();
            string left = stack.pop();
            string temp = pb.getTemp();
            pb.append("^", left, right, temp);
            stack.push(temp);
        }
        private void action_and()
        {
            string right = stack.pop();
            string left = stack.pop();
            string temp = pb.getTemp();
            pb.append("#", left, right, temp);
            stack.push(temp);
        }
        private void action_or()
        {
            string right = stack.pop();
            string left = stack.pop();
            string temp = pb.getTemp();
            pb.append("%", left, right, temp);
            stack.push(temp);
        }
        private void action_not()
        {
            string right = stack.pop();
            string temp = pb.getTemp();
            pb.append("!", right, "", temp);
            stack.push(temp);
        }
        
        private void action_eq()
        {
            string right = stack.pop();
            string left = stack.pop();
            string temp = pb.getTemp();
            pb.append("==", left, right, temp);
            stack.push(temp);
        }
        private void action_lt()
        {
            string right = stack.pop();
            string left = stack.pop();
            string temp = pb.getTemp();
            pb.append("<", left, right, temp);
            stack.push(temp);
        }
        private void action_gt()
        {
            string right = stack.pop();
            string left = stack.pop();
            string temp = pb.getTemp();
            pb.append(">", left, right, temp);
            stack.push(temp);
        }
        private void action_le()
        {
            string right = stack.pop();
            string left = stack.pop();
            string temp = pb.getTemp();
            pb.append("<=", left, right, temp);
            stack.push(temp);
        }
        private void action_ge()
        {
            string right = stack.pop();
            string left = stack.pop();
            string temp = pb.getTemp();
            pb.append(">=", left, right, temp);
            stack.push(temp);
        }
        private void action_ne()
        {
            string right = stack.pop();
            string left = stack.pop();
            string temp = pb.getTemp();
            pb.append("<>", left, right, temp);
            stack.push(temp);
        }


        private void action_print()
        {
            string left = stack.pop();
            pb.append("print", left, "", "");
        }










        private void error(string word)
        {
            Error error = new Error();
            error.analayzer = "SyntaxAnalyzer";
            error.word = word;
            error.description = "error in " + LA.type ;
            errors.Add(error);
            
        }




        public List<Error> errorReporter()
        {
            return errors;
        }


       

    }
}
