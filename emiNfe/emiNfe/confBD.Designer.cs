namespace criarNfeXML
{
    partial class confBD
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
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.txtBanco = new System.Windows.Forms.TextBox();
            this.lbl_usuario = new System.Windows.Forms.Label();
            this.lbl_senha = new System.Windows.Forms.Label();
            this.lbl_database = new System.Windows.Forms.Label();
            this.btn_salva = new System.Windows.Forms.Button();
            this.btn_sair = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(185, 22);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(139, 20);
            this.txtUser.TabIndex = 0;
            // 
            // txtSenha
            // 
            this.txtSenha.Location = new System.Drawing.Point(185, 48);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.Size = new System.Drawing.Size(139, 20);
            this.txtSenha.TabIndex = 1;
            // 
            // txtBanco
            // 
            this.txtBanco.Location = new System.Drawing.Point(185, 74);
            this.txtBanco.Name = "txtBanco";
            this.txtBanco.Size = new System.Drawing.Size(139, 20);
            this.txtBanco.TabIndex = 3;
            // 
            // lbl_usuario
            // 
            this.lbl_usuario.AutoSize = true;
            this.lbl_usuario.Location = new System.Drawing.Point(32, 29);
            this.lbl_usuario.Name = "lbl_usuario";
            this.lbl_usuario.Size = new System.Drawing.Size(56, 13);
            this.lbl_usuario.TabIndex = 4;
            this.lbl_usuario.Text = "USUÁRIO";
            // 
            // lbl_senha
            // 
            this.lbl_senha.AutoSize = true;
            this.lbl_senha.Location = new System.Drawing.Point(32, 55);
            this.lbl_senha.Name = "lbl_senha";
            this.lbl_senha.Size = new System.Drawing.Size(44, 13);
            this.lbl_senha.TabIndex = 5;
            this.lbl_senha.Text = "SENHA";
            // 
            // lbl_database
            // 
            this.lbl_database.AutoSize = true;
            this.lbl_database.Location = new System.Drawing.Point(32, 83);
            this.lbl_database.Name = "lbl_database";
            this.lbl_database.Size = new System.Drawing.Size(103, 13);
            this.lbl_database.TabIndex = 7;
            this.lbl_database.Text = "BANCO DE DADOS";
            // 
            // btn_salva
            // 
            this.btn_salva.Location = new System.Drawing.Point(60, 144);
            this.btn_salva.Name = "btn_salva";
            this.btn_salva.Size = new System.Drawing.Size(75, 23);
            this.btn_salva.TabIndex = 8;
            this.btn_salva.Text = "SALVAR";
            this.btn_salva.UseVisualStyleBackColor = true;
            this.btn_salva.Click += new System.EventHandler(this.btn_salva_Click);
            // 
            // btn_sair
            // 
            this.btn_sair.Location = new System.Drawing.Point(185, 144);
            this.btn_sair.Name = "btn_sair";
            this.btn_sair.Size = new System.Drawing.Size(75, 23);
            this.btn_sair.TabIndex = 9;
            this.btn_sair.Text = "SAIR";
            this.btn_sair.UseVisualStyleBackColor = true;
            this.btn_sair.Click += new System.EventHandler(this.btn_sair_Click);
            // 
            // confBD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 210);
            this.Controls.Add(this.btn_sair);
            this.Controls.Add(this.btn_salva);
            this.Controls.Add(this.lbl_database);
            this.Controls.Add(this.lbl_senha);
            this.Controls.Add(this.lbl_usuario);
            this.Controls.Add(this.txtBanco);
            this.Controls.Add(this.txtSenha);
            this.Controls.Add(this.txtUser);
            this.Name = "confBD";
            this.Text = "Conf. de banco de dados";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.TextBox txtBanco;
        private System.Windows.Forms.Label lbl_usuario;
        private System.Windows.Forms.Label lbl_senha;
        private System.Windows.Forms.Label lbl_database;
        private System.Windows.Forms.Button btn_salva;
        private System.Windows.Forms.Button btn_sair;
    }
}