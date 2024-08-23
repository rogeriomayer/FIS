using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;


using System.Collections.ObjectModel;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraSpreadsheet;
using FMC.Solutions.NPPLUS.Library.Class;
using DevExpress.Spreadsheet;
using System.Drawing;
using DevExpress.XtraSpreadsheet.Model;
using Worksheet = DevExpress.Spreadsheet.Worksheet;
using System.Diagnostics;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using FMC.Solutions.NPPLUS.Library.REST;

namespace FMC.Solutions.NPPLUS.Relatorio
{
    public partial class RelatorioGerencial : XtraForm
    {
        IList<UserLoginResponse> Users = new List<UserLoginResponse>();

        IList<StatusLeadResponse> ListStatusLead = new List<StatusLeadResponse>();

        private int? IdUser;
        public RelatorioGerencial()
        {
            InitializeComponent();

            txtDataDe.Properties.MinValue = DateTime.Today.AddDays(-90);
            txtDataDe.Properties.MaxValue = DateTime.Today;

            txtDataAte.Properties.MinValue = DateTime.Today.AddDays(-90);
            txtDataAte.Properties.MaxValue = DateTime.Today;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDataDe.EditValue != null && txtDataAte.EditValue != null)
                {
                    Splash.Visible(splashScreenManager1, true);
                    splashScreenManager1.SetWaitFormDescription("Carregando usuários...");

                    lblDe.Text = Convert.ToDateTime(txtDataDe.EditValue).ToString("dd/MM/yyyy");
                    lblAte.Text = Convert.ToDateTime(txtDataAte.EditValue).ToString("dd/MM/yyyy");

                    ImageComboBoxItem item = cbUser.SelectedItem as ImageComboBoxItem;
                    if (item.Value.ToString() == "0")
                        ListStatusLead = FisAPI.GetStatusLeads(Users.Select(p => p.IdUserLogin).ToList(), Convert.ToDateTime(txtDataDe.EditValue), Convert.ToDateTime(txtDataAte.EditValue), AppSettings.PRODUCT_TYPE, ((Main)this.Owner).User.OAuth.access_token).ToList();
                    else
                        ListStatusLead = FisAPI.GetStatusLeads(Convert.ToInt32(item.Value), Convert.ToDateTime(txtDataDe.EditValue), Convert.ToDateTime(txtDataAte.EditValue), AppSettings.PRODUCT_TYPE, ((Main)this.Owner).User.OAuth.access_token).ToList();

                    if (Permission.RelatorioGerencial.Acordos)
                    {
                        TbAcordo.PageVisible = true;
                        splashScreenManager1.SetWaitFormDescription("Carregando acordos...");
                        GetAcordo(Convert.ToInt32(item.Value));
                    }
                    else
                    {
                        TbAcordo.PageVisible = false;
                    }

                    if (Permission.RelatorioGerencial.Promessas)
                    {
                        tbPromessa.PageVisible = true;
                        splashScreenManager1.SetWaitFormDescription("Carregando promessas...");
                        GetPromessa(Convert.ToInt32(item.Value));
                    }
                    else
                    {
                        tbPromessa.PageVisible = false;
                    }

                    if (Permission.RelatorioGerencial.Extrato)
                    {
                        tbExtrato.PageVisible = true;
                        splashScreenManager1.SetWaitFormDescription("Carregando extrato de ligações...");
                        GetExtrato(Convert.ToInt32(item.Value));
                    }
                    else
                    {
                        tbExtrato.PageVisible = false;
                    }

                    if (Permission.RelatorioGerencial.Resumo)
                    {
                        tbLigacoes.PageVisible = true;
                        splashScreenManager1.SetWaitFormDescription("Carregando resumo...");
                        GetCalls(Convert.ToInt32(item.Value));
                    }
                    else
                    {
                        tbLigacoes.PageVisible = false;
                    }

                    if (Permission.RelatorioGerencial.Ranking)
                    {
                        gcRanking.Visible = true;
                        splashScreenManager1.SetWaitFormDescription("Carregando ranking...");
                        GetRanking();
                    }
                    else
                    {
                        gcRanking.Visible = false;
                    }
                    Splash.Visible(splashScreenManager1, false);
                }
                else
                {
                    XtraMessageBox.Show("Os campos precisam estar preenchidos.", "Problemas durante a busca", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Splash.Visible(splashScreenManager1, false);
            }
        }

