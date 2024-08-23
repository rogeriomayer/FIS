namespace FMC.Solutions.NPPLUS.Configurations.SystemParameter
{
    partial class ListarSystemParameter
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListarSystemParameter));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            this.btnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gcSystemParameter = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcParameter = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEdit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcAction = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnEditParameter = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcSystemParameter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditParameter)).BeginInit();
            this.SuspendLayout();
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
            this.btnEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.AutoSize = true;
            this.groupControl2.Controls.Add(this.gcSystemParameter);
            this.groupControl2.Location = new System.Drawing.Point(8, 8);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1160, 727);
            this.groupControl2.TabIndex = 76;
            this.groupControl2.Text = "Cadastros";
            // 
            // gcSystemParameter
            // 
            this.gcSystemParameter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcSystemParameter.Location = new System.Drawing.Point(8, 34);
            this.gcSystemParameter.MainView = this.gridView5;
            this.gcSystemParameter.Name = "gcSystemParameter";
            this.gcSystemParameter.Padding = new System.Windows.Forms.Padding(3);
            this.gcSystemParameter.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnEditParameter});
            this.gcSystemParameter.Size = new System.Drawing.Size(1144, 685);
            this.gcSystemParameter.TabIndex = 2;
            this.gcSystemParameter.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5});
            // 
            // gridView5
            // 
            this.gridView5.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcParameter,
            this.gcValue,
            this.gcEdit});
            this.gridView5.CustomizationFormBounds = new System.Drawing.Rectangle(339, 0, 252, 416);
            this.gridView5.GridControl = this.gcSystemParameter;
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView5.OptionsCustomization.AllowSort = false;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            // 
            // gcParameter
            // 
            this.gcParameter.Caption = "Parâmetro";
            this.gcParameter.FieldName = "Parameter";
            this.gcParameter.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("gcParameter.ImageOptions.SvgImage")));
            this.gcParameter.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.gcParameter.MinWidth = 300;
            this.gcParameter.Name = "gcParameter";
            this.gcParameter.OptionsColumn.AllowEdit = false;
            this.gcParameter.OptionsColumn.AllowFocus = false;
            this.gcParameter.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gcParameter.Visible = true;
            this.gcParameter.VisibleIndex = 0;
            this.gcParameter.Width = 300;
            // 
            // gcValue
            // 
            this.gcValue.Caption = "Valor";
            this.gcValue.FieldName = "Value";
            this.gcValue.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("gcValue.ImageOptions.SvgImage")));
            this.gcValue.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.gcValue.MinWidth = 160;
            this.gcValue.Name = "gcValue";
            this.gcValue.OptionsColumn.AllowEdit = false;
            this.gcValue.OptionsColumn.AllowFocus = false;
            this.gcValue.Visible = true;
            this.gcValue.VisibleIndex = 1;
            this.gcValue.Width = 160;
            // 
            // gcEdit
            // 
            this.gcEdit.ColumnEdit = this.btnEditParameter;
            this.gcEdit.MaxWidth = 30;
            this.gcEdit.Name = "gcEdit";
            this.gcEdit.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gcEdit.Visible = true;
            this.gcEdit.VisibleIndex = 2;
            this.gcEdit.Width = 30;
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
            // btnEditParameter
            // 
            this.btnEditParameter.AutoHeight = false;
            editorButtonImageOptions2.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("editorButtonImageOptions2.SvgImage")));
            editorButtonImageOptions2.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btnEditParameter.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btnEditParameter.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btnEditParameter.Name = "btnEditParameter";
            this.btnEditParameter.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnEditParameter_ButtonClick);
            // 
            // ListarSystemParameter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 743);
            this.Controls.Add(this.groupControl2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "ListarSystemParameter";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lista de Parâmetros";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Perfil_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcSystemParameter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditParameter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.Columns.GridColumn gcAction;
        private DevExpress.XtraGrid.GridControl gcSystemParameter;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn gcParameter;
        private DevExpress.XtraGrid.Columns.GridColumn gcValue;
        private DevExpress.XtraGrid.Columns.GridColumn gcEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnEditParameter;
    }
}