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
    public partial class MessageBox : DevExpress.XtraEditors.XtraForm
    {
        private string title;
        private string destaque;
        private string message;
        private bool big;
        private string btnCancel;
        private string btnOk;

        public MessageBox() { }

        public MessageBox(string title, string destaque, string message, bool big = false, string titleBtnCancel = "Cancelar", string titleBtnOk = "Sim")
        {
            try
            {
                InitializeComponent();
                this.title = title;
                this.destaque = destaque;
                this.message = message;
                this.big = big;
                this.btnCancel = titleBtnCancel;
                this.btnOk = titleBtnOk;
            }catch(Exception ex)
            {
                Log.SaveFile("EXCEPTION => [MessageBox->MessageBox]" + ex.Message);
            }
        }


        private void btnCancelAlteracao_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
            
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void MessageBox_Load(object sender, EventArgs e)
        {
            try
            {
                btnCancelAlteracao.Text = this.btnCancel;
                btnConfirmar.Text = this.btnOk;
                this.Text = this.title;
                lblDestaque.Text = this.destaque;
                lblMessage.Text = this.message;
                if (this.big)
                    BigWindow();
            }
            catch (Exception ex)
            {
                Log.SaveFile("EXCEPTION => [MessageBox->MessageBox_Load]" + ex.Message);
            }
        }

        private void BigWindow()
        {
            try
            {
                this.Height = 230;
                btnCancelAlteracao.Location = new Point(194, 147);
                btnConfirmar.Location = new Point(316, 147);
                separator.Location = new Point(14, 130);
                lblDestaque.Location = new Point(67, 25);
                lblMessage.Location = new Point(67, 57);
                lblMessage.Height = 70;
            }catch(Exception ex)
            {
                Log.SaveFile("EXCEPTION => MessageBox -> [BigWindow()]"+ ex.Message);
            }
        }

        private void btnCancelAlteracao_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}