namespace FMC.Solutions.NPPLUS
{
    partial class Login_x
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
            DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::FMC.Solutions.NPPLUS.SplashScreen1), true, true, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.txtPass = new DevExpress.XtraEditors.TextEdit();
            this.txtUser = new DevExpress.XtraEditors.TextEdit();
            this.lblTxtUsuario = new DevExpress.XtraEditors.LabelControl();
            this.lblTxtSenha = new DevExpress.XtraEditors.LabelControl();
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.sidePanel1 = new DevExpress.XtraEditors.SidePanel();
            this.sidePanel2 = new DevExpress.XtraEditors.SidePanel();
            this.sidePanel3 = new DevExpress.XtraEditors.SidePanel();
            this.lblVersion = new DevExpress.XtraEditors.LabelControl();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblError = new DevExpress.XtraEditors.LabelControl();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.marqueeProgressBarControl1 = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblUsuarioP2 = new DevExpress.XtraEditors.LabelControl();
            this.txtUserP2 = new DevExpress.XtraEditors.TextEdit();
            this.lblAlterarSenha = new DevExpress.XtraEditors.LabelControl();
            this.splashScreenManager2 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::FMC.Solutions.NPPLUS.WaitForm1), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.txtPass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUser.Properties)).BeginInit();
            this.sidePanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserP2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // splashScreenManager1
            // 
            splashScreenManager1.ClosingDelay = 500;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(188, 333);
            this.txtPass.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtPass.Name = "txtPass";
            this.txtPass.Properties.Appearance.Font = new System.Drawing.Font("Barlow Semi Condensed Medium", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.txtPass.Properties.Appearance.Options.UseFont = true;
            this.txtPass.Properties.Appearance.Options.UseTextOptions = true;
            this.txtPass.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.txtPass.Properties.AutoHeight = false;
            this.txtPass.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtPass.Properties.Padding = new System.Windows.Forms.Padding(3);
            this.txtPass.Properties.UseSystemPasswordChar = true;
            this.txtPass.Size = new System.Drawing.Size(152, 35);
            this.txtPass.TabIndex = 3;
            this.txtPass.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPass_KeyUp);
            // 
            // txtUser
            // 
            this.txtUser.EditValue = "";
            this.txtUser.Location = new System.Drawing.Point(25, 276);
            this.txtUser.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtUser.Name = "txtUser";
            this.txtUser.Properties.Appearance.Font = new System.Drawing.Font("Barlow Semi Condensed Medium", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.txtUser.Properties.Appearance.Options.UseFont = true;
            this.txtUser.Properties.Appearance.Options.UseTextOptions = true;
            this.txtUser.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.txtUser.Properties.AutoHeight = false;
            this.txtUser.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtUser.Properties.Padding = new System.Windows.Forms.Padding(3);
            this.txtUser.Size = new System.Drawing.Size(315, 35);
            this.txtUser.TabIndex = 1;
            // 
            // lblTxtUsuario
            // 
            this.lblTxtUsuario.Appearance.Font = new System.Drawing.Font("Barlow Semi Condensed", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblTxtUsuario.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblTxtUsuario.Appearance.Options.UseFont = true;
            this.lblTxtUsuario.Appearance.Options.UseForeColor = true;
            this.lblTxtUsuario.Location = new System.Drawing.Point(28, 256);
            this.lblTxtUsuario.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblTxtUsuario.Name = "lblTxtUsuario";
            this.lblTxtUsuario.Size = new System.Drawing.Size(38, 17);
            this.lblTxtUsuario.TabIndex = 2;
            this.lblTxtUsuario.Text = "Usuário";
            // 
            // lblTxtSenha
            // 
            this.lblTxtSenha.Appearance.Font = new System.Drawing.Font("Barlow Semi Condensed", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblTxtSenha.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblTxtSenha.Appearance.Options.UseFont = true;
            this.lblTxtSenha.Appearance.Options.UseForeColor = true;
            this.lblTxtSenha.Location = new System.Drawing.Point(188, 314);
            this.lblTxtSenha.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblTxtSenha.Name = "lblTxtSenha";
            this.lblTxtSenha.Size = new System.Drawing.Size(47, 17);
            this.lblTxtSenha.TabIndex = 3;
            this.lblTxtSenha.Text = "Senha P2";
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
            this.btnLogin.TabIndex = 4;
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
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // sidePanel1
            // 
            this.sidePanel1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.sidePanel1.Appearance.Options.UseBackColor = true;
            this.sidePanel1.BorderThickness = 0;
            this.sidePanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.sidePanel1.Enabled = false;
            this.sidePanel1.Location = new System.Drawing.Point(0, 514);
            this.sidePanel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.sidePanel1.Name = "sidePanel1";
            this.sidePanel1.Size = new System.Drawing.Size(364, 8);
            this.sidePanel1.TabIndex = 8;
            this.sidePanel1.Text = "sidePanel1";
            // 
            // sidePanel2
            // 
            this.sidePanel2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
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
            this.sidePanel3.Appearance.BackColor = System.Drawing.Color.DarkRed;
            this.sidePanel3.Appearance.Options.UseBackColor = true;
            this.sidePanel3.BorderThickness = 0;
            this.sidePanel3.Controls.Add(this.lblVersion);
            this.sidePanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.sidePanel3.Enabled = false;
            this.sidePanel3.Location = new System.Drawing.Point(0, 0);
            this.sidePanel3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.sidePanel3.Name = "sidePanel3";
            this.sidePanel3.Size = new System.Drawing.Size(364, 30);
            this.sidePanel3.TabIndex = 10;
            this.sidePanel3.Text = "sidePanel3";
            // 
            // lblVersion
            // 
            this.lblVersion.Appearance.Font = new System.Drawing.Font("Barlow Semi Condensed", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblVersion.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblVersion.Appearance.Options.UseFont = true;
            this.lblVersion.Appearance.Options.UseForeColor = true;
            this.lblVersion.Appearance.Options.UseTextOptions = true;
            this.lblVersion.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblVersion.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblVersion.Location = new System.Drawing.Point(245, 4);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(114, 10);
            this.lblVersion.TabIndex = 17;
            this.lblVersion.Text = "v.1.0.0.0";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(97, 56);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(171, 140);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 12;
            this.pictureBox2.TabStop = false;
            // 
            // lblError
            // 
            this.lblError.Appearance.Font = new System.Drawing.Font("Barlow Semi Condensed", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblError.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblError.Appearance.Options.UseFont = true;
            this.lblError.Appearance.Options.UseForeColor = true;
            this.lblError.Appearance.Options.UseTextOptions = true;
            this.lblError.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblError.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblError.Location = new System.Drawing.Point(28, 229);
            this.lblError.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(312, 27);
            this.lblError.TabIndex = 13;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // marqueeProgressBarControl1
            // 
            this.marqueeProgressBarControl1.EditValue = 0;
            this.marqueeProgressBarControl1.Location = new System.Drawing.Point(0, 511);
            this.marqueeProgressBarControl1.Name = "marqueeProgressBarControl1";
            this.marqueeProgressBarControl1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.marqueeProgressBarControl1.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            this.marqueeProgressBarControl1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.marqueeProgressBarControl1.Properties.EndColor = System.Drawing.Color.White;
            this.marqueeProgressBarControl1.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.marqueeProgressBarControl1.Properties.MarqueeAnimationSpeed = 50;
            this.marqueeProgressBarControl1.Properties.StartColor = System.Drawing.Color.White;
            this.marqueeProgressBarControl1.Size = new System.Drawing.Size(364, 3);
            this.marqueeProgressBarControl1.TabIndex = 15;
            this.marqueeProgressBarControl1.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(120, 201);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(125, 27);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // lblUsuarioP2
            // 
            this.lblUsuarioP2.Appearance.Font = new System.Drawing.Font("Barlow Semi Condensed", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblUsuarioP2.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblUsuarioP2.Appearance.Options.UseFont = true;
            this.lblUsuarioP2.Appearance.Options.UseForeColor = true;
            this.lblUsuarioP2.Location = new System.Drawing.Point(25, 313);
            this.lblUsuarioP2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblUsuarioP2.Name = "lblUsuarioP2";
            this.lblUsuarioP2.Size = new System.Drawing.Size(54, 17);
            this.lblUsuarioP2.TabIndex = 18;
            this.lblUsuarioP2.Text = "Usuário P2";
            // 
            // txtUserP2
            // 
            this.txtUserP2.EditValue = "";
            this.txtUserP2.Location = new System.Drawing.Point(25, 333);
            this.txtUserP2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtUserP2.Name = "txtUserP2";
            this.txtUserP2.Properties.Appearance.Font = new System.Drawing.Font("Barlow Semi Condensed Medium", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.txtUserP2.Properties.Appearance.Options.UseFont = true;
            this.txtUserP2.Properties.Appearance.Options.UseTextOptions = true;
            this.txtUserP2.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.txtUserP2.Properties.AutoHeight = false;
            this.txtUserP2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtUserP2.Properties.Padding = new System.Windows.Forms.Padding(3);
            this.txtUserP2.Size = new System.Drawing.Size(154, 35);
            this.txtUserP2.TabIndex = 2;
            // 
            // lblAlterarSenha
            // 
            this.lblAlterarSenha.Appearance.Font = new System.Drawing.Font("Barlow Semi Condensed", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblAlterarSenha.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblAlterarSenha.Appearance.Options.UseFont = true;
            this.lblAlterarSenha.Appearance.Options.UseForeColor = true;
            this.lblAlterarSenha.Appearance.Options.UseTextOptions = true;
            this.lblAlterarSenha.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblAlterarSenha.AppearanceHovered.BackColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Warning;
            this.lblAlterarSenha.AppearanceHovered.Options.UseBackColor = true;
            this.lblAlterarSenha.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblAlterarSenha.Location = new System.Drawing.Point(262, 372);
            this.lblAlterarSenha.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblAlterarSenha.Name = "lblAlterarSenha";
            this.lblAlterarSenha.Size = new System.Drawing.Size(78, 17);
            this.lblAlterarSenha.TabIndex = 19;
            this.lblAlterarSenha.Text = "ALTERAR SENHA";
            this.lblAlterarSenha.Click += new System.EventHandler(this.lblAlterarSenha_Click);
            // 
            // splashScreenManager2
            // 
            this.splashScreenManager2.ClosingDelay = 500;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Stretch;
            this.BackgroundImageStore = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImageStore")));
            this.ClientSize = new System.Drawing.Size(364, 522);
            this.ControlBox = false;
            this.Controls.Add(this.lblAlterarSenha);
            this.Controls.Add(this.lblUsuarioP2);
            this.Controls.Add(this.txtUserP2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.marqueeProgressBarControl1);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.sidePanel3);
            this.Controls.Add(this.sidePanel2);
            this.Controls.Add(this.sidePanel1);
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
            this.sidePanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserP2.Properties)).EndInit();
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
        private DevExpress.XtraEditors.SidePanel sidePanel1;
        private DevExpress.XtraEditors.SidePanel sidePanel2;
        private DevExpress.XtraEditors.SidePanel sidePanel3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private DevExpress.XtraEditors.LabelControl lblError;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private DevExpress.XtraEditors.MarqueeProgressBarControl marqueeProgressBarControl1;
        private DevExpress.XtraEditors.LabelControl lblVersion;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.LabelControl lblUsuarioP2;
        private DevExpress.XtraEditors.TextEdit txtUserP2;
        private DevExpress.XtraEditors.LabelControl lblAlterarSenha;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager2;
    }
}