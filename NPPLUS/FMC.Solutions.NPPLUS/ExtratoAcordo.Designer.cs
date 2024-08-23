namespace FMC.Solutions.NPPLUS
{
    partial class ExtratoAcordo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtratoAcordo));
            this.groupControl9 = new DevExpress.XtraEditors.GroupControl();
            this.pbExtratoAcordos = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.gcAcordo = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAcordo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colData = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSituacao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescricao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperador = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.lblDescontoValor = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.separatorControl2 = new DevExpress.XtraEditors.SeparatorControl();
            this.lblValorFinal = new DevExpress.XtraEditors.LabelControl();
            this.lblValorEntrada = new DevExpress.XtraEditors.LabelControl();
            this.lblDataEntrada = new DevExpress.XtraEditors.LabelControl();
            this.lblVlParcela = new DevExpress.XtraEditors.LabelControl();
            this.lblQtdParcelas = new DevExpress.XtraEditors.LabelControl();
            this.lblDataPrimeiraParcela = new DevExpress.XtraEditors.LabelControl();
            this.lblJuroValor = new DevExpress.XtraEditors.LabelControl();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.gcPagamentos = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.lblTotalPago = new DevExpress.XtraEditors.LabelControl();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.lblParcelasAberto = new DevExpress.XtraEditors.LabelControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.lblParcelasPagas = new DevExpress.XtraEditors.LabelControl();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.bwListaAcordo = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl9)).BeginInit();
            this.groupControl9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbExtratoAcordos.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAcordo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPagamentos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl9
            // 
            this.groupControl9.Controls.Add(this.pbExtratoAcordos);
            this.groupControl9.Controls.Add(this.gcAcordo);
            this.groupControl9.Location = new System.Drawing.Point(12, 12);
            this.groupControl9.Name = "groupControl9";
            this.groupControl9.Size = new System.Drawing.Size(776, 229);
            this.groupControl9.TabIndex = 13;
            this.groupControl9.Text = "Lista de Acordos/Negociação";
            // 
            // pbExtratoAcordos
            // 
            this.pbExtratoAcordos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbExtratoAcordos.EditValue = 0;
            this.pbExtratoAcordos.Location = new System.Drawing.Point(2, 222);
            this.pbExtratoAcordos.Name = "pbExtratoAcordos";
            this.pbExtratoAcordos.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pbExtratoAcordos.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pbExtratoAcordos.Properties.MarqueeAnimationSpeed = 30;
            this.pbExtratoAcordos.Size = new System.Drawing.Size(772, 5);
            this.pbExtratoAcordos.TabIndex = 73;
            // 
            // gcAcordo
            // 
            this.gcAcordo.EmbeddedNavigator.Buttons.Edit.Enabled = false;
            this.gcAcordo.Location = new System.Drawing.Point(9, 36);
            this.gcAcordo.MainView = this.gridView3;
            this.gcAcordo.Name = "gcAcordo";
            this.gcAcordo.Size = new System.Drawing.Size(758, 183);
            this.gcAcordo.TabIndex = 0;
            this.gcAcordo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            this.gcAcordo.Click += new System.EventHandler(this.gcAcordo_Click);
            this.gcAcordo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gcAcordo_KeyUp);
            // 
            // gridView3
            // 
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAcordo,
            this.colData,
            this.colSituacao,
            this.colDescricao,
            this.colOperador});
            this.gridView3.CustomizationFormBounds = new System.Drawing.Rectangle(339, 0, 252, 416);
            this.gridView3.GridControl = this.gcAcordo;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView3.OptionsCustomization.AllowColumnMoving = false;
            this.gridView3.OptionsCustomization.AllowColumnResizing = false;
            this.gridView3.OptionsCustomization.AllowGroup = false;
            this.gridView3.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // colAcordo
            // 
            this.colAcordo.Caption = "Acordo";
            this.colAcordo.FieldName = "Acordo";
            this.colAcordo.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("colAcordo.ImageOptions.Image")));
            this.colAcordo.Name = "colAcordo";
            this.colAcordo.OptionsColumn.AllowEdit = false;
            this.colAcordo.OptionsColumn.AllowFocus = false;
            this.colAcordo.OptionsColumn.AllowMove = false;
            this.colAcordo.OptionsColumn.AllowShowHide = false;
            this.colAcordo.OptionsColumn.ReadOnly = true;
            this.colAcordo.Visible = true;
            this.colAcordo.VisibleIndex = 0;
            this.colAcordo.Width = 63;
            // 
            // colData
            // 
            this.colData.Caption = "Data";
            this.colData.FieldName = "Data";
            this.colData.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("colData.ImageOptions.Image")));
            this.colData.MinWidth = 30;
            this.colData.Name = "colData";
            this.colData.OptionsColumn.AllowEdit = false;
            this.colData.OptionsColumn.AllowFocus = false;
            this.colData.OptionsColumn.AllowMove = false;
            this.colData.OptionsColumn.AllowShowHide = false;
            this.colData.Visible = true;
            this.colData.VisibleIndex = 1;
            this.colData.Width = 96;
            // 
            // colSituacao
            // 
            this.colSituacao.Caption = "Situação";
            this.colSituacao.FieldName = "Situacao";
            this.colSituacao.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("colSituacao.ImageOptions.Image")));
            this.colSituacao.Name = "colSituacao";
            this.colSituacao.OptionsColumn.AllowEdit = false;
            this.colSituacao.OptionsColumn.AllowFocus = false;
            this.colSituacao.OptionsColumn.AllowMove = false;
            this.colSituacao.OptionsColumn.AllowShowHide = false;
            this.colSituacao.Visible = true;
            this.colSituacao.VisibleIndex = 2;
            this.colSituacao.Width = 77;
            // 
            // colDescricao
            // 
            this.colDescricao.Caption = "Descrição";
            this.colDescricao.FieldName = "Descricao";
            this.colDescricao.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("colDescricao.ImageOptions.Image")));
            this.colDescricao.Name = "colDescricao";
            this.colDescricao.OptionsColumn.AllowEdit = false;
            this.colDescricao.OptionsColumn.AllowFocus = false;
            this.colDescricao.OptionsColumn.AllowMove = false;
            this.colDescricao.OptionsColumn.AllowShowHide = false;
            this.colDescricao.Visible = true;
            this.colDescricao.VisibleIndex = 3;
            this.colDescricao.Width = 255;
            // 
            // colOperador
            // 
            this.colOperador.Caption = "Operador";
            this.colOperador.FieldName = "Operador";
            this.colOperador.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("colOperador.ImageOptions.Image")));
            this.colOperador.MinWidth = 240;
            this.colOperador.Name = "colOperador";
            this.colOperador.OptionsColumn.AllowEdit = false;
            this.colOperador.OptionsColumn.AllowFocus = false;
            this.colOperador.OptionsColumn.AllowMove = false;
            this.colOperador.OptionsColumn.AllowShowHide = false;
            this.colOperador.OptionsColumn.ReadOnly = true;
            this.colOperador.Visible = true;
            this.colOperador.VisibleIndex = 4;
            this.colOperador.Width = 240;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.separatorControl1);
            this.groupControl1.Controls.Add(this.lblDescontoValor);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.separatorControl2);
            this.groupControl1.Controls.Add(this.lblValorFinal);
            this.groupControl1.Controls.Add(this.lblValorEntrada);
            this.groupControl1.Controls.Add(this.lblDataEntrada);
            this.groupControl1.Controls.Add(this.lblVlParcela);
            this.groupControl1.Controls.Add(this.lblQtdParcelas);
            this.groupControl1.Controls.Add(this.lblDataPrimeiraParcela);
            this.groupControl1.Controls.Add(this.lblJuroValor);
            this.groupControl1.Controls.Add(this.labelControl13);
            this.groupControl1.Controls.Add(this.labelControl12);
            this.groupControl1.Controls.Add(this.labelControl11);
            this.groupControl1.Controls.Add(this.labelControl10);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Location = new System.Drawing.Point(12, 247);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(393, 256);
            this.groupControl1.TabIndex = 65;
            this.groupControl1.Text = "Detalhes da Negociação";
            // 
            // separatorControl1
            // 
            this.separatorControl1.LineOrientation = System.Windows.Forms.Orientation.Vertical;
            this.separatorControl1.Location = new System.Drawing.Point(254, 46);
            this.separatorControl1.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Padding = new System.Windows.Forms.Padding(0);
            this.separatorControl1.Size = new System.Drawing.Size(10, 162);
            this.separatorControl1.TabIndex = 95;
            // 
            // lblDescontoValor
            // 
            this.lblDescontoValor.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblDescontoValor.Appearance.Options.UseFont = true;
            this.lblDescontoValor.Appearance.Options.UseTextOptions = true;
            this.lblDescontoValor.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblDescontoValor.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDescontoValor.Location = new System.Drawing.Point(282, 120);
            this.lblDescontoValor.Name = "lblDescontoValor";
            this.lblDescontoValor.Size = new System.Drawing.Size(73, 13);
            this.lblDescontoValor.TabIndex = 94;
            this.lblDescontoValor.Text = "-";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(284, 99);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(69, 13);
            this.labelControl2.TabIndex = 93;
            this.labelControl2.Text = "Descontos $";
            // 
            // separatorControl2
            // 
            this.separatorControl2.LineOrientation = System.Windows.Forms.Orientation.Vertical;
            this.separatorControl2.Location = new System.Drawing.Point(105, 46);
            this.separatorControl2.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separatorControl2.Name = "separatorControl2";
            this.separatorControl2.Padding = new System.Windows.Forms.Padding(0);
            this.separatorControl2.Size = new System.Drawing.Size(10, 162);
            this.separatorControl2.TabIndex = 87;
            // 
            // lblValorFinal
            // 
            this.lblValorFinal.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblValorFinal.Appearance.Options.UseFont = true;
            this.lblValorFinal.Appearance.Options.UseTextOptions = true;
            this.lblValorFinal.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblValorFinal.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblValorFinal.Location = new System.Drawing.Point(142, 166);
            this.lblValorFinal.Name = "lblValorFinal";
            this.lblValorFinal.Size = new System.Drawing.Size(83, 13);
            this.lblValorFinal.TabIndex = 86;
            this.lblValorFinal.Text = "-";
            // 
            // lblValorEntrada
            // 
            this.lblValorEntrada.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblValorEntrada.Appearance.Options.UseFont = true;
            this.lblValorEntrada.Appearance.Options.UseTextOptions = true;
            this.lblValorEntrada.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblValorEntrada.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblValorEntrada.Location = new System.Drawing.Point(140, 67);
            this.lblValorEntrada.Name = "lblValorEntrada";
            this.lblValorEntrada.Size = new System.Drawing.Size(86, 13);
            this.lblValorEntrada.TabIndex = 85;
            this.lblValorEntrada.Text = "-";
            // 
            // lblDataEntrada
            // 
            this.lblDataEntrada.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblDataEntrada.Appearance.Options.UseFont = true;
            this.lblDataEntrada.Appearance.Options.UseTextOptions = true;
            this.lblDataEntrada.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblDataEntrada.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDataEntrada.Location = new System.Drawing.Point(284, 68);
            this.lblDataEntrada.Name = "lblDataEntrada";
            this.lblDataEntrada.Size = new System.Drawing.Size(85, 13);
            this.lblDataEntrada.TabIndex = 84;
            this.lblDataEntrada.Text = "-";
            // 
            // lblVlParcela
            // 
            this.lblVlParcela.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblVlParcela.Appearance.Options.UseFont = true;
            this.lblVlParcela.Appearance.Options.UseTextOptions = true;
            this.lblVlParcela.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblVlParcela.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblVlParcela.Location = new System.Drawing.Point(13, 120);
            this.lblVlParcela.Name = "lblVlParcela";
            this.lblVlParcela.Size = new System.Drawing.Size(83, 13);
            this.lblVlParcela.TabIndex = 83;
            this.lblVlParcela.Text = "-";
            // 
            // lblQtdParcelas
            // 
            this.lblQtdParcelas.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblQtdParcelas.Appearance.Options.UseFont = true;
            this.lblQtdParcelas.Appearance.Options.UseTextOptions = true;
            this.lblQtdParcelas.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblQtdParcelas.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblQtdParcelas.Location = new System.Drawing.Point(12, 68);
            this.lblQtdParcelas.Name = "lblQtdParcelas";
            this.lblQtdParcelas.Size = new System.Drawing.Size(88, 13);
            this.lblQtdParcelas.TabIndex = 82;
            this.lblQtdParcelas.Text = "-";
            // 
            // lblDataPrimeiraParcela
            // 
            this.lblDataPrimeiraParcela.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblDataPrimeiraParcela.Appearance.Options.UseFont = true;
            this.lblDataPrimeiraParcela.Appearance.Options.UseTextOptions = true;
            this.lblDataPrimeiraParcela.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblDataPrimeiraParcela.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDataPrimeiraParcela.Location = new System.Drawing.Point(137, 120);
            this.lblDataPrimeiraParcela.Name = "lblDataPrimeiraParcela";
            this.lblDataPrimeiraParcela.Size = new System.Drawing.Size(99, 13);
            this.lblDataPrimeiraParcela.TabIndex = 81;
            this.lblDataPrimeiraParcela.Text = "-";
            // 
            // lblJuroValor
            // 
            this.lblJuroValor.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblJuroValor.Appearance.Options.UseFont = true;
            this.lblJuroValor.Appearance.Options.UseTextOptions = true;
            this.lblJuroValor.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblJuroValor.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblJuroValor.Location = new System.Drawing.Point(11, 167);
            this.lblJuroValor.Name = "lblJuroValor";
            this.lblJuroValor.Size = new System.Drawing.Size(73, 13);
            this.lblJuroValor.TabIndex = 80;
            this.lblJuroValor.Text = "-";
            // 
            // labelControl13
            // 
            this.labelControl13.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl13.Appearance.Options.UseFont = true;
            this.labelControl13.Location = new System.Drawing.Point(154, 145);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(58, 13);
            this.labelControl13.TabIndex = 77;
            this.labelControl13.Text = "Valor Final";
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl12.Appearance.Options.UseFont = true;
            this.labelControl12.Location = new System.Drawing.Point(137, 46);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(93, 13);
            this.labelControl12.TabIndex = 76;
            this.labelControl12.Text = "Valor da Entrada";
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl11.Appearance.Options.UseFont = true;
            this.labelControl11.Location = new System.Drawing.Point(281, 46);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(91, 13);
            this.labelControl11.TabIndex = 75;
            this.labelControl11.Text = "Data da Entrada";
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl10.Appearance.Options.UseFont = true;
            this.labelControl10.Location = new System.Drawing.Point(9, 99);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(91, 13);
            this.labelControl10.TabIndex = 74;
            this.labelControl10.Text = "Valor da Parcela";
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.Location = new System.Drawing.Point(11, 46);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(91, 13);
            this.labelControl9.TabIndex = 73;
            this.labelControl9.Text = "Qtd. de Parcelas";
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Location = new System.Drawing.Point(137, 99);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(105, 13);
            this.labelControl8.TabIndex = 72;
            this.labelControl8.Text = "Data da 1ª Parcela";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(27, 145);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(41, 13);
            this.labelControl7.TabIndex = 70;
            this.labelControl7.Text = "Juros $";
            // 
            // gcPagamentos
            // 
            this.gcPagamentos.Font = new System.Drawing.Font("Tahoma", 7.25F);
            this.gcPagamentos.Location = new System.Drawing.Point(9, 75);
            this.gcPagamentos.MainView = this.gridView2;
            this.gcPagamentos.Name = "gcPagamentos";
            this.gcPagamentos.Size = new System.Drawing.Size(359, 172);
            this.gcPagamentos.TabIndex = 80;
            this.gcPagamentos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6});
            this.gridView2.CustomizationFormBounds = new System.Drawing.Rectangle(339, 0, 252, 416);
            this.gridView2.GridControl = this.gcPagamentos;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsCustomization.AllowColumnMoving = false;
            this.gridView2.OptionsCustomization.AllowColumnResizing = false;
            this.gridView2.OptionsCustomization.AllowGroup = false;
            this.gridView2.OptionsCustomization.AllowMergedGrouping = DevExpress.Utils.DefaultBoolean.False;
            this.gridView2.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7.5F);
            this.gridColumn4.AppearanceCell.Options.UseFont = true;
            this.gridColumn4.Caption = "Vencimento";
            this.gridColumn4.FieldName = "Vencimento";
            this.gridColumn4.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("gridColumn4.ImageOptions.Image")));
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowFocus = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            this.gridColumn4.Width = 107;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7.5F);
            this.gridColumn5.AppearanceCell.Options.UseFont = true;
            this.gridColumn5.Caption = "Valor da Parcela";
            this.gridColumn5.FieldName = "Valor";
            this.gridColumn5.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("gridColumn5.ImageOptions.Image")));
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowFocus = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            this.gridColumn5.Width = 101;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7.5F);
            this.gridColumn6.AppearanceCell.Options.UseFont = true;
            this.gridColumn6.Caption = "Situação do Pagamento";
            this.gridColumn6.FieldName = "Situacao";
            this.gridColumn6.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("gridColumn6.ImageOptions.Image")));
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.AllowFocus = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            this.gridColumn6.Width = 124;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.lblTotalPago);
            this.groupControl2.Controls.Add(this.labelControl16);
            this.groupControl2.Controls.Add(this.lblParcelasAberto);
            this.groupControl2.Controls.Add(this.labelControl15);
            this.groupControl2.Controls.Add(this.lblParcelasPagas);
            this.groupControl2.Controls.Add(this.labelControl14);
            this.groupControl2.Controls.Add(this.gcPagamentos);
            this.groupControl2.Location = new System.Drawing.Point(411, 247);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(377, 256);
            this.groupControl2.TabIndex = 66;
            this.groupControl2.Text = "Parcelas";
            // 
            // lblTotalPago
            // 
            this.lblTotalPago.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblTotalPago.Appearance.Options.UseFont = true;
            this.lblTotalPago.Appearance.Options.UseTextOptions = true;
            this.lblTotalPago.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblTotalPago.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTotalPago.Location = new System.Drawing.Point(281, 55);
            this.lblTotalPago.Name = "lblTotalPago";
            this.lblTotalPago.Size = new System.Drawing.Size(85, 13);
            this.lblTotalPago.TabIndex = 90;
            this.lblTotalPago.Text = "-";
            // 
            // labelControl16
            // 
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl16.Appearance.Options.UseFont = true;
            this.labelControl16.Location = new System.Drawing.Point(293, 34);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(60, 13);
            this.labelControl16.TabIndex = 89;
            this.labelControl16.Text = "Total Pago";
            // 
            // lblParcelasAberto
            // 
            this.lblParcelasAberto.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblParcelasAberto.Appearance.Options.UseFont = true;
            this.lblParcelasAberto.Appearance.Options.UseTextOptions = true;
            this.lblParcelasAberto.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblParcelasAberto.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblParcelasAberto.Location = new System.Drawing.Point(147, 55);
            this.lblParcelasAberto.Name = "lblParcelasAberto";
            this.lblParcelasAberto.Size = new System.Drawing.Size(85, 13);
            this.lblParcelasAberto.TabIndex = 88;
            this.lblParcelasAberto.Text = "-";
            // 
            // labelControl15
            // 
            this.labelControl15.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl15.Appearance.Options.UseFont = true;
            this.labelControl15.Location = new System.Drawing.Point(134, 34);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(111, 13);
            this.labelControl15.TabIndex = 87;
            this.labelControl15.Text = "Parcelas em Aberto";
            // 
            // lblParcelasPagas
            // 
            this.lblParcelasPagas.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblParcelasPagas.Appearance.Options.UseFont = true;
            this.lblParcelasPagas.Appearance.Options.UseTextOptions = true;
            this.lblParcelasPagas.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblParcelasPagas.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblParcelasPagas.Location = new System.Drawing.Point(9, 55);
            this.lblParcelasPagas.Name = "lblParcelasPagas";
            this.lblParcelasPagas.Size = new System.Drawing.Size(85, 13);
            this.lblParcelasPagas.TabIndex = 86;
            this.lblParcelasPagas.Text = "-";
            // 
            // labelControl14
            // 
            this.labelControl14.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl14.Appearance.Options.UseFont = true;
            this.labelControl14.Location = new System.Drawing.Point(9, 34);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(85, 13);
            this.labelControl14.TabIndex = 85;
            this.labelControl14.Text = "Parcelas Pagas";
            // 
            // bwListaAcordo
            // 
            this.bwListaAcordo.WorkerReportsProgress = true;
            this.bwListaAcordo.WorkerSupportsCancellation = true;
            this.bwListaAcordo.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwListaAcordo_DoWork);
            this.bwListaAcordo.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwListaAcordo_ProgressChanged);
            this.bwListaAcordo.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwListaAcordo_RunWorkerCompleted);
            // 
            // ExtratoAcordo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 513);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.groupControl9);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExtratoAcordo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Extrato de Acordos/Negociação";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExtratoAcordo_FormClosing);
            this.Load += new System.EventHandler(this.ExtratoAcordo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl9)).EndInit();
            this.groupControl9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbExtratoAcordos.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAcordo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPagamentos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl9;
        private DevExpress.XtraGrid.GridControl gcAcordo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Columns.GridColumn colAcordo;
        private DevExpress.XtraGrid.Columns.GridColumn colData;
        private DevExpress.XtraGrid.Columns.GridColumn colDescricao;
        private DevExpress.XtraGrid.Columns.GridColumn colSituacao;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl lblValorFinal;
        private DevExpress.XtraEditors.LabelControl lblValorEntrada;
        private DevExpress.XtraEditors.LabelControl lblDataEntrada;
        private DevExpress.XtraEditors.LabelControl lblVlParcela;
        private DevExpress.XtraEditors.LabelControl lblQtdParcelas;
        private DevExpress.XtraEditors.LabelControl lblDataPrimeiraParcela;
        private DevExpress.XtraEditors.LabelControl lblJuroValor;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraGrid.GridControl gcPagamentos;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SeparatorControl separatorControl2;
        private System.ComponentModel.BackgroundWorker bwListaAcordo;
        private DevExpress.XtraEditors.MarqueeProgressBarControl pbExtratoAcordos;
        private DevExpress.XtraEditors.LabelControl lblDescontoValor;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraGrid.Columns.GridColumn colOperador;
        private DevExpress.XtraEditors.LabelControl lblParcelasAberto;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.LabelControl lblParcelasPagas;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.LabelControl lblTotalPago;
        private DevExpress.XtraEditors.LabelControl labelControl16;
    }
}