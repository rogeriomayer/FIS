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


using System.Collections.ObjectModel;
using DevExpress.XtraGrid.Views.Grid;

namespace FMC.Solutions.NPPLUS.Usuario
{
    public partial class Listar : DevExpress.XtraEditors.XtraForm
    {
        public Listar()
        {
            InitializeComponent();
        }

        private void GetUser()
        {
            /*
            UserBLL userBll = new UserBLL();
            ICollection<User> lista = userBll.GetAll().OrderBy(p => p.NmUser).ToList();

            if (lista.Count() > 0)
            {
                gcUsuarios.DataSource = ListaUsuarios(lista);
                gcUsuarios.RefreshDataSource();
            }
            else
            {
                gcUsuarios.DataSource = null;
                gcUsuarios.RefreshDataSource();
            }
            */
        }

        /*
        private ICollection<Usuarios> ListaUsuarios(ICollection<User> lista)
        {
            ICollection<Usuarios> usuarios = new Collection<Usuarios>();
            foreach (User item in lista)
            {
                string supervisor = "-";
                if (item.IdManager.HasValue)
                {
                    User s = new UserBLL().GetBykey(item.IdManager);
                    if (s != null)
                        supervisor = s.NmUser;
                }
                string profile = "-";
                UserProfile userProfile = item.UserProfile.Where(p => p.IdUser == item.IdUser).FirstOrDefault();
                if (userProfile != null)
                    profile = userProfile.AccessProfile.DsAccessProfile;


                usuarios.Add(new Usuarios
                {
                    IdUser = item.IdUser,
                    DsNome = item.NmUser,
                    UserAD = item.DsUserAD,
                    UserP2 = item.Non_Oper_CICS_Login,
                    IdDialer = item.UserDialer,
                    LoginDialer = item.FlLoginDialer ? "SIM" : "NÃO",
                    DsSupervisor = supervisor,
                    Status = item.DtInactive.HasValue ? "INATIVO" : "ATIVO",
                    DsProfile = profile
                });
            }
            return usuarios;
        }
        */

        private class Usuarios
        {
            public int IdUser { get; set; }
            public string DsNome { get; set; }
            public string UserAD { get; set; }
            public string UserP2 { get; set; }
            public string IdDialer { get; set; }
            public string LoginDialer { get; set; }
            public string DsSupervisor { get; set; }
            public string Status { get; set; }
            public string DsProfile { get; set; }
        }

        private void Usuario_Load(object sender, EventArgs e)
        {
            GetUser();
        }

        private void btnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            /*
            ButtonEdit editor = (ButtonEdit)sender;
            GridView gv = gcUsuarios.MainView as GridView;
            Usuarios usuario = (Usuarios)gv.GetFocusedRow();

            int rowSelected = gv.FocusedRowHandle;

            User user = new UserBLL().GetBykey(usuario.IdUser);
            FMC.Solutions.NPPLUS.Usuario.CadastrarUsuario cadastrar = new CadastrarUsuario(user);
            DialogResult drResult = cadastrar.ShowDialog(this);
            if (drResult == DialogResult.OK)
            {
                GetUser();
                //Recarregar lista
                gv.FocusedRowHandle = rowSelected;
            }
            */
        }
    }

}