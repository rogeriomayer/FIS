namespace FMC.Solutions.NPPLUS
{
    partial class Boleto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Boleto));
            this.groupControl9 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIdBoleto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDtGeracao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDtVencimento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colValor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnCopiarLinhaDigitavel = new DevExpress.XtraEditors.SimpleButton();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.lblDataVencimento = new DevExpress.XtraEditors.LabelControl();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.lblID = new DevExpress.XtraEditors.LabelControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.lblLinhaDigitavel = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.separatorControl7 = new DevExpress.XtraEditors.SeparatorControl();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.separatorControl2 = new DevExpress.XtraEditors.SeparatorControl();
            this.lblQtdEnvioSMS = new DevExpress.XtraEditors.LabelControl();
            this.lblQtdEnvioEmail = new DevExpress.XtraEditors.LabelControl();
            this.lblValor = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.txtDataVencimento = new DevExpress.XtraEditors.DateEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.progressBar = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.btnGerar = new DevExpress.XtraEditors.SimpleButton();
            this.txtValor = new DevExpress.XtraEditors.TextEdit();
            this.lblLogradouro = new DevExpress.XtraEditors.LabelControl();
            this.groupControl10 = new DevExpress.XtraEditors.GroupControl();
            this.btnDownload = new DevExpress.XtraEditors.SimpleButton();
            this.btnSMS = new DevExpress.XtraEditors.SimpleButton();
            this.btnEmail = new DevExpress.XtraEditors.SimpleButton();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::FMC.Solutions.NPPLUS.WaitForm1), true, true, true);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl9)).BeginInit();
            this.groupControl9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataVencimento.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataVencimento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl10)).BeginInit();
            this.groupControl10.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl9
            // 
            this.groupControl9.Controls.Add(this.gridControl);
            this.groupControl9.Location = new System.Drawing.Point(12, 114);
            this.groupControl9.Name = "groupControl9";
            this.groupControl9.Size = new System.Drawing.Size(606, 176);
            this.groupControl9.TabIndex = 13;
            this.groupControl9.Text = "Histórico de Boletos";
            // 
            // gridControl
            // 
            this.gridControl.EmbeddedNavigator.Buttons.Edit.Enabled = false;
            this.gridControl.Location = new System.Drawing.Point(9, 37);
            this.gridControl.MainView = this.gridView3;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(588, 130);
            this.gridControl.TabIndex = 4;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            this.gridControl.Click += new System.EventHandler(this.gridControl_Click);
            this.gridControl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridControl_KeyUp);
            // 
            // gridView3
            // 
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIdBoleto,
            this.colDtGeracao,
            this.colDtVencimento,
            this.colValor,
            this.colStatus});
            this.gridView3.CustomizationFormBounds = new System.Drawing.Rectangle(339, 0, 252, 416);
            this.gridView3.GridControl = this.gridControl;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // colIdBoleto
            // 
            this.colIdBoleto.Caption = "ID";
            this.colIdBoleto.FieldName = "IdBoleto";
            this.colIdBoleto.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("colIdBoleto.ImageOptions.Image")));
            this.colIdBoleto.Name = "colIdBoleto";
            this.colIdBoleto.OptionsColumn.AllowEdit = false;
            this.colIdBoleto.OptionsColumn.AllowFocus = false;
            this.colIdBoleto.OptionsColumn.AllowMove = false;
            this.colIdBoleto.OptionsColumn.AllowShowHide = false;
            this.colIdBoleto.OptionsColumn.ReadOnly = true;
            this.colIdBoleto.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            this.colIdBoleto.Visible = true;
            this.colIdBoleto.VisibleIndex = 0;
            this.colIdBoleto.Width = 61;
            // 
            // colDtGeracao
            // 
            this.colDtGeracao.Caption = "Criação";
            this.colDtGeracao.FieldName = "DtGeracao";
            this.colDtGeracao.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("colDtGeracao.ImageOptions.Image")));
            this.colDtGeracao.MinWidth = 30;
            this.colDtGeracao.Name = "colDtGeracao";
            this.colDtGeracao.OptionsColumn.AllowEdit = false;
            this.colDtGeracao.OptionsColumn.AllowFocus = false;
            this.colDtGeracao.OptionsColumn.AllowMove = false;
            this.colDtGeracao.OptionsColumn.AllowShowHide = false;
            this.colDtGeracao.Visible = true;
            this.colDtGeracao.VisibleIndex = 1;
            this.colDtGeracao.Width = 125;
            // 
            // colDtVencimento
            // 
            this.colDtVencimento.Caption = "Vencimento";
            this.colDtVencimento.FieldName = "DtVencimento";
            this.colDtVencimento.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("colDtVencimento.ImageOptions.SvgImage")));
            this.colDtVencimento.ImageOptions.SvgImageSize = new System.Drawing.Size(18, 18);
            this.colDtVencimento.Name = "colDtVencimento";
            this.colDtVencimento.OptionsColumn.AllowEdit = false;
            this.colDtVencimento.OptionsColumn.AllowFocus = false;
            this.colDtVencimento.OptionsColumn.AllowMove = false;
            this.colDtVencimento.OptionsColumn.AllowShowHide = false;
            this.colDtVencimento.Visible = true;
            this.colDtVencimento.VisibleIndex = 2;
            this.colDtVencimento.Width = 103;
            // 
            // colValor
            // 
            this.colValor.AppearanceCell.Options.UseTextOptions = true;
            this.colValor.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colValor.AppearanceHeader.Options.UseTextOptions = true;
            this.colValor.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colValor.Caption = "Valor";
            this.colValor.FieldName = "Valor";
            this.colValor.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("colValor.ImageOptions.SvgImage")));
            this.colValor.ImageOptions.SvgImageSize = new System.Drawing.Size(18, 18);
            this.colValor.Name = "colValor";
            this.colValor.OptionsColumn.AllowEdit = false;
            this.colValor.OptionsColumn.AllowFocus = false;
            this.colValor.OptionsColumn.AllowMove = false;
            this.colValor.OptionsColumn.AllowShowHide = false;
            this.colValor.OptionsColumn.ReadOnly = true;
            this.colValor.Visible = true;
            this.colValor.VisibleIndex = 3;
            this.colValor.Width = 86;
            // 
            // colStatus
            // 
            this.colStatus.AppearanceCell.Options.UseTextOptions = true;
            this.colStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colStatus.AppearanceHeader.Options.UseTextOptions = true;
            this.colStatus.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colStatus.Caption = "Status";
            this.colStatus.FieldName = "Status";
            this.colStatus.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("colStatus.ImageOptions.SvgImage")));
            this.colStatus.ImageOptions.SvgImageSize = new System.Drawing.Size(18, 18);
            this.colStatus.Name = "colStatus";
            this.colStatus.OptionsColumn.AllowEdit = false;
            this.colStatus.OptionsColumn.AllowFocus = false;
            this.colStatus.OptionsColumn.AllowMove = false;
            this.colStatus.OptionsColumn.AllowShowHide = false;
            this.colStatus.OptionsColumn.ReadOnly = true;
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 4;
            this.colStatus.Width = 186;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnCopiarLinhaDigitavel);
            this.groupControl1.Controls.Add(this.lblStatus);
            this.groupControl1.Controls.Add(this.labelControl11);
            this.groupControl1.Controls.Add(this.lblDataVencimento);
            this.groupControl1.Controls.Add(this.labelControl19);
            this.groupControl1.Controls.Add(this.lblID);
            this.groupControl1.Controls.Add(this.labelControl15);
            this.groupControl1.Controls.Add(this.lblLinhaDigitavel);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.separatorControl7);
            this.groupControl1.Controls.Add(this.separatorControl1);
            this.groupControl1.Controls.Add(this.separatorControl2);
            this.groupControl1.Controls.Add(this.lblQtdEnvioSMS);
            this.groupControl1.Controls.Add(this.lblQtdEnvioEmail);
            this.groupControl1.Controls.Add(this.lblValor);
            this.groupControl1.Controls.Add(this.labelControl10);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Location = new System.Drawing.Point(12, 297);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(450, 207);
            this.groupControl1.TabIndex = 65;
            this.groupControl1.Text = "Detalhes do Boleto";
            // 
            // btnCopiarLinhaDigitavel
            // 
            this.btnCopiarLinhaDigitavel.Enabled = false;
            this.btnCopiarLinhaDigitavel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnCopiarLinhaDigitavel.ImageOptions.SvgImage")));
            this.btnCopiarLinhaDigitavel.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.btnCopiarLinhaDigitavel.Location = new System.Drawing.Point(372, 162);
            this.btnCopiarLinhaDigitavel.Name = "btnCopiarLinhaDigitavel";
            this.btnCopiarLinhaDigitavel.Size = new System.Drawing.Size(66, 25);
            this.btnCopiarLinhaDigitavel.TabIndex = 9;
            this.btnCopiarLinhaDigitavel.Text = "Copiar";
            this.btnCopiarLinhaDigitavel.Click += new System.EventHandler(this.btnCopiarLinhaDigitavel_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblStatus.Appearance.Options.UseFont = true;
            this.lblStatus.Appearance.Options.UseTextOptions = true;
            this.lblStatus.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblStatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblStatus.Location = new System.Drawing.Point(15, 97);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(127, 13);
            this.lblStatus.TabIndex = 109;
            this.lblStatus.Text = "-";
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl11.Appearance.Options.UseFont = true;
            this.labelControl11.Location = new System.Drawing.Point(60, 76);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(37, 13);
            this.labelControl11.TabIndex = 108;
            this.labelControl11.Text = "Status";
            // 
            // lblDataVencimento
            // 
            this.lblDataVencimento.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblDataVencimento.Appearance.Options.UseFont = true;
            this.lblDataVencimento.Appearance.Options.UseTextOptions = true;
            this.lblDataVencimento.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblDataVencimento.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDataVencimento.Location = new System.Drawing.Point(162, 59);
            this.lblDataVencimento.Name = "lblDataVencimento";
            this.lblDataVencimento.Size = new System.Drawing.Size(116, 13);
            this.lblDataVencimento.TabIndex = 107;
            this.lblDataVencimento.Text = "-";
            // 
            // labelControl19
            // 
            this.labelControl19.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl19.Appearance.Options.UseFont = true;
            this.labelControl19.Location = new System.Drawing.Point(162, 38);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(114, 13);
            this.labelControl19.TabIndex = 106;
            this.labelControl19.Text = "Data de Vencimento";
            // 
            // lblID
            // 
            this.lblID.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblID.Appearance.Options.UseFont = true;
            this.lblID.Appearance.Options.UseTextOptions = true;
            this.lblID.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblID.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblID.Location = new System.Drawing.Point(12, 57);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(127, 13);
            this.lblID.TabIndex = 103;
            this.lblID.Text = "-";
            // 
            // labelControl15
            // 
            this.labelControl15.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl15.Appearance.Options.UseFont = true;
            this.labelControl15.Location = new System.Drawing.Point(34, 36);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(80, 13);
            this.labelControl15.TabIndex = 102;
            this.labelControl15.Text = "Nosso Número";
            // 
            // lblLinhaDigitavel
            // 
            this.lblLinhaDigitavel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblLinhaDigitavel.Appearance.Options.UseFont = true;
            this.lblLinhaDigitavel.Appearance.Options.UseTextOptions = true;
            this.lblLinhaDigitavel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.lblLinhaDigitavel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblLinhaDigitavel.Location = new System.Drawing.Point(13, 168);
            this.lblLinhaDigitavel.Name = "lblLinhaDigitavel";
            this.lblLinhaDigitavel.Size = new System.Drawing.Size(346, 17);
            this.lblLinhaDigitavel.TabIndex = 101;
            this.lblLinhaDigitavel.Text = "-";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(12, 147);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(83, 13);
            this.labelControl3.TabIndex = 100;
            this.labelControl3.Text = "Linha Digitável";
            // 
            // separatorControl7
            // 
            this.separatorControl7.Location = new System.Drawing.Point(12, 134);
            this.separatorControl7.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separatorControl7.Name = "separatorControl7";
            this.separatorControl7.Padding = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separatorControl7.Size = new System.Drawing.Size(426, 4);
            this.separatorControl7.TabIndex = 99;
            // 
            // separatorControl1
            // 
            this.separatorControl1.LineOrientation = System.Windows.Forms.Orientation.Vertical;
            this.separatorControl1.Location = new System.Drawing.Point(294, 38);
            this.separatorControl1.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Padding = new System.Windows.Forms.Padding(0);
            this.separatorControl1.Size = new System.Drawing.Size(10, 88);
            this.separatorControl1.TabIndex = 95;
            // 
            // separatorControl2
            // 
            this.separatorControl2.LineOrientation = System.Windows.Forms.Orientation.Vertical;
            this.separatorControl2.Location = new System.Drawing.Point(145, 38);
            this.separatorControl2.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separatorControl2.Name = "separatorControl2";
            this.separatorControl2.Padding = new System.Windows.Forms.Padding(0);
            this.separatorControl2.Size = new System.Drawing.Size(14, 87);
            this.separatorControl2.TabIndex = 87;
            // 
            // lblQtdEnvioSMS
            // 
            this.lblQtdEnvioSMS.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblQtdEnvioSMS.Appearance.Options.UseFont = true;
            this.lblQtdEnvioSMS.Appearance.Options.UseTextOptions = true;
            this.lblQtdEnvioSMS.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblQtdEnvioSMS.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblQtdEnvioSMS.Location = new System.Drawing.Point(333, 97);
            this.lblQtdEnvioSMS.Name = "lblQtdEnvioSMS";
            this.lblQtdEnvioSMS.Size = new System.Drawing.Size(83, 13);
            this.lblQtdEnvioSMS.TabIndex = 83;
            this.lblQtdEnvioSMS.Text = "-";
            // 
            // lblQtdEnvioEmail
            // 
            this.lblQtdEnvioEmail.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblQtdEnvioEmail.Appearance.Options.UseFont = true;
            this.lblQtdEnvioEmail.Appearance.Options.UseTextOptions = true;
            this.lblQtdEnvioEmail.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblQtdEnvioEmail.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblQtdEnvioEmail.Location = new System.Drawing.Point(171, 99);
            this.lblQtdEnvioEmail.Name = "lblQtdEnvioEmail";
            this.lblQtdEnvioEmail.Size = new System.Drawing.Size(88, 13);
            this.lblQtdEnvioEmail.TabIndex = 82;
            this.lblQtdEnvioEmail.Text = "-";
            // 
            // lblValor
            // 
            this.lblValor.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblValor.Appearance.Options.UseFont = true;
            this.lblValor.Appearance.Options.UseTextOptions = true;
            this.lblValor.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblValor.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblValor.Location = new System.Drawing.Point(314, 59);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(118, 13);
            this.lblValor.TabIndex = 80;
            this.lblValor.Text = "-";
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl10.Appearance.Options.UseFont = true;
            this.labelControl10.Location = new System.Drawing.Point(333, 76);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(83, 13);
            this.labelControl10.TabIndex = 74;
            this.labelControl10.Text = "Qtd. Envio SMS";
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.Location = new System.Drawing.Point(171, 78);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(89, 13);
            this.labelControl9.TabIndex = 73;
            this.labelControl9.Text = "Qtd. Envio Email";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(358, 38);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(29, 13);
            this.labelControl7.TabIndex = 70;
            this.labelControl7.Text = "Valor";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.txtDataVencimento);
            this.groupControl2.Controls.Add(this.labelControl6);
            this.groupControl2.Controls.Add(this.progressBar);
            this.groupControl2.Controls.Add(this.btnGerar);
            this.groupControl2.Controls.Add(this.txtValor);
            this.groupControl2.Controls.Add(this.lblLogradouro);
            this.groupControl2.Location = new System.Drawing.Point(12, 12);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(606, 95);
            this.groupControl2.TabIndex = 66;
            this.groupControl2.Text = "Novo Boleto";
            // 
            // txtDataVencimento
            // 
            this.txtDataVencimento.EditValue = null;
            this.txtDataVencimento.Location = new System.Drawing.Point(240, 55);
            this.txtDataVencimento.Name = "txtDataVencimento";
            this.txtDataVencimento.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDataVencimento.Properties.Appearance.Options.UseFont = true;
            this.txtDataVencimento.Properties.AutoHeight = false;
            this.txtDataVencimento.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataVencimento.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataVencimento.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.ClassicNew;
            this.txtDataVencimento.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            this.txtDataVencimento.Size = new System.Drawing.Size(207, 31);
            this.txtDataVencimento.TabIndex = 2;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(244, 35);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(114, 13);
            this.labelControl6.TabIndex = 87;
            this.labelControl6.Text = "Data de Vencimento";
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
            this.progressBar.Size = new System.Drawing.Size(602, 5);
            this.progressBar.TabIndex = 73;
            // 
            // btnGerar
            // 
            this.btnGerar.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnGerar.Appearance.Options.UseFont = true;
            this.btnGerar.Appearance.Options.UseTextOptions = true;
            this.btnGerar.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnGerar.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnGerar.ImageOptions.SvgImage")));
            this.btnGerar.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.btnGerar.Location = new System.Drawing.Point(455, 55);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(142, 31);
            this.btnGerar.TabIndex = 3;
            this.btnGerar.Text = "Gerar Boleto";
            this.btnGerar.Click += new System.EventHandler(this.btnGerar_Click);
            // 
            // txtValor
            // 
            this.txtValor.Location = new System.Drawing.Point(8, 55);
            this.txtValor.Margin = new System.Windows.Forms.Padding(4);
            this.txtValor.Name = "txtValor";
            this.txtValor.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtValor.Properties.Appearance.Options.UseFont = true;
            this.txtValor.Properties.AutoHeight = false;
            this.txtValor.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.None;
            this.txtValor.Properties.Mask.BeepOnError = true;
            this.txtValor.Properties.Mask.EditMask = "c2";
            this.txtValor.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtValor.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtValor.Properties.MaxLength = 40;
            this.txtValor.Size = new System.Drawing.Size(224, 31);
            this.txtValor.TabIndex = 1;
            this.txtValor.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Asterisk;
            // 
            // lblLogradouro
            // 
            this.lblLogradouro.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblLogradouro.Appearance.Options.UseFont = true;
            this.lblLogradouro.Location = new System.Drawing.Point(12, 35);
            this.lblLogradouro.Name = "lblLogradouro";
            this.lblLogradouro.Size = new System.Drawing.Size(29, 13);
            this.lblLogradouro.TabIndex = 52;
            this.lblLogradouro.Text = "Valor";
            // 
            // groupControl10
            // 
            this.groupControl10.Controls.Add(this.btnDownload);
            this.groupControl10.Controls.Add(this.btnSMS);
            this.groupControl10.Controls.Add(this.btnEmail);
            this.groupControl10.Location = new System.Drawing.Point(468, 297);
            this.groupControl10.Name = "groupControl10";
            this.groupControl10.Size = new System.Drawing.Size(150, 207);
            this.groupControl10.TabIndex = 67;
            this.groupControl10.Text = "Ações";
            // 
            // btnDownload
            // 
            this.btnDownload.Enabled = false;
            this.btnDownload.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDownload.ImageOptions.SvgImage")));
            this.btnDownload.Location = new System.Drawing.Point(10, 147);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(130, 47);
            this.btnDownload.TabIndex = 7;
            this.btnDownload.Text = "Download";
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnSMS
            // 
            this.btnSMS.Enabled = false;
            this.btnSMS.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnSMS.ImageOptions.SvgImage")));
            this.btnSMS.Location = new System.Drawing.Point(10, 89);
            this.btnSMS.Name = "btnSMS";
            this.btnSMS.Size = new System.Drawing.Size(130, 53);
            this.btnSMS.TabIndex = 6;
            this.btnSMS.Text = "Enviar por SMS";
            this.btnSMS.Click += new System.EventHandler(this.btnSMS_Click);
            // 
            // btnEmail
            // 
            this.btnEmail.Enabled = false;
            this.btnEmail.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnEmail.ImageOptions.SvgImage")));
            this.btnEmail.Location = new System.Drawing.Point(10, 31);
            this.btnEmail.Name = "btnEmail";
            this.btnEmail.Size = new System.Drawing.Size(130, 53);
            this.btnEmail.TabIndex = 5;
            this.btnEmail.Text = "Enviar por Email";
            this.btnEmail.Click += new System.EventHandler(this.btnEmail_Click);
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // Boleto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 524);
            this.Controls.Add(this.groupControl10);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.groupControl9);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Boleto";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Boleto";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Boleto_FormClosing);
            this.Load += new System.EventHandler(this.Boleto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl9)).EndInit();
            this.groupControl9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataVencimento.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataVencimento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl10)).EndInit();
            this.groupControl10.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl9;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Columns.GridColumn colIdBoleto;
        private DevExpress.XtraGrid.Columns.GridColumn colDtGeracao;
        private DevExpress.XtraGrid.Columns.GridColumn colValor;
        private DevExpress.XtraGrid.Columns.GridColumn colDtVencimento;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl lblQtdEnvioSMS;
        private DevExpress.XtraEditors.LabelControl lblQtdEnvioEmail;
        private DevExpress.XtraEditors.LabelControl lblValor;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.SeparatorControl separatorControl2;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.MarqueeProgressBarControl progressBar;
        private DevExpress.XtraEditors.SimpleButton btnGerar;
        private DevExpress.XtraEditors.TextEdit txtValor;
        private DevExpress.XtraEditors.LabelControl lblLogradouro;
        private DevExpress.XtraEditors.LabelControl lblDataVencimento;
        private DevExpress.XtraEditors.LabelControl labelControl19;
        private DevExpress.XtraEditors.LabelControl lblID;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.LabelControl lblLinhaDigitavel;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SeparatorControl separatorControl7;
        private DevExpress.XtraEditors.LabelControl lblStatus;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.GroupControl groupControl10;
        private DevExpress.XtraEditors.SimpleButton btnDownload;
        private DevExpress.XtraEditors.SimpleButton btnSMS;
        private DevExpress.XtraEditors.SimpleButton btnEmail;
        private DevExpress.XtraEditors.SimpleButton btnCopiarLinhaDigitavel;
        private DevExpress.XtraEditors.DateEdit txtDataVencimento;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
    }
}