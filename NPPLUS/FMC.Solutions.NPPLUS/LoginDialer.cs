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
using FMC.Solutions.NPPLUS.Library.Class;
using FMC.Solutions.NPPLUS.Library.APIDialer;
using DevExpress.XtraEditors.Controls;

namespace FMC.Solutions.NPPLUS
{
    public partial class LoginDialer : DevExpress.XtraEditors.XtraForm
    {
        public string AgentDialer;
        public bool Logado;

        public LoginDialer()
        {
            InitializeComponent();

        }

        private void LoginDialer_Load(object sender, EventArgs e)
        {
            /*
            this.Agente = ((Main)this.Owner).User.UserDialer;
            this.txtLogin.Text = Agente;
            */
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            AgentDialer = txtLogin.Text;

            if (DialerConnection.LoginDialer(txtLogin.Text, txtDialerPass.Text, txtRamal.Text))
            {
                Logado = true;
                this.Close();
            }
            else
            {
                Logado = false;
                XtraMessageBox.Show("Aconteceu algum problema durante o login, tente novamente. Dialer não conectado!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


    }
}