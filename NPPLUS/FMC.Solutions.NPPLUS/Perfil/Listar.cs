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

namespace FMC.Solutions.NPPLUS.Perfil
{
    public partial class Listar : DevExpress.XtraEditors.XtraForm
    {
        //public User user;
        public Listar()
        {
            InitializeComponent();
        }

        private void GetPerfil()
        {
            /*
            var accessProfileBll = new AccessProfileBLL();
            ICollection<AccessProfile> lista = accessProfileBll.GetAll();

            if (lista.Count() > 0)
            {
                gcPerfil.DataSource = ListaPerfil(lista);
                gcPerfil.RefreshDataSource();
            }
            else
            {
                gcPerfil.DataSource = null;
                gcPerfil.RefreshDataSource();
            }
            */
        }

        /*
        private ICollection<Perfil> ListaPerfil(ICollection<AccessProfile> lista)
        {
            ICollection<Perfil> perfil = new Collection<Perfil>();
            foreach (AccessProfile item in lista)
            {
                perfil.Add(new Perfil
                {
                    IdAccessProfile = item.IdAccessProfile,
                    DsAccessProfile = item.DsAccessProfile,
                    DtInsert = item.DtInsert.ToString("dd/MM/yyyy"),
                    DtUpdate = item.DtUpdate.HasValue ? item.DtUpdate?.ToString("dd/MM/yyyy HH:mm") : "-",
                    Status = item.DtInactive.HasValue ? "INATIVO" : "ATIVO"
                });
            }
            return perfil;
        }
        */

        private class Perfil
        {
            public int IdAccessProfile { get; set; }
            public string DsAccessProfile { get; set; }
            public string DtInsert { get; set; }
            public string DtUpdate { get; set; }
            public string Status { get; set; }
        }

        private void btnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit editor = (ButtonEdit)sender;
            GridView gv = gcPerfil.MainView as GridView;
            Perfil perfil = (Perfil)gv.GetFocusedRow();

            /*
            AccessProfile accessProfile = new AccessProfileBLL().GetBykey(perfil.IdAccessProfile);
            CadastrarPerfil cadastrar = new CadastrarPerfil(accessProfile);
            DialogResult drResult = cadastrar.ShowDialog(this);
            if (drResult == DialogResult.OK)
            {
                GetPerfil();
                //Recarregar lista
            }
            */
        }

        private void Perfil_Load(object sender, EventArgs e)
        {
            //this.user = ((Main)this.Owner).User;
            GetPerfil();
        }
    }

}