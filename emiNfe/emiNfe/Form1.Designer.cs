namespace criarNfeXML
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
            this.txt = new System.Windows.Forms.TextBox();
            this.lbl1 = new System.Windows.Forms.Label();
            this.txt2 = new System.Windows.Forms.TextBox();
            this.lbl2 = new System.Windows.Forms.Label();
            this.txt_alerta = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // txt
            // 
            this.txt.Location = new System.Drawing.Point(12, 27);
            this.txt.Multiline = true;
            this.txt.Name = "txt";
            this.txt.ReadOnly = true;
            this.txt.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt.Size = new System.Drawing.Size(523, 224);
            this.txt.TabIndex = 1;
            this.txt.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(9, 11);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(71, 13);
            this.lbl1.TabIndex = 4;
            this.lbl1.Text = "Console NFe:";
            // 
            // txt2
            // 
            this.txt2.Location = new System.Drawing.Point(12, 412);
            this.txt2.Multiline = true;
            this.txt2.Name = "txt2";
            this.txt2.ReadOnly = true;
            this.txt2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt2.Size = new System.Drawing.Size(523, 50);
            this.txt2.TabIndex = 5;
            this.txt2.TextChanged += new System.EventHandler(this.txt2_TextChanged);
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(9, 396);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(107, 13);
            this.lbl2.TabIndex = 6;
            this.lbl2.Text = "Console de conexao:";
            // 
            // txt_alerta
            // 
            this.txt_alerta.Location = new System.Drawing.Point(12, 278);
            this.txt_alerta.Multiline = true;
            this.txt_alerta.Name = "txt_alerta";
            this.txt_alerta.ReadOnly = true;
            this.txt_alerta.Size = new System.Drawing.Size(523, 101);
            this.txt_alerta.TabIndex = 7;
            this.txt_alerta.TextChanged += new System.EventHandler(this.txt_alerta_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 262);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Alerta:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(452, 9);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(78, 13);
            this.linkLabel1.TabIndex = 9;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Limpar console";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(452, 262);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(78, 13);
            this.linkLabel2.TabIndex = 10;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Limpar console";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(452, 396);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(78, 13);
            this.linkLabel3.TabIndex = 11;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "Limpar console";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(542, 477);
            this.ControlBox = false;
            this.Controls.Add(this.linkLabel3);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_alerta);
            this.Controls.Add(this.txt2);
            this.Controls.Add(this.txt);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.lbl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.TextBox txt2;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.TextBox txt_alerta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel linkLabel3;
    }
}

