using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using FMC.Solutions.NPPLUS.Library.Class;
using FMC.Solutions.NPPLUS.Library.Class.Model;
using FMC.Solutions.NPPLUS.Library.REST;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace FMC.Solutions.NPPLUS
{
    public partial class Boleto : XtraForm
    {
        private Thread _backgroundWorkerThread;
        BoletoGrid BoletoGrid = new BoletoGrid();
        IList<BoletoGrid> BoletosGrid = new List<BoletoGrid>();

        public Boleto()
        {
            InitializeComponent();
        }

        private void Boleto_Load(object sender, EventArgs e)
        {
            try
            {
                progressBar.Hide();

                ClearFields();

                var lista = ((Main)this.Owner).WorkedProduct;
                if (lista != null && lista.Count > 0)
                    foreach (var item in lista)
                    {
                        if (((Main)this.Owner).productSelected.Account == item.Produto.Account)
                        {
                            try
                            {
                                txtValor.Text = item.DetailPayment.VlEntrance.ToString("C2");
                                txtDataVencimento.EditValue = item.DetailPayment.DateEntrance;
                                txtValor.Enabled = false;
                                txtDataVencimento.Enabled = false;
                            }
                            catch { }
                            break;
                        }
                    }
                else
                {
                    var product = ((Main)this.Owner).Person.Cards.Where(p => p.Account == ((Main)this.Owner).productSelected.Account).FirstOrDefault();
                    if (product.StatusLeadResponse.Where(p => p.AgreementResponse != null).Count() > 0)
                    {
                        var agreement = product.StatusLeadResponse.Where(p => p.AgreementResponse != null).FirstOrDefault().AgreementResponse;
                        var parcel = agreement.AgreementParcelResponse.Where(p => p.DtParcel >= DateTime.Today.AddDays(-2) && p.Status == "EmAberto").OrderBy(p => p.DtParcel).FirstOrDefault();
                        if (parcel != null)
                        {
                            txtValor.Text = parcel.VlParcel.ToString("C2");
                            txtDataVencimento.EditValue = parcel.DtParcel;
                        }

                        txtValor.Enabled = false;
                        txtDataVencimento.Enabled = false;
                    }
                    else if (product.StatusLeadResponse.Where(p => p.PromisseResponse != null).Count() > 0)
                    {
                        var promisse = product.StatusLeadResponse.Where(p => p.PromisseResponse != null).OrderByDescending(p => p.PromisseResponse.DtInsert).FirstOrDefault().PromisseResponse;
                        txtValor.Text = promisse.VlPromisse.ToString("C2");
                        txtDataVencimento.EditValue = promisse.DtPromisse;
                        txtValor.Enabled = false;
                        txtDataVencimento.Enabled = false;
                    }
                    else
                    {
                        txtValor.Text = product.VlFull.ToString("C2");
                        txtValor.Enabled = false;
                    }
                }
                Carregar();
                if (!Permission.Boleto.Adicionar)
                    btnGerar.Enabled = false;
                //backgroundWork.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                Splash.Visible(splashScreenManager1, false);
                XtraMessageBox.Show("Erro ao carregar os boletos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Log.SaveFile("ERRO => Boleto_Load -> " + ex.Message);
                //throw ex;
            }
        }

        public void Carregar()
        {
            Splash.Visible(splashScreenManager1, true);
            splashScreenManager1.SetWaitFormDescription("Buscando boletos emitidos...");
            GetBoletos();
            gridControl.DataSource = BoletosGrid;
            gridControl.RefreshDataSource();
            if (BoletosGrid != null && BoletosGrid.Count > 0)
            {
                splashScreenManager1.SetWaitFormDescription("Carregando boletos...");
                PopulaDetalhes(BoletosGrid.FirstOrDefault());
            }
            Splash.Visible(splashScreenManager1, false);
        }

        public void AbortBackgroundWorker()
        {
            if (_backgroundWorkerThread != null)
                _backgroundWorkerThread.Abort();
        }

        private void GetBoletos()
        {
            BoletosGrid = new List<BoletoGrid>();
            try
            {
                var boletos = FisAPI.GetBillets(((Main)this.Owner).productSelected.IdProduct, ((Main)this.Owner).User.OAuth.access_token);

                BoletosGrid = boletos.Select
                    (
                        p => new BoletoGrid()
                        {
                            IdBoleto = p.IdBillet,
                            Status = StatusBoleto(p.DtBillet),
                            DtGeracao = p.DtInsert.ToString("dd/MM/yyyy"),
                            DtVencimento = p.DtBillet.ToString("dd/MM/yyyy"),
                            Valor = p.VlBillet.ToString("N2"),
                            LinhaDigitavel = p.Line,
                            NroEnvioEmail = p.NrSendEmail,
                            NroEnvioSMS = p.NrSendSMS,
                            NossoNumero = p.DocumentNumber,
                            CdAgreementRecupera = p.CdAgreementRecupera,
                            Parcela = p.Parcel
                        }
                     ).OrderBy(p => p.DtVencimento).ToList();
            }
            catch (Exception ex)
            {
                Log.SaveFile("ERRO => GetBoletos -> " + ex.Message);
            }
        }

        private string StatusBoleto(DateTime dtVencimento)
        {
            CardResponse card = ((Main)this.Owner).Person.Cards.Where(p => p.Account == ((Main)this.Owner).productSelected.Account).FirstOrDefault();

            var agreements = card.StatusLeadResponse.Where(p => p.AgreementResponse != null).Select(p => p.AgreementResponse).ToList();

            var agreement = agreements.Where(p => p.CdAgreementRecupera == BoletoGrid.CdAgreementRecupera).OrderByDescending(p => p.DtInsert).FirstOrDefault();

            if (agreement != null && agreement.Status == "Cancelado")
                return "Cancelado";
            else if (dtVencimento < DateTime.Today)
                return "Vencido";
            else
                return "Disponível";


        }


        private DateTime GetDate(string date)
        {
            var array = date.Split('/');
            return new DateTime(Convert.ToInt32(array[2]), Convert.ToInt32(array[1]), Convert.ToInt32(array[0]));
        }

        private void ClearFields()
        {
            gridControl.DataSource = null;
            gridControl.RefreshDataSource();

            lblID.Text = "-";
            lblDataVencimento.Text = "-";
            lblValor.Text = "-";

            lblQtdEnvioEmail.Text = "-";
            lblQtdEnvioSMS.Text = "-";

            lblStatus.Text = "-";

            lblLinhaDigitavel.Text = "-";
            Application.DoEvents();
        }

        private void PopulaDetalhes(BoletoGrid detail)
        {
            if (detail != null)
            {
                lblID.Text = string.IsNullOrEmpty(detail.NossoNumero) ? "-" : detail.NossoNumero;
                lblDataVencimento.Text = string.IsNullOrEmpty(detail.DtVencimento) ? "-" : detail.DtVencimento;
                lblValor.Text = string.IsNullOrEmpty(detail.Valor) ? "-" : detail.Valor;

                lblQtdEnvioEmail.Text = string.IsNullOrEmpty(detail.NroEnvioEmail.ToString()) ? "-" : detail.NroEnvioEmail.ToString();
                lblQtdEnvioSMS.Text = string.IsNullOrEmpty(detail.NroEnvioSMS.ToString()) ? "-" : detail.NroEnvioSMS.ToString();

                lblStatus.Text = string.IsNullOrEmpty(detail.Status) ? "-" : detail.Status;

                lblLinhaDigitavel.Text = string.IsNullOrEmpty(detail.LinhaDigitavel) ? "-" : detail.LinhaDigitavel;
            }
        }

        private void Boleto_FormClosing(object sender, FormClosingEventArgs e)
        {
            AbortBackgroundWorker();
        }

        private void BindBoletos()
        {
            this.BoletosGrid = new List<BoletoGrid>();
            this.gridControl.DataSource = new List<BoletoGrid>();

            this.GetBoletos();
            this.gridControl.DataSource = BoletosGrid;
            Application.DoEvents();
        }

        private void ActionGrid()
        {
            DisableButtons();
            GridView gridView = gridControl.FocusedView as GridView;
            BoletoGrid row = (BoletoGrid)gridView.GetRow(gridView.FocusedRowHandle);
            if (row != null)
            {
                BoletoGrid = row;
                PopulaDetalhes(row);
                EnabledButtons(row);
            }
        }

        private void EnabledButtons(BoletoGrid row)
        {
            if (row != null)
            {
                bool statusBoleto = row.Status != "Cancelado" && row.Status != "Vencido";

                if (Permission.Boleto.Email)
                    btnEmail.Enabled = statusBoleto;

                if (Permission.Boleto.SMS)
                    btnSMS.Enabled = statusBoleto;

                if (Permission.Boleto.Download)
                {
                    btnDownload.Enabled = statusBoleto;

                    btnCopiarLinhaDigitavel.Enabled = statusBoleto;
                }
            }
        }

        private void gridControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                ActionGrid();
            }
        }

        private void gridControl_Click(object sender, EventArgs e)
        {
            ActionGrid();
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            string titulo = "Enviar Boleto Email";
            string destaque = "Tem certeza que deseja enviar este boleto para " + Environment.NewLine + "o e-mail " + ((Main)this.Owner).Person.Email + "?";
            string mensagem = "Caso deseje enviar para outro e-mail atualize o cadastro do cliente antes do envio!";
            MessageBox alert = new MessageBox(titulo, destaque, mensagem, true);
            DialogResult msg = alert.ShowDialog();
            if (msg == DialogResult.OK)
            {
                try
                {
                    var sendEmail = new SendEmailRequest()
                    {
                        idProduct = ((Main)this.Owner).productSelected.IdProduct,
                        codBillet = BoletoGrid.CdAgreementRecupera,
                        cpf = ((Main)this.Owner).Person.CPF,
                        dtPayment = Convert.ToDateTime(BoletoGrid.DtVencimento),
                        email = ((Main)this.Owner).Person.Email,
                        idUserLogin = ((Main)this.Owner).User.IdUser,
                        parcel = BoletoGrid.Parcela
                    };

                    FisAPI.SendBilletEmail(sendEmail, ((Main)this.Owner).productSelected.IdProductType, ((Main)this.Owner).User.OAuth.access_token);

                    XtraMessageBox.Show("Boleto enviado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ReloadBoletos();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(" Falha no envio de boleto.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            GridView gridView = gridControl.FocusedView as GridView;
            BoletoGrid row = (BoletoGrid)gridView.GetRow(gridView.FocusedRowHandle);

            try
            {
                var diretorio = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Boletos", DateTime.Today.ToString("dd-MM-yyyy"));
                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);

                var filename = System.IO.Path.Combine(diretorio, ((Main)this.Owner).productSelected.Account + ".pdf");
                var pdf = FisAPI.GetBilletPDF(((Main)this.Owner).Person.CPF, row.CdAgreementRecupera, Convert.ToDateTime(row.DtVencimento), ((Main)this.Owner).productSelected.IdProductType, ((Main)this.Owner).User.OAuth.access_token);

                if (pdf != null)
                {
                    var count = 1;
                    while (File.Exists(filename))
                    {
                        count++;
                        filename = System.IO.Path.Combine(diretorio, ((Main)this.Owner).productSelected.Account + count.ToString() + ".pdf");
                    }

                    System.IO.File.WriteAllBytes(filename, pdf);

                    System.Diagnostics.Process.Start(filename);
                    XtraMessageBox.Show("Download do boleto realizado com sucesso!" + Environment.NewLine + filename, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    XtraMessageBox.Show("Boleto não encontrado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch
            {
                XtraMessageBox.Show("Erro ao fazer o download do boleto", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void DisableButtons()
        {
            btnCopiarLinhaDigitavel.Enabled = false;
            btnEmail.Enabled = false;
            btnSMS.Enabled = false;
            btnDownload.Enabled = false;
        }

        private bool BoletoExistente(decimal valor, DateTime vencimento)
        {
            try
            {
                foreach (BoletoGrid boleto in BoletosGrid)
                {
                    DateTime venc = Convert.ToDateTime(boleto.DtVencimento.Substring(6, 4) + "-" + boleto.DtVencimento.Substring(3, 2) + "-" + boleto.DtVencimento.Substring(0, 2));
                    decimal val = Convert.ToDecimal(boleto.Valor.Trim().Replace(",", string.Empty).Replace(".", string.Empty).Replace("R$", string.Empty)) / 100;
                    if (venc == vencimento && val == valor)
                    {
                        return true;
                    }
                }
            }
            catch { }
            return false;
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDataVencimento.Text != null && txtValor.Text != null && txtValor.Text != "R$ 0,00")
                {
                    decimal val = Convert.ToDecimal(txtValor.Text.Trim().Replace(",", string.Empty).Replace(".", string.Empty).Replace("R$", string.Empty)) / 100;
                    DateTime venc = Convert.ToDateTime(txtDataVencimento.Text.Substring(6, 4) + "-" + txtDataVencimento.Text.Substring(3, 2) + "-" + txtDataVencimento.Text.Substring(0, 2));

                    if (venc < DateTime.Today)
                    {
                        string erro = "A <b>DATA DE VENCIMENTO</b> deve ser <b>IGUAL</b> ou <b>SUPERIOR</b> à data atual.";
                        XtraMessageBox.Show(erro, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information, DevExpress.Utils.DefaultBoolean.True);
                        this.DialogResult = DialogResult.None;
                    }
                    else if (BoletoExistente(val, venc))
                    {
                        string erro = "Já <b>EXISTE</b> um boleto com este <b>VALOR</b> e esta <b>DATA DE VENCIMENTO</b>.";
                        XtraMessageBox.Show(erro, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information, DevExpress.Utils.DefaultBoolean.True);
                        this.DialogResult = DialogResult.None;
                    }
                    else
                    {
                        string titulo = "Gerar Boleto";
                        string destaque = "Tem certeza que deseja gerar este BOLETO?";

                        MessageBox alert = new MessageBox(titulo, destaque, "Se concordar será gerado um novo boleto para o cliente!", true);
                        DialogResult msg = alert.ShowDialog();
                        if (msg == DialogResult.OK)
                        {
                            try
                            {
                                var product = ((Main)this.Owner).Person.Cards.Where(p => p.Account == ((Main)this.Owner).productSelected.Account).FirstOrDefault();
                                if (product.StatusLeadResponse.Where(p => p.AgreementResponse != null).Count() > 0)
                                {
                                    var agreement = product.StatusLeadResponse.Where(p => p.AgreementResponse != null).FirstOrDefault().AgreementResponse;
                                    var parcel = agreement.AgreementParcelResponse.Where(p => p.DtParcel >= DateTime.Today.AddDays(-2) && p.Status == "EmAberto").OrderBy(p => p.DtParcel).FirstOrDefault();
                                    if (parcel != null)
                                    {
                                        var newBilletRequest = new BilletRequest()
                                        {
                                            IdProduct = product.IdProduct,
                                            IdAgreement = agreement.IdAgreement,
                                            NrParcel = parcel.NrParcel,
                                            CdAgreementRecupera = agreement.CdAgreementRecupera,
                                            VlBillet = parcel.VlParcel,
                                            DtBillet = parcel.DtParcel,
                                            CPF = ((Main)this.Owner).Person.CPF
                                        };

                                        var billet = FisAPI.AddBillet(newBilletRequest, product.IdProductType, ((Main)this.Owner).User.OAuth.access_token);
                                        Carregar();
                                    }
                                    else
                                    {
                                        string erro = "Não existe parcelas em aberto para este acordo!";
                                        XtraMessageBox.Show(erro, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information, DevExpress.Utils.DefaultBoolean.True);
                                        this.DialogResult = DialogResult.None;
                                    }
                                }
                                else
                                {
                                    var newBilletRequest = new BilletRequest()
                                    {
                                        IdProduct = product.IdProduct,
                                        IdAgreement = 0,
                                        NrParcel = 0,
                                        CdAgreementRecupera = "",
                                        VlBillet = val,
                                        DtBillet = venc,
                                        CPF = ((Main)this.Owner).Person.CPF
                                    };

                                    var billet = FisAPI.AddBillet(newBilletRequest, product.IdProductType, ((Main)this.Owner).User.OAuth.access_token);
                                    Carregar();
                                }

                            }
                            catch (Exception ex)
                            {
                                Splash.Visible(splashScreenManager1, false);
                                string error = ex.Message;
                                while (ex.InnerException != null)
                                {
                                    ex = ex.InnerException;
                                    error += " -- " + ex.Message;
                                }
                                string erro = "Falha ao gerar boleto. Tente novamente." + Environment.NewLine + error;
                                XtraMessageBox.Show(erro, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.DialogResult = DialogResult.None;
                            }
                        }
                    }
                }
                else
                {
                    Splash.Visible(splashScreenManager1, false);
                    XtraMessageBox.Show("É obrigatorio preencher o <b>valor</b> e a <b>data do boleto</b>!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error, DevExpress.Utils.DefaultBoolean.True);
                    this.DialogResult = DialogResult.None;
                }

            }
            catch (Exception ex)
            {
                Splash.Visible(splashScreenManager1, false);
                XtraMessageBox.Show("Falha ao gerar boleto. Tente novamente ou chame o supervisor.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;
            }
        }

        /*
        private Billet.Billet GerarBoleto()
        {
            try
            {
                Splash.Visible(splashScreenManager1, true);
                splashScreenManager1.SetWaitFormDescription("Processando requisição...");
                Billet.Billet boleto = GetBoleto();
                ReloadBoletos();
                Splash.Visible(splashScreenManager1, false);
                return boleto;
            }
            catch (Exception ex)
            {
                Splash.Visible(splashScreenManager1, false);
                Log.SaveFile("Erro ao gerar novo boleto [GerarBoleto()] " + Environment.NewLine + ex.Message);
                throw ex;
                //XtraMessageBox.Show("Erro ao gerar novo boleto. " + Environment.NewLine + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        */

        private void btnCopiarLinhaDigitavel_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lblLinhaDigitavel.Text);
            XtraMessageBox.Show("Linha Digitável copiada com sucesso!", "Linha Digitável", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnSMS_Click(object sender, EventArgs e)
        {
            EnviarSMS enviarSms = new EnviarSMS(((Main)this.Owner).Person, ((Main)this.Owner).User, ((Main)this.Owner).productSelected.IdProduct, BoletoGrid.CdAgreementRecupera, Convert.ToDateTime(BoletoGrid.DtVencimento), BoletoGrid.Parcela, ((Main)this.Owner).productSelected.IdProductType);
            DialogResult drResult = enviarSms.ShowDialog();
            if (drResult == DialogResult.OK)
            {
                this.DialogResult = DialogResult.None;
                ReloadBoletos();
            }
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            string titulo = "Registrar Acordo";
            string destaque = "Tem certeza que deseja remover este BOLETO?";
            string mensagem = "Ao confirmar, o <b>BOLETO</b> e toda referência a ele será removido da base de boletos.";
            bool big = true;

            MessageBox alert = new MessageBox(titulo, destaque, mensagem, big);
            DialogResult msg = alert.ShowDialog();
            if (msg == DialogResult.OK)
            {
                GridView gridView = gridControl.FocusedView as GridView;
                BoletoGrid row = (BoletoGrid)gridView.GetRow(gridView.FocusedRowHandle);
                if (row != null)
                {
                    //new BoletoBLL().Delete(new Collection<long> { row.IdBoleto });
                    XtraMessageBox.Show("Boleto deletado com sucesso!", "Exclusão de boleto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    ReloadBoletos();
                }
            }
        }

        private void ReloadBoletos()
        {
            ClearFields();
            Carregar();
        }
    }

    public class ItemCheck
    {
        public long Value { get; set; }

        public string Text { get; set; }
    }

    public class BoletoGrid
    {
        public long IdBoleto { get; set; }

        public string Status { get; set; }

        public string DtGeracao { get; set; }

        public string DtVencimento { get; set; }

        public string Valor { get; set; }

        public string LinhaDigitavel { get; set; }

        public int NroEnvioEmail { get; set; }

        public int NroEnvioSMS { get; set; }

        public string NossoNumero { get; set; }

        public string CdAgreementRecupera { get; set; }

        public int Parcela { get; set; }
    }
}
