using System;
using System.Windows.Forms;
using FMC.Automator.DistribuicaoDesk.BLL;
using FMC.Automator.DistribuicaoDesk.DAO.Model;
using FMC.Systems;
using FMC.Systems.Model;
using FMC.Automator.DistribuicaoDesk.Classes;

using AtmTerminalLib;
using AxAtmTerminalLib;
using FMC.Systems.Class;

namespace FMC.Automator.DistribuicaoDesk
{
    public partial class Form1 : Form
    {
        WCCAccess wccAccess = new WCCAccess();
        BZAAccess bzaAccess = new BZAAccess();
        GlobestarAccess globestarAccess = new GlobestarAccess();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btnProcessar_Click(object sender, EventArgs e)
        {
            try
            {
                /*dados login retirar*/
                /*txtUserWCC.Text = "IF7243A";
                  txtPassWCC.Text = "FMC@@123";
                  txtUserWCC.Text = "IB3221A";
                  txtPassWCC.Text = "FMC@@123";
                  txtUserWCC.Text = "IH5523A ";
                  txtPassWCC.Text = "FMC#0101";
                  txtUserWCC.Text = "IJ2209A";
                  txtPassWCC.Text = "FMC@#013";*/

                /*txtUserGlobestar.Text = "IF7243A";
                  txtPassGlobestar.Text = "FMC@@123";
                  txtUserGlobestar.Text = "IB7608A";
                  txtPassGlobestar.Text = "BRF@0011";
                  txtUserGlobestar.Text = "IB3221A";
                  txtPassGlobestar.Text = "FMC@@123";
                  txtUserGlobestar.Text = "IJ2209A";
                  txtPassGlobestar.Text = "FMC@#014";*/

                /*txtUserBZA.Text = "le5424a";
                  txtPassBZA.Text = "diego@27";
                  txtUserBZA.Text = "LE8821A";
                  txtPassBZA.Text = "FMC@2016";
                  txtUserBZA.Text = "LD7454A";
                  txtPassBZA.Text = "FMC#1234";
                  txtUserBZA.Text = "LE8141A";
                  txtPassBZA.Text = "FMC#@016";
                  txtUserBZA.Text = "LE1870A";
                  txtPassBZA.Text = "FMC@@123";*/


                if (VerificarDadosLogin())
                {
                    if (LoginWCC() && LoginBZA())//&& LoginGlobestar())
                    {
                        Classes.DistribuicaoDesk bll = new Classes.DistribuicaoDesk();
                        tabMainFrame.SelectTab(0);
                        wccAccess.AcessSystem("FBOA");
                        //bll.DistributeDesk(this.wccAccess, this.bzaAccess, this.globestarAccess, this.tabMainFrame, this.pbrContas, lblRegistros, lblTotal, dgvErros);
                        bll.DistributeDesk(this.wccAccess, this.bzaAccess, this.tabMainFrame, this.pbrContas, lblRegistros, lblTotal, dgvErros);

                    }
                    else
                    {
                        MessageBox.Show("Todos os mainframes devem estar logados.");
                    }
                }
                else
                {
                    MessageBox.Show("São obrigatórios todos os campos para login nos mainframes.");
                }
            }
            catch (Exception ex)
            {
                lblErro.Text = "OCORREU UM ERRO " + Environment.NewLine;
                string erro = string.Empty;
                do
                {
                    erro = ex.Message;
                    ex = ex.InnerException;
                } while (ex != null);
                lblErro.Text += erro;
            }
            finally
            {
                this.CloseSystems();
            }
        }

        private bool VerificarDadosLogin()
        {
            return (!string.IsNullOrEmpty(txtUserWCC.Text)) && (!string.IsNullOrEmpty(txtPassWCC.Text)) &&
                   (!string.IsNullOrEmpty(txtUserBZA.Text)) && (!string.IsNullOrEmpty(txtPassBZA.Text));/*&&
                   (!string.IsNullOrEmpty(txtUserGlobestar.Text)) && (!string.IsNullOrEmpty(txtPassGlobestar.Text));*/
        }

        private bool LoginWCC()
        {
            tabMainFrame.SelectTab(0);
            if (axAtmWCC.Session == null)
                axAtmWCC.Session = (IAtmSession)wccAccess.StartSession();

            FMC.Systems.Class.Enumerators.ErrorLogin validating = wccAccess.Login(wccAccess.WCC, FMC.Systems.Class.Enumerators.System.WCC,
                    new LoginSystemInfo() { ckFBQA = false, strUser = txtUserWCC.Text.Trim(), strPass = txtPassWCC.Text.Trim() });

            return MessageLogin.MessageValidate(validating);
        }

        private bool LoginBZA()
        {
            tabMainFrame.SelectTab(1);
            if (axAtmBZA.Session == null)
                axAtmBZA.Session = (IAtmSession)bzaAccess.StartSession();
            FMC.Systems.Class.Enumerators.ErrorLogin validating = bzaAccess.Login(bzaAccess.BZA, FMC.Systems.Class.Enumerators.System.BZA,
                    new LoginSystemInfo() { strUser = txtUserBZA.Text.Trim(), strPass = txtPassBZA.Text.Trim() });

            return MessageLogin.MessageValidate(validating);
        }
        /*private bool LoginGlobestar()
        {
            tabMainFrame.SelectTab(2);
            axAtmGlobestar.Session = (IAtmSession)globestarAccess.StartSession();
            FMC.Systems.Class.Enumerators.ErrorLogin validating = globestarAccess.Login(globestarAccess.Globestar, FMC.Systems.Class.Enumerators.System.Globestar,
                    new LoginSystemInfo() { strUser = txtUserGlobestar.Text.Trim(), strPass = txtPassGlobestar.Text.Trim() });

            return MessageLogin.MessageValidate(validating);
        } */

        private void CloseSystems()
        {
            /* Close WCC */
            tabMainFrame.SelectTab(0);
            wccAccess.SystemClose();
            /* Close BZA */
            tabMainFrame.SelectTab(1);
            bzaAccess.SystemClose();
            /*Close Globestar*/
            //tabMainFrame.SelectTab(2);
            //globestarAccess.SystemClose();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseSystems();
        }
    }
}
