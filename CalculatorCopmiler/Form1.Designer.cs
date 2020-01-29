namespace CopmilerProject
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txt_code = new System.Windows.Forms.RichTextBox();
            this.txt_tokens = new System.Windows.Forms.RichTextBox();
            this.txt_result = new System.Windows.Forms.RichTextBox();
            this.txt_pb_code = new System.Windows.Forms.RichTextBox();
            this.btn_lexical = new System.Windows.Forms.Button();
            this.btn_syntax = new System.Windows.Forms.Button();
            this.btn_pb = new System.Windows.Forms.Button();
            this.btn_run = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_code
            // 
            this.txt_code.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txt_code.Location = new System.Drawing.Point(251, 47);
            this.txt_code.Name = "txt_code";
            this.txt_code.Size = new System.Drawing.Size(497, 472);
            this.txt_code.TabIndex = 1;
            this.txt_code.Text = "";
            // 
            // txt_tokens
            // 
            this.txt_tokens.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txt_tokens.Location = new System.Drawing.Point(19, 47);
            this.txt_tokens.Name = "txt_tokens";
            this.txt_tokens.Size = new System.Drawing.Size(226, 594);
            this.txt_tokens.TabIndex = 2;
            this.txt_tokens.Text = "";
            // 
            // txt_result
            // 
            this.txt_result.BackColor = System.Drawing.SystemColors.InfoText;
            this.txt_result.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txt_result.ForeColor = System.Drawing.Color.White;
            this.txt_result.Location = new System.Drawing.Point(251, 525);
            this.txt_result.Name = "txt_result";
            this.txt_result.Size = new System.Drawing.Size(497, 116);
            this.txt_result.TabIndex = 3;
            this.txt_result.Text = "";
            // 
            // txt_pb_code
            // 
            this.txt_pb_code.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txt_pb_code.Location = new System.Drawing.Point(754, 47);
            this.txt_pb_code.Name = "txt_pb_code";
            this.txt_pb_code.Size = new System.Drawing.Size(214, 594);
            this.txt_pb_code.TabIndex = 4;
            this.txt_pb_code.Text = "";
            // 
            // btn_lexical
            // 
            this.btn_lexical.Location = new System.Drawing.Point(225, 12);
            this.btn_lexical.Name = "btn_lexical";
            this.btn_lexical.Size = new System.Drawing.Size(118, 23);
            this.btn_lexical.TabIndex = 5;
            this.btn_lexical.Text = "lexical analyzer";
            this.btn_lexical.UseVisualStyleBackColor = true;
            this.btn_lexical.Click += new System.EventHandler(this.btn_lexical_Click);
            // 
            // btn_syntax
            // 
            this.btn_syntax.Location = new System.Drawing.Point(359, 12);
            this.btn_syntax.Name = "btn_syntax";
            this.btn_syntax.Size = new System.Drawing.Size(118, 23);
            this.btn_syntax.TabIndex = 6;
            this.btn_syntax.Text = "syntax analyzer";
            this.btn_syntax.UseVisualStyleBackColor = true;
            this.btn_syntax.Click += new System.EventHandler(this.btn_syntax_Click);
            // 
            // btn_pb
            // 
            this.btn_pb.Location = new System.Drawing.Point(494, 12);
            this.btn_pb.Name = "btn_pb";
            this.btn_pb.Size = new System.Drawing.Size(118, 23);
            this.btn_pb.TabIndex = 7;
            this.btn_pb.Text = "program block";
            this.btn_pb.UseVisualStyleBackColor = true;
            this.btn_pb.Click += new System.EventHandler(this.btn_pb_Click);
            // 
            // btn_run
            // 
            this.btn_run.Location = new System.Drawing.Point(630, 12);
            this.btn_run.Name = "btn_run";
            this.btn_run.Size = new System.Drawing.Size(118, 23);
            this.btn_run.TabIndex = 8;
            this.btn_run.Text = "run";
            this.btn_run.UseVisualStyleBackColor = true;
            this.btn_run.Click += new System.EventHandler(this.btn_run_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 647);
            this.Controls.Add(this.btn_run);
            this.Controls.Add(this.btn_pb);
            this.Controls.Add(this.btn_syntax);
            this.Controls.Add(this.btn_lexical);
            this.Controls.Add(this.txt_pb_code);
            this.Controls.Add(this.txt_result);
            this.Controls.Add(this.txt_tokens);
            this.Controls.Add(this.txt_code);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txt_code;
        private System.Windows.Forms.RichTextBox txt_tokens;
        private System.Windows.Forms.RichTextBox txt_result;
        private System.Windows.Forms.RichTextBox txt_pb_code;
        private System.Windows.Forms.Button btn_lexical;
        private System.Windows.Forms.Button btn_syntax;
        private System.Windows.Forms.Button btn_pb;
        private System.Windows.Forms.Button btn_run;
    }
}

