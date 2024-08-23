namespace FMC.Solutions.NPPLUS
{
    partial class CarregarConta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CarregarConta));
            this.groupControl9 = new DevExpress.XtraEditors.GroupControl();
            this.cbTipo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtNumero = new DevExpress.XtraEditors.TextEdit();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl72 = new DevExpress.XtraEditors.LabelControl();
            this.btnConfirmar = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.separatorControl2 = new DevExpress.XtraEditors.SeparatorControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl9)).BeginInit();
            this.groupControl9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbTipo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumero.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl9
            // 
            this.groupControl9.Controls.Add(this.cbTipo);
            this.groupControl9.Controls.Add(this.txtNumero);
            this.groupControl9.Controls.Add(this.labelControl15);
            this.groupControl9.Controls.Add(this.labelControl72);
            this.groupControl9.Location = new System.Drawing.Point(12, 12);
            this.groupControl9.Name = "groupControl9";
            this.groupControl9.Size = new System.Drawing.Size(314, 157);
            this.groupControl9.TabIndex = 13;
            this.groupControl9.Text = "Dados para Pesquisa";
            // 
            // cbTipo
            // 
            this.cbTipo.EditValue = "CPF";
            this.cbTipo.Location = new System.Drawing.Point(9, 54);
            this.cbTipo.Name = "cbTipo";
            this.cbTipo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.cbTipo.Properties.Appearance.Options.UseFont = true;
            this.cbTipo.Properties.AutoHeight = false;
            this.cbTipo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbTipo.Properties.Items.AddRange(new object[] {
            "CPF"});
            this.cbTipo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbTipo.Size = new System.Drawing.Size(294, 30);
            this.cbTipo.TabIndex = 70;
            this.cbTipo.SelectedIndexChanged += new System.EventHandler(this.cbTipo_SelectedIndexChanged);
            // 
            // txtNumero
            // 
            this.txtNumero.Location = new System.Drawing.Point(9, 115);
            this.txtNumero.Margin = new System.Windows.Forms.Padding(4);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtNumero.Properties.Appearance.Options.UseFont = true;
            this.txtNumero.Properties.AutoHeight = false;
            this.txtNumero.Properties.Mask.EditMask = "000.000.000-00";
            this.txtNumero.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            this.txtNumero.Properties.MaxLength = 14;
            this.txtNumero.Properties.Padding = new System.Windows.Forms.Padding(5);
            this.txtNumero.Size = new System.Drawing.Size(293, 30);
            this.txtNumero.TabIndex = 71;
            this.txtNumero.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNumero_KeyUp);
            this.txtNumero.Leave += new System.EventHandler(this.txtNumero_Leave);
            // 
            // labelControl15
            // 
            this.labelControl15.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl15.Appearance.Options.UseFont = true;
            this.labelControl15.Location = new System.Drawing.Point(9, 97);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(50, 13);
            this.labelControl15.TabIndex = 68;
            this.labelControl15.Text = "Números";
            // 
            // labelControl72
            // 
            this.labelControl72.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl72.Appearance.Options.UseFont = true;
            this.labelControl72.Location = new System.Drawing.Point(9, 36);
            this.labelControl72.Name = "labelControl72";
            this.labelControl72.Size = new System.Drawing.Size(24, 13);
            this.labelControl72.TabIndex = 45;
            this.labelControl72.Text = "Tipo";
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnConfirmar.Appearance.Options.UseFont = true;
            this.btnConfirmar.Appearance.Options.UseTextOptions = true;
            this.btnConfirmar.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnConfirmar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConfirmar.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnConfirmar.ImageOptions.SvgImage")));
            this.btnConfirmar.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.btnConfirmar.Location = new System.Drawing.Point(193, 190);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(132, 40);
            this.btnConfirmar.TabIndex = 73;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Appearance.Options.UseTextOptions = true;
            this.btnCancel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnCancel.ImageOptions.SvgImage")));
            this.btnCancel.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.btnCancel.Location = new System.Drawing.Point(13, 190);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(171, 40);
            this.btnCancel.TabIndex = 72;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // separatorControl2
            // 
            this.separatorControl2.Location = new System.Drawing.Point(13, 175);
            this.separatorControl2.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separatorControl2.Name = "separatorControl2";
            this.separatorControl2.Padding = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separatorControl2.Size = new System.Drawing.Size(312, 10);
            this.separatorControl2.TabIndex = 76;
            // 
            // CarregarConta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 241);
            this.Controls.Add(this.separatorControl2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.groupControl9);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CarregarConta";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Carregar Conta";
            this.Load += new System.EventHandler(this.CarregarConta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl9)).EndInit();
            this.groupControl9.ResumeLayout(false);
            this.groupControl9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbTipo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumero.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl9;
        private DevExpress.XtraEditors.LabelControl labelControl72;
        private DevExpress.XtraEditors.SimpleButton btnConfirmar;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SeparatorControl separatorControl2;
        private DevExpress.XtraEditors.ComboBoxEdit cbTipo;
        private DevExpress.XtraEditors.TextEdit txtNumero;
        private DevExpress.XtraEditors.LabelControl labelControl15;
    }
}