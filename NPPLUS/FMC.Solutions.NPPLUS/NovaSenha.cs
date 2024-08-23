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
using FMC.Solutions.NPPLUS.Library.REST;

namespace FMC.Solutions.NPPLUS
{
    public partial class NovaSenha : DevExpress.XtraEditors.XtraForm
    {

        private UserAuthenticate User;
        private string AccessToken;
        private string UID;

        public NovaSenha(UserAuthenticate user, string accessToken, string uid)
        {
            InitializeComponent();

            txtSenhaAtual.Enabled = false;

            User = user;
            AccessToken = accessToken;
            UID = uid;
        }


        private void btnCancelAlteracao_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (txtNovaSenha.Text == txtSenhaAtual.Text)
                XtraMessageBox.Show("A nova senha deve ser diferente da senha atual!", "Ops!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (PasswordUtil.GetPassPoint(txtNovaSenha.Text) < 56)
                XtraMessageBox.Show("A nova senha conter no mínimo 8 caracteres, entre eles letras maiusculas, caracteres especiais e números!", "Ops! Senha fraca...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (txtRepitaNovaSenha.Text != txtNovaSenha.Text)
                XtraMessageBox.Show("A confirmação da senha deve ser idêntica à sua nova senha", "Ops!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {

                if (ChangePassword(AccessToken, UID))
                    this.DialogResult = DialogResult.OK;
                else
                    this.DialogResult = DialogResult.None;

                this.Close();
            }
        }

        private bool ChangePassword(string accessToken, string uid)
        {
            var login = new LoginUser();
            login.User = User.User;

            var alterIdp = new LoginIDP().ChangePassword(accessToken, uid, txtNovaSenha.Text);

            var result = FisAPI.ChangePassword(login, User.OAuth.access_token);

            return alterIdp == "sucesso";

        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void NovaSenha_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.None)
                e.Cancel = true;
        }
    }
}