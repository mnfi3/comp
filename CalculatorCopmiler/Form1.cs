using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CopmilerProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Lexical lexicalAnalyzer;
        Syntax syntaxAnalyzer;
        

        private void btn_lexical_Click(object sender, EventArgs e)
        {
            txt_result.Text = "";
            string input = txt_code.Text;
            input = input.Replace("\t", "");

            string tokensText = "";
            lexicalAnalyzer = new Lexical(input);
            string[] types = lexicalAnalyzer.getTypes();
            string[] lexemes = lexicalAnalyzer.getLexemes();
            List<Error> errors = lexicalAnalyzer.errorReporter();

            for (int i = 0; i < types.Length; i++)
            {
                tokensText += lexemes[i] + "\t: " + types[i] + "\n";
            }

            string errorsText = "";

            for (int i = 0; i < errors.Count; i++)
            {
                errorsText += (i + 1).ToString() + " : error in " + errors[i].analayzer + "\t" + "'" + errors[i].word + "'" +
                    " " + errors[i].description + "\n\n";
            }



            txt_tokens.Text = tokensText;
            txt_result.Text = errorsText;
    

            if (lexicalAnalyzer.errorReporter().Count == 0 && input.Length > 1)
            {
                txt_result.Text += ">>  no  lexical error :) \n>>  ";
            }
            else
            {
                txt_result.Text += ">>  ";
            }
            
        }







        private void btn_syntax_Click(object sender, EventArgs e)
        {
            txt_result.Text = "";
            syntaxAnalyzer = new Syntax(this.lexicalAnalyzer);
            syntaxAnalyzer.start();
            List<Error> syntaxErrors = syntaxAnalyzer.errorReporter();
            string errorsText = "";
            for (int i = 0; i < syntaxErrors.Count; i++)
            {
                errorsText += (i + 1).ToString() + " : error in " + syntaxErrors[i].analayzer + "\t" + "'" + syntaxErrors[i].word + "'" +
                     " " + syntaxErrors[i].description + "\n\n";
            }

            txt_result.Text = errorsText;

            if (lexicalAnalyzer.errorReporter().Count == 0 && syntaxAnalyzer.errorReporter().Count == 0 )
            {
                txt_result.Text += ">>  no  lexical error :) \n>>  no  syntax  error :) \n>>  code compiled successfully :) \n>>  ";
            }
            else if (lexicalAnalyzer.errorReporter().Count == 0 )
            {
                txt_result.Text += ">>  no  lexical error :) \n>>  ";
            }
            else
            {
                txt_result.Text += ">>  ";
            }

        }

        private void btn_pb_Click(object sender, EventArgs e)
        {
            txt_pb_code.Text = syntaxAnalyzer.pb.toString();
        }

        private void btn_run_Click(object sender, EventArgs e)
        {
            txt_result.Text = ">> code running started\n\n" + syntaxAnalyzer.pb.run() + "\n >> code running finished";
        }






        
    }
}
