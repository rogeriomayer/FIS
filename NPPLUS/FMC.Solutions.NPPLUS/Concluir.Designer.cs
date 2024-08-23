namespace FMC.Solutions.NPPLUS
{
    partial class Concluir
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Concluir));
            this.gcObs = new DevExpress.XtraEditors.GroupControl();
            this.txtMemo = new DevExpress.XtraEditors.MemoEdit();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.cbStatusDay = new DevExpress.XtraEditors.ComboBoxEdit();
            this.progressBar = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.lblLogradouro = new DevExpress.XtraEditors.LabelControl();
            this.separatorControl3 = new DevExpress.XtraEditors.SeparatorControl();
            this.btnCancelAcordo = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirmar = new DevExpress.XtraEditors.SimpleButton();
            this.txtDataVlc = new DevExpress.XtraEditors.DateEdit();
            this.gcVlc = new DevExpress.XtraEditors.GroupControl();
            this.txtTelefoneVlc = new DevExpress.XtraEditors.TextEdit();
            this.labelTelefone = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::FMC.Solutions.NPPLUS.WaitForm1), true, true, true);
            this.cbTelefone = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblTelefone = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcObs)).BeginInit();
            this.gcObs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbStatusDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataVlc.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataVlc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcVlc)).BeginInit();
            this.gcVlc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelefoneVlc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbTelefone.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gcObs
            // 
            this.gcObs.Controls.Add(this.txtMemo);
            this.gcObs.Location = new System.Drawing.Point(12, 114);
            this.gcObs.Name = "gcObs";
            this.gcObs.Size = new System.Drawing.Size(499, 130);
            this.gcObs.TabIndex = 1;
            this.gcObs.Text = "Observações";
            // 
            // txtMemo
            // 
            this.txtMemo.Enabled = false;
            this.txtMemo.Location = new System.Drawing.Point(9, 34);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtMemo.Properties.Appearance.Options.UseFont = true;
            this.txtMemo.Properties.Padding = new System.Windows.Forms.Padding(5);
            this.txtMemo.Size = new System.Drawing.Size(481, 86);
            this.txtMemo.TabIndex = 0;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.cbTelefone);
            this.groupControl2.Controls.Add(this.lblTelefone);
            this.groupControl2.Controls.Add(this.cbStatusDay);
            this.groupControl2.Controls.Add(this.progressBar);
            this.groupControl2.Controls.Add(this.lblLogradouro);
            this.groupControl2.Location = new System.Drawing.Point(12, 12);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(499, 95);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "Finalização";
            // 
            // cbStatusDay
            // 
            this.cbStatusDay.Location = new System.Drawing.Point(9, 54);
            this.cbStatusDay.Name = "cbStatusDay";
            this.cbStatusDay.Properties.Appearance.Options.UseFont = true;
            this.cbStatusDay.Properties.Appearance.Options.UseTextOptions = true;
            this.cbStatusDay.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.cbStatusDay.Properties.AutoComplete = false;
            this.cbStatusDay.Properties.AutoHeight = false;
            this.cbStatusDay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbStatusDay.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbStatusDay.Size = new System.Drawing.Size(252, 29);
            this.cbStatusDay.TabIndex = 0;
            this.cbStatusDay.SelectedIndexChanged += new System.EventHandler(this.cbStatusDay_SelectedIndexChanged);
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.EditValue = 0;
            this.progressBar.Location = new System.Drawing.Point(2, 88);
            this.progressBar.Name = "progressBar";
            this.progressBar.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressBar.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.progressBar.Properties.MarqueeAnimationSpeed = 30;
            this.progressBar.Size = new System.Drawing.Size(495, 5);
            this.progressBar.TabIndex = 73;
            // 
            // lblLogradouro
            // 
            this.lblLogradouro.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblLogradouro.Appearance.Options.UseFont = true;
            this.lblLogradouro.Location = new System.Drawing.Point(12, 35);
            this.lblLogradouro.Name = "lblLogradouro";
            this.lblLogradouro.Size = new System.Drawing.Size(24, 13);
            this.lblLogradouro.TabIndex = 52;
            this.lblLogradouro.Text = "Tipo";
            // 
            // separatorControl3
            // 
            this.separatorControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.separatorControl3.Location = new System.Drawing.Point(12, 251);
            this.separatorControl3.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separatorControl3.Name = "separatorControl3";
            this.separatorControl3.Padding = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separatorControl3.Size = new System.Drawing.Size(499, 4);
            this.separatorControl3.TabIndex = 78;
            // 
            // btnCancelAcordo
            // 
            this.btnCancelAcordo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelAcordo.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            this.btnCancelAcordo.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.btnCancelAcordo.Appearance.Options.UseBackColor = true;
            this.btnCancelAcordo.Appearance.Options.UseFont = true;
            this.btnCancelAcordo.Appearance.Options.UseTextOptions = true;
            this.btnCancelAcordo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnCancelAcordo.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelAcordo.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.CommonPalette;
            this.btnCancelAcordo.ImageOptions.SvgImageSize = new System.Drawing.Size(18, 18);
            this.btnCancelAcordo.Location = new System.Drawing.Point(298, 263);
            this.btnCancelAcordo.Name = "btnCancelAcordo";
            this.btnCancelAcordo.Size = new System.Drawing.Size(97, 40);
            this.btnCancelAcordo.TabIndex = 3;
            this.btnCancelAcordo.Text = "CANCELAR";
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmar.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.btnConfirmar.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.btnConfirmar.Appearance.Options.UseBackColor = true;
            this.btnConfirmar.Appearance.Options.UseFont = true;
            this.btnConfirmar.Appearance.Options.UseTextOptions = true;
            this.btnConfirmar.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnConfirmar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConfirmar.Enabled = false;
            this.btnConfirmar.Location = new System.Drawing.Point(403, 263);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(108, 40);
            this.btnConfirmar.TabIndex = 4;
            this.btnConfirmar.Text = "CONCLUIR";
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // txtDataVlc
            // 
            this.txtDataVlc.EditValue = null;
            this.txtDataVlc.Location = new System.Drawing.Point(9, 58);
            this.txtDataVlc.Name = "txtDataVlc";
            this.txtDataVlc.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtDataVlc.Properties.Appearance.Options.UseFont = true;
            this.txtDataVlc.Properties.AutoHeight = false;
            this.txtDataVlc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataVlc.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataVlc.Properties.CalendarTimeProperties.Mask.EditMask = "g";
            this.txtDataVlc.Properties.CalendarTimeProperties.Mask.UseMaskAsDisplayFormat = true;
            this.txtDataVlc.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.ClassicNew;
            this.txtDataVlc.Properties.Mask.EditMask = "g";
            this.txtDataVlc.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.txtDataVlc.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtDataVlc.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            this.txtDataVlc.Size = new System.Drawing.Size(252, 40);
            this.txtDataVlc.TabIndex = 0;
            // 
            // gcVlc
            // 
            this.gcVlc.Controls.Add(this.txtTelefoneVlc);
            this.gcVlc.Controls.Add(this.labelTelefone);
            this.gcVlc.Controls.Add(this.labelControl1);
            this.gcVlc.Controls.Add(this.txtDataVlc);
            this.gcVlc.Location = new System.Drawing.Point(12, 114);
            this.gcVlc.Name = "gcVlc";
            this.gcVlc.Size = new System.Drawing.Size(499, 107);
            this.gcVlc.TabIndex = 2;
            this.gcVlc.Text = "Dados para Voltar a Ligar";
            this.gcVlc.Visible = false;
            // 
            // txtTelefoneVlc
            // 
            this.txtTelefoneVlc.Location = new System.Drawing.Point(271, 58);
            this.txtTelefoneVlc.Margin = new System.Windows.Forms.Padding(4);
            this.txtTelefoneVlc.Name = "txtTelefoneVlc";
            this.txtTelefoneVlc.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtTelefoneVlc.Properties.Appearance.Options.UseFont = true;
            this.txtTelefoneVlc.Properties.AutoHeight = false;
            this.txtTelefoneVlc.Properties.Mask.EditMask = "(99) 0000-00009";
            this.txtTelefoneVlc.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            this.txtTelefoneVlc.Properties.Mask.ShowPlaceHolders = false;
            this.txtTelefoneVlc.Size = new System.Drawing.Size(218, 40);
            this.txtTelefoneVlc.TabIndex = 1;
            this.txtTelefoneVlc.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTelefoneVlc_KeyUp);
            this.txtTelefoneVlc.Leave += new System.EventHandler(this.txtTelefoneVlc_Leave);
            // 
            // labelTelefone
            // 
            this.labelTelefone.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelTelefone.Appearance.Options.UseFont = true;
            this.labelTelefone.Location = new System.Drawing.Point(270, 34);
            this.labelTelefone.Name = "labelTelefone";
            this.labelTelefone.Size = new System.Drawing.Size(49, 13);
            this.labelTelefone.TabIndex = 78;
            this.labelTelefone.Text = "Telefone";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(9, 36);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(67, 13);
            this.labelControl1.TabIndex = 74;
            this.labelControl1.Text = "Data e Hora";
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // cbTelefone
            // 
            this.cbTelefone.Location = new System.Drawing.Point(270, 53);
            this.cbTelefone.Name = "cbTelefone";
            this.cbTelefone.Properties.Appearance.Options.UseFont = true;
            this.cbTelefone.Properties.Appearance.Options.UseTextOptions = true;
            this.cbTelefone.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.cbTelefone.Properties.AutoComplete = false;
            this.cbTelefone.Properties.AutoHeight = false;
            this.cbTelefone.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbTelefone.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbTelefone.Size = new System.Drawing.Size(158, 29);
            this.cbTelefone.TabIndex = 74;
            this.cbTelefone.SelectedIndexChanged += new System.EventHandler(this.cbTelefone_SelectedIndexChanged);
            // 
            // lblTelefone
            // 
            this.lblTelefone.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblTelefone.Appearance.Options.UseFont = true;
            this.lblTelefone.Location = new System.Drawing.Point(273, 34);
            this.lblTelefone.Name = "lblTelefone";
            this.lblTelefone.Size = new System.Drawing.Size(49, 13);
            this.lblTelefone.TabIndex = 75;
            this.lblTelefone.Text = "Telefone";
            // 
            // Concluir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 312);
            this.Controls.Add(this.gcVlc);
            this.Controls.Add(this.separatorControl3);
            this.Controls.Add(this.btnCancelAcordo);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.gcObs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Concluir";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Conclusão";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Concluir_FormClosing);
            this.Load += new System.EventHandler(this.Concluir_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcObs)).EndInit();
            this.gcObs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbStatusDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataVlc.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataVlc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcVlc)).EndInit();
            this.gcVlc.ResumeLayout(false);
            this.gcVlc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelefoneVlc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbTelefone.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gcObs;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.MarqueeProgressBarControl progressBar;
        private DevExpress.XtraEditors.LabelControl lblLogradouro;
        private DevExpress.XtraEditors.SeparatorControl separatorControl3;
        private DevExpress.XtraEditors.SimpleButton btnCancelAcordo;
        private DevExpress.XtraEditors.SimpleButton btnConfirmar;
        private DevExpress.XtraEditors.ComboBoxEdit cbStatusDay;
        private DevExpress.XtraEditors.MemoEdit txtMemo;
        private DevExpress.XtraEditors.GroupControl gcVlc;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit txtDataVlc;
        private DevExpress.XtraEditors.TextEdit txtTelefoneVlc;
        private DevExpress.XtraEditors.LabelControl labelTelefone;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private DevExpress.XtraEditors.ComboBoxEdit cbTelefone;
        private DevExpress.XtraEditors.LabelControl lblTelefone;
    }
}