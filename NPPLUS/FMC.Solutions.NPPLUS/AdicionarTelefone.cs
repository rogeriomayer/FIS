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
using FMC.Solutions.NPPLUS.Library.Class.ViaCEP.Model;
using DevExpress.Skins;
using DevExpress.LookAndFeel;

namespace FMC.Solutions.NPPLUS
{
    public partial class AdicionarTelefone : DevExpress.XtraEditors.XtraForm
    {
        //private readonly Main main;
        private DialogResult msg;

        public AdicionarTelefone()
        {
            InitializeComponent();
        }

        private void AtualizarEndereco_Load(object sender, EventArgs e)
        {
            /*
            txtTelefone.Text = PhoneNumberFormat.PhoneNumber(((Main)this.Owner).arqn.Page2.HomePhoneNumber);
            txtCelular.Text = PhoneNumberFormat.PhoneNumber(((Main)this.Owner).arqn.Page2.MobilePhoneNumber);
            txtTelAdicional.Text = PhoneNumberFormat.PhoneNumber(((Main)this.Owner).arqn.Page2.FaxPhoneNumber);
            txtEmail.Text = ((Main)this.Owner).arqn.Page3.Email.Trim().ToLower();
            */

            if (Permission.Endereco.AdicionarTelefone)
                btnConfirmar.Enabled = true;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                this.ControlBox = false;
                btnConfirmar.Enabled = false;
                btnCancel.Enabled = false;
                //pbBoxBuscaCEP.Show();

                string celular = txtCelular.Text.Replace(" ", "").Replace("_", "").Replace("(", "").Replace(")", "").Replace("-", "");
                string tipo = cbOpcao.Text;
                if (((AtualizarEndereco)this.Owner).Phones.Where(p => p.NrPhone == celular).Count() <= 0)
                {
                    ((AtualizarEndereco)this.Owner).Phones.Add
                        (
                            new PhoneUpdateRequest()
                            {
                                NrPhone = celular,
                                PhoneType = tipo,
                                IdPhone = 0,
                                IdPhoneStatus = 3,
                                Remove = false,
                                Update = false,
                            }
                        );
                }
                this.DialogResult = DialogResult.OK;
                Close();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Erro ao atualizar cadastro.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ControlBox = true;

                if (Permission.Endereco.AdicionarTelefone)
                    btnConfirmar.Enabled = true;

                btnCancel.Enabled = true;
                Log.SaveFile("ERRO btnConfirmar_Click => " + ex.Message);

                Splash.Visible(splashScreenManager1, false);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCelular_Leave(object sender, EventArgs e)
        {
            try
            {
                string phone = txtCelular.Text.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Replace("_", "");
                if (txtCelular.Text.Replace("_", "").Length < 15)
                {
                    if (txtCelular.Properties.Mask.EditMask != "(00) 0000-00009")
                    {
                        txtCelular.Properties.Mask.EditMask = "(00) 0000-00009";
                    }
                    if (phone.Length == 10)
                    {
                        phone = "(" + phone.Substring(0, 2) + ") " + phone.Substring(2, 4) + "-" + phone.Substring(6, 4);
                        txtCelular.Text = phone;
                    }
                }
                else
                {
                    if (txtCelular.Properties.Mask.EditMask != "(00) 00000-0009")
                    {
                        txtCelular.Properties.Mask.EditMask = "(00) 00000-0009";
                        phone = "(" + phone.Substring(0, 2) + ") " + phone.Substring(2, 5) + "-" + phone.Substring(7, 4);
                        txtCelular.Text = phone;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.SaveFile(" EXCEPTION => [txtCelular_Leave] " + ex.Message);
            }
        }
    }
}