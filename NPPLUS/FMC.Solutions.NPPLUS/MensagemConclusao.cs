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

using FMC.Solutions.NPPLUS.Library.Class.Model;
using FMC.Solutions.NPPLUS.Library.REST.Models;

namespace FMC.Solutions.NPPLUS
{
    public partial class MensagemConclusao : DevExpress.XtraEditors.XtraForm
    {
        private string title;
        private string destaque;
        private string message;
        private bool big;
        private string btnCancel;
        private string btnOk;

        private Promisse promisse;
        private Agreement agreement;

        public MensagemConclusao(Promisse _promisse, Agreement _agreement)
        {
            try
            {
                this.promisse = _promisse;
                this.agreement = _agreement;

                InitializeComponent();
            }
            catch (Exception ex)
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

        private void MensagemConclusao_Load(object sender, EventArgs e)
        {
            try
            {
                if (promisse != null)
                {
                    this.Text = "Conclusão da Promessa";
                    lblMessage.Text = RoteiroPromessa();
                }
                else if (agreement != null)
                {
                    this.Text = "Conclusão do Acordo";
                    lblMessage.Text = RoteiroAcordo();
                }

            }
            catch (Exception ex)
            {
                Log.SaveFile("EXCEPTION => [MessageBox->MessageBox_Load]" + ex.Message);
            }
        }

        private void btnCancelAlteracao_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }



        private string RoteiroPromessa()
        {
            string mensagem = "Prezado cliente, conforme combinamos, ficou agendado o pagamento do seu cartão Afinz ";
            if (promisse.NrParcel > 0)
            {
                mensagem += "no valor de entrada {0} para o dia {1} e {2} parcelas de {3}, conto com seu comprometimento para evitar as ligações de cobrança, ";
                mensagem += "bem como as correções de juros e encargos.";
                mensagem = string.Format(mensagem, promisse.VlPromisse.ToString("N2"), promisse.DtPromisse.ToString("dd/MM"), promisse.NrParcel.ToString(), promisse.VlParcel.ToString("N2"));
            }
            else
            {
                mensagem += "no valor de {0} para o dia {1}, conto com seu comprometimento para evitar as ligações de cobrança, ";
                mensagem += "bem como as correções de juros e encargos.";
                mensagem = string.Format(mensagem, promisse.VlPromisse.ToString("N2"), promisse.DtPromisse.ToString("dd/MM"));
            }
            mensagem += Environment.NewLine + "Ficou alguma dúvida sobre o nosso contato?" + Environment.NewLine + Environment.NewLine;
            return mensagem;
        }

        private string RoteiroAcordo()
        {
            string mensagem = "Prezado cliente, conforme combinamos, ficou agendado o pagamento do seu cartão Afinz ";
            if (agreement.QtParcel > 0)
            {
                mensagem += "no valor de entrada {0} para o dia {1} e {2} parcelas de {3}, conto com seu comprometimento para evitar as ligações de cobrança, ";
                mensagem += "bem como as correções de juros e encargos.";
                mensagem = string.Format(mensagem, agreement.VlEntrace.ToString("N2"), agreement.DtEntrace.ToString("dd/MM"), agreement.QtParcel.ToString(), agreement.VlParcel.ToString("N2"));
            }
            else
            {
                mensagem += "no valor de {0} para o dia {1}, conto com seu comprometimento para evitar as ligações de cobrança, ";
                mensagem += "bem como as correções de juros e encargos.";
                mensagem = string.Format(mensagem, agreement.VlEntrace.ToString("N2"), agreement.DtEntrace.ToString("dd/MM"));
            }
            mensagem += Environment.NewLine + "Ficou alguma dúvida sobre o nosso contato?" + Environment.NewLine + Environment.NewLine;

            return mensagem;
        }

    }
}