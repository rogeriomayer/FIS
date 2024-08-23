namespace FMC.Solutions.NPPLUS
{
    partial class AtualizarEndereco
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AtualizarEndereco));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnSearchAddress = new DevExpress.XtraEditors.SimpleButton();
            this.btnPesquisarCEP = new DevExpress.XtraEditors.SimpleButton();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.txtEstado = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtCep = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtCidade = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtBairro = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtComplemento = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtNumero = new DevExpress.XtraEditors.TextEdit();
            this.lblNumero = new DevExpress.XtraEditors.LabelControl();
            this.txtLogradouro = new DevExpress.XtraEditors.TextEdit();
            this.lblLogradouro = new DevExpress.XtraEditors.LabelControl();
            this.btnConfirmar = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.separatorControl2 = new DevExpress.XtraEditors.SeparatorControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.dgvTelefones = new System.Windows.Forms.DataGridView();
            this.numeroDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.preferencialDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ativoDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.telefoneBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.btnAddTelefone = new DevExpress.XtraEditors.SimpleButton();
            this.telefoneBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.bwCEP = new System.ComponentModel.BackgroundWorker();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::FMC.Solutions.NPPLUS.WaitForm1), true, true, true);
            this.telefoneBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEstado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCep.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCidade.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBairro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtComplemento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumero.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLogradouro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTelefones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.telefoneBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.telefoneBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.telefoneBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnSearchAddress);
            this.groupControl1.Controls.Add(this.btnPesquisarCEP);
            this.groupControl1.Controls.Add(this.separatorControl1);
            this.groupControl1.Controls.Add(this.txtEstado);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.txtCep);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.txtCidade);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtBairro);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtComplemento);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtNumero);
            this.groupControl1.Controls.Add(this.lblNumero);
            this.groupControl1.Controls.Add(this.txtLogradouro);
            this.groupControl1.Controls.Add(this.lblLogradouro);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(505, 294);
            this.groupControl1.TabIndex = 14;
            this.groupControl1.Text = "Dados Residenciais";
            // 
            // btnSearchAddress
            // 
            this.btnSearchAddress.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSearchAddress.Appearance.Options.UseFont = true;
            this.btnSearchAddress.Appearance.Options.UseTextOptions = true;
            this.btnSearchAddress.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnSearchAddress.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnSearchAddress.ImageOptions.SvgImage")));
            this.btnSearchAddress.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.btnSearchAddress.Location = new System.Drawing.Point(331, 54);
            this.btnSearchAddress.Name = "btnSearchAddress";
            this.btnSearchAddress.Size = new System.Drawing.Size(164, 31);
            this.btnSearchAddress.TabIndex = 89;
            this.btnSearchAddress.Text = "Buscar por Endereço";
            this.btnSearchAddress.Click += new System.EventHandler(this.btnSearchAddress_Click);
            // 
            // btnPesquisarCEP
            // 
            this.btnPesquisarCEP.Appearance.BackColor = System.Drawing.Color.Gainsboro;
            this.btnPesquisarCEP.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnPesquisarCEP.Appearance.Options.UseBackColor = true;
            this.btnPesquisarCEP.Appearance.Options.UseFont = true;
            this.btnPesquisarCEP.Appearance.Options.UseTextOptions = true;
            this.btnPesquisarCEP.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnPesquisarCEP.Enabled = false;
            this.btnPesquisarCEP.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnPesquisarCEP.ImageOptions.SvgImage")));
            this.btnPesquisarCEP.Location = new System.Drawing.Point(174, 54);
            this.btnPesquisarCEP.Name = "btnPesquisarCEP";
            this.btnPesquisarCEP.Size = new System.Drawing.Size(128, 31);
            this.btnPesquisarCEP.TabIndex = 1;
            this.btnPesquisarCEP.Text = "Pesquisar";
            this.btnPesquisarCEP.Click += new System.EventHandler(this.btnPesquisarCEP_Click);
            // 
            // separatorControl1
            // 
            this.separatorControl1.Location = new System.Drawing.Point(9, 90);
            this.separatorControl1.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Padding = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separatorControl1.Size = new System.Drawing.Size(485, 10);
            this.separatorControl1.TabIndex = 86;
            // 
            // txtEstado
            // 
            this.txtEstado.Location = new System.Drawing.Point(414, 253);
            this.txtEstado.Margin = new System.Windows.Forms.Padding(4);
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtEstado.Properties.Appearance.Options.UseFont = true;
            this.txtEstado.Properties.AutoHeight = false;
            this.txtEstado.Properties.MaxLength = 2;
            this.txtEstado.Size = new System.Drawing.Size(81, 31);
            this.txtEstado.TabIndex = 7;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(418, 233);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(38, 13);
            this.labelControl5.TabIndex = 84;
            this.labelControl5.Text = "Estado";
            // 
            // txtCep
            // 
            this.txtCep.Location = new System.Drawing.Point(9, 54);
            this.txtCep.Margin = new System.Windows.Forms.Padding(4);
            this.txtCep.Name = "txtCep";
            this.txtCep.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtCep.Properties.Appearance.Options.UseFont = true;
            this.txtCep.Properties.AutoHeight = false;
            this.txtCep.Properties.Mask.EditMask = "00.000-000";
            this.txtCep.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            this.txtCep.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtCep.Properties.MaxLength = 10;
            this.txtCep.Size = new System.Drawing.Size(158, 31);
            this.txtCep.TabIndex = 0;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(13, 34);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(20, 13);
            this.labelControl4.TabIndex = 82;
            this.labelControl4.Text = "CEP";
            // 
            // txtCidade
            // 
            this.txtCidade.Location = new System.Drawing.Point(9, 253);
            this.txtCidade.Margin = new System.Windows.Forms.Padding(4);
            this.txtCidade.Name = "txtCidade";
            this.txtCidade.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtCidade.Properties.Appearance.Options.UseFont = true;
            this.txtCidade.Properties.AutoHeight = false;
            this.txtCidade.Properties.MaxLength = 30;
            this.txtCidade.Size = new System.Drawing.Size(397, 31);
            this.txtCidade.TabIndex = 6;
            this.txtCidade.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCidade_KeyUp);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(13, 233);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(38, 13);
            this.labelControl3.TabIndex = 80;
            this.labelControl3.Text = "Cidade";
            // 
            // txtBairro
            // 
            this.txtBairro.Location = new System.Drawing.Point(176, 192);
            this.txtBairro.Margin = new System.Windows.Forms.Padding(4);
            this.txtBairro.Name = "txtBairro";
            this.txtBairro.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtBairro.Properties.Appearance.Options.UseFont = true;
            this.txtBairro.Properties.AutoHeight = false;
            this.txtBairro.Size = new System.Drawing.Size(319, 31);
            this.txtBairro.TabIndex = 5;
            this.txtBairro.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBairro_KeyUp);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(179, 172);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(34, 13);
            this.labelControl2.TabIndex = 78;
            this.labelControl2.Text = "Bairro";
            // 
            // txtComplemento
            // 
            this.txtComplemento.Location = new System.Drawing.Point(9, 192);
            this.txtComplemento.Margin = new System.Windows.Forms.Padding(4);
            this.txtComplemento.Name = "txtComplemento";
            this.txtComplemento.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtComplemento.Properties.Appearance.Options.UseFont = true;
            this.txtComplemento.Properties.AutoHeight = false;
            this.txtComplemento.Size = new System.Drawing.Size(158, 31);
            this.txtComplemento.TabIndex = 4;
            this.txtComplemento.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtComplemento_KeyUp);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(13, 172);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(79, 13);
            this.labelControl1.TabIndex = 76;
            this.labelControl1.Text = "Complemento";
            // 
            // txtNumero
            // 
            this.txtNumero.Location = new System.Drawing.Point(400, 131);
            this.txtNumero.Margin = new System.Windows.Forms.Padding(4);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtNumero.Properties.Appearance.Options.UseFont = true;
            this.txtNumero.Properties.AutoHeight = false;
            this.txtNumero.Properties.MaxLength = 5;
            this.txtNumero.Size = new System.Drawing.Size(95, 31);
            this.txtNumero.TabIndex = 3;
            // 
            // lblNumero
            // 
            this.lblNumero.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblNumero.Appearance.Options.UseFont = true;
            this.lblNumero.Location = new System.Drawing.Point(404, 111);
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.Size = new System.Drawing.Size(44, 13);
            this.lblNumero.TabIndex = 74;
            this.lblNumero.Text = "Número";
            // 
            // txtLogradouro
            // 
            this.txtLogradouro.Location = new System.Drawing.Point(9, 131);
            this.txtLogradouro.Margin = new System.Windows.Forms.Padding(4);
            this.txtLogradouro.Name = "txtLogradouro";
            this.txtLogradouro.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtLogradouro.Properties.Appearance.Options.UseFont = true;
            this.txtLogradouro.Properties.AutoHeight = false;
            this.txtLogradouro.Properties.MaxLength = 40;
            this.txtLogradouro.Size = new System.Drawing.Size(383, 31);
            this.txtLogradouro.TabIndex = 2;
            this.txtLogradouro.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtLogradouro_KeyUp);
            // 
            // lblLogradouro
            // 
            this.lblLogradouro.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblLogradouro.Appearance.Options.UseFont = true;
            this.lblLogradouro.Location = new System.Drawing.Point(13, 111);
            this.lblLogradouro.Name = "lblLogradouro";
            this.lblLogradouro.Size = new System.Drawing.Size(65, 13);
            this.lblLogradouro.TabIndex = 49;
            this.lblLogradouro.Text = "Logradouro";
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
            this.btnConfirmar.Location = new System.Drawing.Point(396, 601);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(118, 40);
            this.btnConfirmar.TabIndex = 13;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Appearance.Options.UseTextOptions = true;
            this.btnCancel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnCancel.ImageOptions.SvgImage")));
            this.btnCancel.Location = new System.Drawing.Point(283, 601);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.btnCancel.Size = new System.Drawing.Size(104, 40);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // separatorControl2
            // 
            this.separatorControl2.Location = new System.Drawing.Point(11, 587);
            this.separatorControl2.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separatorControl2.Name = "separatorControl2";
            this.separatorControl2.Padding = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.separatorControl2.Size = new System.Drawing.Size(504, 10);
            this.separatorControl2.TabIndex = 76;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.dgvTelefones);
            this.groupControl2.Controls.Add(this.txtEmail);
            this.groupControl2.Controls.Add(this.labelControl6);
            this.groupControl2.Controls.Add(this.btnAddTelefone);
            this.groupControl2.Location = new System.Drawing.Point(12, 314);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(505, 269);
            this.groupControl2.TabIndex = 78;
            this.groupControl2.Text = "Contatos";
            this.groupControl2.Paint += new System.Windows.Forms.PaintEventHandler(this.groupControl2_Paint);
            // 
            // dgvTelefones
            // 
            this.dgvTelefones.AllowUserToAddRows = false;
            this.dgvTelefones.AutoGenerateColumns = false;
            this.dgvTelefones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTelefones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.numeroDataGridViewTextBoxColumn,
            this.tipoDataGridViewTextBoxColumn,
            this.preferencialDataGridViewCheckBoxColumn,
            this.ativoDataGridViewCheckBoxColumn});
            this.dgvTelefones.DataSource = this.telefoneBindingSource;
            this.dgvTelefones.Location = new System.Drawing.Point(5, 97);
            this.dgvTelefones.MultiSelect = false;
            this.dgvTelefones.Name = "dgvTelefones";
            this.dgvTelefones.RowHeadersVisible = false;
            this.dgvTelefones.Size = new System.Drawing.Size(349, 150);
            this.dgvTelefones.TabIndex = 92;
            this.dgvTelefones.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTelefones_CellClick);
            // 
            // numeroDataGridViewTextBoxColumn
            // 
            this.numeroDataGridViewTextBoxColumn.DataPropertyName = "Numero";
            this.numeroDataGridViewTextBoxColumn.HeaderText = "Numero";
            this.numeroDataGridViewTextBoxColumn.Name = "numeroDataGridViewTextBoxColumn";
            this.numeroDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tipoDataGridViewTextBoxColumn
            // 
            this.tipoDataGridViewTextBoxColumn.DataPropertyName = "Tipo";
            this.tipoDataGridViewTextBoxColumn.HeaderText = "Tipo";
            this.tipoDataGridViewTextBoxColumn.Name = "tipoDataGridViewTextBoxColumn";
            this.tipoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // preferencialDataGridViewCheckBoxColumn
            // 
            this.preferencialDataGridViewCheckBoxColumn.DataPropertyName = "Preferencial";
            this.preferencialDataGridViewCheckBoxColumn.HeaderText = "Preferencial";
            this.preferencialDataGridViewCheckBoxColumn.Name = "preferencialDataGridViewCheckBoxColumn";
            this.preferencialDataGridViewCheckBoxColumn.Width = 80;
            // 
            // ativoDataGridViewCheckBoxColumn
            // 
            this.ativoDataGridViewCheckBoxColumn.DataPropertyName = "Ativo";
            this.ativoDataGridViewCheckBoxColumn.HeaderText = "Ativo";
            this.ativoDataGridViewCheckBoxColumn.Name = "ativoDataGridViewCheckBoxColumn";
            this.ativoDataGridViewCheckBoxColumn.Width = 50;
            // 
            // telefoneBindingSource
            // 
            this.telefoneBindingSource.DataSource = typeof(FMC.Solutions.NPPLUS.Telefone);
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(5, 50);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtEmail.Properties.Appearance.Options.UseFont = true;
            this.txtEmail.Properties.AutoHeight = false;
            this.txtEmail.Properties.MaxLength = 300;
            this.txtEmail.Size = new System.Drawing.Size(489, 31);
            this.txtEmail.TabIndex = 90;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(9, 30);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(35, 13);
            this.labelControl6.TabIndex = 91;
            this.labelControl6.Text = "E-mail";
            // 
            // btnAddTelefone
            // 
            this.btnAddTelefone.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.btnAddTelefone.Location = new System.Drawing.Point(370, 97);
            this.btnAddTelefone.Name = "btnAddTelefone";
            this.btnAddTelefone.Size = new System.Drawing.Size(124, 30);
            this.btnAddTelefone.TabIndex = 79;
            this.btnAddTelefone.Text = "Adicionar Telefone";
            this.btnAddTelefone.Click += new System.EventHandler(this.btnAddTelefone_Click);
            // 
            // telefoneBindingSource2
            // 
            this.telefoneBindingSource2.DataSource = typeof(FMC.Solutions.NPPLUS.Telefone);
            // 
            // bwCEP
            // 
            this.bwCEP.WorkerReportsProgress = true;
            this.bwCEP.WorkerSupportsCancellation = true;
            this.bwCEP.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwCEP_DoWork);
            this.bwCEP.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwCEP_ProgressChanged);
            this.bwCEP.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwCEP_RunWorkerCompleted);
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // telefoneBindingSource1
            // 
            this.telefoneBindingSource1.DataSource = typeof(FMC.Solutions.NPPLUS.Telefone);
            // 
            // AtualizarEndereco
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(529, 644);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.separatorControl2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AtualizarEndereco";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Atualizar Cadastro";
            this.Load += new System.EventHandler(this.AtualizarEndereco_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEstado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCep.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCidade.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBairro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtComplemento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumero.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLogradouro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTelefones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.telefoneBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.telefoneBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.telefoneBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtLogradouro;
        private DevExpress.XtraEditors.LabelControl lblLogradouro;
        private DevExpress.XtraEditors.SimpleButton btnConfirmar;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SeparatorControl separatorControl2;
        private DevExpress.XtraEditors.TextEdit txtNumero;
        private DevExpress.XtraEditors.LabelControl lblNumero;
        private DevExpress.XtraEditors.TextEdit txtEstado;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtCep;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtCidade;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtBairro;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtComplemento;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.SimpleButton btnPesquisarCEP;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.ComponentModel.BackgroundWorker bwCEP;
        private DevExpress.XtraEditors.SimpleButton btnSearchAddress;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private DevExpress.XtraEditors.SimpleButton btnAddTelefone;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private System.Windows.Forms.BindingSource telefoneBindingSource;
        private System.Windows.Forms.BindingSource telefoneBindingSource1;
        private System.Windows.Forms.DataGridView dgvTelefones;
        private System.Windows.Forms.BindingSource telefoneBindingSource2;
        private System.Windows.Forms.DataGridViewTextBoxColumn numeroDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn preferencialDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ativoDataGridViewCheckBoxColumn;
    }
}