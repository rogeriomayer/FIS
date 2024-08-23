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
using System.Text.RegularExpressions;
using DevExpress.XtraEditors.Repository;
using FMC.Solutions.NPPLUS.Library.REST;
using DevExpress.XtraEditors.Controls;

namespace FMC.Solutions.NPPLUS
{
    public partial class AtualizarEndereco : DevExpress.XtraEditors.XtraForm
    {
        private DialogResult msg;
        public ViaCEP address;

        public IList<PhoneUpdateRequest> Phones = new List<PhoneUpdateRequest>();


        public AtualizarEndereco()
        {
            InitializeComponent();
            //IconsRepository();
        }

        private void btnPesquisarCEP_Click(object sender, EventArgs e)
        {
            btnPesquisarCEP.Enabled = false;
            btnConfirmar.Enabled = false;
            bwCEP.RunWorkerAsync();
        }

        private void PesquisaCEP()
        {
            string cep = txtCep.Text.Trim().Replace(".", "").Replace("-", "");

            try
            {
                WSCorreios.AtendeClienteClient ws = new WSCorreios.AtendeClienteClient();
                WSCorreios.enderecoERP endereco = ws.consultaCEP(cep);
                //GoogleMapsApiUtil.EnderecoGoogleMaps endereco = GoogleMapsApiUtil.GetEnderecoByCep(cep);

                bwCEP.ReportProgress(0, new object[] { "PopulaEndereco", endereco });
            }
            catch (Exception ex)
            {
                string msg = string.Format("Erro ao efetuar busca do CEP: {0}", ex.Message);
                bwCEP.ReportProgress(0, new object[] { "Erro", msg });
            }
        }

        private void PopulaEndereco(WSCorreios.enderecoERP end)
        {
            if (end != null)
            {
                txtLogradouro.Text = end.end;
                txtBairro.Text = end.bairro;
                txtCidade.Text = end.cidade;
                txtEstado.Text = end.uf;
                //txtLogradouro.Text = end.Logradouro;
                //txtBairro.Text = end.Bairro;
                //txtCidade.Text = end.Cidade;
                //txtEstado.Text = end.UF;
            }
        }

        private void bwCEP_DoWork(object sender, DoWorkEventArgs e)
        {
            if (bwCEP.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                PesquisaCEP();
            }
        }

        private void bwCEP_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            object[] obj = (object[])e.UserState;
            if (obj[0].ToString() == "PopulaEndereco")
            {
                PopulaEndereco((WSCorreios.enderecoERP)obj[1]);
            }
            else if (obj[0].ToString() == "Erro")
            {
                XtraMessageBox.Show((string)obj[1], "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bwCEP_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                //XtraMessageBox.Show("Cancelado.");

                if (Permission.Endereco.Atualizar)
                    btnConfirmar.Enabled = true;

                btnPesquisarCEP.Enabled = true;
            }
            else if (!(e.Error == null))
            {
                XtraMessageBox.Show(e.Error.Message);
                if (Permission.Endereco.Atualizar)
                    btnConfirmar.Enabled = true;

                btnPesquisarCEP.Enabled = true;
            }
            else
            {
                bwCEP.CancelAsync();
                if (Permission.Endereco.Atualizar)
                    btnConfirmar.Enabled = true;

                btnPesquisarCEP.Enabled = true;
                SetColor();
            }
        }

        private void AtualizarEndereco_Load(object sender, EventArgs e)
        {
            var person = ((Main)this.Owner).Person;

            if (person.Address != null)
            {
                var address = person.Address;

                txtCep.Text = Convert.ToUInt64(Regex.Replace(address.Cep, "[^0-9]", "")).ToString("D8");
                txtLogradouro.Text = Util.RemoverAcentos(address.Address);
                txtNumero.Text = Util.RemoverAcentos(address.NrAddress);
                txtComplemento.Text = Util.RemoverAcentos(address.Complement);
                txtBairro.Text = Util.RemoverAcentos(address.District);
                txtCidade.Text = Util.RemoverAcentos(address.City);
                txtEstado.Text = Util.RemoverAcentos(address.UF);
                txtEmail.Text = Util.RemoverAcentos(person.Email.ToLower());
                //telefones
                Phones = person.Phones.Select
                    (
                        p => new PhoneUpdateRequest()
                        {
                            NrPhone = p.NrPhone,
                            PhoneType = p.PhoneType,
                            IdPhone = p.IdPhone,
                            IdPhoneStatus = p.IdPhoneStatus,
                            Update = false,
                        }
                    ).ToList();

                PopulaBoxTelefone();
                btnPesquisarCEP.Enabled = true;

                if (Permission.Endereco.Atualizar)
                    btnConfirmar.Enabled = true;

                SetColor();
            }
        }