        private void GetPromessa(int idUser)
        {
            var listPromisse = ListStatusLead.Where(p => p.PromisseResponse != null && (p.IdUserLogin == idUser || idUser == 0)).Select(p => p.PromisseResponse).ToList();

            if (listPromisse.Count() > 0)
            {
                lblQtdPromessa.Text = listPromisse.Count().ToString();
                lblTotalPromessa.Text = listPromisse.Sum(p => p.VlPromisse).ToString("C2");
                gcPromessa.DataSource = ListaPromessa(listPromisse);
                gcPromessa.RefreshDataSource();

            }
            else
            {
                lblQtdPromessa.Text = "0";
                lblTotalPromessa.Text = "R$ 0,00";
                gcPromessa.DataSource = null;
                gcPromessa.RefreshDataSource();
            }
        }

        private void GetExtrato(int idUser)
        {
            var listStatusLead = ListStatusLead.Where(p => p.IdUserLogin == idUser || idUser == 0).ToList();

            IList<Extract> listExtract = new List<Extract>();
            foreach (var item in listStatusLead)
            {
                try
                {
                    string nmUser = item.UserLogin;
                    string status = item.Status;
                    listExtract.Add(new Extract(item.DtStatusLead, nmUser, item.DsProduct, status));
                }
                catch (Exception ex)
                {

                }
            }

            if (listStatusLead.Count() > 0)
            {
                gcExtrato.DataSource = listExtract.OrderByDescending(p => p.DtInsert).ToList();
                gcExtrato.RefreshDataSource();
            }
            else
            {
                gcExtrato.DataSource = null;
                gcExtrato.RefreshDataSource();
            }

        }

        private void GetCalls(int idUser)
        {
            IList<Call> listCall = ListStatusLead.Where(p => p.IdUserLogin == idUser || idUser == 0)
                 .GroupBy(p => new { p.Status, p.FlEfective })
                 .Select
                 (
                     p => new Call()
                     {
                         Description = p.Key.Status,
                         Efetivo = p.Key.FlEfective,
                         Qtd = p.Count(),
                     }
                 ).Distinct().ToList();

            if (ListStatusLead.Count() > 0)
            {
                lblTotalChamada.Text = ListStatusLead.Count().ToString();
                lblTotalEfetiva.Text = listCall.Where(p => p.Efetivo).Sum(p => p.Qtd).ToString();
                gcLigacao.DataSource = listCall.OrderByDescending(p => p.Qtd).ToList();
                gcLigacao.RefreshDataSource();
            }
            else
            {
                lblTotalChamada.Text = "0";
                lblTotalEfetiva.Text = "0";
                gcLigacao.DataSource = null;
                gcLigacao.RefreshDataSource();
            }

            ChartCall(listCall);

        }

