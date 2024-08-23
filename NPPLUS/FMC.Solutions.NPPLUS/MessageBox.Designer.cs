namespace FMC.Solutions.NPPLUS
{
    partial class MessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageBox));
            this.btnConfirmar = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelAlteracao = new DevExpress.XtraEditors.SimpleButton();
            this.separator = new DevExpress.XtraEditors.SeparatorControl();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblDestaque = new DevExpress.XtraEditors.LabelControl();
            this.lblMessage = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.separator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnConfirmar.Appearance.Options.UseFont = true;
            this.btnConfirmar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConfirmar.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnConfirmar.ImageOptions.SvgImage")));
            this.btnConfirmar.Location = new System.Drawing.Point(316, 106);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(88, 40);
            this.btnConfirmar.TabIndex = 74;
            this.btnConfirmar.Text = "Sim";
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // btnCancelAlteracao
            // 
            this.btnCancelAlteracao.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnCancelAlteracao.Appearance.Options.UseFont = true;
            this.btnCancelAlteracao.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelAlteracao.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnCancelAlteracao.ImageOptions.SvgImage")));
            this.btnCancelAlteracao.Location = new System.Drawing.Point(194, 106);
            this.btnCancelAlteracao.Name = "btnCancelAlteracao";
            this.btnCancelAlteracao.Size = new System.Drawing.Size(111, 40);
            this.btnCancelAlteracao.TabIndex = 75;
            this.btnCancelAlteracao.Text = "Cancelar";
            this.btnCancelAlteracao.Click += new System.EventHandler(this.btnCancelAlteracao_Click_1);
            // 
            // separator
            // 
            this.separator.Location = new System.Drawing.Point(14, 89);
            this.separator.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separator.Name = "separator";
            this.separator.Padding = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separator.Size = new System.Drawing.Size(391, 10);
            this.separator.TabIndex = 76;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.InitialImage = null;
            this.pictureBox2.Location = new System.Drawing.Point(20, 35);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(37, 44);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 77;
            this.pictureBox2.TabStop = false;
            // 
            // lblDestaque
            // 
            this.lblDestaque.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblDestaque.Appearance.Options.UseFont = true;
            this.lblDestaque.Location = new System.Drawing.Point(67, 33);
            this.lblDestaque.Name = "lblDestaque";
            this.lblDestaque.Size = new System.Drawing.Size(6, 16);
            this.lblDestaque.TabIndex = 78;
            this.lblDestaque.Text = "-";
            // 
            // lblMessage
            // 
            this.lblMessage.AllowHtmlString = true;
            this.lblMessage.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lblMessage.Appearance.Options.UseFont = true;
            this.lblMessage.Appearance.Options.UseTextOptions = true;
            this.lblMessage.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lblMessage.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblMessage.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblMessage.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftTop;
            this.lblMessage.Location = new System.Drawing.Point(67, 57);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(330, 22);
            this.lblMessage.TabIndex = 79;
            this.lblMessage.Text = "-";
            // 
            // MessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 159);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblDestaque);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.separator);
            this.Controls.Add(this.btnCancelAlteracao);
            this.Controls.Add(this.btnConfirmar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "-";
            this.Load += new System.EventHandler(this.MessageBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.separator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnConfirmar;
        private DevExpress.XtraEditors.SimpleButton btnCancelAlteracao;
        private DevExpress.XtraEditors.SeparatorControl separator;
        private System.Windows.Forms.PictureBox pictureBox2;
        private DevExpress.XtraEditors.LabelControl lblDestaque;
        private DevExpress.XtraEditors.LabelControl lblMessage;
    }
}