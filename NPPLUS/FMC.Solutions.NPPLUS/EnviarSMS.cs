using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;




using FMC.Solutions.NPPLUS.Library.Class;
using FMC.Solutions.NPPLUS.Library.REST;
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
    public partial class EnviarSMS : XtraForm
    {
        ICollection<string> listaTelefone;

        PersonResponse PersonResponse;
        UserAuthenticate UserAuthenticate;
        string CodBillet;
        DateTime DtVencimento;
        int Parcela;
        int IdProductType;
        long IdProduct;

        public EnviarSMS(PersonResponse person, UserAuthenticate user, long idProduct, string codBillet, DateTime dtVencimento, int parcela, int idProductType)
        {
            this.IdProduct = idProduct;
            this.PersonResponse = person;
            this.UserAuthenticate = user;
            this.DtVencimento = dtVencimento;
            this.Parcela = parcela;
            this.CodBillet = codBillet;
            this.IdProductType = idProductType;
            InitializeComponent();
        }

        ~EnviarSMS()
        {
        }


        private void EnviarSMS_Load(object sender, EventArgs e)
        {
            try
            {
                Splash.Visible(splashScreenManager1, true);
                splashScreenManager1.SetWaitFormDescription("Buscando telefones cadastrados...");
                this.listaTelefone =
                    PersonResponse.Phones.Select
                        (
                            p => p.NrPhone.Length >= 11 ? long.Parse(p.NrPhone).ToString(@"(00) 00000-0000") :
                                (p.NrPhone.Length >= 10 ? long.Parse(p.NrPhone).ToString(@"(00) 0000-0000") : "")
                        ).Distinct().ToList();
                LoadPhone();
                Splash.Visible(splashScreenManager1, false);
            }
            catch (Exception ex) { }

        }


        private void LoadPhone()
        {
            try
            {
                this.ckListBox.Items.Clear();
                ckListBox.DataSource = listaTelefone;
                Application.DoEvents();
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

            this.ControlBox = false;
            IList<ListaTelefone> phoneSend = new List<ListaTelefone>();



            if (ckListBox.CheckedItems.Count > 0)
            {
                string titulo = "Enviar SMS";
                string destaque = "Tem certeza que deseja enviar este SMS?";
                string mensagem = "Ao confirmar, a linha digitável será encaminhada para os números selecionados.";
                bool big = true;
                MessageBox alert = new MessageBox(titulo, destaque, mensagem, big);
                DialogResult msg = alert.ShowDialog();
                if (msg == DialogResult.OK)
                {
                    try
                    {
                        foreach (string item in ckListBox.CheckedItems)
                        {
                            try
                            {
                                var sendSMS = new SendSMSRequest()
                                {
                                    idProduct = this.IdProduct,
                                    codBillet = this.CodBillet,
                                    cpf = this.PersonResponse.CPF,
                                    dtPayment = this.DtVencimento,
                                    phone = item.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", ""),
                                    idUserLogin = this.UserAuthenticate.IdUser,
                                    parcel = this.Parcela
                                };

                                FisAPI.SendBilletSMS(sendSMS, IdProductType, this.UserAuthenticate.OAuth.access_token);

                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(" Falha no envio do sms!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        XtraMessageBox.Show("Linha digitavel enviada!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.DialogResult = DialogResult.OK;
                        this.Close();

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
                        XtraMessageBox.Show(" Falha no envio da linha digitavel.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                XtraMessageBox.Show("Selecione pelo menos 1 telefone!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.DialogResult = DialogResult.None;
                this.ControlBox = true;
            }

        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (txtSMS.Text != null && Util.IsTelefone(txtSMS.Text.Trim()))
            {
                listaTelefone.Add(txtSMS.Text);

                LoadPhone();

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

            ICollection<ListaTelefone> listaPhone = new HashSet<ListaTelefone>();
            foreach (var p in lead.Product.Account.Phone.ToList())
            {

                string phone = p.NrPhone;
                if (phone.Length >= 11)
                    phone = string.Format("{0:(00) 00000-0000}", phone);
                else if (phone.Length >= 10)
                    phone = string.Format("{0:(00) 0000-0000}", phone);

                if (phone.Length >= 10)
                {
                    listaPhone.Add(
                    new ListaTelefone
                    {
                        IdTelefone = p.IdPhone.ToString(),
                        Telefone = phone
                    });
                }
            }

            ckListBox.DataSource = listaPhone;
            ckListBox.DisplayMember = "Telefone";
            ckListBox.ValueMember = "IdTelefone";

            */
        }

        private void txtTelefone_KeyUp(object sender, KeyEventArgs e)
        {
            btnAdicionar.Enabled = Util.IsTelefone(txtSMS.Text);
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
    public class ListaTelefone
    {
        public string Telefone { get; set; }
        public string IdTelefone { get; set; }
    }
}