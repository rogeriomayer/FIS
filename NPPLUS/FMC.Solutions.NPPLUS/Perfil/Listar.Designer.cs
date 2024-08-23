namespace FMC.Solutions.NPPLUS.Perfil
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gcPerfil = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcNome = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDtInsert = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDtUpdate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEdit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gcAction = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcPerfil)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.AutoSize = true;
            this.groupControl2.Controls.Add(this.gcPerfil);
            this.groupControl2.Location = new System.Drawing.Point(8, 8);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1160, 727);
            this.groupControl2.TabIndex = 76;
            this.groupControl2.Text = "Cadastros";
            // 
            // gcPerfil
            // 
            this.gcPerfil.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcPerfil.Location = new System.Drawing.Point(8, 34);
            this.gcPerfil.MainView = this.gridView5;
            this.gcPerfil.Name = "gcPerfil";
            this.gcPerfil.Padding = new System.Windows.Forms.Padding(3);
            this.gcPerfil.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnEdit});
            this.gcPerfil.Size = new System.Drawing.Size(1144, 685);
            this.gcPerfil.TabIndex = 1;
            this.gcPerfil.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5});
            // 
            // gridView5
            // 
            this.gridView5.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcNome,
            this.gcDtInsert,
            this.gcDtUpdate,
            this.gcStatus,
            this.gcEdit});
            this.gridView5.CustomizationFormBounds = new System.Drawing.Rectangle(339, 0, 252, 416);
            this.gridView5.GridControl = this.gcPerfil;
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView5.OptionsCustomization.AllowSort = false;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            // 
            // gcNome
            // 
            this.gcNome.Caption = "Nome do Perfil";
            this.gcNome.FieldName = "DsAccessProfile";
            this.gcNome.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("gcNome.ImageOptions.SvgImage")));
            this.gcNome.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.gcNome.MinWidth = 300;
            this.gcNome.Name = "gcNome";
            this.gcNome.OptionsColumn.AllowEdit = false;
            this.gcNome.OptionsColumn.AllowFocus = false;
            this.gcNome.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gcNome.Visible = true;
            this.gcNome.VisibleIndex = 0;
            this.gcNome.Width = 300;
            // 
            // gcDtInsert
            // 
            this.gcDtInsert.Caption = "Data de Cadastro";
            this.gcDtInsert.FieldName = "DtInsert";
            this.gcDtInsert.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("gcDtInsert.ImageOptions.SvgImage")));
            this.gcDtInsert.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.gcDtInsert.MaxWidth = 160;
            this.gcDtInsert.MinWidth = 160;
            this.gcDtInsert.Name = "gcDtInsert";
            this.gcDtInsert.OptionsColumn.AllowEdit = false;
            this.gcDtInsert.OptionsColumn.AllowFocus = false;
            this.gcDtInsert.Visible = true;
            this.gcDtInsert.VisibleIndex = 1;
            this.gcDtInsert.Width = 160;
            // 
            // gcDtUpdate
            // 
            this.gcDtUpdate.Caption = "Última Atualização";
            this.gcDtUpdate.FieldName = "DtUpdate";
            this.gcDtUpdate.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("gcDtUpdate.ImageOptions.SvgImage")));
            this.gcDtUpdate.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.gcDtUpdate.MaxWidth = 160;
            this.gcDtUpdate.MinWidth = 160;
            this.gcDtUpdate.Name = "gcDtUpdate";
            this.gcDtUpdate.OptionsColumn.AllowEdit = false;
            this.gcDtUpdate.OptionsColumn.AllowFocus = false;
            this.gcDtUpdate.Visible = true;
            this.gcDtUpdate.VisibleIndex = 2;
            this.gcDtUpdate.Width = 160;
            // 
            // gcStatus
            // 
            this.gcStatus.Caption = "Status";
            this.gcStatus.FieldName = "Status";
            this.gcStatus.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("gcStatus.ImageOptions.SvgImage")));
            this.gcStatus.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.gcStatus.MaxWidth = 100;
            this.gcStatus.MinWidth = 100;
            this.gcStatus.Name = "gcStatus";
            this.gcStatus.OptionsColumn.AllowEdit = false;
            this.gcStatus.OptionsColumn.AllowFocus = false;
            this.gcStatus.Visible = true;
            this.gcStatus.VisibleIndex = 3;
            this.gcStatus.Width = 100;
            // 
            // gcEdit
            // 
            this.gcEdit.ColumnEdit = this.btnEdit;
            this.gcEdit.MaxWidth = 30;
            this.gcEdit.Name = "gcEdit";
            this.gcEdit.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gcEdit.Visible = true;
            this.gcEdit.VisibleIndex = 4;
            this.gcEdit.Width = 30;
            // 
            // btnEdit
            // 
            this.btnEdit.AutoHeight = false;
            editorButtonImageOptions1.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("editorButtonImageOptions1.SvgImage")));
            editorButtonImageOptions1.SvgImageSize = new System.Drawing.Size(18, 18);
            this.btnEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "Editar", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btnEdit.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Tag = "";
            this.btnEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnEdit_ButtonClick);
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
            this.Text = "Lista de Perfil";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Perfil_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcPerfil)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gcPerfil;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn gcNome;
        private DevExpress.XtraGrid.Columns.GridColumn gcDtInsert;
        private DevExpress.XtraGrid.Columns.GridColumn gcStatus;
        private DevExpress.XtraGrid.Columns.GridColumn gcDtUpdate;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnEdit;
        private DevExpress.XtraGrid.Columns.GridColumn gcAction;
        private DevExpress.XtraGrid.Columns.GridColumn gcEdit;
    }
}