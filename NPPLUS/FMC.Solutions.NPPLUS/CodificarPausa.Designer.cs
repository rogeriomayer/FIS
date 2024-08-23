namespace FMC.Solutions.NPPLUS
{
    partial class CodificarPausa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodificarPausa));
            this.groupControl9 = new DevExpress.XtraEditors.GroupControl();
            this.cbPausa = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl72 = new DevExpress.XtraEditors.LabelControl();
            this.btnConfirmar = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.separatorControl2 = new DevExpress.XtraEditors.SeparatorControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl9)).BeginInit();
            this.groupControl9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbPausa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl9
            // 
            this.groupControl9.Controls.Add(this.cbPausa);
            this.groupControl9.Controls.Add(this.labelControl72);
            this.groupControl9.Location = new System.Drawing.Point(12, 12);
            this.groupControl9.Name = "groupControl9";
            this.groupControl9.Size = new System.Drawing.Size(279, 95);
            this.groupControl9.TabIndex = 13;
            this.groupControl9.Text = "Pausa";
            // 
            // cbPausa
            // 
            this.cbPausa.Location = new System.Drawing.Point(9, 54);
            this.cbPausa.Name = "cbPausa";
            this.cbPausa.Properties.AutoHeight = false;
            this.cbPausa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbPausa.Size = new System.Drawing.Size(258, 30);
            this.cbPausa.TabIndex = 79;
            // 
            // labelControl72
            // 
            this.labelControl72.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl72.Appearance.Options.UseFont = true;
            this.labelControl72.Location = new System.Drawing.Point(9, 36);
            this.labelControl72.Name = "labelControl72";
            this.labelControl72.Size = new System.Drawing.Size(143, 13);
            this.labelControl72.TabIndex = 45;
            this.labelControl72.Text = "Selecione o tipo da Pausa";
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmar.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnConfirmar.Appearance.Options.UseFont = true;
            this.btnConfirmar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConfirmar.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnConfirmar.ImageOptions.SvgImage")));
            this.btnConfirmar.Location = new System.Drawing.Point(167, 128);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(124, 40);
            this.btnConfirmar.TabIndex = 73;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnCancel.ImageOptions.SvgImage")));
            this.btnCancel.Location = new System.Drawing.Point(47, 128);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(111, 40);
            this.btnCancel.TabIndex = 72;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // separatorControl2
            // 
            this.separatorControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.separatorControl2.Location = new System.Drawing.Point(13, 113);
            this.separatorControl2.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separatorControl2.Name = "separatorControl2";
            this.separatorControl2.Padding = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separatorControl2.Size = new System.Drawing.Size(277, 10);
            this.separatorControl2.TabIndex = 76;
            // 
            // CodificarPausa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 179);
            this.Controls.Add(this.separatorControl2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.groupControl9);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CodificarPausa";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Entrar para Pausa";
            this.Load += new System.EventHandler(this.Pausa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl9)).EndInit();
            this.groupControl9.ResumeLayout(false);
            this.groupControl9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbPausa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl9;
        private DevExpress.XtraEditors.LabelControl labelControl72;
        private DevExpress.XtraEditors.SimpleButton btnConfirmar;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SeparatorControl separatorControl2;
        private DevExpress.XtraEditors.ImageComboBoxEdit cbPausa;
    }
}