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
using System.Globalization;
using FMC.Solutions.NPPLUS.Library.Class;
using DevExpress.XtraGrid.Views.Grid;
using static FMC.Solutions.NPPLUS.Library.Class.Model.DetailPayment;
using FMC.Solutions.NPPLUS.Library.REST;
using FMC.Solutions.NPPLUS.Library.REST.Models;

namespace FMC.Solutions.NPPLUS
{
    public partial class Acordo : DevExpress.XtraEditors.XtraForm
    {

        private ICollection<Discount> listDiscount;
        private DialogResult msg;
        private AgreementSimulateRequest agreementSimulateRequest;

        public Acordo()
        {

            InitializeComponent();
        }

        private void Acordo_Load(object sender, EventArgs e)
        {
            Splash.Visible(splashScreenManager1, true);

            try
            {
                agreementSimulateRequest = new AgreementSimulateRequest();

                lblDiasAtraso.Text = ((Main)this.Owner).productSelected.DiasAtraso.ToString();
                lblSaldo.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", AgreementSimulateResponse.VlFull);
                lblTaxaJuro.Text = AgreementSimulateResponse.PctInterest.ToString("N2");
                txtDataEntrada.Properties.MinValue = DateTime.Today;
                txtDataEntrada.Properties.MaxValue = DateTime.Today.AddDays(4);
                txtDataEntrada.Text = DateTime.Today.ToString("dd/MM/yyyy");

                listDiscount = FisAPI.GetDiscount(((Main)this.Owner).productSelected.IdProductType, ((Main)this.Owner).User.OAuth.access_token);
                this.toolTip.SetToolTip(this.infoDesconto, GetDesconto());

                if (listDiscount != null && listDiscount.Count() > 0)
                {
                    int age = ((Main)this.Owner).productSelected.DiasAtraso;
                    Discount discount = listDiscount.Where(p => age >= p.MinAge && age <= p.MaxAge).OrderByDescending(p => p.MaxDiscount).FirstOrDefault();
                    txtDesconto.Properties.MinValue = 0;
                    if (discount != null)
                        txtDesconto.Properties.MaxValue = discount.MaxDiscount;
                    else
                        txtDesconto.Properties.MaxValue = 0;
                }

                if (!Permission.Acordo.Simular)
                    btnCalcular.Enabled = false;

                if (!Permission.Acordo.Finalizar)
                    btnConfirmar.Enabled = false;

                PopulaBoxParcelas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Splash.Visible(splashScreenManager1, false);
            }

        }

        private AgreementSimulateResponse agreementSimulateResponse;


        private AgreementSimulateResponse AgreementSimulateResponse
        {
            get
            {
                var dtEntrada = string.IsNullOrEmpty(txtDataEntrada.Text) ? DateTime.Today : txtDataEntrada.DateTime;
                var vlEntrada = string.IsNullOrEmpty(txtValorEntrada.Text) ? 0 : Convert.ToDecimal(txtValorEntrada.Text);
                var pctDesconto = string.IsNullOrEmpty(txtDesconto.Text) ? 0 : Convert.ToInt32(txtDesconto.Text);

                if (agreementSimulateRequest.DtEntrace != dtEntrada || agreementSimulateRequest.VlEntrace != vlEntrada || agreementSimulateRequest.PctDiscount != pctDesconto)
                {
                    agreementSimulateRequest.CPF = ((Main)this.Owner).productSelected.CPF;
                    agreementSimulateRequest.Product = ((Main)this.Owner).productSelected.Account;
                    agreementSimulateRequest.DtEntrace = dtEntrada;
                    agreementSimulateRequest.CdSimulate = "";
                    agreementSimulateRequest.VlEntrace = vlEntrada;
                    agreementSimulateRequest.PctDiscount = pctDesconto;
                    agreementSimulateRequest.Age = ((Main)this.Owner).productSelected.DiasAtraso;

                    agreementSimulateResponse = FisAPI.AgreementSimulate(agreementSimulateRequest, ((Main)this.Owner).productSelected.IdProductType, ((Main)this.Owner).User.OAuth.access_token);
                    agreementSimulateRequest.CdSimulate = agreementSimulateResponse.CdSimulate;
                }

                return agreementSimulateResponse;
            }
            set
            {
                agreementSimulateResponse = value;
            }
        }

