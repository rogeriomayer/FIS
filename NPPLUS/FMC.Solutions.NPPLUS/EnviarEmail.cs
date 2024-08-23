using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;




using FMC.Solutions.NPPLUS.Library.Class;
using FMC.Solutions.NPPLUS.Library.REST;
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
using System.Threading;
using System.Windows.Forms;

namespace FMC.Solutions.NPPLUS
{
    public partial class EnviarEmail : XtraForm
    {
        ICollection<string> listaEmail;

        PersonResponse PersonResponse;
        UserAuthenticate UserAuthenticate;
        string CodBillet;
        DateTime DtVencimento;
        int Parcela;
        long IdProduct;

        public EnviarEmail(PersonResponse person, UserAuthenticate user, long idProduct, string codBillet, DateTime dtVencimento, int parcela)
        {
            this.IdProduct = idProduct;
            this.PersonResponse = person;
            this.UserAuthenticate = user;
            this.DtVencimento = dtVencimento;
            this.Parcela = parcela;
            this.CodBillet = codBillet;
            InitializeComponent();
        }


        ~EnviarEmail()
        {
        }


        private void EnviarEmail_Load(object sender, EventArgs e)
        {
            try
            {
                Splash.Visible(splashScreenManager1, true);
                splashScreenManager1.SetWaitFormDescription("Buscando e-mails cadastrados...");
                listaEmail = new Collection<string>();

                if (!string.IsNullOrEmpty(PersonResponse.Email))
                    listaEmail.Add(PersonResponse.Email);

                LoadEmails();
                Splash.Visible(splashScreenManager1, false);
            }
            catch (Exception ex)
            {

            }
        }

        private void LoadEmails()
        {
            try
            {
                this.ckListBox.Items.Clear();

                ckListBox.DataSource = listaEmail;

            }
            catch (Exception ex)
            {

            }
        }

        private void DisableButtons()
        {
            btnConfirmar.Enabled = false;
        }


        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (ckListBox.CheckedItems.Count > 0)
            {
                string titulo = "Enviar Boleto Email";
                string destaque = "Tem certeza que deseja enviar este boleto por e-mail?";
                string mensagem = "Ao confirmar, o boleto será encaminhada para os e-mails selecionados.";
                MessageBox alert = new MessageBox(titulo, destaque, mensagem, true);
                DialogResult msg = alert.ShowDialog();
                if (msg == DialogResult.OK)
                {
                    try
                    {
                        foreach (string item in ckListBox.CheckedItems)
                        {
                            try
                            {
                                var sendEmail = new SendEmailRequest()
                                {
                                    idProduct =  this.IdProduct,
                                    codBillet = this.CodBillet,
                                    cpf = this.PersonResponse.CPF,
                                    dtPayment = this.DtVencimento,
                                    email = item,
                                    idUserLogin = this.UserAuthenticate.IdUser,
                                    parcel = this.Parcela
                                };

                                FisAPI.SendBilletEmail(sendEmail, ((Main)this.Owner).productSelected.IdProductType, this.UserAuthenticate.OAuth.access_token);
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(" Falha no envio de boleto.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        XtraMessageBox.Show("Boleto enviado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.DialogResult = DialogResult.OK;
                        this.Close();

                    }
                    catch (Exception ex)
                    {
                        string error = ex.Message + " " + ex.StackTrace;
                        while (ex.InnerException != null)
                        {
                            error += ex.Message + " " + ex.StackTrace + Environment.NewLine;
                            ex = ex.InnerException;
                        }
                        Log.SaveFile(error);
                        XtraMessageBox.Show(" Falha no envio de boleto.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
            else
            {
                XtraMessageBox.Show("Selecione pelo menos 1 e-mail!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.DialogResult = DialogResult.None;
                this.ControlBox = true;
            }

        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text != null && Util.IsEmail(txtEmail.Text.Trim()))
            {
                listaEmail.Add(txtEmail.Text);

                LoadEmails();

                Application.DoEvents();
            }
            else
            {
                Splash.Visible(splashScreenManager1, false);
                XtraMessageBox.Show("É obrigatorio informar um e-mail válido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadCheck(long idProduct)
        {
            /*
            Product Product = new ProductBLL().GetBykey(idProduct);
            this.ckListBox.Items.Clear();
            //this.ckListBox.DisplayMember = "Text";
            //this.ckListBox.ValueMember = "Value";

            ckListBox.DataSource = lead.Product.Account.Email.Where(p => !string.IsNullOrEmpty(p.DsEmail.Trim())).ToList();
            ckListBox.DisplayMember = "DsEmail";
            ckListBox.ValueMember = "IdEmail";

            */
        }



        private void txtEmail_KeyUp(object sender, KeyEventArgs e)
        {
            btnAdicionar.Enabled = Util.IsEmail(txtEmail.Text);
        }

        private void ckListBox_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (ckListBox.CheckedItems.Count > 0)
            {
                btnConfirmar.Enabled = true;
            }
            else
            {
                btnConfirmar.Enabled = false;
            }
        }
    }
}