        private void ChartCall(IList<Call> call)
        {
            //Chart
            Series series = new Series("Chamadas", ViewType.Doughnut);

            if (call.Count > 0)
                foreach (var item in call)
                    series.Points.Add(new SeriesPoint(item.Description, item.Qtd));
            else
                series.Points.Add(new SeriesPoint("Nenhuma Chamada", 0));

            // Add the series to the chart. 
            chartChamada.Series.Clear();
            chartChamada.Series.Add(series);

            // Specify the text pattern of series labels. 
            series.Label.TextPattern = "{A}: {V:N0} - {VP:P1}";
            series.LegendTextPattern = "{A}";

            // Specify how series points are sorted. 
            series.SeriesPointsSorting = SortingMode.Ascending;
            series.SeriesPointsSortingKey = SeriesPointKey.Argument;

            // Specify the behavior of series labels. 
            ((DoughnutSeriesLabel)series.Label).Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            ((DoughnutSeriesLabel)series.Label).Position = PieSeriesLabelPosition.TwoColumns;
            ((DoughnutSeriesLabel)series.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
            ((DoughnutSeriesLabel)series.Label).ResolveOverlappingMinIndent = 5;


            // Adjust the view-type-specific options of the series. 
            ((DoughnutSeriesView)series.View).ExplodeMode = PieExplodeMode.None;
            //((DoughnutSeriesView)series.View).ExplodedPoints.Add(series.Points[0]);
            //((DoughnutSeriesView)series.View).ExplodedDistancePercentage = 30;
            ((DoughnutSeriesView)series.View).HoleRadiusPercent = 40;
            ((DoughnutSeriesView)series.View).MinAllowedSizePercentage = 70;

            // Access the diagram's options. 
            ((SimpleDiagram)chartChamada.Diagram).Dimension = 2;

            // Add a title to the chart and hide the legend. 
            //ChartTitle chartTitle = new ChartTitle();
            //chartTitle.Text = "Ligações";
            //chartChamada.Titles.Clear();
            //chartChamada.Titles.Add(chartTitle);
            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            chartChamada.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            chartChamada.Refresh();

        }

        private void GetAcordo(int idUser)
        {
            var listAgreement = ListStatusLead.Where(p => p.AgreementResponse != null && (p.IdUserLogin == idUser || idUser == 0)).Select(p => p.AgreementResponse).ToList();

            if (listAgreement.Count() > 0)
            {
                lblQtdAcordo.Text = listAgreement.Count().ToString();
                lblTotalAcordo.Text = listAgreement.Sum(p => p.VlAgreement).ToString("C2");

                IList<AgreementResponse> listaAvista = listAgreement.Where(p => p.QtParcel == 0).ToList();

                lblTotalEntrada.Text = listAgreement.Sum(p => p.VlEntrace).ToString("C2");

                gcAcordo.DataSource = ListaAcordo(listAgreement).OrderByDescending(p => p.DtInsert).ToList();
                gcAcordo.RefreshDataSource();
            }
            else
            {
                lblQtdAcordo.Text = "0";
                lblTotalEntrada.Text = "R$ 0,00";
                lblTotalAcordo.Text = "R$ 0,00";
                gcAcordo.DataSource = null;
                gcAcordo.RefreshDataSource();
            }

        }

        private void GetRanking()
        {
            if (Users.Count > 0)
            {
                gcRanking.DataSource = ListaRanking();
                gcRanking.RefreshDataSource();
            }
            else
            {
                gcRanking.DataSource = null;
                gcRanking.RefreshDataSource();
            }

        }


        private IList<Promessa> ListaPromessa(IList<PromisseResponse> promisses)
        {
            return promisses.Select(
            p => new Promessa
            {
                DtInsert = p.DtInsert.ToString("dd/MM/yyyy HH:mm"),
                NmUser = ListStatusLead.Where(l => l.IdStatusLead == p.IdStatusLead).FirstOrDefault().UserLogin,
                Account = ListStatusLead.Where(l => l.IdStatusLead == p.IdStatusLead).FirstOrDefault().DsProduct,
                DtPromisse = p.DtPromisse.ToString("dd/MM/yyyy"),
                VlPromisse = p.VlPromisse.ToString("C2")
            }).ToList();
        }


        private IList<Acordo> ListaAcordo(IList<AgreementResponse> agreements)
        {
            return agreements.Select(
                p => new Acordo
                {
                    DtInsert = p.DtInsert.ToString("dd/MM/yyyy HH:mm"),
                    NmUser = ListStatusLead.Where(l => l.IdStatusLead == p.IdStatusLead).FirstOrDefault().UserLogin,
                    Account = ListStatusLead.Where(l => l.IdStatusLead == p.IdStatusLead).FirstOrDefault().DsProduct,
                    Type = p.QtParcel == 0 ? "A Vista" : "Parcelado",
                    DtPayment = p.DtEntrace.ToString("dd/MM/yyyy"),
                    VlAgreement = p.VlAgreement.ToString("C2"),
                    VlEntrace = p.VlEntrace > 0 ? p.VlEntrace.ToString("C2") : "-"
                }).ToList();

        }




        private IList<Ranking> ListaRanking()
        {

            IList<Ranking> ranking = new List<Ranking>();


            foreach (var user in Users)
            {
                int qtdPromisse = ListStatusLead.Where(p => p.IdUserLogin == user.IdUserLogin && p.PromisseResponse != null).Count();
                //listPromisse.Where(p => p.StatusLead.IdUser == user.IdUser).Count();
                decimal vlPromisse = ListStatusLead.Where(p => p.IdUserLogin == user.IdUserLogin && p.PromisseResponse != null).Sum(p => p.PromisseResponse.VlPromisse);

                int qtdAgreement = ListStatusLead.Where(p => p.IdUserLogin == user.IdUserLogin && p.AgreementResponse != null).Count();

                //Agreement Total VL
                //IList<Agreement> listaAvista = listAgreement.Where(p => p.QtParcel == 0 && p.StatusLead.IdUser == user.IdUser).ToList();
                //IList<Agreement> listaParcel = listAgreement.Where(p => p.QtParcel > 0 && p.StatusLead.IdUser == user.IdUser).ToList();

                decimal vlAgreement = ListStatusLead.Where(p => p.IdUserLogin == user.IdUserLogin && p.AgreementResponse != null).Sum(p => p.AgreementResponse.VlAgreement);

                ranking.Add(new Ranking
                {
                    NmUser = user.DsName,
                    Promisse = qtdPromisse,
                    Agreement = qtdAgreement,
                    VlPromisse = vlPromisse,
                    VlAgreement = vlAgreement,
                    VlTotal = vlAgreement + vlPromisse
                });
            }

            return ranking.OrderByDescending(p => p.Promisse).ThenByDescending(p => p.Agreement).ToList();

        }

        private void RelatorioGerencial_Load(object sender, EventArgs e)
        {
            try
            {
                Splash.Visible(splashScreenManager1, true);
                splashScreenManager1.SetWaitFormDescription("Carregando usuários...");

                Users = FisAPI.GetUsersIdManager(((Main)this.Owner).User.IdUser, ((Main)this.Owner).User.OAuth.access_token).ToList();
                ListStatusLead = FisAPI.GetStatusLeads(Users.Select(p => p.IdUserLogin).ToList(), DateTime.Today, DateTime.Today, AppSettings.PRODUCT_TYPE, ((Main)this.Owner).User.OAuth.access_token).ToList();

                IList<UserLoginResponse> users = new List<UserLoginResponse>();

                //if (Permission.RelatorioGerencial.Todos)
                users = Users.OrderBy(p => p.DsName).ToList();
                //else
                //    users = Users.Where(p=> p.id .OrderBy(p => p.DsName).ToList();

                cbUser.Properties.Items.Add(new ImageComboBoxItem()
                {
                    Value = 0,
                    Description = "Todos Operadores"
                });
                foreach (var user in users)
                {
                    cbUser.Properties.Items.Add(new ImageComboBoxItem()
                    {
                        Value = user.IdUserLogin,
                        Description = user.DsName
                    });
                }
                cbUser.SelectedIndex = 0;
                if (Permission.RelatorioGerencial.Acordos)
                {
                    TbAcordo.PageVisible = true;
                    splashScreenManager1.SetWaitFormDescription("Carregando acordos...");
                    GetAcordo(0);
                }
                else
                {
                    TbAcordo.PageVisible = false;
                    gcAcordo.Visible = false;
                }
                if (Permission.RelatorioGerencial.Promessas)
                {
                    tbPromessa.PageVisible = true;
                    splashScreenManager1.SetWaitFormDescription("Carregando promessas...");
                    GetPromessa(0);
                }
                else
                {
                    tbPromessa.PageVisible = false;
                    gcPromessa.Visible = false;
                }

                if (Permission.RelatorioGerencial.Extrato)
                {
                    tbExtrato.PageVisible = true;
                    splashScreenManager1.SetWaitFormDescription("Carregando extrato de ligações...");
                    GetExtrato(0);
                }
                else
                {
                    tbExtrato.PageVisible = false;
                    gcExtrato.Visible = false;
                }

                if (Permission.RelatorioGerencial.Resumo)
                {
                    tbLigacoes.PageVisible = true;
                    splashScreenManager1.SetWaitFormDescription("Carregando chamadas...");
                    GetCalls(0);
                }
                else
                {
                    tbLigacoes.PageVisible = false;
                    gcLigacao.Visible = false;
                }

                if (Permission.RelatorioGerencial.Extrato)
                {
                    gcRanking.Visible = true;
                    splashScreenManager1.SetWaitFormDescription("Carregando ranking...");
                    GetRanking();
                }
                else
                {
                    gcRanking.Visible = false;
                }

                lblDe.Text = DateTime.Today.ToString("dd/MM/yyyy");
                lblAte.Text = DateTime.Today.ToString("dd/MM/yyyy");

                Splash.Visible(splashScreenManager1, false);
            }
            catch (Exception ex)
            {
                Splash.Visible(splashScreenManager1, false);
            }

        }

        private class Promessa
        {
            public string DtInsert { get; set; }
            public string NmUser { get; set; }
            public string Account { get; set; }
            public string DtPromisse { get; set; }
            public string VlPromisse { get; set; }
        }

        private class Ranking
        {
            public string NmUser { get; set; }
            public int Promisse { get; set; }
            public int Agreement { get; set; }
            public decimal VlPromisse { get; set; }
            public decimal VlAgreement { get; set; }
            public decimal VlTotal { get; set; }
        }

        private class Acordo
        {
            public string DtInsert { get; set; }
            public string NmUser { get; set; }
            public string Account { get; set; }
            public string Type { get; set; }
            public string DtPayment { get; set; }
            public string VlAgreement { get; set; }
            public string VlEntrace { get; set; }
        }

        private class Call
        {
            public Call()
            {
                /*
                var codif = new StatusDayBLL().GetCodification(codification);
                this.Description = codif.DescriptionCodification;
                this.Qtd = qtd;
                this.Efetivo = codif.Effective == "Y";
                */
            }

            public string Description { get; set; }
            public int Qtd { get; set; }
            public bool Efetivo { get; set; }
        }

        private class Extract
        {
            public Extract(DateTime dtInsert, string nmUser, string account, string codification)
            {
                this.DtInsert = dtInsert.ToString("dd/MM/yyyy HH:mm");
                this.NmUser = nmUser;
                this.Account = account;
                this.Description = codification;
            }

            public string DtInsert { get; set; }
            public string NmUser { get; set; }
            public string Account { get; set; }
            public string Description { get; set; }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {

        }

        void PrintingSystem_XlSheetCreated(object sender, XlSheetCreatedEventArgs e)
        {
            if (e.Index == 0)
                e.SheetName = "Acordos Realizados";
            else if (e.Index == 1)
                e.SheetName = "Promessas Realizadas";
            else if (e.Index == 2)
                e.SheetName = "Extrato de Ligações";
            else if (e.Index == 3)
                e.SheetName = "Resumo de Ligações";
        }

        void PrintingSystem_XlSheetCreatedRanking(object sender, XlSheetCreatedEventArgs e)
        {
            if (e.Index == 0)
                e.SheetName = "Ranking";
        }

        private void groupControl3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupControl3_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (Permission.RelatorioGerencial.Ranking)
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Excel (.xlsx)|*.xlsx";
                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        Splash.Visible(splashScreenManager1, true);
                        splashScreenManager1.SetWaitFormDescription("Aguarde, baixando relatório de ranking...");

                        var printingSystem = new PrintingSystemBase();
                        var compositeLink = new CompositeLinkBase();
                        compositeLink.PrintingSystemBase = printingSystem;

                        var link1 = new PrintableComponentLinkBase();
                        link1.Component = gcRanking;
                        compositeLink.Links.Add(link1);
                        compositeLink.PrintingSystemBase.XlSheetCreated += PrintingSystem_XlSheetCreatedRanking;

                        var options = new XlsxExportOptions();
                        options.ExportMode = XlsxExportMode.SingleFilePageByPage;
                        compositeLink.CreatePageForEachLink();

                        compositeLink.ExportToXlsx(saveDialog.FileName, options);
                        Process.Start(new ProcessStartInfo(saveDialog.FileName) { UseShellExecute = true });
                        Splash.Visible(splashScreenManager1, false);
                    }
                }
        }

