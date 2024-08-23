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


namespace FMC.Solutions.NPPLUS
{
    public partial class Roteiro : DevExpress.XtraEditors.XtraForm
    {
        private string title;
        private string destaque;
        private string message;
        private bool big;
        private string btnCancel;
        private string btnOk;

        public Roteiro() { }

        public Roteiro(string title, string destaque, string message, string titleBtnCancel = "Cancelar", string titleBtnOk = "Sim")
        {
            try
            {
                InitializeComponent();
                this.title = title;
                this.destaque = destaque;
                this.message = message;
                this.btnCancel = titleBtnCancel;
                this.btnOk = titleBtnOk;
            }catch(Exception ex)
            {
                Log.SaveFile("EXCEPTION => [MessageBox->MessageBox]" + ex.Message);
            }
        }


        private void btnCancelAlteracao_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void Roteiro_Load(object sender, EventArgs e)
        {
            try
            {
                btnCancelAlteracao.Text = this.btnCancel;
                btnConfirmar.Text = this.btnOk;
                this.Text = title;
                lblDestaque.Text = destaque;
                lblMessage.UseMnemonic = false;
                lblMessage.Text = message;
            }
            catch (Exception ex)
            {
                Log.SaveFile("EXCEPTION => [Roteiro->RoteiroLoad_Load]" + ex.Message);
            }
        }

        private void btnCancelAlteracao_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}