using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using FMC.Solutions.NPPLUS.Library.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FMC.Solutions.NPPLUS
{
    public partial class Configuracoes : XtraForm
    {
        private Thread _backgroundWorkerThread;
        BoletoGrid boletoGrid;
        //Lead lead;
        //User user;
        Main main;

        public Configuracoes()
        {
            InitializeComponent();
        }



        private void Configuracoes_Load(object sender, EventArgs e)
        {
            /*
            this.main = (Main)this.Owner;
            user = main.user;
            if (user.UserProfileScreen == null || (user.UserProfileScreen.Count > 0 && user.UserProfileScreen.FirstOrDefault().FlEstatementDetail))
                tsExtratoDetalhado.IsOn = true;

            if (user.UserProfileScreen == null || (user.UserProfileScreen.Count > 0 && user.UserProfileScreen.FirstOrDefault().FlOccurrence))
                tsOcorrenciaSimplificada.IsOn = true;
                */
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            string titulo = "Configurações";
            string destaque = "Deseja salvar estas novas configurações?";
            string mensagem = "Ao salvar, o carregamento de contas será afetado.";
            bool big = false;
            MessageBox alert = new MessageBox(titulo, destaque, mensagem, big);
            DialogResult msg = alert.ShowDialog();
            if (msg == DialogResult.OK)
            {
                try
                {
                    /*
                    UserBLL userBLL = new UserBLL();
                    var usuario = userBLL.GetBykey(user.IdUser);
                    var profile = usuario.UserProfileScreen.FirstOrDefault();
                    if (profile != null)
                    {
                        profile.FlEstatementDetail = tsExtratoDetalhado.IsOn;
                        profile.FlOccurrence = tsOcorrenciaSimplificada.IsOn;
                        profile.FlOccurrenceDetail = false;
                        profile.DtUpdate = DateTime.Now;
                        userBLL.Update(usuario);
                    }
                    else
                    {
                        usuario.UserProfileScreen.Add(new UserProfileScreen
                        {
                            FlEstatementDetail = tsExtratoDetalhado.IsOn,
                            FlOccurrence = tsOcorrenciaSimplificada.IsOn,
                            FlOccurrenceDetail = false,
                            Theme = "The Bezier",
                            DtUpdate = DateTime.Now
                        });
                        userBLL.Update(usuario);
                    }
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    */
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    while (ex.InnerException != null)
                    {
                        error += ex.Message + Environment.NewLine;
                        ex = ex.InnerException;
                    }
                    Log.SaveFile(error);
                    XtraMessageBox.Show(" Falha ao salvar as configurações. Tente novamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    this.DialogResult = DialogResult.None;
                    this.ControlBox = true;
                }

            }
            else
            {
                this.DialogResult = DialogResult.None;
                this.ControlBox = true;
            }
        }
    }
}