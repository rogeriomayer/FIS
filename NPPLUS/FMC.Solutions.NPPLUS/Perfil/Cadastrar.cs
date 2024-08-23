using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Globalization;

using FMC.Solutions.NPPLUS.Library.Class;


using DevExpress.XtraGrid.Views.Grid;
using FMC.Solutions.NPPLUS.Library.Class.ViaCEP.Model;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using System.Collections.ObjectModel;

namespace FMC.Solutions.NPPLUS.Perfil
{
    public partial class CadastrarPerfil : DevExpress.XtraEditors.XtraForm
    {
        //private AccessProfile accessProfile;
        private DialogResult msg;

        public CadastrarPerfil()
        {
            InitializeComponent();
        }

        /*
        public CadastrarPerfil(AccessProfile _accessProfile)
        {
            this.accessProfile = _accessProfile;
            InitializeComponent();
        }
        */
        private void Cadastrar(string perfil, DateTime? dtInactive)
        {
            /*
            List<TreeListNode> nodes = treeList.GetAllCheckedNodes();
            ICollection<AccessProfileConstantAccess> apca = new Collection<AccessProfileConstantAccess>();
            TreeListColumn columnId = treeList.Columns["Id"];
            foreach (var node in nodes)
                apca.Add(new AccessProfileConstantAccess()
                {
                    IdConstantAccess = Convert.ToInt32(node.GetValue(columnId))
                });

            AccessProfileBLL accessProfileBLL = new AccessProfileBLL();
            AccessProfile item = new AccessProfile()
            {
                DsAccessProfile = perfil,
                DtInsert = DateTime.Now,
                IdUserInsert = ((Main)this.Owner).User.IdUser,
                DtInactive = dtInactive,
                IdUserInactive = dtInactive.HasValue ? ((Main)this.Owner).User.IdUser : (int?)null,
                AccessProfileConstantAccess = apca
            };
            accessProfileBLL.Add(item);
            */
        }

        private void Atualizar(string perfil, DateTime? dtInactive)
        {
            /*
            var apcaBLL = new AccessProfileConstantAccessBLL();
            var accessProfileBLL = new AccessProfileBLL();
            var apca = new Collection<AccessProfileConstantAccess>();
            var apcaOld = apcaBLL.GetByIdAccessProfile(accessProfile.IdAccessProfile);

            List<TreeListNode> nodes = treeList.GetAllCheckedNodes();
            TreeListColumn columnId = treeList.Columns["Id"];

            //Itens selecionados
            foreach (var node in nodes)
                apca.Add(new AccessProfileConstantAccess() { IdConstantAccess = Convert.ToInt32(node.GetValue(columnId)) });

            //Remover itens desmarcados
            foreach (var item in apcaOld)
                if (!apca.Any(p => p.IdConstantAccess == item.IdConstantAccess))
                    apcaBLL.Delete(item);

            AccessProfile ap = accessProfileBLL.GetBykey(accessProfile.IdAccessProfile);
            ap.DsAccessProfile = perfil;

            ap.DtUpdate = DateTime.Now;
            ap.IdUserUpdate = ((Listar)this.Owner).user.IdUser;
            ap.DtInactive = dtInactive;
            ap.IdUserInactive = dtInactive.HasValue ? ((Listar)this.Owner).user.IdUser : (int?)null;

            //Itens para inserir
            foreach (var item in apca)
                if (!ap.AccessProfileConstantAccess.Any(p => p.IdConstantAccess == item.IdConstantAccess))
                    ap.AccessProfileConstantAccess.Add(item);

            accessProfileBLL.Update(ap);
            */
        }

        private void CadastrarPerfil_Load(object sender, EventArgs e)
        {
            /*
            if (accessProfile != null)
            {
                ckAtivo.Checked = !accessProfile.DtInactive.HasValue;
                txtNomePerfil.Text = accessProfile.DsAccessProfile;

                if (!Permission.Perfil.Atualizar)
                    btnConfirmar.Enabled = false;
            }

            CreateColumns(treeList);
            CreateNodes(treeList);
            */
        }

