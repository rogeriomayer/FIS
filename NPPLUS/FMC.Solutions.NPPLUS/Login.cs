using DevExpress.XtraEditors;
using FMC.Solutions.NPPLUS.Library.Class;
using FMC.Solutions.NPPLUS.Library.REST;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FMC.Solutions.NPPLUS
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        public bool LoginSuccessful = false;
        public bool Valid = false;
        PrivateFontCollection pfc = new PrivateFontCollection();
        private UserAuthenticate User;

        public Login()
        {
            InitializeComponent();
        }

        ~Login()
        {
            this.Dispose();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtUser.Text.Trim()) && string.IsNullOrEmpty(txtPass.Text.Trim()))
                {
                    lblError.Text = "Senha não preenchida, digite sua senha e continue!";
                }
                else
                {
                    lblError.Text = "";
                    btnLogin.Enabled = false;
                    var parameters = FisAPI.GetParameters(AppSettings.PRODUCT_TYPE);

                    //IdpAccessInfo idp = new IdpAccessInfo() { sub = "E5674556", success = true, ProfileList = "Supervisor" };
                    //IdpAccessInfo idp = new IdpAccessInfo() { sub = "rogerio.mayer", success = true, ProfileList = "Administrador" };

                    IdpAccessInfo idp = LoginIDP.Login(txtUser.Text.Trim(), txtPass.Text, parameters);

                    if (idp.success)
                    {
                        Log.SaveFile("idp.success -- " + idp.sub);

                        LoginUser login = new LoginUser() { User = idp.sub, DsName = idp.FullName, Profiles = idp.ProfileList.Split(',').ToList(), idProductType = AppSettings.PRODUCT_TYPE };

                        UserIDP userData = LoginIDP.GetUserData(idp.UID, idp.accessToken);

                        //UserIDP userData = new UserIDP() { forcePasswordChange = false, passwordExpirationDate = DateTime.Now.AddDays(1) };

                        if (userData.forcePasswordChange || userData.passwordExpirationDate <= DateTime.Now)
                        {
                            string alterarSenha = parameters.Where(p => p.Key == "LINK_ALTERAR_SENHA_IDP").FirstOrDefault().Value;
                            string msg = "É necessário alteração da senha no IDP antes do login no NPPlus!" + Environment.NewLine +
                                "Favor entrar no caminho abaixo para alterar sua senha!" + Environment.NewLine + Environment.NewLine +
                                 alterarSenha;
                            XtraMessageBox.Show(msg, "Alteração de senha", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            System.Diagnostics.Process.Start(alterarSenha);

                            txtPass.Text = "";
                            txtUser.Text = "";
                        }
                        else
                        {
                            User = FisAPI.GetLogin(login);

                            if (User != null)
                            {
                                Program.SetMainForm(new Main(User, parameters));
                                Program.ShowMainForm();
                                this.Close();
                            }
                            else
                                lblError.Text = "Erro ao cadastrar usuário no NPPLUS!";
                        }
                    }
                    else
                    {
                        lblError.Text = idp.message;
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = "btnLogin_Click " + ex.Message;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    erro += ex.Message;
                }
                lblError.Text = erro;

                throw new Exception(erro);
            }
            finally
            {
                btnLogin.Enabled = true;
            }
        }

        //Atribuir Fonte Específica
        private Font CustomFont(string font, string extension, float size, FontStyle style, GraphicsUnit unit)
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(Path.Combine(Application.StartupPath, "Library", "Fonts", font + "." + extension));
            //Application.StartupPath.Substring(0, Application.StartupPath.IndexOf("bin"))
            return new Font(pfc.Families[0], size, style, unit);
        }

        private void Login_Load(object sender, EventArgs e)
        {
            lblLoginAtendimento.Font = CustomFont("BarlowSemiCondensed-Medium", "ttf", 22, FontStyle.Regular, GraphicsUnit.Pixel);

            lblTxtUsuario.Font = CustomFont("BarlowSemiCondensed-Regular", "ttf", 13, FontStyle.Regular, GraphicsUnit.Pixel);
            lblTxtSenha.Font = CustomFont("BarlowSemiCondensed-Regular", "ttf", 13, FontStyle.Regular, GraphicsUnit.Pixel);
            txtUser.Font = CustomFont("BarlowSemiCondensed-Medium", "ttf", 19, FontStyle.Bold, GraphicsUnit.Pixel);
            txtPass.Font = CustomFont("BarlowSemiCondensed-Medium", "ttf", 19, FontStyle.Bold, GraphicsUnit.Pixel);

            txtUser.Text = Environment.UserName;
        }

        private void txtPass_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLogin.PerformClick();
        }

        private bool AlterarSenha(string accessToken, string uid)
        {
            try
            {
                NovaSenha ns = new NovaSenha(User, accessToken, uid);
                DialogResult alteraSenha = ns.ShowDialog(this);
                if (alteraSenha == DialogResult.OK)
                {
                    XtraMessageBox.Show("Senha alterada com sucesso.", "Alteração finalizada!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLogin.Enabled = true;
                    return true;
                }
                else
                {
                    btnLogin.Enabled = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Splash.Visible(splashScreenManager2, false);
                XtraMessageBox.Show(ex.Message, "Erro ao alterar senha!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnLogin.Enabled = true;
                return false;
            }
        }
    }

    public class LoginUser
    {
        public string User { get; set; }
        public string DsName { get; set; }
        public ICollection<string> Profiles { get; set; }
        public byte idProductType { get; set; }
    }
}