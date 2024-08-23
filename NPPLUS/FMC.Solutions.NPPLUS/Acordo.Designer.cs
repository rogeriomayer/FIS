namespace FMC.Solutions.NPPLUS
{
    partial class Acordo
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Acordo));
            this.groupControl9 = new DevExpress.XtraEditors.GroupControl();
            this.lblTaxaJuro = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.lblDiasAtraso = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lblSaldo = new DevExpress.XtraEditors.LabelControl();
            this.labelControl72 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtDesconto = new DevExpress.XtraEditors.SpinEdit();
            this.txtDataEntrada = new DevExpress.XtraEditors.DateEdit();
            this.infoDesconto = new DevExpress.XtraEditors.LabelControl();
            this.btnCalcular = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.txtValorEntrada = new DevExpress.XtraEditors.TextEdit();
            this.lblValorEntrada = new DevExpress.XtraEditors.LabelControl();
            this.lblDtEntrada = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gcParcelas = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColQtdParcelas = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColValorParcela = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColDesconto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColDtPrimeiraParcela = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColCetMensal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColCetAnual = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColVlCetMensal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColVlCetAnual = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColTotalGeral = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnConfirmar = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelAcordo = new DevExpress.XtraEditors.SimpleButton();
            this.separatorControl2 = new DevExpress.XtraEditors.SeparatorControl();
            this.toolTip = new DevExpress.Utils.ToolTipController(this.components);
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::FMC.Solutions.NPPLUS.WaitForm1), true, true, true);
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl9)).BeginInit();
            this.groupControl9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesconto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataEntrada.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataEntrada.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValorEntrada.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcParcelas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl9
            // 
            this.groupControl9.Controls.Add(this.lblTaxaJuro);
            this.groupControl9.Controls.Add(this.labelControl6);
            this.groupControl9.Controls.Add(this.lblDiasAtraso);
            this.groupControl9.Controls.Add(this.labelControl4);
            this.groupControl9.Controls.Add(this.lblSaldo);
            this.groupControl9.Controls.Add(this.labelControl72);
            this.groupControl9.Location = new System.Drawing.Point(12, 12);
            this.groupControl9.Name = "groupControl9";
            this.groupControl9.Size = new System.Drawing.Size(951, 80);
            this.groupControl9.TabIndex = 13;
            this.groupControl9.Text = "Resumo do Acordo";
            // 
            // lblTaxaJuro
            // 
            this.lblTaxaJuro.Appearance.Options.UseTextOptions = true;
            this.lblTaxaJuro.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblTaxaJuro.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTaxaJuro.Location = new System.Drawing.Point(862, 53);
            this.lblTaxaJuro.Name = "lblTaxaJuro";
            this.lblTaxaJuro.Size = new System.Drawing.Size(69, 13);
            this.lblTaxaJuro.TabIndex = 67;
            this.lblTaxaJuro.Text = "-";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(857, 36);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(79, 13);
            this.labelControl6.TabIndex = 66;
            this.labelControl6.Text = "Taxa de Juros";
            // 
            // lblDiasAtraso
            // 
            this.lblDiasAtraso.Appearance.Options.UseTextOptions = true;
            this.lblDiasAtraso.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblDiasAtraso.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDiasAtraso.Location = new System.Drawing.Point(426, 53);
            this.lblDiasAtraso.Name = "lblDiasAtraso";
            this.lblDiasAtraso.Size = new System.Drawing.Size(82, 13);
            this.lblDiasAtraso.TabIndex = 65;
            this.lblDiasAtraso.Text = "-";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(426, 36);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(82, 13);
            this.labelControl4.TabIndex = 64;
            this.labelControl4.Text = "Dias de Atraso";
            // 
            // lblSaldo
            // 
            this.lblSaldo.Appearance.Options.UseTextOptions = true;
            this.lblSaldo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.lblSaldo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblSaldo.Location = new System.Drawing.Point(9, 53);
            this.lblSaldo.Name = "lblSaldo";
            this.lblSaldo.Size = new System.Drawing.Size(92, 13);
            this.lblSaldo.TabIndex = 46;
            this.lblSaldo.Text = "-";
            // 
            // labelControl72
            // 
            this.labelControl72.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl72.Appearance.Options.UseFont = true;
            this.labelControl72.Location = new System.Drawing.Point(9, 36);
            this.labelControl72.Name = "labelControl72";
            this.labelControl72.Size = new System.Drawing.Size(94, 13);
            this.labelControl72.TabIndex = 45;
            this.labelControl72.Text = "Saldo Atualizado";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtDesconto);
            this.groupControl1.Controls.Add(this.txtDataEntrada);
            this.groupControl1.Controls.Add(this.infoDesconto);
            this.groupControl1.Controls.Add(this.btnCalcular);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.txtValorEntrada);
            this.groupControl1.Controls.Add(this.lblValorEntrada);
            this.groupControl1.Controls.Add(this.lblDtEntrada);
            this.groupControl1.Location = new System.Drawing.Point(12, 99);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(951, 110);
            this.groupControl1.TabIndex = 14;
            this.groupControl1.Text = "Calcular Acordo";
            // 
            // txtDesconto
            // 
            this.txtDesconto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDesconto.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtDesconto.Location = new System.Drawing.Point(21, 58);
            this.txtDesconto.Name = "txtDesconto";
            this.txtDesconto.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtDesconto.Properties.Appearance.Options.UseFont = true;
            this.txtDesconto.Properties.Appearance.Options.UseTextOptions = true;
            this.txtDesconto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtDesconto.Properties.AutoHeight = false;
            this.txtDesconto.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDesconto.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtDesconto.Properties.IsFloatValue = false;
            this.txtDesconto.Properties.Mask.EditMask = "N00";
            this.txtDesconto.Properties.MaxLength = 3;
            this.txtDesconto.Size = new System.Drawing.Size(107, 40);
            this.txtDesconto.TabIndex = 71;
            this.txtDesconto.ValueChanged += new System.EventHandler(this.txtDesconto_ValueChanged);
            this.txtDesconto.EditValueChanged += new System.EventHandler(this.txtDesconto_EditValueChanged);
            this.txtDesconto.TextChanged += new System.EventHandler(this.txtDesconto_TextChanged);
            this.txtDesconto.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDesconto_KeyUp);
            // 
            // txtDataEntrada
            // 
            this.txtDataEntrada.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDataEntrada.EditValue = null;
            this.txtDataEntrada.Location = new System.Drawing.Point(250, 58);
            this.txtDataEntrada.Name = "txtDataEntrada";
            this.txtDataEntrada.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtDataEntrada.Properties.Appearance.Options.UseFont = true;
            this.txtDataEntrada.Properties.AutoHeight = false;
            this.txtDataEntrada.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataEntrada.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataEntrada.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.ClassicNew;
            this.txtDataEntrada.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            this.txtDataEntrada.Size = new System.Drawing.Size(172, 40);
            this.txtDataEntrada.TabIndex = 72;
            this.txtDataEntrada.DrawItem += new DevExpress.XtraEditors.Calendar.CustomDrawDayNumberCellEventHandler(this.txtDataEntrada_DrawItem);
            this.txtDataEntrada.DisableCalendarDate += new DevExpress.XtraEditors.Calendar.DisableCalendarDateEventHandler(this.txtDataEntrada_DisableCalendarDate);
            this.txtDataEntrada.EditValueChanged += new System.EventHandler(this.txtDataEntrada_EditValueChanged);
            // 
            // infoDesconto
            // 
            this.infoDesconto.AllowHtmlString = true;
            this.infoDesconto.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.True;
            this.infoDesconto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.infoDesconto.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.infoDesconto.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("infoDesconto.ImageOptions.SvgImage")));
            this.infoDesconto.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.infoDesconto.Location = new System.Drawing.Point(111, 36);
            this.infoDesconto.Name = "infoDesconto";
            this.infoDesconto.Size = new System.Drawing.Size(17, 16);
            this.infoDesconto.TabIndex = 79;
            // 
            // btnCalcular
            // 
            this.btnCalcular.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCalcular.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnCalcular.Appearance.Options.UseFont = true;
            this.btnCalcular.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnCalcular.ImageOptions.SvgImage")));
            this.btnCalcular.Location = new System.Drawing.Point(735, 58);
            this.btnCalcular.Name = "btnCalcular";
            this.btnCalcular.Size = new System.Drawing.Size(152, 40);
            this.btnCalcular.TabIndex = 74;
            this.btnCalcular.Text = "Calcular";
            this.btnCalcular.Click += new System.EventHandler(this.btnCalcular_Click);
            // 
            // labelControl9
            // 
            this.labelControl9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.Location = new System.Drawing.Point(21, 38);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(46, 13);
            this.labelControl9.TabIndex = 71;
            this.labelControl9.Text = "% Desc.";
            // 
            // txtValorEntrada
            // 
            this.txtValorEntrada.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtValorEntrada.Location = new System.Drawing.Point(484, 58);
            this.txtValorEntrada.Margin = new System.Windows.Forms.Padding(4);
            this.txtValorEntrada.Name = "txtValorEntrada";
            this.txtValorEntrada.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtValorEntrada.Properties.Appearance.Options.UseFont = true;
            this.txtValorEntrada.Properties.AutoHeight = false;
            this.txtValorEntrada.Properties.Mask.EditMask = "f";
            this.txtValorEntrada.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtValorEntrada.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtValorEntrada.Size = new System.Drawing.Size(186, 40);
            this.txtValorEntrada.TabIndex = 73;
            this.txtValorEntrada.EditValueChanged += new System.EventHandler(this.txtValorEntrada_EditValueChanged);
            // 
            // lblValorEntrada
            // 
            this.lblValorEntrada.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblValorEntrada.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblValorEntrada.Appearance.Options.UseFont = true;
            this.lblValorEntrada.Location = new System.Drawing.Point(484, 38);
            this.lblValorEntrada.Name = "lblValorEntrada";
            this.lblValorEntrada.Size = new System.Drawing.Size(93, 13);
            this.lblValorEntrada.TabIndex = 49;
            this.lblValorEntrada.Text = "Valor da Entrada";
            // 
            // lblDtEntrada
            // 
            this.lblDtEntrada.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDtEntrada.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblDtEntrada.Appearance.Options.UseFont = true;
            this.lblDtEntrada.Location = new System.Drawing.Point(251, 38);
            this.lblDtEntrada.Name = "lblDtEntrada";
            this.lblDtEntrada.Size = new System.Drawing.Size(91, 13);
            this.lblDtEntrada.TabIndex = 47;
            this.lblDtEntrada.Text = "Data da Entrada";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gcParcelas);
            this.groupControl2.Location = new System.Drawing.Point(12, 219);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(951, 257);
            this.groupControl2.TabIndex = 15;
            this.groupControl2.Text = "Opções de Pagamento";
            // 
            // gcParcelas
            // 
            this.gcParcelas.Location = new System.Drawing.Point(9, 36);
            this.gcParcelas.MainView = this.gridView1;
            this.gcParcelas.Name = "gcParcelas";
            this.gcParcelas.Size = new System.Drawing.Size(933, 212);
            this.gcParcelas.TabIndex = 75;
            this.gcParcelas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gcParcelas.Click += new System.EventHandler(this.gcParcelas_Click);
            // 
            // gridView1
            // 
            this.gridView1.ActiveFilterEnabled = false;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColQtdParcelas,
            this.gridColTotal,
            this.gridColValorParcela,
            this.gridColDesconto,
            this.gridColDtPrimeiraParcela,
            this.gridColCetMensal,
            this.gridColCetAnual,
            this.gridColVlCetMensal,
            this.gridColVlCetAnual,
            this.gridColTotalGeral});
            this.gridView1.CustomizationFormBounds = new System.Drawing.Rectangle(339, 0, 252, 416);
            this.gridView1.GridControl = this.gcParcelas;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            this.gridView1.OptionsCustomization.AllowColumnResizing = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowMergedGrouping = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsView.ShowPreviewRowLines = DevExpress.Utils.DefaultBoolean.True;
            // 
            // gridColQtdParcelas
            // 
            this.gridColQtdParcelas.Caption = "Qtd. Parcelas";
            this.gridColQtdParcelas.FieldName = "QtdParcela";
            this.gridColQtdParcelas.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("gridColQtdParcelas.ImageOptions.Image")));
            this.gridColQtdParcelas.MaxWidth = 98;
            this.gridColQtdParcelas.MinWidth = 98;
            this.gridColQtdParcelas.Name = "gridColQtdParcelas";
            this.gridColQtdParcelas.OptionsColumn.AllowEdit = false;
            this.gridColQtdParcelas.OptionsColumn.AllowFocus = false;
            this.gridColQtdParcelas.Visible = true;
            this.gridColQtdParcelas.VisibleIndex = 0;
            this.gridColQtdParcelas.Width = 98;
            // 
            // gridColTotal
            // 
            this.gridColTotal.AppearanceCell.Options.UseTextOptions = true;
            this.gridColTotal.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColTotal.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColTotal.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColTotal.Caption = "Entrada";
            this.gridColTotal.FieldName = "Entrada";
            this.gridColTotal.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("gridColTotal.ImageOptions.Image")));
            this.gridColTotal.MaxWidth = 80;
            this.gridColTotal.MinWidth = 80;
            this.gridColTotal.Name = "gridColTotal";
            this.gridColTotal.OptionsColumn.AllowEdit = false;
            this.gridColTotal.OptionsColumn.AllowFocus = false;
            this.gridColTotal.ShowUnboundExpressionMenu = true;
            this.gridColTotal.Visible = true;
            this.gridColTotal.VisibleIndex = 1;
            this.gridColTotal.Width = 80;
            // 
            // gridColValorParcela
            // 
            this.gridColValorParcela.AppearanceCell.Options.UseTextOptions = true;
            this.gridColValorParcela.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColValorParcela.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColValorParcela.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColValorParcela.Caption = "Valor Parcela";
            this.gridColValorParcela.FieldName = "Valor";
            this.gridColValorParcela.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("gridColValorParcela.ImageOptions.Image")));
            this.gridColValorParcela.MaxWidth = 95;
            this.gridColValorParcela.MinWidth = 95;
            this.gridColValorParcela.Name = "gridColValorParcela";
            this.gridColValorParcela.OptionsColumn.AllowEdit = false;
            this.gridColValorParcela.OptionsColumn.AllowFocus = false;
            this.gridColValorParcela.Visible = true;
            this.gridColValorParcela.VisibleIndex = 2;
            this.gridColValorParcela.Width = 95;
            // 
            // gridColDesconto
            // 
            this.gridColDesconto.AppearanceCell.Options.UseTextOptions = true;
            this.gridColDesconto.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColDesconto.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColDesconto.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColDesconto.Caption = "Desconto";
            this.gridColDesconto.FieldName = "Desconto";
            this.gridColDesconto.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("gridColDesconto.ImageOptions.Image")));
            this.gridColDesconto.MaxWidth = 82;
            this.gridColDesconto.MinWidth = 80;
            this.gridColDesconto.Name = "gridColDesconto";
            this.gridColDesconto.OptionsColumn.AllowEdit = false;
            this.gridColDesconto.OptionsColumn.AllowFocus = false;
            this.gridColDesconto.Visible = true;
            this.gridColDesconto.VisibleIndex = 3;
            this.gridColDesconto.Width = 82;
            // 
            // gridColDtPrimeiraParcela
            // 
            this.gridColDtPrimeiraParcela.AppearanceCell.Options.UseTextOptions = true;
            this.gridColDtPrimeiraParcela.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColDtPrimeiraParcela.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColDtPrimeiraParcela.Caption = "Dt Primeira Parcela";
            this.gridColDtPrimeiraParcela.FieldName = "DtParcela";
            this.gridColDtPrimeiraParcela.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("gridColDtPrimeiraParcela.ImageOptions.SvgImage")));
            this.gridColDtPrimeiraParcela.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.gridColDtPrimeiraParcela.MinWidth = 120;
            this.gridColDtPrimeiraParcela.Name = "gridColDtPrimeiraParcela";
            this.gridColDtPrimeiraParcela.OptionsColumn.AllowEdit = false;
            this.gridColDtPrimeiraParcela.OptionsColumn.AllowFocus = false;
            this.gridColDtPrimeiraParcela.Visible = true;
            this.gridColDtPrimeiraParcela.VisibleIndex = 4;
            this.gridColDtPrimeiraParcela.Width = 130;
            // 
            // gridColCetMensal
            // 
            this.gridColCetMensal.AppearanceCell.Options.UseTextOptions = true;
            this.gridColCetMensal.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColCetMensal.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColCetMensal.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColCetMensal.Caption = "CET Mensal";
            this.gridColCetMensal.FieldName = "CetMensal";
            this.gridColCetMensal.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("gridColCetMensal.ImageOptions.SvgImage")));
            this.gridColCetMensal.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.gridColCetMensal.Name = "gridColCetMensal";
            this.gridColCetMensal.OptionsColumn.AllowEdit = false;
            this.gridColCetMensal.OptionsColumn.AllowFocus = false;
            this.gridColCetMensal.Visible = true;
            this.gridColCetMensal.VisibleIndex = 5;
            this.gridColCetMensal.Width = 91;
            // 
            // gridColCetAnual
            // 
            this.gridColCetAnual.AppearanceCell.Options.UseTextOptions = true;
            this.gridColCetAnual.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColCetAnual.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColCetAnual.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColCetAnual.Caption = "CET Anual";
            this.gridColCetAnual.FieldName = "CetAnual";
            this.gridColCetAnual.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("gridColCetAnual.ImageOptions.SvgImage")));
            this.gridColCetAnual.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.gridColCetAnual.MinWidth = 85;
            this.gridColCetAnual.Name = "gridColCetAnual";
            this.gridColCetAnual.OptionsColumn.AllowEdit = false;
            this.gridColCetAnual.OptionsColumn.AllowFocus = false;
            this.gridColCetAnual.Visible = true;
            this.gridColCetAnual.VisibleIndex = 7;
            this.gridColCetAnual.Width = 90;
            // 
            // gridColVlCetMensal
            // 
            this.gridColVlCetMensal.AppearanceCell.Options.UseTextOptions = true;
            this.gridColVlCetMensal.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColVlCetMensal.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColVlCetMensal.Caption = "Vl. CET Mensal";
            this.gridColVlCetMensal.FieldName = "VlCetMensal";
            this.gridColVlCetMensal.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.gridColVlCetMensal.MinWidth = 70;
            this.gridColVlCetMensal.Name = "gridColVlCetMensal";
            this.gridColVlCetMensal.OptionsColumn.AllowEdit = false;
            this.gridColVlCetMensal.OptionsColumn.AllowFocus = false;
            this.gridColVlCetMensal.Visible = true;
            this.gridColVlCetMensal.VisibleIndex = 6;
            this.gridColVlCetMensal.Width = 100;
            // 
            // gridColVlCetAnual
            // 
            this.gridColVlCetAnual.AppearanceCell.Options.UseTextOptions = true;
            this.gridColVlCetAnual.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColVlCetAnual.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColVlCetAnual.Caption = "Vl. CET Anual";
            this.gridColVlCetAnual.FieldName = "VlCetAnual";
            this.gridColVlCetAnual.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.gridColVlCetAnual.MinWidth = 80;
            this.gridColVlCetAnual.Name = "gridColVlCetAnual";
            this.gridColVlCetAnual.OptionsColumn.AllowEdit = false;
            this.gridColVlCetAnual.OptionsColumn.AllowFocus = false;
            this.gridColVlCetAnual.Visible = true;
            this.gridColVlCetAnual.VisibleIndex = 8;
            this.gridColVlCetAnual.Width = 90;
            // 
            // gridColTotalGeral
            // 
            this.gridColTotalGeral.AppearanceCell.Options.UseTextOptions = true;
            this.gridColTotalGeral.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColTotalGeral.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColTotalGeral.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColTotalGeral.Caption = "Total";
            this.gridColTotalGeral.FieldName = "Total";
            this.gridColTotalGeral.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("gridColTotalGeral.ImageOptions.Image")));
            this.gridColTotalGeral.MaxWidth = 85;
            this.gridColTotalGeral.MinWidth = 85;
            this.gridColTotalGeral.Name = "gridColTotalGeral";
            this.gridColTotalGeral.OptionsColumn.AllowEdit = false;
            this.gridColTotalGeral.OptionsColumn.AllowFocus = false;
            this.gridColTotalGeral.Visible = true;
            this.gridColTotalGeral.VisibleIndex = 9;
            this.gridColTotalGeral.Width = 85;
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmar.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnConfirmar.Appearance.Options.UseFont = true;
            this.btnConfirmar.Appearance.Options.UseTextOptions = true;
            this.btnConfirmar.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnConfirmar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConfirmar.Enabled = false;
            this.btnConfirmar.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnConfirmar.ImageOptions.SvgImage")));
            this.btnConfirmar.Location = new System.Drawing.Point(845, 496);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(118, 40);
            this.btnConfirmar.TabIndex = 77;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // btnCancelAcordo
            // 
            this.btnCancelAcordo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelAcordo.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelAcordo.Appearance.Options.UseFont = true;
            this.btnCancelAcordo.Appearance.Options.UseTextOptions = true;
            this.btnCancelAcordo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnCancelAcordo.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelAcordo.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnCancelAcordo.ImageOptions.SvgImage")));
            this.btnCancelAcordo.Location = new System.Drawing.Point(732, 496);
            this.btnCancelAcordo.Name = "btnCancelAcordo";
            this.btnCancelAcordo.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.btnCancelAcordo.Size = new System.Drawing.Size(104, 40);
            this.btnCancelAcordo.TabIndex = 76;
            this.btnCancelAcordo.Text = "Cancelar";
            this.btnCancelAcordo.Click += new System.EventHandler(this.btnCancelAcordo_Click);
            // 
            // separatorControl2
            // 
            this.separatorControl2.Location = new System.Drawing.Point(13, 482);
            this.separatorControl2.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separatorControl2.Name = "separatorControl2";
            this.separatorControl2.Padding = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separatorControl2.Size = new System.Drawing.Size(949, 10);
            this.separatorControl2.TabIndex = 76;
            // 
            // toolTip
            // 
            this.toolTip.AllowHtmlText = true;
            this.toolTip.Appearance.BackColor = System.Drawing.Color.AntiqueWhite;
            this.toolTip.Appearance.Options.UseBackColor = true;
            this.toolTip.Rounded = true;
            this.toolTip.ShowBeak = true;
            this.toolTip.ToolTipLocation = DevExpress.Utils.ToolTipLocation.TopRight;
            this.toolTip.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip;
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn1.Caption = "Valor Parcela";
            this.gridColumn1.FieldName = "Valor";
            this.gridColumn1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("gridColumn1.ImageOptions.Image")));
            this.gridColumn1.MaxWidth = 95;
            this.gridColumn1.MinWidth = 95;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            this.gridColumn1.Width = 95;
            // 
            // Acordo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelAcordo;
            this.ClientSize = new System.Drawing.Size(975, 547);
            this.Controls.Add(this.separatorControl2);
            this.Controls.Add(this.btnCancelAcordo);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.groupControl9);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Acordo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Acordo";
            this.Load += new System.EventHandler(this.Acordo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl9)).EndInit();
            this.groupControl9.ResumeLayout(false);
            this.groupControl9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesconto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataEntrada.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataEntrada.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValorEntrada.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcParcelas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl9;
        private DevExpress.XtraEditors.LabelControl lblSaldo;
        private DevExpress.XtraEditors.LabelControl labelControl72;
        private DevExpress.XtraEditors.LabelControl lblTaxaJuro;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl lblDiasAtraso;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnCalcular;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.TextEdit txtValorEntrada;
        private DevExpress.XtraEditors.LabelControl lblValorEntrada;
        private DevExpress.XtraEditors.LabelControl lblDtEntrada;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gcParcelas;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColQtdParcelas;
        private DevExpress.XtraGrid.Columns.GridColumn gridColValorParcela;
        private DevExpress.XtraGrid.Columns.GridColumn gridColTotal;
        private DevExpress.XtraEditors.SimpleButton btnConfirmar;
        private DevExpress.XtraEditors.SimpleButton btnCancelAcordo;
        private DevExpress.XtraEditors.SeparatorControl separatorControl2;
        private DevExpress.Utils.ToolTipController toolTip;
        private DevExpress.XtraEditors.LabelControl infoDesconto;
        private DevExpress.XtraEditors.DateEdit txtDataEntrada;
        private DevExpress.XtraEditors.SpinEdit txtDesconto;
        private DevExpress.XtraGrid.Columns.GridColumn gridColDesconto;
        private DevExpress.XtraGrid.Columns.GridColumn gridColTotalGeral;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColCetMensal;
        private DevExpress.XtraGrid.Columns.GridColumn gridColCetAnual;
        private DevExpress.XtraGrid.Columns.GridColumn gridColDtPrimeiraParcela;
        private DevExpress.XtraGrid.Columns.GridColumn gridColVlCetMensal;
        private DevExpress.XtraGrid.Columns.GridColumn gridColVlCetAnual;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}