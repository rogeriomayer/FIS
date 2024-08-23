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
    public partial class ExtratoResumido : XtraForm
    {
        private readonly Main main;
        private Thread _backgroundWorkerThread;
        private ICollection<ItemExtrato> ListaExtrato = new Collection<ItemExtrato>();

        public ExtratoResumido()
        {
            InitializeComponent();
        }

        public ExtratoResumido(Main main)
        {
            this.main = main;
            InitializeComponent();
        }

        ~ExtratoResumido()
        {
            bwLista.Dispose();
        }


        private void ExtratoResumido_Load(object sender, EventArgs e)
        {
            pbExtrato.Show();
            bwLista.RunWorkerAsync();

            gcExtrato.DataSource = ListaExtrato;
            gcExtrato.RefreshDataSource();
            //pbExtratoAcordos.Hide();
            //bwListaAcordo.CancelAsync();
        }

        public void AbortBackgroundWorker()
        {
            if (_backgroundWorkerThread != null)
                _backgroundWorkerThread.Abort();
        }

        private void bwLista_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (bwLista.CancellationPending == true)
                {
                    e.Cancel = true;
                    bwLista.CancelAsync();
                    bwLista.Dispose();
                    GC.Collect();
                }
                else
                {
                    _backgroundWorkerThread = Thread.CurrentThread;
                    /*Próxima Fatura*/
                    /*
                    VA82 va82 = SessionP2.Session.VA82(main.productSelected.Card.Trim(), main.productSelected.OrgCMS);
                    va82.GoProximaFatura();
                    ItemExtrato itemExtrato = Extrato(va82);
                    if (itemExtrato != null)
                        ListaExtrato.Add(itemExtrato);
                    bwLista.ReportProgress(0, "Update");
                    */

                    /*Fatura Atual*/
                    /*
                    va82.GoFaturaCorrente();
                    itemExtrato = Extrato(va82);
                    if (itemExtrato != null)
                        ListaExtrato.Add(itemExtrato);
                    bwLista.ReportProgress(0, "Update");

                    //Fatura Anterior 1
                    va82.GoFaturaAnterior1();
                    itemExtrato = Extrato(va82);
                    if (itemExtrato != null)
                        ListaExtrato.Add(itemExtrato);
                    bwLista.ReportProgress(0, "Update");
                    */

                    //Fatura Anterior 2
                    /*
                    va82.GoFaturaAnterior2();
                    itemExtrato = Extrato(va82);
                    if (itemExtrato != null)
                        ListaExtrato.Add(itemExtrato);
                    bwLista.ReportProgress(0, "Update");

                    //Fatura Anterior 3
                    va82.GoFaturaAnterior3();
                    itemExtrato = Extrato(va82);
                    if (itemExtrato != null)
                        ListaExtrato.Add(itemExtrato);
                    bwLista.ReportProgress(0, "Update");
                    */

                    //Fatura Anterior 4
                    /*
                    va82.GoFaturaAnterior4();
                    itemExtrato = Extrato(va82);
                    if (itemExtrato != null)
                        ListaExtrato.Add(itemExtrato);
                    bwLista.ReportProgress(0, "Update");

                    //Fatura Anterior 5
                    va82.GoFaturaAnterior5();
                    itemExtrato = Extrato(va82);
                    if (itemExtrato != null)
                        ListaExtrato.Add(itemExtrato);
                    bwLista.ReportProgress(0, "Update");
                    */

                    //Fatura Anterior 6
                    /*
                    va82.GoFaturaAnterior6();
                    itemExtrato = Extrato(va82);
                    if (itemExtrato != null)
                        ListaExtrato.Add(itemExtrato);
                    bwLista.ReportProgress(0, "Update");
                    */
                }
            }
            catch (ThreadAbortException exception) { }
        }

        private void bwLista_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            gcExtrato.DataSource = ListaExtrato;
            gcExtrato.RefreshDataSource();
            //pbExtratoAcordos.Hide();
        }

        private void bwLista_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
                bwLista.CancelAsync();
            }
            pbExtrato.Hide();
        }

        /*
        private ItemExtrato Extrato(VA82 va82)
        {
            if (va82 != null)
                return new ItemExtrato
                {
                    Data = va82.DataVencimento?.ToString("dd/MM/yyyy"),
                    Saldo = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.ValorAtual),
                    Minimo = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.ValorMinimo),
                    Pagamento = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.Pagamento),
                    ProdutosServicos = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.ProdutosServicos),
                    SaldoAnterior = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.SaldoAnterior),
                    Saques = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.Saques),
                    TransacoesDisputa = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.TransacoesDisputa),
                    ComprasRotativas = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.ComprasRotativas),
                    ComprasParceladas = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.ComprasParceladas),
                    Ajustes = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.Ajustes),
                    Encargos = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.Encargos),
                    Juros = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.Juros),
                    Multa = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.Multa),
                    VencimentosProjetados = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.VencimentosProjetados),
                    AutorizacoesPendentes = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.AutorizacoesPendentes),
                    Transporte = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.Transporte),
                    TransDolar = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.TransDolar)
                };

            return null;
        }
        */

        private partial class ItemExtrato
        {
            public string Data { get; set; }
            public string Saldo { get; set; }
            public string Minimo { get; set; }
            public string Pagamento { get; set; }
            public string ProdutosServicos { get; set; }
            public string SaldoAnterior { get; set; }
            public string Saques { get; set; }
            public string TransacoesDisputa { get; set; }
            public string ComprasRotativas { get; set; }
            public string ComprasParceladas { get; set; }
            public string Ajustes { get; set; }
            public string Encargos { get; set; }
            public string Juros { get; set; }
            public string Multa { get; set; }
            public string VencimentosProjetados { get; set; }
            public string AutorizacoesPendentes { get; set; }
            public string Transporte { get; set; }
            public string TransDolar { get; set; }

        }

        private void ExtratoResumido_FormClosing(object sender, FormClosingEventArgs e)
        {
            //bwLista.CancelAsync();
            AbortBackgroundWorker();
            //SessionP2.Session.ARIQ(main.productSelected.Card.Trim(), main.productSelected.OrgCMS);
        }

        private void gcExtrato_Click(object sender, EventArgs e)
        {
            ActionGrid();
        }

        private void gcExtrato_KeyUp(object sender, KeyEventArgs e)
        {
            //if(e.KeyValue)
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                ActionGrid();
            }
        }
        private void ActionGrid()
        {
            GridView gridView = gcExtrato.FocusedView as GridView;
            ItemExtrato row = (ItemExtrato)gridView.GetRow(gridView.FocusedRowHandle);
            //DataRow row = gridView.GetDataRow(gridView.GetSelectedRows()[0]);
            //DataRow focusedDataRow = gridView.GetFocusedDataRow();
            if (row != null)
            {
                PopulaDetalhes(row);
            }
        }
        private void PopulaDetalhes(ItemExtrato detail)
        {
            if (detail != null)
            {
                lblSaldoAnterior.Text = detail.SaldoAnterior;
                lblSaldoAtual.Text = detail.Saldo;
                lblPagamentoMinimo.Text = detail.Minimo;
                lblPagamento.Text = detail.Pagamento;
                lblProdutosServicos.Text = detail.ProdutosServicos;
                lblSaques.Text = detail.Saques;
                lblTransacoesDisputa.Text = detail.TransacoesDisputa;
                lblComprasRotativas.Text = detail.ComprasRotativas;
                lblComprasParceladas.Text = detail.ComprasParceladas;
                lblAjustes.Text = detail.Ajustes;
                lblEncargos.Text = detail.Encargos;
                lblJuros.Text = detail.Juros;
                lblMulta.Text = detail.Multa;
                lblVencimentosProjetados.Text = detail.VencimentosProjetados;
                lblAutorizacoesPendentes.Text = detail.AutorizacoesPendentes;
                lblTransporte.Text = detail.Transporte;
                lblTransDolar.Text = detail.TransDolar;
            }
        }
    }
}