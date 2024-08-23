using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using FMC.Solutions.NPPLUS.Library.Class;
using FMC.Solutions.NPPLUS.Library.REST;
using FMC.Solutions.NPPLUS.Library.REST.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace FMC.Solutions.NPPLUS
{
    public partial class Concluir : XtraForm
    {
        private Thread _backgroundWorkerThread;
        UserAuthenticate User;
        CardResponse Card;
        StatusResponse selectedStatus;
        ICollection<StatusResponse> listStatus;


        public Concluir()
        {
            InitializeComponent();
        }

        public Concluir(CardResponse card, UserAuthenticate user)
        {
            this.Card = card;
            this.User = user;
            InitializeComponent();
        }


        private void Concluir_Load(object sender, EventArgs e)
        {
            try
            {
                progressBar.Hide();
                PopulaComboStatus();
                PopulaComboTelefone();
            }
            catch (Exception ex)
            {
                Log.SaveFile("EXCEPTION => [Concluir_Load]");
            }
            //progressBar.Show();
            //if (!backgroundWork.IsBusy)
            //    backgroundWork.RunWorkerAsync();

        }

        private void PopulaComboTelefone()
        {
            cbTelefone.Properties.Items.Clear();
            var listaTelefones = ((Main)this.Owner).Person.Phones.ToList();
            int count = 0;
            int principal = 0;
            foreach (var phone in listaTelefones.Where(p => p.Blacklist == false).ToList())
            {
                string p = phone.NrPhone;
                if (p.Length >= 11)
                    p = Convert.ToUInt64(Regex.Replace(p, "[^0-9]", "")).ToString("(00) 00000-0000");
                else if (p.Length >= 10)
                    p = Convert.ToUInt64(Regex.Replace(p, "[^0-9]", "")).ToString("(00) 0000-0000");

                cbTelefone.Properties.Items.Add(p);
                if (((Main)this.Owner).bilhete != null && !string.IsNullOrEmpty(((Main)this.Owner).bilhete.NumeroTelefone))
                {
                    if (phone.NrPhone.Contains(((Main)this.Owner).bilhete.NumeroTelefone))
                        principal = count;
                }
                else if (phone.IdPhoneStatus == 1)
                    principal = count;

                count++;

            }
            
                cbTelefone.SelectedIndex = principal;
        }

        private void PopulaComboStatus()
        {
            listStatus = FisAPI.GetStatus(Card.IdProductType, ((Main)this.Owner).User.OAuth.access_token);

            string tipoLigacao = ((Main)this.Owner).TipoLigacao;

            cbStatusDay.Properties.Items.Add("Selecione um tipo de finalização".ToUpper());
            foreach (var item in listStatus.Where(p => p.FlShowUser).ToList())
            {
                cbStatusDay.Properties.Items.Add(item.CdStatus.ToUpper() + " - " + item.DsStatus);
            }
            cbStatusDay.SelectedIndex = 0;

        }


        private void Concluir_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void DisableButtons()
        {
            btnConfirmar.Enabled = false;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            this.ControlBox = false;

            string mails = string.Empty;

            if (string.IsNullOrEmpty(cbTelefone.Text) || string.IsNullOrWhiteSpace(cbTelefone.Text))
            {
                XtraMessageBox.Show("Selecione o telefone de atendimento antes de Concluir!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.DialogResult = DialogResult.None;
                this.ControlBox = true;
                return;
            }

            if (selectedStatus.FlCallBack)
            {
                if (string.IsNullOrEmpty(txtDataVlc.Text) || string.IsNullOrEmpty(txtTelefoneVlc.Text))
                {
                    XtraMessageBox.Show("Os campos precisam estar preenchidos para Concluir!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.DialogResult = DialogResult.None;
                    this.ControlBox = true;
                    return;
                }
            }


            string titulo = "Finalização - " + selectedStatus.CdStatus;
            string destaque = "Deseja concluir o atendimento para esta conta?";
            string mensagem = "Ao confirmar, os dados serão incluídos no sistema. " +
                "<br><br><b>O FINALIZADOR escolhido por você para esta conta: <size=13><color=0, 128, 0>" + selectedStatus.DsStatus.ToUpper() + "</color></size></b>";
            bool big = true;
            MessageBox alert = new MessageBox(titulo, destaque, mensagem, big);
            DialogResult msg = alert.ShowDialog();
            if (msg == DialogResult.OK)
            {
                try
                {
                    Splash.Visible(splashScreenManager1, true);
                    splashScreenManager1.SetWaitFormDescription("Registrando ações...");

                    //Adiciona conta trabalhada
                    ((Main)this.Owner).WorkedProduct.Add
                    (

                        new Library.Class.Model.WorkedProduct
                        {
                            Produto = ((Main)this.Owner).productSelected,
                            Tipo = Library.Class.Model.WorkedProduct.Type.Outros,
                            CodFinalizacao = selectedStatus.CdStatus.Trim(),
                            IdStatus = selectedStatus.IdStatus,
                            IdStatusDialer = selectedStatus.IdStatusDialer,
                            DataRetorno = selectedStatus.FlCallBack ? Convert.ToDateTime(txtDataVlc.Text) : (DateTime?)null,
                            TelefoneRetorno = selectedStatus.FlCallBack ? txtTelefoneVlc.Text.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "") : "",
                            DsStatusResum = txtMemo.Text.Trim()
                        }

                    );

                    if (!((Main)this.Owner).FlCloseCall)
                    {

                        var statusLead = new StatusLead()
                        {
                            IdLead = Card.IdLead,
                            IdStatus = listStatus.Where(p => p.CdStatus == selectedStatus.CdStatus).FirstOrDefault().IdStatus,
                            DsDescription = txtMemo.Text.Trim(),
                            DtInsert = DateTime.Now,
                            IdUserLogin = ((Main)this.Owner).User.IdUser
                        };

                        if (selectedStatus.FlCallBack)
                        {
                            statusLead.CallBack.Add
                                (
                                    new CallBack()
                                    {
                                        NrPhone = txtTelefoneVlc.Text.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", ""),
                                        DtCallBack = txtDataVlc.DateTime,
                                        DtInsert = DateTime.Now
                                    }
                                );
                        }
                        string telefone = cbTelefone.Text.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
                        var result = FisAPI.PostStatusLead(statusLead, ((Main)this.Owner).productSelected.CPF, telefone, ((Main)this.Owner).User.OAuth.access_token);
                    }


                    Splash.Visible(splashScreenManager1, false);
                    Log.SaveFile("==== WORKEDPRODUCT INSERIDO ====");
                    this.Close();
                }
                catch (Exception ex)
                {
                    Splash.Visible(splashScreenManager1, false);


                    string error = Environment.NewLine + "===== "
                        + ((Main)this.Owner).productSelected.Account + " ====="
                        + Environment.NewLine
                        + ex.StackTrace
                        + Environment.NewLine
                        + " ERRO AO CONCLUIR "
                        + Environment.NewLine
                        + ex.Message;

                    while (ex.InnerException != null)
                    {
                        error += ex.StackTrace + Environment.NewLine + ex.Message + Environment.NewLine;
                        ex = ex.InnerException;
                    }

                    Log.SaveFile(error);
                    XtraMessageBox.Show("Falha ao concluir conta, tente novamente e informe seu supervisor.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    this.DialogResult = DialogResult.None;
                    this.ControlBox = true;
                    this.Cursor = Cursors.Default;
                    progressBar.Hide();
                }
            }
        }


        private void txtTelefoneVlc_Leave(object sender, EventArgs e)
        {
            string phone = txtTelefoneVlc.Text.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Replace("_", "");
            if (txtTelefoneVlc.Text.Replace("_", "").Length < 15)
            {
                if (txtTelefoneVlc.Properties.Mask.EditMask != "(00) 0000-00009")
                {
                    txtTelefoneVlc.Properties.Mask.EditMask = "(00) 0000-00009";
                    //txtTelefoneVlc.Text = phone;
                }
                if (phone.Length == 10)
                {
                    phone = "(" + phone.Substring(0, 2) + ") " + phone.Substring(2, 4) + "-" + phone.Substring(6, 4);
                    txtTelefoneVlc.Text = phone;
                }
            }
            else
            {
                if (txtTelefoneVlc.Properties.Mask.EditMask != "(00) 00000-0009")
                {
                    txtTelefoneVlc.Properties.Mask.EditMask = "(00) 00000-0009";
                    phone = "(" + phone.Substring(0, 2) + ") " + phone.Substring(2, 5) + "-" + phone.Substring(7, 4);
                    txtTelefoneVlc.Text = phone;
                }

            }
        }

        private void txtTelefoneVlc_KeyUp(object sender, KeyEventArgs e)
        {
            string phone = txtTelefoneVlc.Text.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Replace("_", "");
            if (txtTelefoneVlc.Text.Replace("_", "").Length < 15)
            {
                if (txtTelefoneVlc.Properties.Mask.EditMask != "(00) 0000-00009")
                {
                    txtTelefoneVlc.Properties.Mask.EditMask = "(00) 0000-00009";
                    //txtTelefoneVlc.Text = phone;
                }
                if (phone.Length == 10)
                {
                    phone = "(" + phone.Substring(0, 2) + ") " + phone.Substring(2, 4) + "-" + phone.Substring(6, 4);
                    txtTelefoneVlc.Text = phone;
                }
            }
            else
            {
                if (txtTelefoneVlc.Properties.Mask.EditMask != "(00) 00000-0009")
                {
                    txtTelefoneVlc.Properties.Mask.EditMask = "(00) 00000-0009";
                    phone = "(" + phone.Substring(0, 2) + ") " + phone.Substring(2, 5) + "-" + phone.Substring(7, 4);
                    txtTelefoneVlc.Text = phone;
                }

            }
        }

        private void cbStatusDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cbStatus = sender as ComboBoxEdit;

            txtMemo.Text = "";
            txtMemo.Enabled = false;

            if (cbStatus.SelectedIndex != -1 && cbStatus.SelectedIndex != 0)
            {
                btnConfirmar.Enabled = (!string.IsNullOrEmpty(cbTelefone.Text)) || (!string.IsNullOrWhiteSpace(cbTelefone.Text));

                string selectStatus = cbStatus.SelectedItem.ToString().Split('-').FirstOrDefault().Trim();
                selectedStatus = listStatus.Where(p => selectStatus.ToUpper() == p.CdStatus.ToUpper()).FirstOrDefault();
                if (selectedStatus != null)
                {
                    if (selectedStatus.FlCallBack)
                    {
                        gcVlc.Visible = true;
                        gcVlc.Text = "Dados para Voltar a Ligar";
                        txtDataVlc.Properties.Mask.EditMask = "dd/MM/yyyy HH:mm";
                        txtDataVlc.EditValue = DateTime.Now.AddHours(3);
                        txtTelefoneVlc.Text = ((Main)this.Owner).PhoneDiscado;
                        gcObs.Visible = false;

                    }
                    else
                    {
                        gcVlc.Visible = false;
                        gcVlc.Text = "Dados para Voltar a Ligar";
                        gcObs.Visible = true;
                        txtMemo.Enabled = true;
                    }
                }
            }
            else
            {
                btnConfirmar.Enabled = false;
            }
            Application.DoEvents();
        }

        private void cbTelefone_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cb = sender as ComboBoxEdit;

            if (cb.SelectedIndex != -1)
                btnConfirmar.Enabled = cbStatusDay.SelectedIndex != -1 && cbStatusDay.SelectedIndex != 0;
            else
                btnConfirmar.Enabled = false;
        }
    }
}