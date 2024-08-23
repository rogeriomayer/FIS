using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;


using FMC.Solutions.NPPLUS.Library.Class;
using FMC.Solutions.NPPLUS.Library.Class.ViaCEP;
using FMC.Solutions.NPPLUS.Library.Class.ViaCEP.Model;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace FMC.Solutions.NPPLUS
{
    public partial class PesquisarLogradouro : XtraForm
    {
        private readonly AtualizarEndereco atualizarEndereco;
        private Thread _backgroundWorkerThread;
        //private ICollection<AgreementHistory> agreementHistory = null;
        ICollection<ViaCEP> lista = new HashSet<ViaCEP>();
        ViaCEP viaCEP;

        public PesquisarLogradouro()
        {
            InitializeComponent();
        }

        public PesquisarLogradouro(AtualizarEndereco atualizarEndereco, string logradouro, string cidade, string estado)
        {
            this.atualizarEndereco = atualizarEndereco;
            txtLogradouro.Text = logradouro;
            txtCidade.Text = cidade;
            txtEstado.Text = estado;
            InitializeComponent();
        }

        ~PesquisarLogradouro()
        {
            bw.Dispose();
        }


        private void PesquisarLogradouro_Load(object sender, EventArgs e)
        {
            progressBar.Hide();
        }

        public void AbortBackgroundWorker()
        {
            if (_backgroundWorkerThread != null)
                _backgroundWorkerThread.Abort();
        }

        private void ClearFields()
        {
            gc.DataSource = null;
            gc.RefreshDataSource();

            Application.DoEvents();

        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            if (bw.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                lista = Search.ByAddress(txtEstado.Text, txtCidade.Text, txtLogradouro.Text);
                bw.ReportProgress(0, "Popular");
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            gc.DataSource = lista;
            gc.RefreshDataSource();
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                //XtraMessageBox.Show("Cancelado.");
                progressBar.Hide();
                btnPesquisar.Enabled = true;
            }
            else if (!(e.Error == null))
            {
                XtraMessageBox.Show(e.Error.Message);
                progressBar.Hide();
                btnPesquisar.Enabled = true;
            }
            else
            {
                bw.CancelAsync();
                progressBar.Hide();
                btnPesquisar.Enabled = true;
            }
        }

        private void gc_KeyUp(object sender, KeyEventArgs e)
        {
            //if(e.KeyValue)
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                ActionGrid();
            }
        }

        private void ActionGrid()
        {
            GridView gridView = gc.FocusedView as GridView;
            viaCEP = (ViaCEP)gridView.GetRow(gridView.FocusedRowHandle);
            btnConfirmar.Enabled = true;
        }

        private void PesquisarLogradouro_FormClosing(object sender, FormClosingEventArgs e)
        {
            bw.CancelAsync();
            AbortBackgroundWorker();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            ClearFields();
            btnConfirmar.Enabled = false;
            progressBar.Show();
            bw.RunWorkerAsync();
        }

        private void gc_Click(object sender, EventArgs e)
        {
            ActionGrid();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            atualizarEndereco.address = viaCEP;
        }
    }


}