        private void PopulaBoxTelefone()
        {
            try
            {
                ICollection<Telefone> listaTelefone = new HashSet<Telefone>();

                foreach (var phone in Phones.ToList())
                {
                    string p = phone.NrPhone;
                    if (p.Length >= 11)
                        p = Convert.ToUInt64(Regex.Replace(p, "[^0-9]", "")).ToString("(00) 00000-0000");
                    else if (p.Length >= 10)
                        p = Convert.ToUInt64(Regex.Replace(p, "[^0-9]", "")).ToString("(00) 0000-0000");

                    if (p.Length >= 10)
                    {
                        if (!listaTelefone.Any(ph => Regex.Replace(ph.Numero, "[^0-9]", "") == Regex.Replace(p, "[^0-9]", "")))
                            listaTelefone.Add(
                                new Telefone
                                {
                                    idTelefone = phone.IdPhone,
                                    Numero = p,
                                    Tipo = phone.PhoneType,
                                    Ativo = phone.IdPhoneStatus != 4,
                                    Preferencial = phone.IdPhoneStatus == 1

                                });
                    }
                }

                dgvTelefones.DataSource = null;
                dgvTelefones.DataSource = listaTelefone.ToList();
                dgvTelefones.Refresh();
                Application.DoEvents();
            }
            catch (Exception e)
            {
                Log.SaveFile("ERRO AO POPULAR BOX TELEFONE [PopulaBoxTelefone]");
                throw e;
            }
        }

