namespace criarNfeXML
{
    partial class form_pai
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.nFeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iniciarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emitirNFeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pararToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conexaoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iniciarTesteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iniciarTesteDeCertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.confConexaoBDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.confCertDigitalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nFeToolStripMenuItem,
            this.conexaoToolStripMenuItem,
            this.configurarToolStripMenuItem,
            this.sairToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(553, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // nFeToolStripMenuItem
            // 
            this.nFeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iniciarToolStripMenuItem,
            this.emitirNFeToolStripMenuItem,
            this.pararToolStripMenuItem});
            this.nFeToolStripMenuItem.Name = "nFeToolStripMenuItem";
            this.nFeToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.nFeToolStripMenuItem.Text = "NFe";
            this.nFeToolStripMenuItem.Click += new System.EventHandler(this.nFeToolStripMenuItem_Click);
            // 
            // iniciarToolStripMenuItem
            // 
            this.iniciarToolStripMenuItem.Name = "iniciarToolStripMenuItem";
            this.iniciarToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.iniciarToolStripMenuItem.Text = "Iniciar";
            this.iniciarToolStripMenuItem.Click += new System.EventHandler(this.iniciarToolStripMenuItem_Click);
            // 
            // emitirNFeToolStripMenuItem
            // 
            this.emitirNFeToolStripMenuItem.Name = "emitirNFeToolStripMenuItem";
            this.emitirNFeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.emitirNFeToolStripMenuItem.Text = "Emitir NFe";
            this.emitirNFeToolStripMenuItem.Click += new System.EventHandler(this.emitirNFeToolStripMenuItem_Click);
            // 
            // pararToolStripMenuItem
            // 
            this.pararToolStripMenuItem.Name = "pararToolStripMenuItem";
            this.pararToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.pararToolStripMenuItem.Text = "Pausar Nfe";
            this.pararToolStripMenuItem.Click += new System.EventHandler(this.pararToolStripMenuItem_Click);
            // 
            // conexaoToolStripMenuItem
            // 
            this.conexaoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iniciarTesteToolStripMenuItem,
            this.iniciarTesteDeCertToolStripMenuItem});
            this.conexaoToolStripMenuItem.Name = "conexaoToolStripMenuItem";
            this.conexaoToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.conexaoToolStripMenuItem.Text = "Testes";
            // 
            // iniciarTesteToolStripMenuItem
            // 
            this.iniciarTesteToolStripMenuItem.Name = "iniciarTesteToolStripMenuItem";
            this.iniciarTesteToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.iniciarTesteToolStripMenuItem.Text = "Testar conexao";
            this.iniciarTesteToolStripMenuItem.Click += new System.EventHandler(this.iniciarTesteToolStripMenuItem_Click);
            // 
            // iniciarTesteDeCertToolStripMenuItem
            // 
            this.iniciarTesteDeCertToolStripMenuItem.Name = "iniciarTesteDeCertToolStripMenuItem";
            this.iniciarTesteDeCertToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.iniciarTesteDeCertToolStripMenuItem.Text = "Testar certificado";
            this.iniciarTesteDeCertToolStripMenuItem.Click += new System.EventHandler(this.iniciarTesteDeCertToolStripMenuItem_Click);
            // 
            // configurarToolStripMenuItem
            // 
            this.configurarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.confConexaoBDToolStripMenuItem,
            this.confCertDigitalToolStripMenuItem});
            this.configurarToolStripMenuItem.Name = "configurarToolStripMenuItem";
            this.configurarToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.configurarToolStripMenuItem.Text = "Configurar";
            // 
            // confConexaoBDToolStripMenuItem
            // 
            this.confConexaoBDToolStripMenuItem.Name = "confConexaoBDToolStripMenuItem";
            this.confConexaoBDToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.confConexaoBDToolStripMenuItem.Text = "Conf. Conexao BD";
            this.confConexaoBDToolStripMenuItem.Click += new System.EventHandler(this.confConexaoBDToolStripMenuItem_Click);
            // 
            // confCertDigitalToolStripMenuItem
            // 
            this.confCertDigitalToolStripMenuItem.Name = "confCertDigitalToolStripMenuItem";
            this.confCertDigitalToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.confCertDigitalToolStripMenuItem.Text = "Conf. Cert. Digital";
            this.confCertDigitalToolStripMenuItem.Click += new System.EventHandler(this.confCertDigitalToolStripMenuItem_Click);
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.sairToolStripMenuItem.Text = "Sair";
            this.sairToolStripMenuItem.Click += new System.EventHandler(this.sairToolStripMenuItem_Click);
            // 
            // form_pai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(553, 508);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "form_pai";
            this.Text = "Emissor NFe 1.0";
            this.Load += new System.EventHandler(this.form_pai_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem nFeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iniciarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pararToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem conexaoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iniciarTesteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem confConexaoBDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem confCertDigitalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iniciarTesteDeCertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emitirNFeToolStripMenuItem;
    }
}