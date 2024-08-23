namespace FMC.Solutions.NPPLUS
{
    partial class ExtratoResumido
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtratoResumido));
            this.groupControl9 = new DevExpress.XtraEditors.GroupControl();
            this.pbExtrato = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.gcExtrato = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colData = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSituacao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colValMinimo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPagamento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bwLista = new System.ComponentModel.BackgroundWorker();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lblProdutosServicos = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.separatorControl7 = new DevExpress.XtraEditors.SeparatorControl();
            this.separatorControl3 = new DevExpress.XtraEditors.SeparatorControl();
            this.lblTransDolar = new DevExpress.XtraEditors.LabelControl();
            this.labelControl20 = new DevExpress.XtraEditors.LabelControl();
            this.lblTransporte = new DevExpress.XtraEditors.LabelControl();
            this.labelControl22 = new DevExpress.XtraEditors.LabelControl();
            this.lblAutorizacoesPendentes = new DevExpress.XtraEditors.LabelControl();
            this.lblVencimentosProjetados = new DevExpress.XtraEditors.LabelControl();
            this.labelControl25 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl26 = new DevExpress.XtraEditors.LabelControl();
            this.lblTransacoesDisputa = new DevExpress.XtraEditors.LabelControl();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.lblComprasParceladas = new DevExpress.XtraEditors.LabelControl();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.lblMulta = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.lblJuros = new DevExpress.XtraEditors.LabelControl();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.lblPagamento = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lblPagamentoMinimo = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.separatorControl2 = new DevExpress.XtraEditors.SeparatorControl();
            this.lblComprasRotativas = new DevExpress.XtraEditors.LabelControl();
            this.lblSaques = new DevExpress.XtraEditors.LabelControl();
            this.lblEncargos = new DevExpress.XtraEditors.LabelControl();
            this.lblAjustes = new DevExpress.XtraEditors.LabelControl();
            this.lblSaldoAtual = new DevExpress.XtraEditors.LabelControl();
            this.lblSaldoAnterior = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl9)).BeginInit();
            this.groupControl9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbExtrato.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcExtrato)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl9
            // 
            this.groupControl9.Controls.Add(this.pbExtrato);
            this.groupControl9.Controls.Add(this.gcExtrato);
            this.groupControl9.Location = new System.Drawing.Point(12, 12);
            this.groupControl9.Name = "groupControl9";
            this.groupControl9.Size = new System.Drawing.Size(594, 255);
            this.groupControl9.TabIndex = 13;
            this.groupControl9.Text = "Lista do Resumo";
            // 
            // pbExtrato
            // 
            this.pbExtrato.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbExtrato.EditValue = 0;
            this.pbExtrato.Location = new System.Drawing.Point(2, 248);
            this.pbExtrato.Name = "pbExtrato";
            this.pbExtrato.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pbExtrato.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pbExtrato.Properties.MarqueeAnimationSpeed = 30;
            this.pbExtrato.Size = new System.Drawing.Size(590, 5);
            this.pbExtrato.TabIndex = 73;
            // 
            // gcExtrato
            // 
            this.gcExtrato.EmbeddedNavigator.Buttons.Edit.Enabled = false;
            this.gcExtrato.Location = new System.Drawing.Point(9, 36);
            this.gcExtrato.MainView = this.gridView3;
            this.gcExtrato.Name = "gcExtrato";
            this.gcExtrato.Size = new System.Drawing.Size(576, 209);
            this.gcExtrato.TabIndex = 0;
            this.gcExtrato.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            this.gcExtrato.Click += new System.EventHandler(this.gcExtrato_Click);
            this.gcExtrato.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gcExtrato_KeyUp);
            // 
            // gridView3
            // 
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colData,
            this.colSituacao,
            this.colValMinimo,
            this.colPagamento});
            this.gridView3.CustomizationFormBounds = new System.Drawing.Rectangle(339, 0, 252, 416);
            this.gridView3.GridControl = this.gcExtrato;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView3.OptionsCustomization.AllowSort = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // colData
            // 
            this.colData.Caption = "Data da Fatura";
            this.colData.FieldName = "Data";
            this.colData.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("colData.ImageOptions.Image")));
            this.colData.MaxWidth = 130;
            this.colData.MinWidth = 130;
            this.colData.Name = "colData";
            this.colData.OptionsColumn.AllowEdit = false;
            this.colData.OptionsColumn.AllowFocus = false;
            this.colData.OptionsColumn.AllowMove = false;
            this.colData.OptionsColumn.AllowShowHide = false;
            this.colData.Visible = true;
            this.colData.VisibleIndex = 0;
            this.colData.Width = 130;
            // 
            // colSituacao
            // 
            this.colSituacao.Caption = "Saldo";
            this.colSituacao.FieldName = "Saldo";
            this.colSituacao.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("colSituacao.ImageOptions.SvgImage")));
            this.colSituacao.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.colSituacao.MaxWidth = 133;
            this.colSituacao.MinWidth = 133;
            this.colSituacao.Name = "colSituacao";
            this.colSituacao.OptionsColumn.AllowEdit = false;
            this.colSituacao.OptionsColumn.AllowFocus = false;
            this.colSituacao.OptionsColumn.AllowMove = false;
            this.colSituacao.OptionsColumn.AllowShowHide = false;
            this.colSituacao.Visible = true;
            this.colSituacao.VisibleIndex = 1;
            this.colSituacao.Width = 133;
            // 
            // colValMinimo
            // 
            this.colValMinimo.Caption = "Valor Mínimo";
            this.colValMinimo.FieldName = "Minimo";
            this.colValMinimo.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("colValMinimo.ImageOptions.SvgImage")));
            this.colValMinimo.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.colValMinimo.MaxWidth = 143;
            this.colValMinimo.MinWidth = 143;
            this.colValMinimo.Name = "colValMinimo";
            this.colValMinimo.OptionsColumn.AllowEdit = false;
            this.colValMinimo.OptionsColumn.AllowFocus = false;
            this.colValMinimo.OptionsColumn.AllowMove = false;
            this.colValMinimo.OptionsColumn.AllowShowHide = false;
            this.colValMinimo.Visible = true;
            this.colValMinimo.VisibleIndex = 2;
            this.colValMinimo.Width = 143;
            // 
            // colPagamento
            // 
            this.colPagamento.Caption = "Pagamento";
            this.colPagamento.FieldName = "Pagamento";
            this.colPagamento.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("colPagamento.ImageOptions.SvgImage")));
            this.colPagamento.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.colPagamento.MaxWidth = 143;
            this.colPagamento.MinWidth = 143;
            this.colPagamento.Name = "colPagamento";
            this.colPagamento.OptionsColumn.AllowEdit = false;
            this.colPagamento.OptionsColumn.AllowFocus = false;
            this.colPagamento.OptionsColumn.AllowMove = false;
            this.colPagamento.OptionsColumn.AllowShowHide = false;
            this.colPagamento.Visible = true;
            this.colPagamento.VisibleIndex = 3;
            this.colPagamento.Width = 143;
            // 
            // bwLista
            // 
            this.bwLista.WorkerReportsProgress = true;
            this.bwLista.WorkerSupportsCancellation = true;
            this.bwLista.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLista_DoWork);
            this.bwLista.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwLista_ProgressChanged);
            this.bwLista.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLista_RunWorkerCompleted);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.lblProdutosServicos);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.separatorControl7);
            this.groupControl1.Controls.Add(this.separatorControl3);
            this.groupControl1.Controls.Add(this.lblTransDolar);
            this.groupControl1.Controls.Add(this.labelControl20);
            this.groupControl1.Controls.Add(this.lblTransporte);
            this.groupControl1.Controls.Add(this.labelControl22);
            this.groupControl1.Controls.Add(this.lblAutorizacoesPendentes);
            this.groupControl1.Controls.Add(this.lblVencimentosProjetados);
            this.groupControl1.Controls.Add(this.labelControl25);
            this.groupControl1.Controls.Add(this.labelControl26);
            this.groupControl1.Controls.Add(this.lblTransacoesDisputa);
            this.groupControl1.Controls.Add(this.labelControl16);
            this.groupControl1.Controls.Add(this.lblComprasParceladas);
            this.groupControl1.Controls.Add(this.labelControl18);
            this.groupControl1.Controls.Add(this.lblMulta);
            this.groupControl1.Controls.Add(this.labelControl10);
            this.groupControl1.Controls.Add(this.lblJuros);
            this.groupControl1.Controls.Add(this.labelControl14);
            this.groupControl1.Controls.Add(this.lblPagamento);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.lblPagamentoMinimo);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.separatorControl1);
            this.groupControl1.Controls.Add(this.separatorControl2);
            this.groupControl1.Controls.Add(this.lblComprasRotativas);
            this.groupControl1.Controls.Add(this.lblSaques);
            this.groupControl1.Controls.Add(this.lblEncargos);
            this.groupControl1.Controls.Add(this.lblAjustes);
            this.groupControl1.Controls.Add(this.lblSaldoAtual);
            this.groupControl1.Controls.Add(this.lblSaldoAnterior);
            this.groupControl1.Controls.Add(this.labelControl12);
            this.groupControl1.Controls.Add(this.labelControl11);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Location = new System.Drawing.Point(12, 274);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(594, 266);
            this.groupControl1.TabIndex = 66;
            this.groupControl1.Text = "Detalhes da Negociação";
            // 
            // lblProdutosServicos
            // 
            this.lblProdutosServicos.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblProdutosServicos.Appearance.Options.UseFont = true;
            this.lblProdutosServicos.Appearance.Options.UseTextOptions = true;
            this.lblProdutosServicos.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblProdutosServicos.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblProdutosServicos.Location = new System.Drawing.Point(305, 109);
            this.lblProdutosServicos.Name = "lblProdutosServicos";
            this.lblProdutosServicos.Size = new System.Drawing.Size(99, 13);
            this.lblProdutosServicos.TabIndex = 120;
            this.lblProdutosServicos.Text = "-";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(298, 88);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(112, 13);
            this.labelControl3.TabIndex = 119;
            this.labelControl3.Text = "Produtos e Serviços";
            // 
            // separatorControl7
            // 
            this.separatorControl7.Location = new System.Drawing.Point(12, 69);
            this.separatorControl7.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separatorControl7.Name = "separatorControl7";
            this.separatorControl7.Padding = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separatorControl7.Size = new System.Drawing.Size(571, 4);
            this.separatorControl7.TabIndex = 118;
            // 
            // separatorControl3
            // 
            this.separatorControl3.LineOrientation = System.Windows.Forms.Orientation.Vertical;
            this.separatorControl3.Location = new System.Drawing.Point(427, 93);
            this.separatorControl3.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separatorControl3.Name = "separatorControl3";
            this.separatorControl3.Padding = new System.Windows.Forms.Padding(0);
            this.separatorControl3.Size = new System.Drawing.Size(10, 161);
            this.separatorControl3.TabIndex = 117;
            // 
            // lblTransDolar
            // 
            this.lblTransDolar.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblTransDolar.Appearance.Options.UseFont = true;
            this.lblTransDolar.Appearance.Options.UseTextOptions = true;
            this.lblTransDolar.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblTransDolar.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTransDolar.Location = new System.Drawing.Point(479, 244);
            this.lblTransDolar.Name = "lblTransDolar";
            this.lblTransDolar.Size = new System.Drawing.Size(73, 13);
            this.lblTransDolar.TabIndex = 116;
            this.lblTransDolar.Text = "-";
            // 
            // labelControl20
            // 
            this.labelControl20.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl20.Appearance.Options.UseFont = true;
            this.labelControl20.Location = new System.Drawing.Point(456, 222);
            this.labelControl20.Name = "labelControl20";
            this.labelControl20.Size = new System.Drawing.Size(119, 13);
            this.labelControl20.TabIndex = 115;
            this.labelControl20.Text = "Transações em Dólar";
            // 
            // lblTransporte
            // 
            this.lblTransporte.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblTransporte.Appearance.Options.UseFont = true;
            this.lblTransporte.Appearance.Options.UseTextOptions = true;
            this.lblTransporte.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblTransporte.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTransporte.Location = new System.Drawing.Point(479, 199);
            this.lblTransporte.Name = "lblTransporte";
            this.lblTransporte.Size = new System.Drawing.Size(73, 13);
            this.lblTransporte.TabIndex = 114;
            this.lblTransporte.Text = "-";
            // 
            // labelControl22
            // 
            this.labelControl22.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl22.Appearance.Options.UseFont = true;
            this.labelControl22.Location = new System.Drawing.Point(484, 177);
            this.labelControl22.Name = "labelControl22";
            this.labelControl22.Size = new System.Drawing.Size(63, 13);
            this.labelControl22.TabIndex = 113;
            this.labelControl22.Text = "Transporte";
            // 
            // lblAutorizacoesPendentes
            // 
            this.lblAutorizacoesPendentes.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblAutorizacoesPendentes.Appearance.Options.UseFont = true;
            this.lblAutorizacoesPendentes.Appearance.Options.UseTextOptions = true;
            this.lblAutorizacoesPendentes.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblAutorizacoesPendentes.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblAutorizacoesPendentes.Location = new System.Drawing.Point(472, 154);
            this.lblAutorizacoesPendentes.Name = "lblAutorizacoesPendentes";
            this.lblAutorizacoesPendentes.Size = new System.Drawing.Size(86, 13);
            this.lblAutorizacoesPendentes.TabIndex = 111;
            this.lblAutorizacoesPendentes.Text = "-";
            // 
            // lblVencimentosProjetados
            // 
            this.lblVencimentosProjetados.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblVencimentosProjetados.Appearance.Options.UseFont = true;
            this.lblVencimentosProjetados.Appearance.Options.UseTextOptions = true;
            this.lblVencimentosProjetados.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblVencimentosProjetados.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblVencimentosProjetados.Location = new System.Drawing.Point(473, 109);
            this.lblVencimentosProjetados.Name = "lblVencimentosProjetados";
            this.lblVencimentosProjetados.Size = new System.Drawing.Size(85, 13);
            this.lblVencimentosProjetados.TabIndex = 110;
            this.lblVencimentosProjetados.Text = "-";
            // 
            // labelControl25
            // 
            this.labelControl25.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl25.Appearance.Options.UseFont = true;
            this.labelControl25.Location = new System.Drawing.Point(447, 132);
            this.labelControl25.Name = "labelControl25";
            this.labelControl25.Size = new System.Drawing.Size(137, 13);
            this.labelControl25.TabIndex = 109;
            this.labelControl25.Text = "Autorizações Pendentes";
            // 
            // labelControl26
            // 
            this.labelControl26.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl26.Appearance.Options.UseFont = true;
            this.labelControl26.Location = new System.Drawing.Point(446, 88);
            this.labelControl26.Name = "labelControl26";
            this.labelControl26.Size = new System.Drawing.Size(138, 13);
            this.labelControl26.TabIndex = 108;
            this.labelControl26.Text = "Vencimentos Projetados";
            // 
            // lblTransacoesDisputa
            // 
            this.lblTransacoesDisputa.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblTransacoesDisputa.Appearance.Options.UseFont = true;
            this.lblTransacoesDisputa.Appearance.Options.UseTextOptions = true;
            this.lblTransacoesDisputa.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblTransacoesDisputa.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTransacoesDisputa.Location = new System.Drawing.Point(318, 244);
            this.lblTransacoesDisputa.Name = "lblTransacoesDisputa";
            this.lblTransacoesDisputa.Size = new System.Drawing.Size(73, 13);
            this.lblTransacoesDisputa.TabIndex = 107;
            this.lblTransacoesDisputa.Text = "-";
            // 
            // labelControl16
            // 
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl16.Appearance.Options.UseFont = true;
            this.labelControl16.Location = new System.Drawing.Point(288, 222);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(132, 13);
            this.labelControl16.TabIndex = 106;
            this.labelControl16.Text = "Transações em Disputa";
            // 
            // lblComprasParceladas
            // 
            this.lblComprasParceladas.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblComprasParceladas.Appearance.Options.UseFont = true;
            this.lblComprasParceladas.Appearance.Options.UseTextOptions = true;
            this.lblComprasParceladas.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblComprasParceladas.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblComprasParceladas.Location = new System.Drawing.Point(318, 199);
            this.lblComprasParceladas.Name = "lblComprasParceladas";
            this.lblComprasParceladas.Size = new System.Drawing.Size(73, 13);
            this.lblComprasParceladas.TabIndex = 105;
            this.lblComprasParceladas.Text = "-";
            // 
            // labelControl18
            // 
            this.labelControl18.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl18.Appearance.Options.UseFont = true;
            this.labelControl18.Location = new System.Drawing.Point(297, 177);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(115, 13);
            this.labelControl18.TabIndex = 104;
            this.labelControl18.Text = "Compras Parceladas";
            // 
            // lblMulta
            // 
            this.lblMulta.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblMulta.Appearance.Options.UseFont = true;
            this.lblMulta.Appearance.Options.UseTextOptions = true;
            this.lblMulta.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblMulta.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblMulta.Location = new System.Drawing.Point(167, 244);
            this.lblMulta.Name = "lblMulta";
            this.lblMulta.Size = new System.Drawing.Size(73, 13);
            this.lblMulta.TabIndex = 103;
            this.lblMulta.Text = "-";
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl10.Appearance.Options.UseFont = true;
            this.labelControl10.Location = new System.Drawing.Point(187, 222);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(32, 13);
            this.labelControl10.TabIndex = 102;
            this.labelControl10.Text = "Multa";
            // 
            // lblJuros
            // 
            this.lblJuros.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblJuros.Appearance.Options.UseFont = true;
            this.lblJuros.Appearance.Options.UseTextOptions = true;
            this.lblJuros.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblJuros.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblJuros.Location = new System.Drawing.Point(167, 199);
            this.lblJuros.Name = "lblJuros";
            this.lblJuros.Size = new System.Drawing.Size(73, 13);
            this.lblJuros.TabIndex = 101;
            this.lblJuros.Text = "-";
            // 
            // labelControl14
            // 
            this.labelControl14.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl14.Appearance.Options.UseFont = true;
            this.labelControl14.Location = new System.Drawing.Point(188, 177);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(31, 13);
            this.labelControl14.TabIndex = 100;
            this.labelControl14.Text = "Juros";
            // 
            // lblPagamento
            // 
            this.lblPagamento.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblPagamento.Appearance.Options.UseFont = true;
            this.lblPagamento.Appearance.Options.UseTextOptions = true;
            this.lblPagamento.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblPagamento.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblPagamento.Location = new System.Drawing.Point(30, 200);
            this.lblPagamento.Name = "lblPagamento";
            this.lblPagamento.Size = new System.Drawing.Size(73, 13);
            this.lblPagamento.TabIndex = 99;
            this.lblPagamento.Text = "-";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(34, 178);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(65, 13);
            this.labelControl4.TabIndex = 98;
            this.labelControl4.Text = "Pagamento";
            // 
            // lblPagamentoMinimo
            // 
            this.lblPagamentoMinimo.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblPagamentoMinimo.Appearance.Options.UseFont = true;
            this.lblPagamentoMinimo.Appearance.Options.UseTextOptions = true;
            this.lblPagamentoMinimo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblPagamentoMinimo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblPagamentoMinimo.Location = new System.Drawing.Point(30, 155);
            this.lblPagamentoMinimo.Name = "lblPagamentoMinimo";
            this.lblPagamentoMinimo.Size = new System.Drawing.Size(73, 13);
            this.lblPagamentoMinimo.TabIndex = 97;
            this.lblPagamentoMinimo.Text = "-";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(12, 133);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(109, 13);
            this.labelControl2.TabIndex = 96;
            this.labelControl2.Text = "Pagamento Mínimo";
            // 
            // separatorControl1
            // 
            this.separatorControl1.LineOrientation = System.Windows.Forms.Orientation.Vertical;
            this.separatorControl1.Location = new System.Drawing.Point(272, 93);
            this.separatorControl1.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Padding = new System.Windows.Forms.Padding(0);
            this.separatorControl1.Size = new System.Drawing.Size(10, 161);
            this.separatorControl1.TabIndex = 95;
            // 
            // separatorControl2
            // 
            this.separatorControl2.LineOrientation = System.Windows.Forms.Orientation.Vertical;
            this.separatorControl2.Location = new System.Drawing.Point(127, 93);
            this.separatorControl2.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separatorControl2.Name = "separatorControl2";
            this.separatorControl2.Padding = new System.Windows.Forms.Padding(0);
            this.separatorControl2.Size = new System.Drawing.Size(10, 161);
            this.separatorControl2.TabIndex = 87;
            // 
            // lblComprasRotativas
            // 
            this.lblComprasRotativas.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblComprasRotativas.Appearance.Options.UseFont = true;
            this.lblComprasRotativas.Appearance.Options.UseTextOptions = true;
            this.lblComprasRotativas.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblComprasRotativas.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblComprasRotativas.Location = new System.Drawing.Point(311, 154);
            this.lblComprasRotativas.Name = "lblComprasRotativas";
            this.lblComprasRotativas.Size = new System.Drawing.Size(86, 13);
            this.lblComprasRotativas.TabIndex = 85;
            this.lblComprasRotativas.Text = "-";
            // 
            // lblSaques
            // 
            this.lblSaques.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblSaques.Appearance.Options.UseFont = true;
            this.lblSaques.Appearance.Options.UseTextOptions = true;
            this.lblSaques.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblSaques.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblSaques.Location = new System.Drawing.Point(24, 244);
            this.lblSaques.Name = "lblSaques";
            this.lblSaques.Size = new System.Drawing.Size(85, 13);
            this.lblSaques.TabIndex = 84;
            this.lblSaques.Text = "-";
            // 
            // lblEncargos
            // 
            this.lblEncargos.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblEncargos.Appearance.Options.UseFont = true;
            this.lblEncargos.Appearance.Options.UseTextOptions = true;
            this.lblEncargos.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblEncargos.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblEncargos.Location = new System.Drawing.Point(159, 154);
            this.lblEncargos.Name = "lblEncargos";
            this.lblEncargos.Size = new System.Drawing.Size(88, 13);
            this.lblEncargos.TabIndex = 82;
            this.lblEncargos.Text = "-";
            // 
            // lblAjustes
            // 
            this.lblAjustes.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblAjustes.Appearance.Options.UseFont = true;
            this.lblAjustes.Appearance.Options.UseTextOptions = true;
            this.lblAjustes.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblAjustes.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblAjustes.Location = new System.Drawing.Point(154, 109);
            this.lblAjustes.Name = "lblAjustes";
            this.lblAjustes.Size = new System.Drawing.Size(99, 13);
            this.lblAjustes.TabIndex = 81;
            this.lblAjustes.Text = "-";
            // 
            // lblSaldoAtual
            // 
            this.lblSaldoAtual.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblSaldoAtual.Appearance.Options.UseFont = true;
            this.lblSaldoAtual.Appearance.Options.UseTextOptions = true;
            this.lblSaldoAtual.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblSaldoAtual.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblSaldoAtual.Location = new System.Drawing.Point(30, 110);
            this.lblSaldoAtual.Name = "lblSaldoAtual";
            this.lblSaldoAtual.Size = new System.Drawing.Size(73, 13);
            this.lblSaldoAtual.TabIndex = 80;
            this.lblSaldoAtual.Text = "-";
            // 
            // lblSaldoAnterior
            // 
            this.lblSaldoAnterior.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblSaldoAnterior.Appearance.Options.UseFont = true;
            this.lblSaldoAnterior.Appearance.Options.UseTextOptions = true;
            this.lblSaldoAnterior.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblSaldoAnterior.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblSaldoAnterior.Location = new System.Drawing.Point(285, 40);
            this.lblSaldoAnterior.Name = "lblSaldoAnterior";
            this.lblSaldoAnterior.Size = new System.Drawing.Size(98, 17);
            this.lblSaldoAnterior.TabIndex = 79;
            this.lblSaldoAnterior.Text = "-";
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl12.Appearance.Options.UseFont = true;
            this.labelControl12.Location = new System.Drawing.Point(300, 132);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(108, 13);
            this.labelControl12.TabIndex = 76;
            this.labelControl12.Text = "Compras Rotativas";
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl11.Appearance.Options.UseFont = true;
            this.labelControl11.Location = new System.Drawing.Point(46, 222);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(41, 13);
            this.labelControl11.TabIndex = 75;
            this.labelControl11.Text = "Saques";
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.Location = new System.Drawing.Point(178, 132);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(51, 13);
            this.labelControl9.TabIndex = 73;
            this.labelControl9.Text = "Encargos";
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Location = new System.Drawing.Point(182, 88);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(43, 13);
            this.labelControl8.TabIndex = 72;
            this.labelControl8.Text = "Ajustes";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(34, 88);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(64, 13);
            this.labelControl7.TabIndex = 70;
            this.labelControl7.Text = "Saldo Atual";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl5.Location = new System.Drawing.Point(177, 40);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(102, 17);
            this.labelControl5.TabIndex = 68;
            this.labelControl5.Text = "Saldo Anterior";
            // 
            // ExtratoResumido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 551);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.groupControl9);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExtratoResumido";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Extrato Resumido";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExtratoResumido_FormClosing);
            this.Load += new System.EventHandler(this.ExtratoResumido_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl9)).EndInit();
            this.groupControl9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbExtrato.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcExtrato)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl9;
        private DevExpress.XtraGrid.GridControl gcExtrato;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Columns.GridColumn colData;
        private DevExpress.XtraGrid.Columns.GridColumn colValMinimo;
        private DevExpress.XtraGrid.Columns.GridColumn colSituacao;
        private System.ComponentModel.BackgroundWorker bwLista;
        private DevExpress.XtraEditors.MarqueeProgressBarControl pbExtrato;
        private DevExpress.XtraGrid.Columns.GridColumn colPagamento;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.SeparatorControl separatorControl2;
        private DevExpress.XtraEditors.LabelControl lblComprasRotativas;
        private DevExpress.XtraEditors.LabelControl lblSaques;
        private DevExpress.XtraEditors.LabelControl lblEncargos;
        private DevExpress.XtraEditors.LabelControl lblAjustes;
        private DevExpress.XtraEditors.LabelControl lblSaldoAtual;
        private DevExpress.XtraEditors.LabelControl lblSaldoAnterior;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl lblPagamentoMinimo;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lblPagamento;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SeparatorControl separatorControl3;
        private DevExpress.XtraEditors.LabelControl lblTransDolar;
        private DevExpress.XtraEditors.LabelControl labelControl20;
        private DevExpress.XtraEditors.LabelControl lblTransporte;
        private DevExpress.XtraEditors.LabelControl labelControl22;
        private DevExpress.XtraEditors.LabelControl lblAutorizacoesPendentes;
        private DevExpress.XtraEditors.LabelControl lblVencimentosProjetados;
        private DevExpress.XtraEditors.LabelControl labelControl25;
        private DevExpress.XtraEditors.LabelControl labelControl26;
        private DevExpress.XtraEditors.LabelControl lblTransacoesDisputa;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.LabelControl lblComprasParceladas;
        private DevExpress.XtraEditors.LabelControl labelControl18;
        private DevExpress.XtraEditors.LabelControl lblMulta;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl lblJuros;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.SeparatorControl separatorControl7;
        private DevExpress.XtraEditors.LabelControl lblProdutosServicos;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}