        private void CreateColumns(TreeList tl)
        {
            tl.BeginUpdate();
            TreeListColumn col2 = tl.Columns.Add();
            col2.Caption = "Id";
            col2.VisibleIndex = 0;
            col2.Visible = false;

            TreeListColumn col1 = tl.Columns.Add();
            col1.Caption = "Permissões";
            col1.VisibleIndex = 1;

            tl.EndUpdate();
        }

        private void CreateNodes(TreeList tl)
        {
            /*
            ConstantAccessBLL constantAccessBLL = new ConstantAccessBLL();
            ICollection<ConstantAccess> listaConstantes = constantAccessBLL.GetAll().OrderBy(p => p.DsConstantAccess).ToList();
            ICollection<AccessProfileConstantAccess> apca = new Collection<AccessProfileConstantAccess>();

            if (accessProfile != null)
                apca = new AccessProfileConstantAccessBLL().GetByIdAccessProfile(accessProfile.IdAccessProfile);

            string lastMenu = "";
            tl.BeginUnboundLoad();
            TreeListNode parentForRootNodes = null;
            TreeListNode rootNode = null;
            foreach (ConstantAccess item in listaConstantes)
            {
                IList<string> arr = item.DsConstantAccess.Split('\\');
                if (arr.Count == 2)
                {
                    if (rootNode != null)
                        rootNode.ExpandAll();
                    rootNode = tl.AppendNode(new object[] { item.IdConstantAccess, arr[1] }, parentForRootNodes);
                    lastMenu = arr[1];
                }
                else if (arr.Count > 2)
                {
                    if(lastMenu != arr[1])
                    {
                        if (rootNode != null)
                            rootNode.ExpandAll();
                        rootNode = tl.AppendNode(new object[] { item.IdConstantAccess, arr[1] }, parentForRootNodes);
                        lastMenu = arr[1];
                    }
                    CheckState state = CheckState.Unchecked;
                    if (apca.Any(p => p.IdConstantAccess == item.IdConstantAccess))
                        state = CheckState.Checked;

                    string level = string.Empty;
                    for (int i = 2; i < arr.Count; i++)
                        if (i == 2)
                            level = arr[i];
                        else
                            level += " • " + arr[i];

                    tl.AppendNode(new object[] { item.IdConstantAccess, level }, rootNode, state);
                }
            }

            if (rootNode != null)
                rootNode.ExpandAll();

            tl.OptionsBehavior.Editable = false;
            tl.OptionsCustomization.AllowColumnMoving = false;
            tl.OptionsCustomization.AllowFilter = false;
            tl.OptionsView.ShowHorzLines = false;
            tl.OptionsView.ShowVertLines = false;
            tl.EndUnboundLoad();
            */
        }

        private void treeList_CustomDrawNodeCheckBox(object sender, CustomDrawNodeCheckBoxEventArgs e)
        {
            var node = e.Node;
            if (node.Level == -1 || node.Level == 0)
            {
                e.ObjectArgs.State = DevExpress.Utils.Drawing.ObjectState.Disabled;
                e.Handled = true;
            }
        }

        private void treeList_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            var node = e.Node;
            if (node.Level == -1 || node.Level == 0)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                string perfil = txtNomePerfil.Text.Trim();
                DateTime? dtInactive = ckAtivo.Checked ? (DateTime?)null : DateTime.Now;

                if (string.IsNullOrEmpty(perfil))
                {
                    XtraMessageBox.Show("É obrigatório preencher o Nome do Perfil.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.None;
                }
                else
                {
                    Splash.Visible(splashScreenManager1, true);
                    splashScreenManager1.SetWaitFormDescription("Registrando informações...");

                    /*
                    if (accessProfile == null)
                        Cadastrar(perfil, dtInactive);
                    else
                        Atualizar(perfil, dtInactive);
                    */
                    Splash.Visible(splashScreenManager1, false);
                }
            }
            catch (Exception ex)
            {
                Splash.Visible(splashScreenManager1, false);
                XtraMessageBox.Show("Erro ao inserir/atualizar perfil.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }


}