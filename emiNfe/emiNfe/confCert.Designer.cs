namespace criarNfeXML
{
    partial class confCert
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
            this.btn_salvar = new System.Windows.Forms.Button();
            this.btn_sair = new System.Windows.Forms.Button();
            this.txt_cnpj = new System.Windows.Forms.TextBox();
            this.txt_serie = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_salvar
            // 
            this.btn_salvar.Location = new System.Drawing.Point(53, 102);
            this.btn_salvar.Name = "btn_salvar";
            this.btn_salvar.Size = new System.Drawing.Size(75, 23);
            this.btn_salvar.TabIndex = 0;
            this.btn_salvar.Text = "SALVAR";
            this.btn_salvar.UseVisualStyleBackColor = true;
            this.btn_salvar.Click += new System.EventHandler(this.btn_salvar_Click);
            // 
            // btn_sair
            // 
            this.btn_sair.Location = new System.Drawing.Point(170, 102);
            this.btn_sair.Name = "btn_sair";
            this.btn_sair.Size = new System.Drawing.Size(75, 23);
            this.btn_sair.TabIndex = 1;
            this.btn_sair.Text = "SAIR";
            this.btn_sair.UseVisualStyleBackColor = true;
            this.btn_sair.Click += new System.EventHandler(this.button2_Click);
            // 
            // txt_cnpj
            // 
            this.txt_cnpj.Location = new System.Drawing.Point(112, 19);
            this.txt_cnpj.Name = "txt_cnpj";
            this.txt_cnpj.Size = new System.Drawing.Size(160, 20);
            this.txt_cnpj.TabIndex = 2;
            // 
            // txt_serie
            // 
            this.txt_serie.Location = new System.Drawing.Point(112, 56);
            this.txt_serie.Name = "txt_serie";
            this.txt_serie.Size = new System.Drawing.Size(160, 20);
            this.txt_serie.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "CNPJ:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Nº DE SERIE:";
            // 
            // confCert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 134);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_serie);
            this.Controls.Add(this.txt_cnpj);
            this.Controls.Add(this.btn_sair);
            this.Controls.Add(this.btn_salvar);
            this.Name = "confCert";
            this.Text = "confCert";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_salvar;
        private System.Windows.Forms.Button btn_sair;
        private System.Windows.Forms.TextBox txt_cnpj;
        private System.Windows.Forms.TextBox txt_serie;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}