        private string GetDesconto()
        {
            string r = "";

            if (listDiscount.Count > 0)
            {

                r += "<b>Mín. Parcelas</b><nbsp><nbsp><nbsp><nbsp>";
                r += "<b>Máx. Parcelas</b><nbsp><nbsp><nbsp><nbsp>";
                r += "<b>Máx. Desconto</b><br><br>";

                foreach (Discount discount in listDiscount)
                {
                    if (((Main)this.Owner).productSelected.DiasAtraso >= discount.MinAge && ((Main)this.Owner).productSelected.DiasAtraso <= discount.MaxAge)
                    {
                        r += "<nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp>" + discount.MinParcel.ToString().PadLeft(2, '0');
                        r += "<nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp>" + discount.MaxParcel.ToString("").PadLeft(2, '0').Replace("1000", "+<nbsp><nbsp>");
                        r += "<nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp><nbsp>" + (discount.MaxDiscount.ToString().PadLeft(2, '0')) + "%<br>";
                    }
                }
            }
            return r;
        }


        private void btnCancelAcordo_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnCalcular_Click(object sender, EventArgs e)
        {
            Splash.Visible(splashScreenManager1, true);
            try
            {
                btnCalcular.Enabled = false;
                btnConfirmar.Enabled = false;
                LimpaBoxParcelas();


                string msgAlert = string.Empty;

                // msgAlert = "• A <b>Data de Entrada</b> está incorreta, por este motivo ela foi corrigida.\n";//Mensagem
                if (!string.IsNullOrEmpty(msgAlert))
                {
                    XtraMessageBox.AllowHtmlText = true;
                    XtraMessageBox.Show(msgAlert, "Atenção - Correção Realizada na Simulação", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                //Validar desconto
                //Validar data entrada
                //Systems.P2.Screens.AgreementSimulate simulate = null;
                //if (simulate != null)
                PopulaBoxParcelas();

                if (Permission.Acordo.Simular)
                    btnCalcular.Enabled = true;
            }
            catch (Exception ex)
            {
                Splash.Visible(splashScreenManager1, false);
                Log.SaveFile(ex);
                XtraMessageBox.Show("Falha ao simular o acordo.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                Splash.Visible(splashScreenManager1, false);
            }
        }

        private void PopulaBoxParcelas()
        {
            gcParcelas.DataSource = AgreementSimulateResponse.ParcelResponse.Select(
                p =>
                    new Parcela
                    {
                        QtdParcela = p.NrParcel,
                        Entrada = p.ValueEntrace.ToString("N2"),
                        Valor = p.VlParcel.ToString("N2"),
                        Total = p.VlFull.ToString("N2"),
                        Desconto = p.VlDiscount.ToString("N2"),
                        CetAnual = p.PctYearCET.ToString("N2") + "%",
                        CetMensal = p.PctMonthCET.ToString("N2") + "%",
                        VlCetAnual = p.VlYearCET.ToString("N2"),
                        VlCetMensal = p.VlMonthCET.ToString("N2"),
                        DtParcela = p.DtParcel.ToString("dd/MM/yyyy"),
                        CdParcelPlan = p.CdParcel
                    }

                ).ToList();
            gcParcelas.RefreshDataSource();
        }

        /*
        private void PopulaBoxParcelas()
        {
            ICollection<Parcela> listaParcelas = new HashSet<Parcela>();
            foreach (var item in AgreementSimulateResponse.ParcelResponse)
            {
                decimal desconto = item.VlDiscount;
                if (desconto < 0) desconto = 0;

                decimal vlParcela = item.VlParcel;


                //DateTime dataVencimento = dataEntrada.Value;
                //if (!ckParcSemEnt.Checked && !ckAVista.Checked)
                //    dataVencimento = dataEntrada.Value.AddMonths(1);

                //if (dataEntrada.Value < dataEntradaAVista && (ckAVista.Checked || ckParcSemEnt.Checked))
                //    dataVencimento = dataEntradaAVista;
                //else
                //    dataVencimento = arss.FirstInstallDate;

                //decimal vlFinanciado = arss.AgreementBalance - (arss.AgreementBalance * (percentualDesconto / 100)) - valorEntrada;
                //int prazo = item.NBR;

                //var cetAnual = CET.CalcularCet(Convert.ToDouble(vlFinanciado), Convert.ToDouble(vlParcela), prazo, DateTime.Today, dataVencimento);
                //var cetMensal = Math.Pow(1 + (cetAnual / 100), 1 / 12d) - 1;
                //var vlCetAnual = Convert.ToDecimal(item.NBR * (double)item.InstallmentAmount * cetAnual) / 100;
                //var vlCetMensal = Convert.ToDecimal((item.NBR * (double)item.InstallmentAmount) * (cetMensal));

                listaParcelas.Add(
                    new Parcela
                    {
                        QtdParcela = item.NBR,
                        Valor = item.InstallmentAmount.ToString("N2"),
                        Total = (item.NBR * item.InstallmentAmount).ToString("N2"),
                        Desconto = desconto.ToString("N2"),
                        TotalGeral = (valorEntrada + (item.NBR * item.InstallmentAmount)).ToString("N2"),
                        CetAnual = cetAnual.ToString("N2") + "%",
                        CetMensal = (cetMensal * 100).ToString("N2") + "%",
                        VlCetAnual = vlCetAnual.ToString("N2"),
                        VlCetMensal = vlCetMensal.ToString("N2"),
                        DtParcela = arss.FirstInstallDate.ToString("dd/MM/yyyy")
                    });
                if (ckAVista.Checked)
                    break;
            }
            gcParcelas.DataSource = listaParcelas;
            gcParcelas.RefreshDataSource();
        }
        */

        private void LimpaBoxParcelas()
        {
            gcParcelas.DataSource = null;
            gcParcelas.RefreshDataSource();
            btnConfirmar.Enabled = false;
        }

        private class Parcela
        {
            public int QtdParcela { get; set; }
            public string Entrada { get; set; }
            public string Valor { get; set; }
            public string Total { get; set; }
            public string Desconto { get; set; }
            public string TotalGeral { get; set; }
            public string CetMensal { get; set; }
            public string CetAnual { get; set; }
            public string VlCetMensal { get; set; }
            public string VlCetAnual { get; set; }
            public string DtParcela { get; set; }
            public string CdParcelPlan { get; set; }
        }

        private void txtDataEntrada_DisableCalendarDate(object sender, DevExpress.XtraEditors.Calendar.DisableCalendarDateEventArgs e)
        {
            if (e.Date.DayOfWeek == DayOfWeek.Sunday) e.IsDisabled = true;
            //if (e.Date.DayOfWeek == DayOfWeek.Saturday) e.IsDisabled = true;
            if (e.Date < DateTime.Today) e.IsDisabled = true;
            if (e.Date > DateTime.Today.AddDays(5)) e.IsDisabled = true;
            //if (e.Date.Day == 29 || e.Date.Day == 30 || e.Date.Day == 31) { e.IsDisabled = true; }
        }

        private void gcParcelas_Click(object sender, EventArgs e)
        {
            if (gcParcelas.DataSource != null)
            {
                if (Permission.Acordo.Finalizar)
                    btnConfirmar.Enabled = true;
            }
        }

        private void txtDesconto_ValueChanged(object sender, EventArgs e)
        {
            var value = txtDesconto.Text;
        }

        private void txtDesconto_KeyUp(object sender, KeyEventArgs e)
        {
            decimal value = Convert.ToDecimal(txtDesconto.Text);
            int maxValue = Convert.ToInt32(txtDesconto.Properties.MaxValue);
            if (value > txtDesconto.Properties.MaxValue)
            {
                XtraMessageBox.Show("Percentual ultrapassa o máximo de desconto permitido, por este motivo foi corrigido para " + txtDesconto.Properties.MaxValue + ".", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDesconto.Text = maxValue.ToString();
            }
        }

        private void txtDesconto_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDataEntrada_DrawItem(object sender, DevExpress.XtraEditors.Calendar.CustomDrawDayNumberCellEventArgs e)
        {
            //e.Date
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {

                this.ControlBox = false;
                GridView gridView = gcParcelas.FocusedView as GridView;
                Parcela row = (Parcela)gridView.GetRow(gridView.FocusedRowHandle);

                string titulo = "Registrar Acordo";
                string destaque = "Tem certeza que deseja formalizar este ACORDO?";
                string mensagem = string.Empty;
                if (row.QtdParcela > 0)
                    mensagem = "Renegociação: Cliente informa pagamento parcelado no valor total de R$" + row.Total + " com entrada de R$" + row.Entrada + " para o dia " + Convert.ToDateTime(txtDataEntrada.Text).ToString("dd/MM") + " mais " + row.QtdParcela + " parcelas de R$ " + row.Valor + " com vencimento para todo dia " + Convert.ToDateTime(row.DtParcela).ToString("dd") + " . " + ((Main)this.Owner).User.DsName;
                else
                    mensagem = "Renegociação: Cliente informa pagamento a vista no valor total de R$" + row.Total + " para o dia " + Convert.ToDateTime(txtDataEntrada.Text).ToString("dd/MM") + " . " + ((Main)this.Owner).User.DsName;

                Roteiro roteiro = new Roteiro(titulo, destaque, mensagem);
                //MessageBox alert = new MessageBox(titulo, destaque, mensagem, big);
                msg = roteiro.ShowDialog();
                if (msg == DialogResult.OK)
                {
                    try
                    {
                        Splash.Visible(splashScreenManager1, true);
                        splashScreenManager1.SetWaitFormDescription("Registrando acordo...");

                        ((Main)this.Owner).WorkedProduct.Add(
                           new Library.Class.Model.WorkedProduct
                           {
                               Produto = ((Main)this.Owner).productSelected,
                               Tipo = Library.Class.Model.WorkedProduct.Type.Acordo,
                               CodFinalizacao = ((Main)this.Owner).ParametersResponse.Where(P => P.Key == "ACORDO_CODE").FirstOrDefault().Value,
                               IdStatus = Convert.ToInt32(((Main)this.Owner).ParametersResponse.Where(P => P.Key == "ACORDO_ID").FirstOrDefault().Value),
                               IdStatusDialer = "1000",
                               DsStatusResum = mensagem,
                               DetailPayment = new Library.Class.Model.DetailPayment
                               {
                                   TipoPagamento = row.QtdParcela == 0 ? TypePayment.AVista : (Convert.ToDecimal(row.Entrada) > 0 ? TypePayment.Parcelado : TypePayment.SemEntrada),
                                   DateEntrance = string.IsNullOrEmpty(txtDataEntrada.Text) ? DateTime.Now : Convert.ToDateTime(txtDataEntrada.Text),
                                   QtdParcel = row.QtdParcela,
                                   VlEntrance = Convert.ToDecimal(row.Entrada),
                                   VlParcel = Convert.ToDecimal(row.Valor)
                               },
                               StatusLead = new StatusLead()
                               {
                                   IdLead = ((Main)this.Owner).productSelected.IdLead,
                                   IdStatus = Convert.ToInt32(((Main)this.Owner).ParametersResponse.Where(P => P.Key == "ACORDO_ID").FirstOrDefault().Value),
                                   DsDescription = mensagem,
                                   DtInsert = DateTime.Now,
                                   IdUserLogin = ((Main)this.Owner).User.IdUser,
                                   Agreement = new HashSet<Agreement>()
                                   {
                                        CreateAgreement(row)
                                   }
                               }
                           }
                       );
                        Splash.Visible(splashScreenManager1, false);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        Splash.Visible(splashScreenManager1, false);
                        string erro = "Erro ao registrar acordo. " + Environment.NewLine + ex.Message;
                        XtraMessageBox.Show(erro, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            catch (Exception ex)
            {
                Splash.Visible(splashScreenManager1, false);
                Log.SaveFile("EXCEPTION => Form Acordo -> [btnConfirmar_Click]" + ex.Message);
                this.ControlBox = true;
            }
        }
        private Agreement CreateAgreement(Parcela row)
        {
            DateTime dtEntrada = string.IsNullOrEmpty(txtDataEntrada.Text) ? DateTime.Now : Convert.ToDateTime(txtDataEntrada.Text);
            var agrement = new Agreement()
            {
                QtParcel = row.QtdParcela,
                VlEntrace = Convert.ToDecimal(row.Entrada),
                DtEntrace = dtEntrada,
                VlParcel = Convert.ToDecimal(row.Valor),
                PcDiscount = Convert.ToDecimal(row.Desconto),
                VlAgreement = Convert.ToDecimal(row.Total),
                CdParcelPlan = row.CdParcelPlan,
                CdPaymentOption = agreementSimulateRequest.CdSimulate,
                DtInsert = DateTime.Now
            };

            return agrement;
        }

        private void txtDesconto_EditValueChanged(object sender, EventArgs e)
        {
            LimpaBoxParcelas();
        }

        private void txtDataEntrada_EditValueChanged(object sender, EventArgs e)
        {
            LimpaBoxParcelas();
        }

        private void txtValorEntrada_EditValueChanged(object sender, EventArgs e)
        {
            LimpaBoxParcelas();
        }

        private string RoteiroAVista(string card, decimal total, decimal desconto, Parcela pagto, decimal taxa, DateTime vencimento, string cetMensal, string cetAnual, string vlCetMensal, string vlCetAnual)
        {
            string mensagem = "Estou confirmando em sistema o acordo do seu cartão <b>" + card + "</b> com o valor total de <b>" + total.ToString("C2") + "</b>." + Environment.NewLine;
            if (desconto > 0)
                mensagem += Environment.NewLine + "Estamos concedendo um desconto de <b>" + desconto.ToString("N2") + "</b> por cento." + Environment.NewLine;
            mensagem += Environment.NewLine + "Ficou definido um pagamento à vista de <b>R$ " + pagto.TotalGeral + "</b> com o vencimento para o dia <b>" + vencimento.ToString("dd/MM/yyyy") + "</b>." + Environment.NewLine;
            if (taxa > 0)
                mensagem += Environment.NewLine + "*Para este acordo está inclusa a taxa de <b>" + taxa.ToString("N2") + "</b> por cento AO MÊS." + Environment.NewLine;

            mensagem += Environment.NewLine + "<b>Custo Efetivo Total</b>" + Environment.NewLine;
            mensagem += "<b>Mensal:</b> " + cetMensal + " <b>R$ " + vlCetMensal + "</b>" + Environment.NewLine;
            mensagem += "<b>Anual:</b> " + cetAnual + " <b>R$ " + vlCetAnual + "</b>" + Environment.NewLine;

            mensagem += Environment.NewLine + "APÓS A FORMALIZAÇÃO NÃO SERÁ POSSÍVEL CANCELAR" + Environment.NewLine;

            mensagem += Environment.NewLine + "Você me autoriza a confirmar o acordo nessas condições?" + Environment.NewLine;

            mensagem += Environment.NewLine + "<i>Um momento enquanto registro o acordo.</i>" + Environment.NewLine;

            return mensagem;
        }

        private string RoteiroAPrazo(string card, decimal total, decimal desconto, decimal entrada, Parcela pagto, decimal taxa, DateTime vencimento, string cetMensal, string cetAnual, string vlCetMensal, string vlCetAnual, bool semEntrada = false)
        {
            string mensagem = "Estou confirmando em sistema o acordo do seu cartão <b>" + card + "</b> com o valor total de <b>" + total.ToString("C2") + "</b>." + Environment.NewLine;
            if (desconto > 0)
                mensagem += Environment.NewLine + "Estamos concedendo um desconto de <b>" + desconto.ToString("N2") + "</b> por cento." + Environment.NewLine;

            mensagem += Environment.NewLine + "Ficou definido um acordo parcelado, ";

            if (!semEntrada)
                mensagem += "COM ENTRADA de <b> " + entrada.ToString("C2") + "</b> para o dia <b>" + vencimento.ToString("dd/MM/yyyy") + "</b> e mais <b>" + pagto.QtdParcela + "</b> parcelas de <b>R$ " + pagto.Valor + "</b> com o primeiro vencimento para o dia <b>" + pagto.DtParcela + "</b> e demais vencimentos todo dia <b>" + pagto.DtParcela.Substring(0, 2) + "</b>." + Environment.NewLine;
            else
                mensagem += "SEM ENTRADA e <b>" + pagto.QtdParcela + "</b> parcelas de <b>R$ " + pagto.Valor + "</b> com o primeiro vencimento para o dia <b>" + vencimento.ToString("dd/MM/yyyy") + "</b> e demais vencimentos todo dia <b>" + pagto.DtParcela.Substring(0, 2) + "</b>." + Environment.NewLine;

            if (taxa > 0)
                mensagem += Environment.NewLine + "*Para este acordo está inclusa a taxa de <b>" + taxa.ToString("N2") + "</b> por cento AO MÊS." + Environment.NewLine;

            mensagem += Environment.NewLine + "<b>Custo Efetivo Total</b>" + Environment.NewLine;
            mensagem += "<b>Mensal:</b> " + cetMensal + " <b>R$ " + vlCetMensal + "</b>" + Environment.NewLine;
            mensagem += "<b>Anual:</b> " + cetAnual + " <b>R$ " + vlCetAnual + "</b>" + Environment.NewLine;

            mensagem += Environment.NewLine + "APÓS A FORMALIZAÇÃO NÃO SERÁ POSSÍVEL CANCELAR" + Environment.NewLine;

            mensagem += Environment.NewLine + "Você me autoriza a confirmar o acordo nessas condições?" + Environment.NewLine;

            mensagem += Environment.NewLine + "<i>Aguarde um momento enquanto registro o acordo.</i>" + Environment.NewLine;

            return mensagem;
        }
    }


}