namespace FMC.Solutions.NPPLUS
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.txtPass = new DevExpress.XtraEditors.TextEdit();
            this.txtUser = new DevExpress.XtraEditors.TextEdit();
            this.lblTxtUsuario = new DevExpress.XtraEditors.LabelControl();
            this.lblTxtSenha = new DevExpress.XtraEditors.LabelControl();
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.lblLoginAtendimento = new DevExpress.XtraEditors.LabelControl();
            this.sidePanel1 = new DevExpress.XtraEditors.SidePanel();
            this.sidePanel2 = new DevExpress.XtraEditors.SidePanel();
            this.sidePanel3 = new DevExpress.XtraEditors.SidePanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblError = new DevExpress.XtraEditors.LabelControl();
            this.splashScreenManager2 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::FMC.Solutions.NPPLUS.WaitForm1), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.txtPass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(25, 333);
            this.txtPass.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtPass.Name = "txtPass";
            this.txtPass.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.txtPass.Properties.Appearance.Options.UseFont = true;
            this.txtPass.Properties.Appearance.Options.UseTextOptions = true;
            this.txtPass.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.txtPass.Properties.AutoHeight = false;
            this.txtPass.Properties.Padding = new System.Windows.Forms.Padding(3);
            this.txtPass.Properties.UseSystemPasswordChar = true;
            this.txtPass.Size = new System.Drawing.Size(315, 35);
            this.txtPass.TabIndex = 2;
            // 
            // txtUser
            // 
            this.txtUser.EditValue = "";
            this.txtUser.Location = new System.Drawing.Point(25, 274);
            this.txtUser.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtUser.Name = "txtUser";
            this.txtUser.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.txtUser.Properties.Appearance.Options.UseFont = true;
            this.txtUser.Properties.Appearance.Options.UseTextOptions = true;
            this.txtUser.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.txtUser.Properties.AutoHeight = false;
            this.txtUser.Properties.Padding = new System.Windows.Forms.Padding(3);
            this.txtUser.Size = new System.Drawing.Size(315, 35);
            this.txtUser.TabIndex = 1;
            // 
            // lblTxtUsuario
            // 
            this.lblTxtUsuario.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblTxtUsuario.Appearance.Options.UseFont = true;
            this.lblTxtUsuario.Location = new System.Drawing.Point(28, 254);
            this.lblTxtUsuario.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblTxtUsuario.Name = "lblTxtUsuario";
            this.lblTxtUsuario.Size = new System.Drawing.Size(47, 16);
            this.lblTxtUsuario.TabIndex = 2;
            this.lblTxtUsuario.Text = "Usuário";
            // 
            // lblTxtSenha
            // 
            this.lblTxtSenha.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblTxtSenha.Appearance.Options.UseFont = true;
            this.lblTxtSenha.Location = new System.Drawing.Point(28, 314);
            this.lblTxtSenha.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblTxtSenha.Name = "lblTxtSenha";
            this.lblTxtSenha.Size = new System.Drawing.Size(39, 16);
            this.lblTxtSenha.TabIndex = 3;
            this.lblTxtSenha.Text = "Senha";
            // 
            // btnLogin
            // 
            this.btnLogin.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.btnLogin.Appearance.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.btnLogin.Appearance.Options.UseBackColor = true;
            this.btnLogin.Appearance.Options.UseFont = true;
            this.btnLogin.Location = new System.Drawing.Point(25, 401);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(315, 45);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Login";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            this.btnCancelar.Appearance.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.btnCancelar.Appearance.Options.UseBackColor = true;
            this.btnCancelar.Appearance.Options.UseFont = true;
            this.btnCancelar.Location = new System.Drawing.Point(25, 454);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(315, 45);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // lblLoginAtendimento
            // 
            this.lblLoginAtendimento.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblLoginAtendimento.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(151)))), ((int)(((byte)(211)))));
            this.lblLoginAtendimento.Appearance.Options.UseFont = true;
            this.lblLoginAtendimento.Appearance.Options.UseForeColor = true;
            this.lblLoginAtendimento.Location = new System.Drawing.Point(59, 185);
            this.lblLoginAtendimento.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblLoginAtendimento.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblLoginAtendimento.Name = "lblLoginAtendimento";
            this.lblLoginAtendimento.Size = new System.Drawing.Size(68, 26);
            this.lblLoginAtendimento.TabIndex = 7;
            this.lblLoginAtendimento.Text = "LOGIN";
            // 
            // sidePanel1
            // 
            this.sidePanel1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(143)))), ((int)(((byte)(200)))));
            this.sidePanel1.Appearance.Options.UseBackColor = true;
            this.sidePanel1.BorderThickness = 0;
            this.sidePanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.sidePanel1.Enabled = false;
            this.sidePanel1.Location = new System.Drawing.Point(0, 511);
            this.sidePanel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.sidePanel1.Name = "sidePanel1";
            this.sidePanel1.Size = new System.Drawing.Size(364, 11);
            this.sidePanel1.TabIndex = 8;
            this.sidePanel1.Text = "sidePanel1";
            // 
            // sidePanel2
            // 
            this.sidePanel2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(143)))), ((int)(((byte)(200)))));
            this.sidePanel2.Appearance.Options.UseBackColor = true;
            this.sidePanel2.BorderThickness = 0;
            this.sidePanel2.Enabled = false;
            this.sidePanel2.Location = new System.Drawing.Point(0, 30);
            this.sidePanel2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.sidePanel2.Name = "sidePanel2";
            this.sidePanel2.Size = new System.Drawing.Size(364, 10);
            this.sidePanel2.TabIndex = 9;
            this.sidePanel2.Text = "sidePanel2";
            // 
            // sidePanel3
            // 
            this.sidePanel3.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(151)))), ((int)(((byte)(211)))));
            this.sidePanel3.Appearance.Options.UseBackColor = true;
            this.sidePanel3.BorderThickness = 0;
            this.sidePanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.sidePanel3.Enabled = false;
            this.sidePanel3.Location = new System.Drawing.Point(0, 0);
            this.sidePanel3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.sidePanel3.Name = "sidePanel3";
            this.sidePanel3.Size = new System.Drawing.Size(364, 30);
            this.sidePanel3.TabIndex = 10;
            this.sidePanel3.Text = "sidePanel3";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(118, 51);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 128);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(59, 64);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(247, 105);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 12;
            this.pictureBox2.TabStop = false;
            // 
            // lblError
            // 
            this.lblError.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblError.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblError.Appearance.Options.UseFont = true;
            this.lblError.Appearance.Options.UseForeColor = true;
            this.lblError.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblError.Location = new System.Drawing.Point(28, 212);
            this.lblError.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(312, 42);
            this.lblError.TabIndex = 13;
            // 
            // splashScreenManager2
            // 
            this.splashScreenManager2.ClosingDelay = 500;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 522);
            this.ControlBox = false;
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.sidePanel3);
            this.Controls.Add(this.sidePanel2);
            this.Controls.Add(this.sidePanel1);
            this.Controls.Add(this.lblLoginAtendimento);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.lblTxtSenha);
            this.Controls.Add(this.lblTxtUsuario);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.txtPass);
            this.EnableAcrylicAccent = true;
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Glow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtPass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtPass;
        private DevExpress.XtraEditors.TextEdit txtUser;
        private DevExpress.XtraEditors.LabelControl lblTxtUsuario;
        private DevExpress.XtraEditors.LabelControl lblTxtSenha;
        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private DevExpress.XtraEditors.LabelControl lblLoginAtendimento;
        private DevExpress.XtraEditors.SidePanel sidePanel1;
        private DevExpress.XtraEditors.SidePanel sidePanel2;
        private DevExpress.XtraEditors.SidePanel sidePanel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private DevExpress.XtraEditors.LabelControl lblError;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager2;
    }
}