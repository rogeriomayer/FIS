using DevExpress.XtraEditors;


using FMC.Solutions.NPPLUS.Library.Class;

using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace FMC.Solutions.NPPLUS
{
    public partial class Login_x : DevExpress.XtraEditors.XtraForm
    {
        public bool LoginSuccessful = false;
        public bool valid = false;
        public bool passExpired = false;
        public string senhaAtual = string.Empty;
        public string novaSenha = string.Empty;
        public string confirmNovaSenha = string.Empty;
        PrivateFontCollection pfc = new PrivateFontCollection();
        private UserA user;
        public string hash = "$FMCUDI$V1$10000$V/OIGzjhulYDMXkNX/Dx1XFYVoQKoD5cEXmPUjPk8ZEZWlH6";

        public Login_x()
        {
            InitializeComponent();
        }

        ~Login_x()
        {
            this.Dispose();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.WorkerSupportsCancellation == true)
                backgroundWorker.CancelAsync();
            Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtUser.Text.Trim()) && !string.IsNullOrEmpty(txtUserP2.Text.Trim()) && string.IsNullOrEmpty(txtPass.Text.Trim()))
                {
                    lblError.Text = "Senha não preenchida, digite sua senha e continue!";
                }
                else
                {
                    lblError.Text = "";
                    backgroundWorker.RunWorkerAsync();
                    marqueeProgressBarControl1.Visible = true;
                    btnLogin.Enabled = false;
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
                throw new Exception(erro);

            }
        }

        //Atribuir Fonte Específica
        private Font CustomFont(string font, string extension, float size, FontStyle style, GraphicsUnit unit)
        {
            pfc.AddFontFile(Path.Combine(Application.StartupPath, "Library", "Fonts", font + "." + extension));
            //Application.StartupPath.Substring(0, Application.StartupPath.IndexOf("bin"))
            return new Font(pfc.Families[0], size, style, unit);
        }

        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                lblVersion.Text = "v." + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                //lblLoginAtendimento.Font = CustomFont("BarlowSemiCondensed-Medium", "ttf", 22, FontStyle.Regular, GraphicsUnit.Pixel);

                lblTxtUsuario.Font = CustomFont("BarlowSemiCondensed-Regular", "ttf", 13, FontStyle.Regular, GraphicsUnit.Pixel);
                lblUsuarioP2.Font = CustomFont("BarlowSemiCondensed-Regular", "ttf", 13, FontStyle.Regular, GraphicsUnit.Pixel);
                lblTxtSenha.Font = CustomFont("BarlowSemiCondensed-Regular", "ttf", 13, FontStyle.Regular, GraphicsUnit.Pixel);
                lblAlterarSenha.Font = CustomFont("BarlowSemiCondensed-Regular", "ttf", 11, FontStyle.Regular, GraphicsUnit.Pixel);
                txtUser.Font = CustomFont("BarlowSemiCondensed-Medium", "ttf", 19, FontStyle.Bold, GraphicsUnit.Pixel);
                txtPass.Font = CustomFont("BarlowSemiCondensed-Medium", "ttf", 19, FontStyle.Bold, GraphicsUnit.Pixel);
                txtUserP2.Font = CustomFont("BarlowSemiCondensed-Medium", "ttf", 19, FontStyle.Bold, GraphicsUnit.Pixel);


                txtUser.Text = Environment.UserName;

                /*
                UserBLL userBll = new UserBLL();
                if (!string.IsNullOrEmpty(txtUser.Text.Trim()))
                {
                    user = userBll.GetByUserAd(txtUser.Text.Trim());
                    if (user != null)
                        txtUserP2.Text = user.Non_Oper_CICS_Login;
                }
                */

            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
        }

        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (backgroundWorker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                try
                {
                    //Login válido            
                    //UserBLL userBll = new UserBLL();
                    //user = userBll.GetByUserAd(txtUser.Text.Trim());
                    //user = userBll.GetByUserAd("administrador");
                }
                catch (Exception ex)
                {
                    string erro = "AbstractRepository " + ex.Message;
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                        erro += ex.Message;
                    }
                    throw new Exception(erro);
                }

            }
        }

        private void backgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            if (e.UserState == "Senha Expirada")
            {
                //NovaSenha ns = new NovaSenha();
                //DialogResult alteraSenha = ns.ShowDialog(this);
                //if (alteraSenha == DialogResult.OK)
                //{
                //    try
                //    {
                //        //new SessionP2(user.Non_Oper_CICS_Login, txtPass.Text);
                //        new SessionP2(user.Non_Oper_CICS_Login, senhaAtual, novaSenha);
                //        //SessionP2.Session.UpdatePassword(senhaAtual, novaSenha, confirmNovaSenha);
                //        XtraMessageBox.Show("Senha alterada com sucesso.", "Alteração finalizada!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        valid = true;
                //    }
                //    catch (Exception ex)
                //    {
                //        XtraMessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        backgroundWorker.CancelAsync();
                //        marqueeProgressBarControl1.Hide();
                //        btnLogin.Enabled = true;
                //    }
                //}
                backgroundWorker.CancelAsync();
                marqueeProgressBarControl1.Hide();
                btnLogin.Enabled = true;
            }
            else if (e.UserState == "Outro Terminal")
            {
                backgroundWorker.CancelAsync();
                marqueeProgressBarControl1.Hide();
                btnLogin.Enabled = true;
                XtraMessageBox.Show("Falha ao tentar logar no Mainframe! Você está logado em outro terminal.", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (e.UserState == "Erro Login")
            {
                backgroundWorker.CancelAsync();
                marqueeProgressBarControl1.Hide();
                btnLogin.Enabled = true;
            }
            else
            {
                backgroundWorker.CancelAsync();
                marqueeProgressBarControl1.Hide();
                btnLogin.Enabled = true;
                XtraMessageBox.Show("Aconteceu algum erro não identificado durante o login, tente novamente.\n" + e.UserState.ToString(), "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                btnLogin.Enabled = true;
            }
            else if (!(e.Error == null))
            {
                string erro = e.Error.Message + " -- " + e.Error.StackTrace;
                if (e.Error.InnerException != null)
                    erro += e.Error.InnerException.Message + " -- " + e.Error.StackTrace; ;

                XtraMessageBox.Show(erro);

                btnLogin.Enabled = true;
            }
            else
            {
                if (valid)
                {
                    //Program.SetMainForm(new Main(user, txtUserP2.Text));
                    Program.ShowMainForm();
                    this.Close();
                }
                else
                {
                    marqueeProgressBarControl1.Hide();
                    btnLogin.Enabled = true;
                    txtPass.Text = "";
                    if (passExpired)
                        XtraMessageBox.Show("Senha expirada, necessário alteração.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //else
                    //    XtraMessageBox.Show("Aconteceu algum erro não identificado durante o login, tente novamente.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void txtPass_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLogin.PerformClick();
        }

        private void lblAlterarSenha_Click(object sender, EventArgs e)
        {
            try
            {
                NovaSenha ns = new NovaSenha();
                DialogResult alteraSenha = ns.ShowDialog(this);
                if (alteraSenha == DialogResult.OK)
                {
                    try
                    {
                        Splash.Visible(splashScreenManager2, true);
                        splashScreenManager2.SetWaitFormDescription("Efetuando alteração de senha...");
                        //new SessionP2(user.Non_Oper_CICS_Login, txtPass.Text);
                        //new SessionP2(user.Non_Oper_CICS_Login, senhaAtual, novaSenha);
                        //SessionP2.Session.UpdatePassword(senhaAtual, novaSenha, confirmNovaSenha);
                        Splash.Visible(splashScreenManager2, false);
                        XtraMessageBox.Show("Senha alterada com sucesso.", "Alteração finalizada!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnLogin.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        Splash.Visible(splashScreenManager2, false);
                        XtraMessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnLogin.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}