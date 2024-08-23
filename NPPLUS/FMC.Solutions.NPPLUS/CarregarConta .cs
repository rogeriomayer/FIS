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

using System.Text.RegularExpressions;

namespace FMC.Solutions.NPPLUS
{
    public partial class CarregarConta : DevExpress.XtraEditors.XtraForm
    {
        private string cpf;
        public CarregarConta(string _cpf)
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(_cpf) && !string.IsNullOrWhiteSpace(_cpf))
            {
                this.cpf = _cpf;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (((Main)this.Owner).TipoLigacao == "INBOUND")
                ((Main)this.Owner).FlCloseCall = true;
            this.Close();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarBusca())
                {
                    string numero = Regex.Replace(txtNumero.Text.Trim(), "[^0-9]", "");

                    ((Main)this.Owner).TipoPesquisa = (Main.LoadType)cbTipo.SelectedIndex;
                    ((Main)this.Owner).NumeroPesquisa = numero;
                    this.Close();
                }
                else
                {
                    XtraMessageBox.Show("É necessário selecionar um tipo e informar o número corretamente para realizar a busca!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Log.SaveFile("BTN CONFIRMAR - CARREGAR CONTA => EXCEPTION ===>" + ex.Message);
                XtraMessageBox.Show("Aconteceu uma falha, tente novamente!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private bool ValidarBusca()
        {
            try
            {
                string numero = Regex.Replace(txtNumero.Text.Trim(), "[^0-9]", "");
                if (cbTipo.SelectedIndex == 0 && numero.Length == 11)
                {
                    return true;
                }
                else if (cbTipo.SelectedIndex == 1 && numero.Length == 19)
                {
                    return true;
                }
                else if (cbTipo.SelectedIndex == 2 && numero.Length == 16)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.SaveFile("VALIDAR BUSCA EXCEPTION ===>" + ex.Message);
                return false;
            }
        }

        private void ValidarSelectTipo()
        {
            string numero = txtNumero.Text;
            numero = numero.Replace(".", "").Replace("-", "").Replace(" ", "");
            if (cbTipo.SelectedIndex == 0)
            {
                txtNumero.Properties.MaxLength = 14;
                txtNumero.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
                txtNumero.Properties.Mask.EditMask = "000.000.000-00";
                txtNumero.Properties.Mask.UseMaskAsDisplayFormat = true;

                if (numero.Length >= 11)
                    numero = numero.Substring(0, 11);
                txtNumero.Text = string.Format("{0:000.000.000-00}", numero);

            }
            else if (cbTipo.SelectedIndex == 1)
            {
                txtNumero.Properties.MaxLength = 21;
                txtNumero.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
                txtNumero.Properties.Mask.EditMask = "999 0000000000000 000";
                txtNumero.Properties.Mask.UseMaskAsDisplayFormat = true;

                if (numero.Length >= 19)
                    numero = numero.Substring(0, 19);
                if (numero.Length >= 3)
                {
                    if (numero.Substring(0, 3) == "000")
                        txtNumero.Text = string.Format("{0:999 0000000000000 000}", numero);
                    else
                        txtNumero.Text = string.Format("000{0: 0000000000000 000}", numero);
                }

            }
            else
            {
                txtNumero.Properties.MaxLength = 19;
                txtNumero.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
                txtNumero.Properties.Mask.EditMask = "0000 0000 0000 0000";
                txtNumero.Properties.Mask.UseMaskAsDisplayFormat = true;

                if (numero.Length >= 16)
                    numero = numero.Substring(0, 16);
                txtNumero.Text = string.Format("{0:0000 0000 0000 0000}", numero);
            }
        }

        private void cbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidarSelectTipo();
        }

        private void txtNumero_Leave(object sender, EventArgs e)
        {
            string numero = txtNumero.Text;
            numero = numero.Replace(".", "").Replace("-", "").Replace(" ", "").Replace("_", "");
            if (cbTipo.SelectedIndex == 1)
            {
                if (numero.Length >= 19)
                    numero = numero.Substring(0, 19);
                if (numero.Length > 3)
                {
                    if (numero.Substring(0, 3) == "000")
                        txtNumero.Text = string.Format("{0:999 0000000000000 000}", numero);
                    else
                        txtNumero.Text = string.Format("000{0: 0000000000000 000}", numero);
                }
            }
        }

        private void txtNumero_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ValidarSelectTipo();
                btnConfirmar.PerformClick();
            }
        }

        private void CarregarConta_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.cpf))
            {
                txtNumero.EditValue = this.cpf;
                txtNumero.Text = this.cpf;
            }

            if (((Main)this.Owner).TipoLigacao == "INBOUND")
                btnCancel.Text = "Finalizar Chamada";
        }
    }
}