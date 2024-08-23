using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;


using FMC.Solutions.NPPLUS.Library.Class;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace FMC.Solutions.NPPLUS
{
    public partial class ExtratoAcordo : XtraForm
    {
        private readonly Main _main;
        private Thread _backgroundWorkerThread;
        //private ICollection<AgreementHistory> agreementHistory = null;

        public ExtratoAcordo()
        {
            InitializeComponent();
        }

        public ExtratoAcordo(Main main)
        {
            InitializeComponent();
            this._main = main;
        }

        ~ExtratoAcordo()
        {
            bwListaAcordo.Dispose();
        }


        private void ExtratoAcordo_Load(object sender, EventArgs e)
        {
            try
            {
                Log.SaveFile("**** EXTRATO ACORDO > [ExtratoAcordo_Load] ****");
                pbExtratoAcordos.Show();
                bwListaAcordo.RunWorkerAsync();

                gcAcordo.DataSource = Acordos();
                gcAcordo.RefreshDataSource();
                pbExtratoAcordos.Hide();
                //bwListaAcordo.CancelAsync();
            }
            catch (Exception ex)
            {
                Log.SaveFile("EXCEPTION => EXTRATO ACORDO > [ExtratoAcordo_Load] ****" + ex.Message);
            }

        }

        public void AbortBackgroundWorker()
        {
            if (_backgroundWorkerThread != null)
                _backgroundWorkerThread.Abort();
        }

        private IList<AgreementResponse> AgreementResponse;
        private IList<StatusLeadResponse> StatusLeadResponse;

        private ICollection<ListaAcordo> Acordos()
        {

            ICollection<ListaAcordo> listaAcordo = new HashSet<ListaAcordo>();
            try
            {

                AgreementResponse = _main.Person.Cards.Where(p => p.Account == _main.productSelected.Account).FirstOrDefault().StatusLeadResponse.Where(p => p.AgreementResponse != null).Select(p => p.AgreementResponse).ToList();
                StatusLeadResponse = _main.Person.Cards.Where(p => p.Account == _main.productSelected.Account).FirstOrDefault().StatusLeadResponse.ToList();

                listaAcordo = AgreementResponse.Select(
                    p => new ListaAcordo()
                    {
                        Acordo = p.IdAgreement.ToString(),
                        Data = p.DtInsert,
                        Situacao = p.Status,
                        Descricao = StatusLeadResponse.Where(s => s.IdStatusLead == p.IdStatusLead).FirstOrDefault().Description,
                        Operador = StatusLeadResponse.Where(s => s.IdStatusLead == p.IdStatusLead).FirstOrDefault().UserLogin
                    }).ToList();

            }
            catch (Exception ex)
            {
                Log.SaveFile("EXCEPTION => EXTRATO ACORDO > [Acordos()] ****" + ex.Message);
            }
            return listaAcordo.OrderByDescending(p => p.Acordo).ThenByDescending(p => p.Data).ToList();

        }

        private void ClearFields()
        {
            gcAcordo.DataSource = null;
            gcAcordo.RefreshDataSource();

            gcPagamentos.DataSource = null;
            gcPagamentos.RefreshDataSource();

            lblDescontoValor.Text = "-";
            lblJuroValor.Text = "-";
            lblDataPrimeiraParcela.Text = "-";
            lblQtdParcelas.Text = "-";
            lblVlParcela.Text = "-";
            lblDataEntrada.Text = "-";
            lblValorEntrada.Text = "-";
            lblValorFinal.Text = "-";
            lblParcelasPagas.Text = "-";
            lblParcelasAberto.Text = "-";
            lblTotalPago.Text = "-";

            Application.DoEvents();

        }

        private void bwListaAcordo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (bwListaAcordo.CancellationPending == true)
                {
                    e.Cancel = true;
                    bwListaAcordo.CancelAsync();
                    bwListaAcordo.Dispose();
                    GC.Collect();
                }
                else
                {
                    _backgroundWorkerThread = Thread.CurrentThread;
                    /*
                    if (_main.arxq != null)
                        _main.arxq.CarregaAcordos(_main.productSelected.Account);
                    */
                    bwListaAcordo.ReportProgress(0, "Clear");
                }
            }
            catch (ThreadAbortException exception)
            {

            }


        }

        private void bwListaAcordo_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            gcAcordo.DataSource = Acordos();
            gcAcordo.RefreshDataSource();
            pbExtratoAcordos.Hide();
        }

        private void bwListaAcordo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                // XtraMessageBox.Show("Cancelado.");
            }
            else if (!(e.Error == null))
            {
                XtraMessageBox.Show(e.Error.Message);
            }
            else
            {
                bwListaAcordo.CancelAsync();
            }
            pbExtratoAcordos.Hide();
            if (this.AgreementResponse.Count > 0)
                PopulaDetalhes(this.AgreementResponse.OrderByDescending(p => p.DtInsert).FirstOrDefault());
        }


        private void gcAcordo_Click(object sender, EventArgs e)
        {
            ActionGrid();
        }

        private void PopulaDetalhes(AgreementResponse acordo)
        {
            try
            {


                if (acordo != null)
                {
                    lblDescontoValor.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", acordo.PcDiscount);

                    lblJuroValor.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", acordo.VlInterest);

                    lblDataPrimeiraParcela.Text = "-";
                    lblVlParcela.Text = "-";

                    if (acordo.QtParcel > 0)
                    {
                        var primeiraParcela = acordo.QtParcel > 0 && acordo.AgreementParcelResponse != null && acordo.AgreementParcelResponse.Count > 1 ? acordo.AgreementParcelResponse.OrderByDescending(p => p.NrParcel).ToList()[1] : null;
                        if (primeiraParcela != null)
                        {
                            lblDataPrimeiraParcela.Text = primeiraParcela.DtParcel.ToString("dd/MM/yyyy");
                            lblVlParcela.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", primeiraParcela.VlParcel);
                        }
                    }

                    lblQtdParcelas.Text = acordo.QtParcel.ToString();

                    lblDataEntrada.Text = acordo.DtEntrace.ToString("dd/MM/yyyy");

                    //if (detail.NxtPymntDueDte.HasValue && detail.NxtPymntDueDte < DateTime.Today)
                    //{
                    //    TimeSpan total = Convert.ToDateTime(DateTime.Today) - Convert.ToDateTime(detail.NxtPymntDueDte);
                    //    lblDiasAtrasoPagamento.Text = total.Days.ToString();
                    //}
                    //else
                    //{
                    //    lblDiasAtrasoPagamento.Text = "-";
                    //}


                    lblValorEntrada.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", acordo.VlParcel);
                    lblValorFinal.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", acordo.VlAgreement);

                    gcPagamentos.DataSource = Pagamentos(acordo);
                    gcPagamentos.RefreshDataSource();

                }
            }
            catch (Exception ex)
            {
                Log.SaveFile("EXCEPTION => EXTRATO ACORDO > [PopulaDetalhes()] ****" + ex.Message);
            }
        }


        private ICollection<ListaPagamento> Pagamentos(AgreementResponse acordo)
        {
            ICollection<ListaPagamento> listaPagamentos = new HashSet<ListaPagamento>();
            //StatusAcordoBLL statusAcordoBll = new StatusAcordoBLL();

            lblParcelasPagas.Text = acordo.AgreementParcelResponse.Where(p => p.Status == "Liquidada").Count().ToString();
            lblParcelasAberto.Text = acordo.AgreementParcelResponse.Where(p => p.Status != "Liquidada").Count().ToString();
            decimal totalPago = acordo.AgreementParcelResponse.Where(p => p.Status == "Liquidada").Sum(p => p.VlParcel);
            lblTotalPago.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", totalPago);


            listaPagamentos = acordo.AgreementParcelResponse.Select(
               p => new ListaPagamento()
               {
                   Vencimento = p.DtParcel.ToString("dd/MM/yyyy"),
                   Valor = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", p.VlParcel),
                   Situacao = p.Status
               }).ToList();

            return listaPagamentos;
        }

        private string SituacaoPagto(string flag)
        {
            string sit = "-";

            switch (flag)
            {
                case "0":
                    sit = "Parcela à faturar";
                    break;
                case "1":
                    sit = "Parcela faturada";
                    break;
                case "2":
                    sit = "Parcela vencida dentro da carência";
                    break;
                case "3":
                    sit = "Parcela vencida após a carência";
                    break;
                case "4":
                    sit = "Pagamento efetuado";
                    break;
                default:
                    sit = flag;
                    break;
            }
            return sit;
        }
        private partial class ListaAcordo
        {
            public string Acordo { get; set; }
            public DateTime Data { get; set; }
            public string Situacao { get; set; }
            public string Descricao { get; set; }
            public string Operador { get; set; }
        }

        private partial class ListaPagamento
        {
            public string Vencimento { get; set; }
            public string Valor { get; set; }
            public string Situacao { get; set; }

        }

        private void gcAcordo_KeyUp(object sender, KeyEventArgs e)
        {
            //if(e.KeyValue)
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                ActionGrid();
            }
        }

        private void ActionGrid()
        {
            try
            {
                Log.SaveFile("**** EXTRATO ACORDO > [ActionGrid()] ****");
                GridView gridView = gcAcordo.FocusedView as GridView;
                ListaAcordo row = (ListaAcordo)gridView.GetRow(gridView.FocusedRowHandle);

                if (this.AgreementResponse.Count > 0)
                {
                    foreach (var agreement in this.AgreementResponse)
                    {
                        if (agreement.IdAgreement.ToString() == row.Acordo)
                        {
                            PopulaDetalhes(agreement);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.SaveFile("EXCEPTION => EXTRATO ACORDO > [ActionGrid()] ****" + ex.Message);
            }
        }

        private void ExtratoAcordo_FormClosing(object sender, FormClosingEventArgs e)
        {
            bwListaAcordo.CancelAsync();
            AbortBackgroundWorker();
        }
    }


}