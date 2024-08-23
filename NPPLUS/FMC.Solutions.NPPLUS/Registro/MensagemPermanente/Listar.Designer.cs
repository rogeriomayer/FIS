namespace FMC.Solutions.NPPLUS.Registro.MensagemPermanente
{
    partial class Listar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Listar));
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions1 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions2 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            this.gcAction = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMensagemPermanente = new DevExpress.XtraGrid.GridControl();
            this.gridView6 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::FMC.Solutions.NPPLUS.WaitForm1), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.gcMensagemPermanente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcAction
            // 
            this.gcAction.AppearanceCell.Options.UseTextOptions = true;
            this.gcAction.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcAction.AppearanceHeader.Options.UseTextOptions = true;
            this.gcAction.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcAction.Caption = "Ações";
            this.gcAction.FieldName = "Action";
            this.gcAction.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("gcAction.ImageOptions.SvgImage")));
            this.gcAction.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.gcAction.Name = "gcAction";
            this.gcAction.OptionsColumn.AllowEdit = false;
            this.gcAction.OptionsColumn.AllowFocus = false;
            this.gcAction.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gcAction.Visible = true;
            this.gcAction.VisibleIndex = 6;
            this.gcAction.Width = 94;
            // 
            // gcMensagemPermanente
            // 
            this.gcMensagemPermanente.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcMensagemPermanente.Location = new System.Drawing.Point(5, 31);
            this.gcMensagemPermanente.MainView = this.gridView6;
            this.gcMensagemPermanente.MinimumSize = new System.Drawing.Size(841, 381);
            this.gcMensagemPermanente.Name = "gcMensagemPermanente";
            this.gcMensagemPermanente.Size = new System.Drawing.Size(1150, 691);
            this.gcMensagemPermanente.TabIndex = 40;
            this.gcMensagemPermanente.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView6,
            this.gridView1});
            // 
            // gridView6
            // 
            this.gridView6.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn17,
            this.gridColumn22});
            this.gridView6.CustomizationFormBounds = new System.Drawing.Rectangle(339, 0, 252, 416);
            this.gridView6.GridControl = this.gcMensagemPermanente;
            this.gridView6.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.None, "", null, "")});
            this.gridView6.Name = "gridView6";
            this.gridView6.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView6.OptionsCustomization.AllowColumnMoving = false;
            this.gridView6.OptionsCustomization.AllowColumnResizing = false;
            this.gridView6.OptionsCustomization.AllowMergedGrouping = DevExpress.Utils.DefaultBoolean.False;
            this.gridView6.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridView6.OptionsCustomization.AllowSort = false;
            this.gridView6.OptionsFilter.AllowFilterEditor = false;
            this.gridView6.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Data";
            this.gridColumn13.FieldName = "Data";
            this.gridColumn13.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("gridColumn13.ImageOptions.Image")));
            this.gridColumn13.MaxWidth = 120;
            this.gridColumn13.MinWidth = 85;
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.OptionsColumn.AllowFocus = false;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 0;
            this.gridColumn13.Width = 85;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Hora";
            this.gridColumn14.FieldName = "Hora";
            this.gridColumn14.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("gridColumn14.ImageOptions.Image")));
            this.gridColumn14.MaxWidth = 120;
            this.gridColumn14.MinWidth = 65;
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowEdit = false;
            this.gridColumn14.OptionsColumn.AllowFocus = false;
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 1;
            this.gridColumn14.Width = 65;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "Cod";
            this.gridColumn15.FieldName = "Cod";
            this.gridColumn15.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("gridColumn15.ImageOptions.Image")));
            this.gridColumn15.MaxWidth = 100;
            this.gridColumn15.MinWidth = 50;
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.AllowEdit = false;
            this.gridColumn15.OptionsColumn.AllowFocus = false;
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 2;
            this.gridColumn15.Width = 50;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "Tipo";
            this.gridColumn17.FieldName = "Action";
            this.gridColumn17.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("gridColumn17.ImageOptions.Image")));
            this.gridColumn17.MaxWidth = 150;
            this.gridColumn17.MinWidth = 70;
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.OptionsColumn.AllowEdit = false;
            this.gridColumn17.OptionsColumn.AllowFocus = false;
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 3;
            this.gridColumn17.Width = 70;
            // 
            // gridColumn22
            // 
            this.gridColumn22.Caption = "Descrição";
            this.gridColumn22.FieldName = "Description";
            this.gridColumn22.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("gridColumn22.ImageOptions.Image")));
            this.gridColumn22.MinWidth = 185;
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.OptionsColumn.AllowEdit = false;
            this.gridColumn22.OptionsColumn.AllowFocus = false;
            this.gridColumn22.Visible = true;
            this.gridColumn22.VisibleIndex = 4;
            this.gridColumn22.Width = 544;
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gcMensagemPermanente;
            this.gridView1.Name = "gridView1";
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.AutoSize = true;
            this.groupControl2.Controls.Add(this.gcMensagemPermanente);
            buttonImageOptions1.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonImageOptions1.SvgImage")));
            buttonImageOptions1.SvgImageSize = new System.Drawing.Size(16, 16);
            buttonImageOptions2.Location = DevExpress.XtraEditors.ButtonPanel.ImageLocation.BeforeText;
            buttonImageOptions2.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonImageOptions2.SvgImage")));
            buttonImageOptions2.SvgImageSize = new System.Drawing.Size(16, 16);
            this.groupControl2.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Recarregar", true, buttonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1),
            new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Adicionar Novo", true, buttonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1)});
            this.groupControl2.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.groupControl2.Location = new System.Drawing.Point(8, 7);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1160, 727);
            this.groupControl2.TabIndex = 76;
            this.groupControl2.Text = "Registros";
            this.groupControl2.CustomButtonClick += new DevExpress.XtraBars.Docking2010.BaseButtonEventHandler(this.groupControl2_CustomButtonClick);
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // Listar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 743);
            this.Controls.Add(this.groupControl2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "Listar";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mensagens Permanentes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Listar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcMensagemPermanente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraGrid.Columns.GridColumn gcAction;
        private DevExpress.XtraGrid.GridControl gcMensagemPermanente;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
    }
}