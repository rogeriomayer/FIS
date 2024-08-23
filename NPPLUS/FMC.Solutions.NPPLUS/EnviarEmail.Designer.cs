namespace FMC.Solutions.NPPLUS
{
    partial class EnviarEmail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnviarEmail));
            this.groupControl9 = new DevExpress.XtraEditors.GroupControl();
            this.ckListBox = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.btnAdicionar = new DevExpress.XtraEditors.SimpleButton();
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.lblLogradouro = new DevExpress.XtraEditors.LabelControl();
            this.separatorControl3 = new DevExpress.XtraEditors.SeparatorControl();
            this.btnCancelAcordo = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirmar = new DevExpress.XtraEditors.SimpleButton();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::FMC.Solutions.NPPLUS.WaitForm1), true, true, true);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl9)).BeginInit();
            this.groupControl9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckListBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl3)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl9
            // 
            this.groupControl9.Controls.Add(this.ckListBox);
            this.groupControl9.Location = new System.Drawing.Point(12, 114);
            this.groupControl9.Name = "groupControl9";
            this.groupControl9.Size = new System.Drawing.Size(450, 176);
            this.groupControl9.TabIndex = 13;
            this.groupControl9.Text = "Lista de Emails";
            // 
            // ckListBox
            // 
            this.ckListBox.Location = new System.Drawing.Point(9, 36);
            this.ckListBox.Name = "ckListBox";
            this.ckListBox.Size = new System.Drawing.Size(431, 130);
            this.ckListBox.TabIndex = 3;
            this.ckListBox.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.ckListBox_ItemCheck);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.btnAdicionar);
            this.groupControl2.Controls.Add(this.txtEmail);
            this.groupControl2.Controls.Add(this.lblLogradouro);
            this.groupControl2.Location = new System.Drawing.Point(12, 12);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(450, 95);
            this.groupControl2.TabIndex = 66;
            this.groupControl2.Text = "Cadastrar Email";
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnAdicionar.Appearance.Options.UseFont = true;
            this.btnAdicionar.Appearance.Options.UseTextOptions = true;
            this.btnAdicionar.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnAdicionar.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnAdicionar.ImageOptions.SvgImage")));
            this.btnAdicionar.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.btnAdicionar.Location = new System.Drawing.Point(327, 55);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(114, 31);
            this.btnAdicionar.TabIndex = 2;
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(8, 55);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtEmail.Properties.Appearance.Options.UseFont = true;
            this.txtEmail.Properties.AutoHeight = false;
            this.txtEmail.Properties.Mask.EditMask = "90:00:00>LL";
            this.txtEmail.Properties.MaxLength = 40;
            this.txtEmail.Size = new System.Drawing.Size(311, 31);
            this.txtEmail.TabIndex = 1;
            this.txtEmail.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Asterisk;
            this.txtEmail.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEmail_KeyUp);
            // 
            // lblLogradouro
            // 
            this.lblLogradouro.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblLogradouro.Appearance.Options.UseFont = true;
            this.lblLogradouro.Location = new System.Drawing.Point(12, 35);
            this.lblLogradouro.Name = "lblLogradouro";
            this.lblLogradouro.Size = new System.Drawing.Size(30, 13);
            this.lblLogradouro.TabIndex = 52;
            this.lblLogradouro.Text = "Email";
            // 
            // separatorControl3
            // 
            this.separatorControl3.Location = new System.Drawing.Point(9, 304);
            this.separatorControl3.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separatorControl3.Name = "separatorControl3";
            this.separatorControl3.Padding = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separatorControl3.Size = new System.Drawing.Size(450, 10);
            this.separatorControl3.TabIndex = 78;
            // 
            // btnCancelAcordo
            // 
            this.btnCancelAcordo.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelAcordo.Appearance.Options.UseFont = true;
            this.btnCancelAcordo.Appearance.Options.UseTextOptions = true;
            this.btnCancelAcordo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnCancelAcordo.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelAcordo.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnCancelAcordo.ImageOptions.SvgImage")));
            this.btnCancelAcordo.Location = new System.Drawing.Point(227, 317);
            this.btnCancelAcordo.Name = "btnCancelAcordo";
            this.btnCancelAcordo.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.btnCancelAcordo.Size = new System.Drawing.Size(104, 40);
            this.btnCancelAcordo.TabIndex = 4;
            this.btnCancelAcordo.Text = "Cancelar";
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnConfirmar.Appearance.Options.UseFont = true;
            this.btnConfirmar.Appearance.Options.UseTextOptions = true;
            this.btnConfirmar.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnConfirmar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConfirmar.Enabled = false;
            this.btnConfirmar.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnConfirmar.ImageOptions.SvgImage")));
            this.btnConfirmar.Location = new System.Drawing.Point(341, 317);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(118, 40);
            this.btnConfirmar.TabIndex = 5;
            this.btnConfirmar.Text = "Enviar";
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // EnviarEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 373);
            this.Controls.Add(this.separatorControl3);
            this.Controls.Add(this.btnCancelAcordo);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl9);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EnviarEmail";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enviar por Email";
            this.Load += new System.EventHandler(this.EnviarEmail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl9)).EndInit();
            this.groupControl9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ckListBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl9;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton btnAdicionar;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private DevExpress.XtraEditors.LabelControl lblLogradouro;
        private DevExpress.XtraEditors.CheckedListBoxControl ckListBox;
        private DevExpress.XtraEditors.SeparatorControl separatorControl3;
        private DevExpress.XtraEditors.SimpleButton btnCancelAcordo;
        private DevExpress.XtraEditors.SimpleButton btnConfirmar;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
    }
}