        private void SetColor()
        {
            Skin Skin = CommonSkins.GetSkin(UserLookAndFeel.Default);
            Color themeForeColor = Skin.Colors.GetColor("ControlText");
            if (txtLogradouro.Text.Length > 200)
            {
                txtLogradouro.ForeColor = Color.Red;
                txtLogradouro.ToolTip = "Número máximo de caracteres permitido é 40.\nRemova " + (txtLogradouro.Text.Length - 40) + " caracteres."; ;
                txtLogradouro.ToolTipTitle = "Atenção!";
                txtLogradouro.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Asterisk;
                txtLogradouro.ShowToolTips = true;
            }
            else
            {
                txtLogradouro.ForeColor = themeForeColor;
                txtLogradouro.ShowToolTips = false;
            }

            if (txtComplemento.Text.Length > 200)
            {
                txtComplemento.ForeColor = Color.Red;
                txtComplemento.ToolTip = "Número máximo de caracteres permitido é 15.\nRemova " + (txtComplemento.Text.Length - 15) + " caracteres.";
                txtComplemento.ToolTipTitle = "Atenção!";
                txtComplemento.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Asterisk;
                txtComplemento.ShowToolTips = true;
            }
            else
            {
                txtComplemento.ForeColor = themeForeColor;
                txtComplemento.ShowToolTips = false;
            }

            if (txtBairro.Text.Length > 200)
            {
                txtBairro.ForeColor = Color.Red;
                txtBairro.ToolTip = "Número máximo de caracteres permitido é 20.\nRemova " + (txtBairro.Text.Length - 20) + " caracteres.";
                txtBairro.ToolTipTitle = "Atenção!";
                txtBairro.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Asterisk;
                txtBairro.ShowToolTips = true;
            }
            else
            {
                txtBairro.ForeColor = themeForeColor;
                txtBairro.ShowToolTips = false;
            }

            if (txtCidade.Text.Length > 100)
            {
                txtCidade.ForeColor = Color.Red;
                txtCidade.ToolTip = "Número máximo de caracteres permitido é 30.\nRemova " + (txtCidade.Text.Length - 30) + " caracteres.";
                txtCidade.ToolTipTitle = "Atenção!";
                txtCidade.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Asterisk;
                txtCidade.ShowToolTips = true;
            }
            else
            {
                txtCidade.ForeColor = themeForeColor;
                txtCidade.ShowToolTips = false;
            }


        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                this.ControlBox = false;
                btnConfirmar.Enabled = false;
                btnCancel.Enabled = false;
                btnPesquisarCEP.Enabled = false;
                //pbBoxBuscaCEP.Show();

                var personUpdateRequest = new PersonUpdateRequest();

                personUpdateRequest.IdPerson = ((Main)this.Owner).Person.IdPerson;

                personUpdateRequest.IdUserLogin = ((Main)this.Owner).User.IdUser;

                personUpdateRequest.AddressUpdateRequest = new AddressUpdateRequest();

                personUpdateRequest.AddressUpdateRequest.Cep = Regex.Replace(txtCep.Text.Trim(), "[^0-9]", "");
                personUpdateRequest.AddressUpdateRequest.Address = Util.RemoverAcentos(txtLogradouro.Text.Trim());
                personUpdateRequest.AddressUpdateRequest.NrAddress = Util.RemoverAcentos(txtNumero.Text.Trim());
                personUpdateRequest.AddressUpdateRequest.Complement = Util.RemoverAcentos(txtComplemento.Text.Trim());
                personUpdateRequest.AddressUpdateRequest.District = Util.RemoverAcentos(txtBairro.Text.Trim());
                personUpdateRequest.AddressUpdateRequest.City = Util.RemoverAcentos(txtCidade.Text.Trim());
                personUpdateRequest.AddressUpdateRequest.UF = Util.RemoverAcentos(txtEstado.Text.Trim());

                if ((!string.IsNullOrWhiteSpace(txtEmail.Text)) && !Util.IsEmail(txtEmail.Text))
                {
                    XtraMessageBox.Show("O campo E-mail está preenchido incorretamente. Verifique-os, preencha corretamente e tente novamente.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.None;
                    this.ControlBox = true;

                    if (Permission.Endereco.Atualizar)
                        btnConfirmar.Enabled = true;

                    btnPesquisarCEP.Enabled = true;
                    btnCancel.Enabled = true;
                    return;
                }
                else
                {
                    personUpdateRequest.Email = txtEmail.Text.Trim().ToLower();
                }

                if (string.IsNullOrEmpty(personUpdateRequest.AddressUpdateRequest.Cep) ||
                    string.IsNullOrEmpty(personUpdateRequest.AddressUpdateRequest.Address) ||
                    string.IsNullOrEmpty(personUpdateRequest.AddressUpdateRequest.NrAddress) ||
                    string.IsNullOrEmpty(personUpdateRequest.AddressUpdateRequest.District) ||
                    string.IsNullOrEmpty(personUpdateRequest.AddressUpdateRequest.City) ||
                    string.IsNullOrEmpty(personUpdateRequest.AddressUpdateRequest.UF))
                {
                    XtraMessageBox.Show("Algum campo está preenchido incorretamente. Verifique-os, preencha corretamente e tente novamente.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.None;
                    this.ControlBox = true;

                    if (Permission.Endereco.Atualizar)
                        btnConfirmar.Enabled = true;

                    btnPesquisarCEP.Enabled = true;
                    btnCancel.Enabled = true;
                }
                else
                {

                    if (Atualiza(personUpdateRequest, ((Main)this.Owner).productSelected.IdProductType))
                    {
                        ((Main)this.Owner).Person.Address.Cep = Regex.Replace(txtCep.Text.Trim(), "[^0-9]", "");
                        ((Main)this.Owner).Person.Address.Address = Util.RemoverAcentos(txtLogradouro.Text.Trim());
                        ((Main)this.Owner).Person.Address.NrAddress = Util.RemoverAcentos(txtNumero.Text.Trim());
                        ((Main)this.Owner).Person.Address.Complement = Util.RemoverAcentos(txtComplemento.Text.Trim());
                        ((Main)this.Owner).Person.Address.District = Util.RemoverAcentos(txtBairro.Text.Trim());
                        ((Main)this.Owner).Person.Address.City = Util.RemoverAcentos(txtCidade.Text.Trim());
                        ((Main)this.Owner).Person.Address.UF = Util.RemoverAcentos(txtEstado.Text.Trim());
                        ((Main)this.Owner).Person.Email = txtEmail.Text.Trim().ToLower();

                        foreach (var phone in Phones)
                        {
                            if (((Main)this.Owner).Person.Phones.Where(p => p.NrPhone == phone.NrPhone).Count() <= 0)
                                ((Main)this.Owner).Person.Phones.Add
                                    (
                                        new PhoneResponse()
                                        {
                                            NrPhone = phone.NrPhone,
                                            PhoneType = phone.PhoneType,
                                            IdPhoneStatus = phone.IdPhoneStatus,
                                        }
                                    );
                        }

                        this.DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        //bwAtualizar.RunWorkerAsync();
                        this.DialogResult = DialogResult.None;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Erro ao atualizar cadastro.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ControlBox = true;

                if (Permission.Endereco.Atualizar)
                    btnConfirmar.Enabled = true;

                btnPesquisarCEP.Enabled = true;
                btnCancel.Enabled = true;
                Log.SaveFile("ERRO btnConfirmar_Click => " + ex.Message);

                Splash.Visible(splashScreenManager1, false);
            }
        }

        /*
        protected void btnExcluir_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                ButtonEdit editor = (ButtonEdit)sender;
                DevExpress.XtraEditors.Controls.EditorButton Button = e.Button;
                GridView gv = gcTelefones.MainView as GridView;


                ((RepositoryItemButtonEdit)gv.Columns[2].RealColumnEdit).Buttons[0].Enabled = false;
                ((RepositoryItemButtonEdit)gv.Columns[2].RealColumnEdit).Buttons[0].Enabled = true;

                Application.DoEvents();
            }
            catch (Exception ex)
            {

            }
        }
        */

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtComplemento_KeyUp(object sender, KeyEventArgs e)
        {
            SetColor();
        }

        private void txtBairro_KeyUp(object sender, KeyEventArgs e)
        {
            SetColor();
        }

        private void txtCidade_KeyUp(object sender, KeyEventArgs e)
        {
            SetColor();
        }

        private void txtLogradouro_KeyUp(object sender, KeyEventArgs e)
        {
            SetColor();
        }
        private bool Atualiza(PersonUpdateRequest personUpdateRequest, int productType)
        {
            try
            {
                Splash.Visible(splashScreenManager1, true);

                splashScreenManager1.SetWaitFormDescription("Atualizando dados cadastrais...");
                personUpdateRequest.PhoneUpdateRequest.Clear();
                personUpdateRequest.PhoneUpdateRequest = Phones;
                /*
                foreach (var phone in Phones)
                {
                    personUpdateRequest.PhoneUpdateRequest.Add
                        (
                            new PhoneUpdateRequest()
                            {
                                IdPhone = phone.IdPhone,
                                NrPhone = phone.NrPhone,
                                PhoneType = phone.PhoneType,
                                IdPhoneStatus = phone.IdPhoneStatus,
                                Remove = false,
                                Update = false
                            }
                        );
                }
                */

                var response = FisAPI.PostPerson(personUpdateRequest, productType, ((Main)this.Owner).User.OAuth.access_token);

                if (Permission.Endereco.Atualizar)
                    btnConfirmar.Enabled = true;

                btnPesquisarCEP.Enabled = true;
                btnCancel.Enabled = true;
                this.DialogResult = DialogResult.OK;
                Splash.Visible(splashScreenManager1, false);
                return true;
            }
            catch (Exception ex)
            {
                Splash.Visible(splashScreenManager1, false);

                if (Permission.Endereco.Atualizar)
                    btnConfirmar.Enabled = true;

                btnPesquisarCEP.Enabled = true;
                btnCancel.Enabled = true;

                Exception exception = ex;
                string error = exception.Message;

                while (exception.InnerException != null)
                {
                    exception = exception.InnerException;
                    error += exception.StackTrace + Environment.NewLine;
                    error += exception.Message + " " + Environment.NewLine;
                }

                XtraMessageBox.Show(error, "Erro ao atualizar dados cadastrais!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }


        private void btnAddTelefone_Click(object sender, EventArgs e)
        {
            try
            {
                AdicionarTelefone at = new AdicionarTelefone();
                DialogResult drResult = at.ShowDialog(this);
                if (drResult == DialogResult.OK)
                {
                    Splash.Visible(splashScreenManager1, true);
                    splashScreenManager1.SetWaitFormDescription("Atualizando informações cadastrais...");
                    PopulaBoxTelefone();
                    Splash.Visible(splashScreenManager1, false);
                }
            }
            catch (Exception ex)
            {
                Log.SaveFile("Erro ao atualizar telefones");
            }
            finally
            {
                Splash.Visible(splashScreenManager1, false);
            }
        }

        private void groupControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void NewPhone(string phone, int type)
        {
            try
            {
                /*
                Log.SaveFile("*** UpdatePhone ***");
                if (!string.IsNullOrWhiteSpace(phone))
                {
                    Phone telefone = new Phone();
                    telefone.NrPhone = phone;
                    telefone.IdAccount = ((Main)this.Owner).Lead.Product.IdAccount;
                    telefone.IdPhoneType = type;
                    telefone.IdPhoneStatus = 2;
                    telefone.DtInsert = DateTime.Now;
                    new PhoneBLL().Add(telefone);
                }
                */
            }
            catch (Exception ex)
            {
                Log.SaveFile("ERRO => NewPhone -> " + ex.Message);
            }
        }



        private void btnSearchAddress_Click(object sender, EventArgs e)
        {
            try
            {
                PesquisarLogradouro pl = new PesquisarLogradouro(this, txtLogradouro.Text.Trim(), txtCidade.Text.Trim(), txtEstado.Text.Trim());
                DialogResult drResult = pl.ShowDialog();
                if (drResult == DialogResult.OK)
                    PopulaEndereco(address);
            }
            catch (Exception ex)
            {
                Log.SaveFile(" EXCEPTION => [btnSearchAddress_Click] " + ex.Message);
            }
        }

        private void PopulaEndereco(ViaCEP end)
        {
            if (end != null)
            {
                txtCep.Text = end.ZipCode;
                txtLogradouro.Text = end.Address1;
                txtBairro.Text = end.Neighborhood;
                txtCidade.Text = end.City;
                txtEstado.Text = end.State;
            }
        }

        private void dgvTelefones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                foreach (var phone in Phones.ToList())
                {
                    string p = phone.NrPhone;
                    if (p.Length >= 11)
                        p = Convert.ToUInt64(Regex.Replace(p, "[^0-9]", "")).ToString("(00) 00000-0000");
                    else if (p.Length >= 10)
                        p = Convert.ToUInt64(Regex.Replace(p, "[^0-9]", "")).ToString("(00) 0000-0000");

                    var tel = dgvTelefones.Rows[dgvTelefones.SelectedCells[0].RowIndex].Cells[0].Value;

                    if (tel.ToString() == p)
                        phone.IdPhoneStatus = 1;
                    else
                    {
                        if (phone.IdPhoneStatus == 1)
                            phone.IdPhoneStatus = 3;
                    }

                }
                PopulaBoxTelefone();
            }
            else if (e.ColumnIndex == 3)
            {
                foreach (var phone in Phones.ToList())
                {
                    string p = phone.NrPhone;
                    if (p.Length >= 11)
                        p = Convert.ToUInt64(Regex.Replace(p, "[^0-9]", "")).ToString("(00) 00000-0000");
                    else if (p.Length >= 10)
                        p = Convert.ToUInt64(Regex.Replace(p, "[^0-9]", "")).ToString("(00) 0000-0000");

                    var tel = dgvTelefones.Rows[dgvTelefones.SelectedCells[0].RowIndex].Cells[0].Value;
                    var celChecked = dgvTelefones.Rows[dgvTelefones.SelectedCells[0].RowIndex].Cells[3].Value;

                    if (tel.ToString() == p)
                    {
                        if (celChecked.ToString().ToLower() == "true")
                        {
                            if (phone.IdPhone > 0)
                                phone.IdPhoneStatus = 4;
                            else
                            {
                                Phones.Remove(phone);
                                PopulaBoxTelefone();
                                return;
                            }
                        }
                        else
                            phone.IdPhoneStatus = 3;
                    }
                }
                PopulaBoxTelefone();
            }

        }
    }
}