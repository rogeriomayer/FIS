namespace FMC.Solutions.NPPLUS
{
    partial class PesquisarLogradouro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PesquisarLogradouro));
            this.groupControl9 = new DevExpress.XtraEditors.GroupControl();
            this.gc = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCep = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLogradouro = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colComplemento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBairro = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCidade = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUf = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bw = new System.ComponentModel.BackgroundWorker();
            this.btnPesquisar = new DevExpress.XtraEditors.SimpleButton();
            this.txtLogradouro = new DevExpress.XtraEditors.TextEdit();
            this.lblLogradouro = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtEstado = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtCidade = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.progressBar = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.separatorControl2 = new DevExpress.XtraEditors.SeparatorControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirmar = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl9)).BeginInit();
            this.groupControl9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLogradouro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEstado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCidade.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl9
            // 
            this.groupControl9.Controls.Add(this.gc);
            this.groupControl9.Location = new System.Drawing.Point(12, 114);
            this.groupControl9.Name = "groupControl9";
            this.groupControl9.Size = new System.Drawing.Size(884, 240);
            this.groupControl9.TabIndex = 13;
            this.groupControl9.Text = "Lista de Endereços";
            // 
            // gc
            // 
            this.gc.EmbeddedNavigator.Buttons.Edit.Enabled = false;
            this.gc.Location = new System.Drawing.Point(9, 36);
            this.gc.MainView = this.gridView3;
            this.gc.Name = "gc";
            this.gc.Size = new System.Drawing.Size(866, 194);
            this.gc.TabIndex = 0;
            this.gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            this.gc.Click += new System.EventHandler(this.gc_Click);
            this.gc.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gc_KeyUp);
            // 
            // gridView3
            // 
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCep,
            this.colLogradouro,
            this.colComplemento,
            this.colBairro,
            this.colCidade,
            this.colUf});
            this.gridView3.CustomizationFormBounds = new System.Drawing.Rectangle(339, 0, 252, 416);
            this.gridView3.GridControl = this.gc;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // colCep
            // 
            this.colCep.Caption = "Cep";
            this.colCep.FieldName = "ZipCode";
            this.colCep.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("colCep.ImageOptions.SvgImage")));
            this.colCep.ImageOptions.SvgImageSize = new System.Drawing.Size(18, 18);
            this.colCep.MaxWidth = 85;
            this.colCep.Name = "colCep";
            this.colCep.OptionsColumn.AllowEdit = false;
            this.colCep.OptionsColumn.AllowFocus = false;
            this.colCep.OptionsColumn.AllowMove = false;
            this.colCep.OptionsColumn.AllowShowHide = false;
            this.colCep.OptionsColumn.ReadOnly = true;
            this.colCep.Visible = true;
            this.colCep.VisibleIndex = 0;
            this.colCep.Width = 85;
            // 
            // colLogradouro
            // 
            this.colLogradouro.Caption = "Logradouro";
            this.colLogradouro.FieldName = "Address1";
            this.colLogradouro.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("colLogradouro.ImageOptions.SvgImage")));
            this.colLogradouro.ImageOptions.SvgImageSize = new System.Drawing.Size(18, 18);
            this.colLogradouro.MaxWidth = 230;
            this.colLogradouro.MinWidth = 30;
            this.colLogradouro.Name = "colLogradouro";
            this.colLogradouro.OptionsColumn.AllowEdit = false;
            this.colLogradouro.OptionsColumn.AllowFocus = false;
            this.colLogradouro.OptionsColumn.AllowMove = false;
            this.colLogradouro.OptionsColumn.AllowShowHide = false;
            this.colLogradouro.Visible = true;
            this.colLogradouro.VisibleIndex = 1;
            this.colLogradouro.Width = 230;
            // 
            // colComplemento
            // 
            this.colComplemento.Caption = "Complemento";
            this.colComplemento.FieldName = "Complement";
            this.colComplemento.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("colComplemento.ImageOptions.Image")));
            this.colComplemento.MaxWidth = 129;
            this.colComplemento.MinWidth = 129;
            this.colComplemento.Name = "colComplemento";
            this.colComplemento.OptionsColumn.AllowEdit = false;
            this.colComplemento.OptionsColumn.AllowFocus = false;
            this.colComplemento.OptionsColumn.AllowMove = false;
            this.colComplemento.OptionsColumn.AllowShowHide = false;
            this.colComplemento.Visible = true;
            this.colComplemento.VisibleIndex = 2;
            this.colComplemento.Width = 129;
            // 
            // colBairro
            // 
            this.colBairro.Caption = "Bairro";
            this.colBairro.FieldName = "Neighborhood";
            this.colBairro.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("colBairro.ImageOptions.Image")));
            this.colBairro.MaxWidth = 150;
            this.colBairro.MinWidth = 150;
            this.colBairro.Name = "colBairro";
            this.colBairro.OptionsColumn.AllowEdit = false;
            this.colBairro.OptionsColumn.AllowFocus = false;
            this.colBairro.OptionsColumn.AllowMove = false;
            this.colBairro.OptionsColumn.AllowShowHide = false;
            this.colBairro.Visible = true;
            this.colBairro.VisibleIndex = 3;
            this.colBairro.Width = 150;
            // 
            // colCidade
            // 
            this.colCidade.Caption = "Cidade";
            this.colCidade.FieldName = "City";
            this.colCidade.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("colCidade.ImageOptions.SvgImage")));
            this.colCidade.ImageOptions.SvgImageSize = new System.Drawing.Size(18, 18);
            this.colCidade.MaxWidth = 180;
            this.colCidade.MinWidth = 180;
            this.colCidade.Name = "colCidade";
            this.colCidade.OptionsColumn.AllowEdit = false;
            this.colCidade.OptionsColumn.AllowFocus = false;
            this.colCidade.OptionsColumn.AllowMove = false;
            this.colCidade.OptionsColumn.AllowShowHide = false;
            this.colCidade.OptionsColumn.ReadOnly = true;
            this.colCidade.Visible = true;
            this.colCidade.VisibleIndex = 4;
            this.colCidade.Width = 180;
            // 
            // colUf
            // 
            this.colUf.Caption = "Estado";
            this.colUf.FieldName = "State";
            this.colUf.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("colUf.ImageOptions.SvgImage")));
            this.colUf.ImageOptions.SvgImageSize = new System.Drawing.Size(18, 18);
            this.colUf.MaxWidth = 65;
            this.colUf.MinWidth = 65;
            this.colUf.Name = "colUf";
            this.colUf.OptionsColumn.AllowEdit = false;
            this.colUf.OptionsColumn.AllowFocus = false;
            this.colUf.OptionsColumn.AllowMove = false;
            this.colUf.OptionsColumn.AllowShowHide = false;
            this.colUf.OptionsColumn.ReadOnly = true;
            this.colUf.Visible = true;
            this.colUf.VisibleIndex = 5;
            this.colUf.Width = 65;
            // 
            // bw
            // 
            this.bw.WorkerReportsProgress = true;
            this.bw.WorkerSupportsCancellation = true;
            this.bw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_DoWork);
            this.bw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bw_ProgressChanged);
            this.bw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_RunWorkerCompleted);
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Appearance.BackColor = System.Drawing.Color.Gainsboro;
            this.btnPesquisar.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnPesquisar.Appearance.Options.UseBackColor = true;
            this.btnPesquisar.Appearance.Options.UseFont = true;
            this.btnPesquisar.Appearance.Options.UseTextOptions = true;
            this.btnPesquisar.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnPesquisar.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnPesquisar.ImageOptions.SvgImage")));
            this.btnPesquisar.Location = new System.Drawing.Point(747, 55);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(128, 31);
            this.btnPesquisar.TabIndex = 50;
            this.btnPesquisar.Text = "Pesquisar";
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // txtLogradouro
            // 
            this.txtLogradouro.Location = new System.Drawing.Point(8, 55);
            this.txtLogradouro.Margin = new System.Windows.Forms.Padding(4);
            this.txtLogradouro.Name = "txtLogradouro";
            this.txtLogradouro.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtLogradouro.Properties.Appearance.Options.UseFont = true;
            this.txtLogradouro.Properties.AutoHeight = false;
            this.txtLogradouro.Properties.MaxLength = 40;
            this.txtLogradouro.Size = new System.Drawing.Size(390, 31);
            this.txtLogradouro.TabIndex = 51;
            this.txtLogradouro.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Asterisk;
            // 
            // lblLogradouro
            // 
            this.lblLogradouro.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblLogradouro.Appearance.Options.UseFont = true;
            this.lblLogradouro.Location = new System.Drawing.Point(12, 35);
            this.lblLogradouro.Name = "lblLogradouro";
            this.lblLogradouro.Size = new System.Drawing.Size(65, 13);
            this.lblLogradouro.TabIndex = 52;
            this.lblLogradouro.Text = "Logradouro";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtEstado);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.txtCidade);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.progressBar);
            this.groupControl1.Controls.Add(this.btnPesquisar);
            this.groupControl1.Controls.Add(this.txtLogradouro);
            this.groupControl1.Controls.Add(this.lblLogradouro);
            this.groupControl1.Location = new System.Drawing.Point(12, 11);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(884, 95);
            this.groupControl1.TabIndex = 53;
            this.groupControl1.Text = "Dados para Pesquisa";
            // 
            // txtEstado
            // 
            this.txtEstado.Location = new System.Drawing.Point(658, 55);
            this.txtEstado.Margin = new System.Windows.Forms.Padding(4);
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtEstado.Properties.Appearance.Options.UseFont = true;
            this.txtEstado.Properties.AutoHeight = false;
            this.txtEstado.Properties.MaxLength = 2;
            this.txtEstado.Size = new System.Drawing.Size(81, 31);
            this.txtEstado.TabIndex = 86;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(662, 35);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(38, 13);
            this.labelControl5.TabIndex = 88;
            this.labelControl5.Text = "Estado";
            // 
            // txtCidade
            // 
            this.txtCidade.Location = new System.Drawing.Point(406, 55);
            this.txtCidade.Margin = new System.Windows.Forms.Padding(4);
            this.txtCidade.Name = "txtCidade";
            this.txtCidade.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtCidade.Properties.Appearance.Options.UseFont = true;
            this.txtCidade.Properties.AutoHeight = false;
            this.txtCidade.Properties.MaxLength = 30;
            this.txtCidade.Size = new System.Drawing.Size(244, 31);
            this.txtCidade.TabIndex = 85;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(410, 35);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(38, 13);
            this.labelControl3.TabIndex = 87;
            this.labelControl3.Text = "Cidade";
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
            this.progressBar.Size = new System.Drawing.Size(880, 5);
            this.progressBar.TabIndex = 73;
            // 
            // separatorControl2
            // 
            this.separatorControl2.Location = new System.Drawing.Point(12, 359);
            this.separatorControl2.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separatorControl2.Name = "separatorControl2";
            this.separatorControl2.Padding = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separatorControl2.Size = new System.Drawing.Size(884, 10);
            this.separatorControl2.TabIndex = 79;
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Appearance.Options.UseTextOptions = true;
            this.btnCancel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnCancel.ImageOptions.SvgImage")));
            this.btnCancel.Location = new System.Drawing.Point(665, 373);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.btnCancel.Size = new System.Drawing.Size(104, 40);
            this.btnCancel.TabIndex = 77;
            this.btnCancel.Text = "Cancelar";
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
            this.btnConfirmar.Location = new System.Drawing.Point(778, 373);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(118, 40);
            this.btnConfirmar.TabIndex = 78;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // PesquisarLogradouro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 424);
            this.Controls.Add(this.separatorControl2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.groupControl9);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PesquisarLogradouro";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pesquisar Logradouro";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PesquisarLogradouro_FormClosing);
            this.Load += new System.EventHandler(this.PesquisarLogradouro_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl9)).EndInit();
            this.groupControl9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLogradouro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEstado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCidade.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl9;
        private DevExpress.XtraGrid.GridControl gc;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Columns.GridColumn colCep;
        private DevExpress.XtraGrid.Columns.GridColumn colLogradouro;
        private DevExpress.XtraGrid.Columns.GridColumn colBairro;
        private DevExpress.XtraGrid.Columns.GridColumn colComplemento;
        private System.ComponentModel.BackgroundWorker bw;
        private DevExpress.XtraGrid.Columns.GridColumn colCidade;
        private DevExpress.XtraGrid.Columns.GridColumn colUf;
        private DevExpress.XtraEditors.SimpleButton btnPesquisar;
        private DevExpress.XtraEditors.TextEdit txtLogradouro;
        private DevExpress.XtraEditors.LabelControl lblLogradouro;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.MarqueeProgressBarControl progressBar;
        private DevExpress.XtraEditors.TextEdit txtEstado;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtCidade;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SeparatorControl separatorControl2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnConfirmar;
    }
}