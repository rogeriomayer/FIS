namespace FMC.Solutions.NPPLUS.Usuario
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
            this.gcUsuarios = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcNome = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcUser = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCics = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcAgente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.LoginDialer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSupervisor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Perfil = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEdit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gcAction = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcUsuarios)).BeginInit();
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
            this.groupControl2.Controls.Add(this.gcUsuarios);
            this.groupControl2.Location = new System.Drawing.Point(8, 8);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1160, 727);
            this.groupControl2.TabIndex = 76;
            this.groupControl2.Text = "Cadastros";
            // 
            // gcUsuarios
            // 
            this.gcUsuarios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcUsuarios.Location = new System.Drawing.Point(8, 34);
            this.gcUsuarios.MainView = this.gridView5;
            this.gcUsuarios.Name = "gcUsuarios";
            this.gcUsuarios.Padding = new System.Windows.Forms.Padding(3);
            this.gcUsuarios.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnEdit});
            this.gcUsuarios.Size = new System.Drawing.Size(1144, 685);
            this.gcUsuarios.TabIndex = 1;
            this.gcUsuarios.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5});
            // 
            // gridView5
            // 
            this.gridView5.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcNome,
            this.gcUser,
            this.gcCics,
            this.gcAgente,
            this.LoginDialer,
            this.gcStatus,
            this.gcSupervisor,
            this.Perfil,
            this.gcEdit});
            this.gridView5.CustomizationFormBounds = new System.Drawing.Rectangle(339, 0, 252, 416);
            this.gridView5.GridControl = this.gcUsuarios;
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView5.OptionsCustomization.AllowSort = false;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            // 
            // gcNome
            // 
            this.gcNome.Caption = "Nome";
            this.gcNome.FieldName = "DsNome";
            this.gcNome.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("gcNome.ImageOptions.SvgImage")));
            this.gcNome.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.gcNome.MinWidth = 250;
            this.gcNome.Name = "gcNome";
            this.gcNome.OptionsColumn.AllowEdit = false;
            this.gcNome.OptionsColumn.AllowFocus = false;
            this.gcNome.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gcNome.Visible = true;
            this.gcNome.VisibleIndex = 0;
            this.gcNome.Width = 253;
            // 
            // gcUser
            // 
            this.gcUser.Caption = "Usuário de AD";
            this.gcUser.FieldName = "UserAD";
            this.gcUser.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("gcUser.ImageOptions.SvgImage")));
            this.gcUser.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.gcUser.MinWidth = 160;
            this.gcUser.Name = "gcUser";
            this.gcUser.OptionsColumn.AllowEdit = false;
            this.gcUser.OptionsColumn.AllowFocus = false;
            this.gcUser.Visible = true;
            this.gcUser.VisibleIndex = 1;
            this.gcUser.Width = 160;
            // 
            // gcCics
            // 
            this.gcCics.Caption = "Usuário P2";
            this.gcCics.FieldName = "UserP2";
            this.gcCics.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("gcCics.ImageOptions.SvgImage")));
            this.gcCics.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.gcCics.MaxWidth = 80;
            this.gcCics.MinWidth = 95;
            this.gcCics.Name = "gcCics";
            this.gcCics.OptionsColumn.AllowEdit = false;
            this.gcCics.OptionsColumn.AllowFocus = false;
            this.gcCics.Visible = true;
            this.gcCics.VisibleIndex = 2;
            this.gcCics.Width = 95;
            // 
            // gcAgente
            // 
            this.gcAgente.Caption = "Agente Dialer";
            this.gcAgente.FieldName = "IdDialer";
            this.gcAgente.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("gcAgente.ImageOptions.SvgImage")));
            this.gcAgente.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.gcAgente.MaxWidth = 110;
            this.gcAgente.MinWidth = 110;
            this.gcAgente.Name = "gcAgente";
            this.gcAgente.OptionsColumn.AllowEdit = false;
            this.gcAgente.OptionsColumn.AllowFocus = false;
            this.gcAgente.Visible = true;
            this.gcAgente.VisibleIndex = 3;
            this.gcAgente.Width = 110;
            // 
            // LoginDialer
            // 
            this.LoginDialer.Caption = "Login Dialer";
            this.LoginDialer.FieldName = "LoginDialer";
            this.LoginDialer.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("LoginDialer.ImageOptions.SvgImage")));
            this.LoginDialer.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.LoginDialer.MaxWidth = 100;
            this.LoginDialer.MinWidth = 100;
            this.LoginDialer.Name = "LoginDialer";
            this.LoginDialer.OptionsColumn.AllowEdit = false;
            this.LoginDialer.OptionsColumn.AllowFocus = false;
            this.LoginDialer.Visible = true;
            this.LoginDialer.VisibleIndex = 5;
            this.LoginDialer.Width = 100;
            // 
            // gcStatus
            // 
            this.gcStatus.Caption = "Status";
            this.gcStatus.FieldName = "Status";
            this.gcStatus.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("gcStatus.ImageOptions.SvgImage")));
            this.gcStatus.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.gcStatus.MaxWidth = 80;
            this.gcStatus.MinWidth = 80;
            this.gcStatus.Name = "gcStatus";
            this.gcStatus.OptionsColumn.AllowEdit = false;
            this.gcStatus.OptionsColumn.AllowFocus = false;
            this.gcStatus.Visible = true;
            this.gcStatus.VisibleIndex = 4;
            this.gcStatus.Width = 80;
            // 
            // gcSupervisor
            // 
            this.gcSupervisor.Caption = "Supervisor";
            this.gcSupervisor.FieldName = "DsSupervisor";
            this.gcSupervisor.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("gcSupervisor.ImageOptions.SvgImage")));
            this.gcSupervisor.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.CommonPalette;
            this.gcSupervisor.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.gcSupervisor.MaxWidth = 150;
            this.gcSupervisor.MinWidth = 150;
            this.gcSupervisor.Name = "gcSupervisor";
            this.gcSupervisor.OptionsColumn.AllowEdit = false;
            this.gcSupervisor.OptionsColumn.AllowFocus = false;
            this.gcSupervisor.Visible = true;
            this.gcSupervisor.VisibleIndex = 6;
            this.gcSupervisor.Width = 150;
            // 
            // Perfil
            // 
            this.Perfil.Caption = "Perfil";
            this.Perfil.FieldName = "DsProfile";
            this.Perfil.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("Perfil.ImageOptions.SvgImage")));
            this.Perfil.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.CommonPalette;
            this.Perfil.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.Perfil.MaxWidth = 80;
            this.Perfil.MinWidth = 80;
            this.Perfil.Name = "Perfil";
            this.Perfil.OptionsColumn.AllowEdit = false;
            this.Perfil.OptionsColumn.AllowFocus = false;
            this.Perfil.Visible = true;
            this.Perfil.VisibleIndex = 7;
            this.Perfil.Width = 80;
            // 
            // gcEdit
            // 
            this.gcEdit.ColumnEdit = this.btnEdit;
            this.gcEdit.MaxWidth = 30;
            this.gcEdit.Name = "gcEdit";
            this.gcEdit.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gcEdit.Visible = true;
            this.gcEdit.VisibleIndex = 8;
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
            this.Text = "Usuários";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Usuario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcUsuarios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gcUsuarios;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn gcNome;
        private DevExpress.XtraGrid.Columns.GridColumn gcUser;
        private DevExpress.XtraGrid.Columns.GridColumn gcAgente;
        private DevExpress.XtraGrid.Columns.GridColumn LoginDialer;
        private DevExpress.XtraGrid.Columns.GridColumn gcStatus;
        private DevExpress.XtraGrid.Columns.GridColumn gcCics;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnEdit;
        private DevExpress.XtraGrid.Columns.GridColumn gcAction;
        private DevExpress.XtraGrid.Columns.GridColumn gcEdit;
        private DevExpress.XtraGrid.Columns.GridColumn gcSupervisor;
        private DevExpress.XtraGrid.Columns.GridColumn Perfil;
    }
}