        private void groupControl2_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (Permission.RelatorioGerencial.Acordos || Permission.RelatorioGerencial.Promessas || Permission.RelatorioGerencial.Extrato || Permission.RelatorioGerencial.Resumo)
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Excel (.xlsx)|*.xlsx";
                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        Splash.Visible(splashScreenManager1, true);
                        splashScreenManager1.SetWaitFormDescription("Aguarde, baixando relatórios operacionais...");

                        var printingSystem = new PrintingSystemBase();
                        var compositeLink = new CompositeLinkBase();
                        compositeLink.PrintingSystemBase = printingSystem;


                        var link1 = new PrintableComponentLinkBase();
                        link1.Component = gcPromessa;
                        var link2 = new PrintableComponentLinkBase();
                        link2.Component = gcAcordo;
                        var link3 = new PrintableComponentLinkBase();
                        link3.Component = gcLigacao;
                        var link4 = new PrintableComponentLinkBase();
                        link4.Component = gcExtrato;

                        if (Permission.RelatorioGerencial.Acordos)
                            compositeLink.Links.Add(link2);
                        if (Permission.RelatorioGerencial.Promessas)
                            compositeLink.Links.Add(link1);
                        if (Permission.RelatorioGerencial.Resumo)
                            compositeLink.Links.Add(link3);
                        if (Permission.RelatorioGerencial.Extrato)
                            compositeLink.Links.Add(link4);
                        compositeLink.PrintingSystemBase.XlSheetCreated += PrintingSystem_XlSheetCreated;

                        var options = new XlsxExportOptions();
                        options.ExportMode = XlsxExportMode.SingleFilePageByPage;
                        compositeLink.CreatePageForEachLink();

                        compositeLink.ExportToXlsx(saveDialog.FileName, options);
                        Process.Start(new ProcessStartInfo(saveDialog.FileName) { UseShellExecute = true });

                        Splash.Visible(splashScreenManager1, false);
                    }
                }
        }
    }

}