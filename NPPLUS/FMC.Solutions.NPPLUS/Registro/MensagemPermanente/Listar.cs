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

using FMC.Solutions.NPPLUS.Library.Class;
using DevExpress.XtraBars.Docking;

namespace FMC.Solutions.NPPLUS.Registro.MensagemPermanente
{
    public partial class Listar : DevExpress.XtraEditors.XtraForm
    {
        public string account;
        public string orgCMS;
        public Listar()
        {
            InitializeComponent();
        }

        private void Listar_Load(object sender, EventArgs e)
        {
            foreach(DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton button in groupControl2.CustomHeaderButtons)
            {
                if (!Permission.MensagemPermanente.Cadastrar && button.Caption == "Adicionar Novo")
                    button.Visible = false;
            }
            
            account = ((Main)this.Owner).productSelected.Account;
            orgCMS = ((Main)this.Owner).productSelected.OrgCMS;
            //GetAshi();
        }

        /*
        private void GetAshi()
        {
            try
            {
                Splash.Visible(splashScreenManager1, true);
                splashScreenManager1.SetWaitFormDescription("Recarregando Mensagens Permanentes...");

                gcMensagemPermanente.DataSource = null;
                gcMensagemPermanente.RefreshDataSource();

                ASHI ashi = SessionP2.Session.ASHI(account, orgCMS);
                var mensagemPermanente = ashi.ListaMensagemPermanente();
                UserBLL userBll = new UserBLL();
                ICollection<User> lista = userBll.GetAll().OrderBy(p => p.NmUser).ToList();

                if (mensagemPermanente != null)
                {
                    gcMensagemPermanente.DataSource = mensagemPermanente.OrderByDescending(p => p.Data).ThenByDescending(p => p.Hora).ToList();
                    gcMensagemPermanente.RefreshDataSource();
                }
                Splash.Visible(splashScreenManager1, false);
            }
            catch (Exception ex)
            {
                Splash.Visible(splashScreenManager1, false);
            }
        }
        */

        private void groupControl2_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "Adicionar Novo")
            {
                Cadastrar at = new Cadastrar();
                DialogResult drResult = at.ShowDialog(this);
            }
            else
            {
                //GetAshi();
            }
        }
    }

}