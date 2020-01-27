using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopmilerProject
{
    class Lexical
    {
        private string codeText;
        private string optimizedCodeText;
        private Dictionary<string, string> keyWords = new Dictionary<string, string>();
        private char[] signs = new char[16];//we have 15 sign
        private int tokenCounter = 0;

        private string[] lexemes;//include lexemes
        private string[] types;//include tokens
        
        List<Error> errors = new List<Error>();



        public Lexical(string input)
        {
            codeText = input;
            optimizedCodeText = input;
            fillKeyWords();
            fillSigns();
            optimizeSpaces();
            explodeCodeText();
            assignTypes();
        }




        public Token nextToken()
        {
            Token token = new Token();

            if (tokenCounter < types.Length)
            {
                token.type = types[tokenCounter];
                token.lexeme = lexemes[tokenCounter];
            }
            else
            {
                token.type = "T_$";
                token.lexeme = "$";
            }

            tokenCounter++;
            return token;
        }






        private void fillKeyWords()
        {
            //add reserved words

            keyWords.Add("print", "T_print");
            keyWords.Add("if", "T_if");
            keyWords.Add("else", "T_else");
            keyWords.Add("while", "T_while");
            keyWords.Add("do", "T_do");


            //add signs to reserved strings
            keyWords.Add("(", "T_(");
            keyWords.Add(")", "T_)");
            keyWords.Add(";", "T_;");
            keyWords.Add("=", "T_=");
            keyWords.Add("+", "T_+");
            keyWords.Add("-", "T_-");
            keyWords.Add("*", "T_*");
            keyWords.Add("^", "T_^");
            keyWords.Add("/", "T_/");
            keyWords.Add("{", "T_{");
            keyWords.Add("}", "T_}");
            keyWords.Add("#", "T_#");
            keyWords.Add("%", "T_%");
            keyWords.Add("!", "T_!");
            keyWords.Add("<", "T_<");
            keyWords.Add(">", "T_>");
        }


        private void fillSigns()
        {
            signs[0] = '(';
            signs[1] = ')';
            signs[2] = ';';
            signs[3] = '=';
            signs[4] = '+';
            signs[5] = '-';
            signs[6] = '*';
            signs[7] = '/';
            signs[8] = '^';
            signs[9] = '{';
            signs[10] = '}';
            signs[11] = '#';
            signs[12] = '%';
            signs[13] = '!';
            signs[14] = '<';
            signs[15] = '>';
        }


        private void optimizeSpaces()
        {
            string optimizedText = optimizedCodeText;
            for (int i = 0; i < optimizedCodeText.Length; i++)
            {
                for (int j = 0; j < signs.Length; j++)
                {
                    if (optimizedCodeText[i] == signs[j])
                    {
                        //when scientific symbol  detected
                        if ((signs[j] == '-' || signs[j] == '+') && (optimizedCodeText[i - 1] == 'e' || optimizedCodeText[i - 1] == 'E') && (optimizedCodeText[i - 2] >= '0' && optimizedCodeText[i - 2] <= '9') && (optimizedCodeText[i + 1] >= '0' && optimizedCodeText[i + 1] <= '9'))
                        {
                            i++;
                        }
                        // else if((signs[j] == '-' || signs[j] == '+') && i < optimizedCodeText.Length-1 && i > 0)
                        // {

                       // }
                        else
                        {
                            optimizedText = optimizedCodeText.Insert(i + 1, " ");
                            optimizedCodeText = optimizedText;
                            optimizedText = optimizedCodeText.Insert(i, " ");
                            optimizedCodeText = optimizedText;
                            i++;
                        }

                    }
                }
            }
        }



        private void explodeCodeText()
        {
            char[] spliteChars = { ' ', '\n', '\r' };
            lexemes = mSplit(optimizedCodeText, spliteChars);

            for (int i = 0; i < lexemes.Length; i++)
            {
                if (lexemes[i] == "" || lexemes[i] == " " || lexemes[i] == "\n" || lexemes[i] == "\r")
                {
                    lexemes = lexemes.Where(w => w != lexemes[i]).ToArray();
                }
            }
        }




        private void assignTypes()
        {
            types = new string[lexemes.Length];
            bool isLikeNumber = false;
            bool isLikeId = false;
            int dotCount = 0;
            int typeIndex = 0;


            for (int i = 0; i < lexemes.Length; i++)
            {
                //lexeme exist in keywords ?
                if (keyWords.ContainsKey(lexemes[i]))
                {
                    types[typeIndex] = keyWords[lexemes[i]];
                    typeIndex++;
                }



                //lexeme is number ?
                else if ((lexemes[i][0] >= '0' && lexemes[i][0] <= '9') || lexemes[i][0] == '.')
                {
                    isLikeNumber = true;
                    for (int j = 1; j < lexemes[i].Length; j++)
                    {

                        if ((lexemes[i][j] < '0' || lexemes[i][j] > '9') && lexemes[i][j] != '.' && lexemes[i][j] != 'e' && lexemes[i][j] != 'E' && lexemes[i][j] != '-' && lexemes[i][j] != '+')
                        {
                            isLikeNumber = false;
                            break;
                        }
                    }

                    if (isLikeNumber)
                    {
                        dotCount = 0;
                        int eCount = 0;
                        int signCount = 0;
                        for (int j = 0; j < lexemes[i].Length; j++)
                        {
                            if (lexemes[i][j] == '.')
                                dotCount++;
                        }
                        for (int j = 0; j < lexemes[i].Length; j++)
                        {
                            if (lexemes[i][j] == 'e' || lexemes[i][j] == 'E')
                                eCount++;
                        }
                        for (int j = 0; j < lexemes[i].Length; j++)
                        {
                            if (lexemes[i][j] == '-')
                                signCount++;

                            if (lexemes[i][j] == '+')
                                signCount++;
                        }



                        if (dotCount == 0 && eCount == 0 && signCount == 0)
                        {
                            //types[typeIndex] = "T_int";
                            types[typeIndex] = "T_num";
                            typeIndex++;
                        }
                        else if (dotCount == 1 && eCount == 0 && signCount == 0)
                        {
                            //types[typeIndex] = "T_float";
                            types[typeIndex] = "T_num";
                            typeIndex++;
                        }
                        else if (dotCount == 1 && eCount == 1)
                        {
                            bool isScientificSymbol = true;
                            //find e index
                            for (int j = 1; j < lexemes[i].Length; j++)
                            {
                                if ((lexemes[i][j] == 'e' || lexemes[i][j] == 'E'))
                                {
                                    if (signCount == 1)
                                    {
                                        if ((lexemes[i].IndexOf('-') != (j + 1) && lexemes[i].IndexOf('+') != (j + 1)))
                                        {
                                            isScientificSymbol = false;
                                            break;
                                        }
                                        if (lexemes[i].Length <= j + 2)
                                        {
                                            isScientificSymbol = false;
                                            break;
                                        }
                                    }
                                    else if (signCount == 0)
                                    {
                                        if (signCount == 0 && lexemes[i].Length <= j + 1)
                                        {
                                            isScientificSymbol = false;
                                            break;
                                        }
                                    }
                                    if (lexemes[i].IndexOf('.') >= (j - 1))
                                    {
                                        isScientificSymbol = false;
                                        break;
                                    }

                                }
                            }

                            if (isScientificSymbol)
                            {
                                //types[typeIndex] = "T_float";
                                types[typeIndex] = "T_num";
                                typeIndex++;
                            }
                            else
                            {
                                types[typeIndex] = "T_unknown";
                                typeIndex++;
                            }


                        }
                        else
                        {
                            types[typeIndex] = "T_unknown";
                            typeIndex++;
                        }
                    }
                    else
                    {
                        types[typeIndex] = "T_unknown";
                        typeIndex++;
                    }

                }




                //lexeme is a id ?
                else if ((lexemes[i][0] >= 'A' && lexemes[i][0] <= 'Z') || (lexemes[i][0] >= 'a' && lexemes[i][0] <= 'z') || lexemes[i][0] == '_')
                {
                    isLikeId = true;
                    for (int j = 1; j < lexemes[i].Length; j++)
                    {
                        if (((lexemes[i][j] < 'A' || lexemes[i][j] > 'Z') && (lexemes[i][j] < 'a' || lexemes[i][j] > 'z')) && (lexemes[i][j] < '0' || lexemes[i][j] > '9')  && lexemes[i][j] != '_')
                        {
                            isLikeId = false;
                            break;
                        }
                    }
                    if (isLikeId)
                    {
                        types[typeIndex] = "T_id";
                        typeIndex++;
                    }
                    else
                    {
                        types[typeIndex] = "T_unknown";
                        typeIndex++;
                    }
                }



                //lexeme not (keyword & number & id)
                else
                {
                    types[typeIndex] = "T_unknown";
                    typeIndex++;
                }


            }





            
            
            List<string> listLexemes = new List<string>(lexemes);
            List<string> listTypes = new List<string>(types);

            for (int i = 0; i < listTypes.Count; i++)
            {
                //assign signed(negative) float and int
                if (listTypes[i] == "T_-" || listTypes[i] == "T_+")
                {
                    //if ((listTypes[i + 1] == "T_int" || listTypes[i + 1] == "T_float") && i > 0)
                    if (listTypes[i + 1] == "T_num" && i > 0)
                    {
                        if (listTypes[i - 1] == "T_-" || listTypes[i - 1] == "T_+" || listTypes[i - 1] == "T_*" ||
                            listTypes[i - 1] == "T_/" || listTypes[i - 1] == "T_^" || listTypes[i - 1] == "T_(" || listTypes[i - 1] == "T_=")
                        {
                            listLexemes[i + 1] = listLexemes[i] + listLexemes[i + 1];

                            listLexemes.RemoveAt(i);
                            listTypes.RemoveAt(i);

                        }
                    }
                }




                //find ==
                if (listTypes[i] == "T_=" && listTypes[i + 1] == "T_=")
                {
                    listLexemes[i + 1] = listLexemes[i] + listLexemes[i + 1];
                    listTypes[i + 1] = "T_==";
                    listLexemes.RemoveAt(i);
                    listTypes.RemoveAt(i);
                }
                //find <>
                if (listTypes[i] == "T_<" && listTypes[i + 1] == "T_>")
                {
                    listLexemes[i + 1] = listLexemes[i] + listLexemes[i + 1];
                    listTypes[i + 1] = "T_<>";
                    listLexemes.RemoveAt(i);
                    listTypes.RemoveAt(i);
                }
                //find <=
                if (listTypes[i] == "T_<" && listTypes[i + 1] == "T_=")
                {
                    listLexemes[i + 1] = listLexemes[i] + listLexemes[i + 1];
                    listTypes[i + 1] = "T_<=";
                    listLexemes.RemoveAt(i);
                    listTypes.RemoveAt(i);
                }
                //find >=
                if (listTypes[i] == "T_>" && listTypes[i + 1] == "T_=")
                {
                    listLexemes[i + 1] = listLexemes[i] + listLexemes[i + 1];
                    listTypes[i + 1] = "T_>=";
                    listLexemes.RemoveAt(i);
                    listTypes.RemoveAt(i);
                }


            }

            lexemes = listLexemes.ToArray();
            types = listTypes.ToArray();
        }










        public List<Error> errorReporter()
        {
            for (int i = 0; i < types.Length; i++)
            {
                if (types[i] == "T_unknown")
                {
                    Error error = new Error();
                    error.word = lexemes[i];
                    error.analayzer = "LexicalAnalyzer";
                    error.description = "has undefined type";
                    errors.Add(error);
                }
            }
            return errors;
        }






        private string[] mSplit(string text, char[] chars){
            text += " ";
            bool is_set_first_index = false;
            int first_index = 0;
            int second_index = 0;
            List<string> output = new List<string>();

            for (int i = 0; i<text.Length ; i++)
            {
                bool is_equal = false;
                for (int j = 0; j < chars.Length; j++)
                {
                    if (text[i] == chars[j])
                    {
                        is_equal = true;
                        break;
                    } 
                }




                if (is_equal == false)
                {
                    if (is_set_first_index == false)
                    {
                        is_set_first_index = true;
                        first_index = i;
                    }
                }
                else
                {
                    if (is_set_first_index == true)
                    {
                        is_set_first_index = false;
                        second_index = i - 1;
                        string word = text.Substring(first_index, (second_index - first_index + 1));
                        output.Add(word);
                    }
                }

            }

                return output.ToArray();
        }






        public string[] getLexemes()
        {
            return lexemes;
        }


        public string[] getTypes()
        {
            return types;
        }

       
        public string getCodeText()
        {
            return codeText;
        }

        public string getOptimizedCodeText()
        {
            return optimizedCodeText;
        }

    }
}
