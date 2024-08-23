using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;


using System.Collections.ObjectModel;
using DevExpress.XtraCharts;
using FMC.Solutions.NPPLUS.Library.REST;
using FMC.Solutions.NPPLUS.Library.Class;

namespace FMC.Solutions.NPPLUS.Relatorio
{
    public partial class RelatorioOperacional : DevExpress.XtraEditors.XtraForm
    {
        public RelatorioOperacional()
        {
            InitializeComponent();

            txtDataDe.Properties.MinValue = DateTime.Today.AddDays(-90);
            txtDataDe.Properties.MaxValue = DateTime.Today;

            txtDataAte.Properties.MinValue = DateTime.Today.AddDays(-90);
            txtDataAte.Properties.MaxValue = DateTime.Today;

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtDataDe.EditValue != null && txtDataAte.EditValue != null)
            {
                lblDe.Text = Convert.ToDateTime(txtDataDe.EditValue).ToString("dd/MM/yyyy");
                lblAte.Text = Convert.ToDateTime(txtDataAte.EditValue).ToString("dd/MM/yyyy");

                var listStatusLead = FisAPI.GetStatusLeads(((Main)this.Owner).User.IdUser, Convert.ToDateTime(txtDataDe.EditValue), Convert.ToDateTime(txtDataAte.EditValue), AppSettings.PRODUCT_TYPE, ((Main)this.Owner).User.OAuth.access_token);

                GetPromessa(listStatusLead.Select(p => p.PromisseResponse).ToList());
                GetAcordo(listStatusLead.Select(p => p.AgreementResponse).ToList());
                GetCalls(listStatusLead.ToList());
            }
            else
            {
                XtraMessageBox.Show("Os campos precisam estar preenchidos.", "Problemas durante a busca", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetPromessa(IList<PromisseResponse> listPromisseResponse)
        {
            listPromisseResponse = listPromisseResponse.Where(p => p != null).ToList();
            if (listPromisseResponse.Count() > 0)
            {
                lblQtdPromessa.Text = listPromisseResponse.Count().ToString();
                lblTotalPromessa.Text = listPromisseResponse.Sum(p => p.VlPromisse).ToString("C2");
                gcPromessa.DataSource = listPromisseResponse.Select
                    (
                        p => new Promessa
                        {
                            DtInsert = p.DtInsert.ToString("dd/MM/yyyy HH:mm"),
                            DtPromisse = p.DtPromisse.ToString("dd/MM/yyyy"),
                            VlPromisse = p.VlPromisse.ToString("C2")
                        }
                     ).ToList();
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

        private void GetCalls(IList<StatusLeadResponse> statusLeadResponse)
        {
            IList<Call> listCall = statusLeadResponse
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

            if (statusLeadResponse.Count() > 0)
            {
                lblTotalChamada.Text = statusLeadResponse.Count().ToString();
                lblTotalEfetiva.Text = listCall.Where(p => p.Efetivo).Sum(p => p.Qtd).ToString();
                gcLigacao.DataSource = listCall;
                gcLigacao.RefreshDataSource();
            }
            else
            {
                lblTotalChamada.Text = "0";
                lblTotalEfetiva.Text = "0";
                gcLigacao.DataSource = null;
                gcLigacao.RefreshDataSource();
            }

            ChartCall(listCall.Distinct().ToList());

        }

        private void ChartCall(ICollection<Call> call)
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

        private void GetAcordo(IList<AgreementResponse> listAgreementResponse)
        {
            listAgreementResponse = listAgreementResponse.Where(p => p != null).ToList();
            if (listAgreementResponse.Count() > 0)
            {
                lblQtdAcordo.Text = listAgreementResponse.Count().ToString();
                lblTotalAcordo.Text = listAgreementResponse.Sum(p => p.VlAgreement).ToString("C2");

                ICollection<AgreementResponse> listaAvista = listAgreementResponse.Where(p => p.QtParcel == 0).ToList();
                ICollection<AgreementResponse> listaParcel = listAgreementResponse.Where(p => p.QtParcel > 0).ToList();

                lblTotalEntrada.Text = (listaAvista.Sum(p => p.VlAgreement) + listaParcel.Sum(p => p.VlEntrace)).ToString("C2");

                gcAcordo.DataSource = listAgreementResponse.Select
                    (
                        p => new Acordo
                        {
                            DtInsert = p.DtInsert.ToString("dd/MM/yyyy HH:mm"),
                            Type = p.QtParcel == 0 ? "A Vista" : "Parcelado",
                            DtPayment = p.DtEntrace.ToString("dd/MM/yyyy"),
                            VlAgreement = p.VlAgreement.ToString("C2"),
                            VlEntrace = p.VlEntrace > 0 ? p.VlEntrace.ToString("C2") : "-"
                        }
                    ).ToList();
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



        private class Promessa
        {
            public string DtInsert { get; set; }
            public string DtPromisse { get; set; }
            public string VlPromisse { get; set; }
        }

        private class Acordo
        {
            public string DtInsert { get; set; }
            public string Type { get; set; }
            public string DtPayment { get; set; }
            public string VlAgreement { get; set; }
            public string VlEntrace { get; set; }
        }

        private class Call
        {
            public string Description { get; set; }
            public int Qtd { get; set; }
            public bool Efetivo { get; set; }
        }

        private void RelatorioOperacional_Load(object sender, EventArgs e)
        {
            var listStatusLead = FisAPI.GetStatusLeads(((Main)this.Owner).User.IdUser, DateTime.Today, DateTime.Today, AppSettings.PRODUCT_TYPE, ((Main)this.Owner).User.OAuth.access_token);

            GetPromessa(listStatusLead.Select(p => p.PromisseResponse).ToList());
            GetAcordo(listStatusLead.Select(p => p.AgreementResponse).ToList());
            GetCalls(listStatusLead.ToList());
            lblDe.Text = DateTime.Today.ToString("dd/MM/yyyy");
            lblAte.Text = DateTime.Today.ToString("dd/MM/yyyy");
        }
    }

}