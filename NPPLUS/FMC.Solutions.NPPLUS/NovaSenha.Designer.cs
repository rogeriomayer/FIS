namespace FMC.Solutions.NPPLUS
{
    partial class NovaSenha
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NovaSenha));
            this.btnConfirmar = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelAlteracao = new DevExpress.XtraEditors.SimpleButton();
            this.separatorControl2 = new DevExpress.XtraEditors.SeparatorControl();
            this.txtSenhaAtual = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtNovaSenha = new DevExpress.XtraEditors.TextEdit();
            this.txtRepitaNovaSenha = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSenhaAtual.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNovaSenha.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRepitaNovaSenha.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnConfirmar.Appearance.Options.UseFont = true;
            this.btnConfirmar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConfirmar.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnConfirmar.ImageOptions.SvgImage")));
            this.btnConfirmar.Location = new System.Drawing.Point(262, 163);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(124, 40);
            this.btnConfirmar.TabIndex = 74;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // btnCancelAlteracao
            // 
            this.btnCancelAlteracao.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnCancelAlteracao.Appearance.Options.UseFont = true;
            this.btnCancelAlteracao.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelAlteracao.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnCancelAlteracao.ImageOptions.SvgImage")));
            this.btnCancelAlteracao.Location = new System.Drawing.Point(140, 163);
            this.btnCancelAlteracao.Name = "btnCancelAlteracao";
            this.btnCancelAlteracao.Size = new System.Drawing.Size(111, 40);
            this.btnCancelAlteracao.TabIndex = 75;
            this.btnCancelAlteracao.Text = "Cancelar";
            this.btnCancelAlteracao.Click += new System.EventHandler(this.btnCancelAlteracao_Click);
            // 
            // separatorControl2
            // 
            this.separatorControl2.Location = new System.Drawing.Point(14, 146);
            this.separatorControl2.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separatorControl2.Name = "separatorControl2";
            this.separatorControl2.Padding = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separatorControl2.Size = new System.Drawing.Size(372, 10);
            this.separatorControl2.TabIndex = 76;
            // 
            // txtSenhaAtual
            // 
            this.txtSenhaAtual.Location = new System.Drawing.Point(13, 34);
            this.txtSenhaAtual.Margin = new System.Windows.Forms.Padding(4);
            this.txtSenhaAtual.Name = "txtSenhaAtual";
            this.txtSenhaAtual.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 13F);
            this.txtSenhaAtual.Properties.Appearance.Options.UseFont = true;
            this.txtSenhaAtual.Properties.AutoHeight = false;
            this.txtSenhaAtual.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSenhaAtual.Properties.MaxLength = 8;
            this.txtSenhaAtual.Properties.Padding = new System.Windows.Forms.Padding(5);
            this.txtSenhaAtual.Properties.UseSystemPasswordChar = true;
            this.txtSenhaAtual.Size = new System.Drawing.Size(374, 33);
            this.txtSenhaAtual.TabIndex = 77;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(13, 14);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(67, 13);
            this.labelControl3.TabIndex = 78;
            this.labelControl3.Text = "Senha atual";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(13, 79);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(65, 13);
            this.labelControl4.TabIndex = 80;
            this.labelControl4.Text = "Nova senha";
            // 
            // txtNovaSenha
            // 
            this.txtNovaSenha.Location = new System.Drawing.Point(13, 98);
            this.txtNovaSenha.Margin = new System.Windows.Forms.Padding(4);
            this.txtNovaSenha.Name = "txtNovaSenha";
            this.txtNovaSenha.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 13F);
            this.txtNovaSenha.Properties.Appearance.Options.UseFont = true;
            this.txtNovaSenha.Properties.AutoHeight = false;
            this.txtNovaSenha.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNovaSenha.Properties.MaxLength = 8;
            this.txtNovaSenha.Properties.Padding = new System.Windows.Forms.Padding(5);
            this.txtNovaSenha.Properties.UseSystemPasswordChar = true;
            this.txtNovaSenha.Size = new System.Drawing.Size(183, 33);
            this.txtNovaSenha.TabIndex = 83;
            // 
            // txtRepitaNovaSenha
            // 
            this.txtRepitaNovaSenha.Location = new System.Drawing.Point(204, 98);
            this.txtRepitaNovaSenha.Margin = new System.Windows.Forms.Padding(4);
            this.txtRepitaNovaSenha.Name = "txtRepitaNovaSenha";
            this.txtRepitaNovaSenha.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 13F);
            this.txtRepitaNovaSenha.Properties.Appearance.Options.UseFont = true;
            this.txtRepitaNovaSenha.Properties.AutoHeight = false;
            this.txtRepitaNovaSenha.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRepitaNovaSenha.Properties.MaxLength = 8;
            this.txtRepitaNovaSenha.Properties.Padding = new System.Windows.Forms.Padding(5);
            this.txtRepitaNovaSenha.Properties.UseSystemPasswordChar = true;
            this.txtRepitaNovaSenha.Size = new System.Drawing.Size(183, 33);
            this.txtRepitaNovaSenha.TabIndex = 85;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(204, 79);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(115, 13);
            this.labelControl2.TabIndex = 84;
            this.labelControl2.Text = "Repita a nova senha";
            // 
            // NovaSenha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 218);
            this.Controls.Add(this.txtRepitaNovaSenha);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtNovaSenha);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtSenhaAtual);
            this.Controls.Add(this.separatorControl2);
            this.Controls.Add(this.btnCancelAlteracao);
            this.Controls.Add(this.btnConfirmar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NovaSenha";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alteração de Senha";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NovaSenha_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSenhaAtual.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNovaSenha.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRepitaNovaSenha.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnConfirmar;
        private DevExpress.XtraEditors.SimpleButton btnCancelAlteracao;
        private DevExpress.XtraEditors.SeparatorControl separatorControl2;
        private DevExpress.XtraEditors.TextEdit txtSenhaAtual;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtNovaSenha;
        private DevExpress.XtraEditors.TextEdit txtRepitaNovaSenha;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}