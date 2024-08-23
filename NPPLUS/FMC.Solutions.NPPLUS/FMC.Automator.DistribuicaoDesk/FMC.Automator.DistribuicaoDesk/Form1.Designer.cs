namespace FMC.Automator.DistribuicaoDesk
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.lblTot = new System.Windows.Forms.Label();
            this.lblRegistros = new System.Windows.Forms.Label();
            this.btnProcessar = new System.Windows.Forms.Button();
            this.pbrContas = new System.Windows.Forms.ProgressBar();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblErro = new System.Windows.Forms.Label();
            this.dgvErros = new System.Windows.Forms.DataGridView();
            this.ID_DEBTOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ERRO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabMainFrame = new System.Windows.Forms.TabControl();
            this.tabWCC = new System.Windows.Forms.TabPage();
            this.tabBZA = new System.Windows.Forms.TabPage();
            this.txtUserWCC = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassWCC = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPassBZA = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUserBZA = new System.Windows.Forms.TextBox();
            this.axAtmWCC = new AxAtmTerminalLib.AxAtmTerminal();
            this.axAtmBZA = new AxAtmTerminalLib.AxAtmTerminal();
            ((System.ComponentModel.ISupportInitialize)(this.dgvErros)).BeginInit();
            this.tabMainFrame.SuspendLayout();
            this.tabWCC.SuspendLayout();
            this.tabBZA.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axAtmWCC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axAtmBZA)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "TOTAL:";
            // 
            // lblTot
            // 
            this.lblTot.AutoSize = true;
            this.lblTot.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTot.Location = new System.Drawing.Point(142, 113);
            this.lblTot.Name = "lblTot";
            this.lblTot.Size = new System.Drawing.Size(103, 13);
            this.lblTot.TabIndex = 7;
            this.lblTot.Text = "PROCESSADOS:";
            // 
            // lblRegistros
            // 
            this.lblRegistros.AutoSize = true;
            this.lblRegistros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegistros.ForeColor = System.Drawing.Color.Red;
            this.lblRegistros.Location = new System.Drawing.Point(93, 113);
            this.lblRegistros.Name = "lblRegistros";
            this.lblRegistros.Size = new System.Drawing.Size(0, 13);
            this.lblRegistros.TabIndex = 9;
            // 
            // btnProcessar
            // 
            this.btnProcessar.Location = new System.Drawing.Point(527, 116);
            this.btnProcessar.Name = "btnProcessar";
            this.btnProcessar.Size = new System.Drawing.Size(83, 23);
            this.btnProcessar.TabIndex = 11;
            this.btnProcessar.Text = "PROCESSAR";
            this.btnProcessar.UseVisualStyleBackColor = true;
            this.btnProcessar.Click += new System.EventHandler(this.btnProcessar_Click);
            // 
            // pbrContas
            // 
            this.pbrContas.Location = new System.Drawing.Point(15, 145);
            this.pbrContas.Name = "pbrContas";
            this.pbrContas.Size = new System.Drawing.Size(595, 23);
            this.pbrContas.TabIndex = 12;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(251, 113);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(0, 13);
            this.lblTotal.TabIndex = 14;
            // 
            // lblErro
            // 
            this.lblErro.AutoSize = true;
            this.lblErro.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblErro.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErro.ForeColor = System.Drawing.Color.Red;
            this.lblErro.Location = new System.Drawing.Point(15, 183);
            this.lblErro.Name = "lblErro";
            this.lblErro.Size = new System.Drawing.Size(0, 13);
            this.lblErro.TabIndex = 15;
            // 
            // dgvErros
            // 
            this.dgvErros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvErros.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID_DEBTOR,
            this.ERRO});
            this.dgvErros.Location = new System.Drawing.Point(15, 549);
            this.dgvErros.Name = "dgvErros";
            this.dgvErros.ReadOnly = true;
            this.dgvErros.RowHeadersVisible = false;
            this.dgvErros.Size = new System.Drawing.Size(595, 139);
            this.dgvErros.TabIndex = 16;
            this.dgvErros.Visible = false;
            // 
            // ID_DEBTOR
            // 
            this.ID_DEBTOR.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ID_DEBTOR.DataPropertyName = "ID_DEBTOR";
            this.ID_DEBTOR.Frozen = true;
            this.ID_DEBTOR.HeaderText = "ID DEBTOR";
            this.ID_DEBTOR.Name = "ID_DEBTOR";
            this.ID_DEBTOR.ReadOnly = true;
            this.ID_DEBTOR.Width = 105;
            // 
            // ERRO
            // 
            this.ERRO.DataPropertyName = "ERRO";
            this.ERRO.Frozen = true;
            this.ERRO.HeaderText = "ERRO";
            this.ERRO.Name = "ERRO";
            this.ERRO.ReadOnly = true;
            this.ERRO.Width = 470;
            // 
            // tabMainFrame
            // 
            this.tabMainFrame.Controls.Add(this.tabWCC);
            this.tabMainFrame.Controls.Add(this.tabBZA);
            this.tabMainFrame.Location = new System.Drawing.Point(15, 174);
            this.tabMainFrame.Name = "tabMainFrame";
            this.tabMainFrame.SelectedIndex = 0;
            this.tabMainFrame.Size = new System.Drawing.Size(595, 360);
            this.tabMainFrame.TabIndex = 17;
            // 
            // tabWCC
            // 
            this.tabWCC.Controls.Add(this.axAtmWCC);
            this.tabWCC.Location = new System.Drawing.Point(4, 22);
            this.tabWCC.Name = "tabWCC";
            this.tabWCC.Padding = new System.Windows.Forms.Padding(3);
            this.tabWCC.Size = new System.Drawing.Size(587, 334);
            this.tabWCC.TabIndex = 0;
            this.tabWCC.Text = "WCC";
            this.tabWCC.UseVisualStyleBackColor = true;
            // 
            // tabBZA
            // 
            this.tabBZA.Controls.Add(this.axAtmBZA);
            this.tabBZA.Location = new System.Drawing.Point(4, 22);
            this.tabBZA.Name = "tabBZA";
            this.tabBZA.Padding = new System.Windows.Forms.Padding(3);
            this.tabBZA.Size = new System.Drawing.Size(587, 334);
            this.tabBZA.TabIndex = 1;
            this.tabBZA.Text = "BZA";
            this.tabBZA.UseVisualStyleBackColor = true;
            // 
            // txtUserWCC
            // 
            this.txtUserWCC.Location = new System.Drawing.Point(51, 23);
            this.txtUserWCC.Name = "txtUserWCC";
            this.txtUserWCC.Size = new System.Drawing.Size(100, 20);
            this.txtUserWCC.TabIndex = 18;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPassWCC);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtUserWCC);
            this.groupBox1.Location = new System.Drawing.Point(12, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(170, 90);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Login WCC";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Senha:";
            // 
            // txtPassWCC
            // 
            this.txtPassWCC.Location = new System.Drawing.Point(51, 51);
            this.txtPassWCC.Name = "txtPassWCC";
            this.txtPassWCC.PasswordChar = '*';
            this.txtPassWCC.Size = new System.Drawing.Size(100, 20);
            this.txtPassWCC.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Usuário:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtPassBZA);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtUserBZA);
            this.groupBox2.Location = new System.Drawing.Point(216, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(170, 90);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Login BZA";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Senha:";
            // 
            // txtPassBZA
            // 
            this.txtPassBZA.Location = new System.Drawing.Point(52, 54);
            this.txtPassBZA.Name = "txtPassBZA";
            this.txtPassBZA.PasswordChar = '*';
            this.txtPassBZA.Size = new System.Drawing.Size(100, 20);
            this.txtPassBZA.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Usuário:";
            // 
            // txtUserBZA
            // 
            this.txtUserBZA.Location = new System.Drawing.Point(52, 26);
            this.txtUserBZA.Name = "txtUserBZA";
            this.txtUserBZA.Size = new System.Drawing.Size(100, 20);
            this.txtUserBZA.TabIndex = 18;
            // 
            // axAtmWCC
            // 
            this.axAtmWCC.Location = new System.Drawing.Point(7, 4);
            this.axAtmWCC.Name = "axAtmWCC";
            this.axAtmWCC.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAtmWCC.OcxState")));
            this.axAtmWCC.Size = new System.Drawing.Size(574, 327);
            this.axAtmWCC.TabIndex = 0;
            // 
            // axAtmBZA
            // 
            this.axAtmBZA.Location = new System.Drawing.Point(7, 4);
            this.axAtmBZA.Name = "axAtmBZA";
            this.axAtmBZA.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAtmBZA.OcxState")));
            this.axAtmBZA.Size = new System.Drawing.Size(574, 324);
            this.axAtmBZA.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 695);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabMainFrame);
            this.Controls.Add(this.dgvErros);
            this.Controls.Add(this.lblErro);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.pbrContas);
            this.Controls.Add(this.btnProcessar);
            this.Controls.Add(this.lblRegistros);
            this.Controls.Add(this.lblTot);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "DISTRIBUIÇÃO DE DESKS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvErros)).EndInit();
            this.tabMainFrame.ResumeLayout(false);
            this.tabWCC.ResumeLayout(false);
            this.tabBZA.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axAtmWCC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axAtmBZA)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTot;
        private System.Windows.Forms.Label lblRegistros;
        private System.Windows.Forms.Button btnProcessar;
        private System.Windows.Forms.ProgressBar pbrContas;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblErro;
        private System.Windows.Forms.DataGridView dgvErros;
        private System.Windows.Forms.TabControl tabMainFrame;
        private System.Windows.Forms.TabPage tabWCC;
        private System.Windows.Forms.TabPage tabBZA;
        private AxAtmTerminalLib.AxAtmTerminal axAtmWCC;
        private AxAtmTerminalLib.AxAtmTerminal axAtmBZA;
        private System.Windows.Forms.TextBox txtUserWCC;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassWCC;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPassBZA;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUserBZA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_DEBTOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ERRO;
    }
}

