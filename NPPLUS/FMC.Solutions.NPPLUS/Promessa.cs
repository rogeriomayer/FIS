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
using System.Text.RegularExpressions;
using FMC.Solutions.NPPLUS.Library.REST.Models;
using FMC.Solutions.NPPLUS.Library.REST;

namespace FMC.Solutions.NPPLUS
{
    public partial class Promessa : XtraForm
    {
        //private DateTime? dataEntrada;
        private DialogResult msg;

        private ICollection<PromisseTypeResponse> TiposPromessa;

        public Promessa()
        {
            InitializeComponent();
        }

        public Promessa(Main _main)
        {
            InitializeComponent();
        }

        private void Promessa_Load(object sender, EventArgs e)
        {
            TiposPromessa = new HashSet<PromisseTypeResponse>();
            AjustaTela("Promessa");

            lblSaldo.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", ((Main)this.Owner).productSelected.Saldo);
            lblDiasAtraso.Text = ((Main)this.Owner).productSelected.DiasAtraso.ToString();

            txtDataPromessa.Properties.MinValue = DateTime.Today;
            txtDataPromessa.Properties.MaxValue = DateTime.Today.AddDays(4);

            txtDataEntrada.Properties.MinValue = DateTime.Today;
            txtDataEntrada.Properties.MaxValue = DateTime.Today.AddDays(4);

            cbTipoPromessa.SelectedIndex = 0;
        }

        private void AjustaTela(string tipoPromessa)
        {
            if (tipoPromessa.ToUpper().Contains("PARCELA"))
            {
                pnlParcelado.Visible = true;
                pnlPromessa.Visible = false;
                this.Size = new Size(338, 520);
                this.gcPromessa.Size = new Size(313, 320);
                TipoPromessa();
            }
            else
            {
                pnlParcelado.Visible = false;
                pnlPromessa.Visible = true;
                this.Size = new Size(338, 390);
                this.gcPromessa.Size = new Size(313, 190);
            }
            Application.DoEvents();

        }

        private void TipoPromessa()
        {
            TiposPromessa = FisAPI.GetTipoPromessa(((Main)this.Owner).productSelected.IdProductType, ((Main)this.Owner).User.OAuth.access_token);
            foreach (var tipo in TiposPromessa.Where(p => p.FlParcel).ToList())
                cbTipoParcelamento.Properties.Items.Add(tipo.DsPromisseType);
        }

        private void txtDataEntrada_DisableCalendarDate(object sender, DevExpress.XtraEditors.Calendar.DisableCalendarDateEventArgs e)
        {
            if (e.Date.DayOfWeek == DayOfWeek.Sunday)
                e.IsDisabled = true;
        }

        private void txtDataPromessa_DisableCalendarDate(object sender, DevExpress.XtraEditors.Calendar.DisableCalendarDateEventArgs e)
        {
            if (e.Date.DayOfWeek == DayOfWeek.Sunday)
                e.IsDisabled = true;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            var tipoPromessa = TiposPromessa.Where(p => p.DsPromisseType == cbTipoParcelamento.Text).FirstOrDefault();


            //dataEntrada = Convert.ToDateTime(txtDataPromessa.Text);


            if (tipoPromessa == null)
            {
                if (string.IsNullOrEmpty(txtDataPromessa.Text) || string.IsNullOrEmpty(txtValorPromessa.Text))
                {
                    XtraMessageBox.Show("Todos os campos precisam estar preenchidos para efetuar o registro.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtDataEntrada.Text) ||
                    string.IsNullOrEmpty(txtValorEntrada.Text) ||
                    string.IsNullOrEmpty(txtValorParcela.Text) ||
                    string.IsNullOrEmpty(cbParcelas.Text) ||
                    string.IsNullOrEmpty(cbTipoParcelamento.Text)
                    )
                {
                    XtraMessageBox.Show("Todos os campos precisam estar preenchidos para efetuar o registro.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            string titulo = "Registrar Promessa";
            string destaque = "Tem certeza que deseja formalizar esta PROMESSA?";
            string mensagem = GetTextoPromessa(tipoPromessa != null);

            MessageBox alert = new MessageBox(titulo, destaque, mensagem, true);
            msg = alert.ShowDialog();
            if (msg == DialogResult.OK)
            {
                try
                {
                    //Adiciona conta trabalhada


                    ((Main)this.Owner).WorkedProduct.Add(
                        new Library.Class.Model.WorkedProduct
                        {
                            Produto = ((Main)this.Owner).productSelected,
                            Tipo = Library.Class.Model.WorkedProduct.Type.Promessa,
                            CodFinalizacao = tipoPromessa != null ?
                                        ((Main)this.Owner).ParametersResponse.Where(P => P.Key == "PROMESSA_PARCELADA_CODE").FirstOrDefault().Value :
                                        ((Main)this.Owner).ParametersResponse.Where(P => P.Key == "PROMESSA_VISTA_CODE").FirstOrDefault().Value,
                            IdStatus = tipoPromessa != null ?
                                       Convert.ToInt32(((Main)this.Owner).ParametersResponse.Where(P => P.Key == "PROMESSA_PARCELADA_ID").FirstOrDefault().Value) :
                                       Convert.ToInt32(((Main)this.Owner).ParametersResponse.Where(P => P.Key == "PROMESSA_VISTA_ID").FirstOrDefault().Value),
                            IdStatusDialer = tipoPromessa != null ? "1001" : "1000",
                            DsStatusResum = mensagem,
                            TipoPromessa = cbTipoPromessa.SelectedItem.ToString(),
                            DetailPayment = new Library.Class.Model.DetailPayment
                            {
                                TipoPagamento = Library.Class.Model.DetailPayment.TypePayment.AVista,
                                DateEntrance = tipoPromessa != null ? Convert.ToDateTime(txtDataEntrada.Text) : Convert.ToDateTime(txtDataPromessa.Text),
                                QtdParcel = tipoPromessa != null ? Convert.ToByte(cbParcelas.Text) : Convert.ToByte(0),
                                VlEntrance = tipoPromessa != null ? Convert.ToDecimal(txtValorEntrada.Text) : Convert.ToDecimal(txtValorPromessa.Text),
                                VlParcel = tipoPromessa != null ? Convert.ToDecimal(txtValorParcela.Text) : decimal.Zero
                            },
                            StatusLead = new StatusLead()
                            {
                                IdLead = ((Main)this.Owner).productSelected.IdLead,
                                IdStatus = 4,
                                DsDescription = mensagem,
                                DtInsert = DateTime.Now,
                                IdUserLogin = ((Main)this.Owner).User.IdUser,
                                Promisse = new List<Promisse>()
                                {
                                        new Promisse
                                        {
                                            DtPromisse = tipoPromessa != null ? Convert.ToDateTime(txtDataEntrada.Text) : Convert.ToDateTime(txtDataPromessa.Text),
                                            VlPromisse = tipoPromessa != null ? Convert.ToDecimal(txtValorEntrada.Text) : Convert.ToDecimal(txtValorPromessa.Text),
                                            IdPromisseType = tipoPromessa != null ?  tipoPromessa.IdPromisseType : Convert.ToByte(1),
                                            NrParcel = tipoPromessa != null ? Convert.ToByte(cbParcelas.Text) : Convert.ToByte(0) ,
                                            VlParcel = tipoPromessa != null ? Convert.ToDecimal(txtValorParcela.Text) : decimal.Zero,
                                            DtInsert=DateTime.Now
                                        }
                                }
                            }

                        }
                    );


                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Erro ao registrar promessa.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.None;
                }
            }
            else
            {
                this.DialogResult = DialogResult.None;
            }

        }

        private string GetTextoPromessa(bool parcelado)
        {
            string texto = "";
            string dataHora = parcelado ? Convert.ToDateTime(txtDataEntrada.Text).ToString("dd/MM") : Convert.ToDateTime(txtDataPromessa.Text).ToString("dd/MM");
            string valor = parcelado ? Convert.ToDecimal(txtValorEntrada.Text).ToString("N2") : Convert.ToDecimal(txtValorPromessa.Text).ToString("N2");
            string nomeOperador = ((Main)this.Owner).User.DsName;

            if (parcelado)
            {
                texto = "Cliente informa pagamento da entrada para o dia {0} no valor R$ {1} com própria fatura/ CPF na Lotérica/ código de barras SMS ou via e-mail.Demais parcelas de R$ {2}.Total do parcelamento: {3} vezes. {4}";
                texto = string.Format(texto, dataHora, valor, Convert.ToDecimal(txtValorParcela.Text).ToString("N2"), cbParcelas.Text, nomeOperador);
            }
            else
            {
                texto = "Cliente informa pagamento para o dia {0} no valor R$ {1} com própria fatura/ CPF na Lotérica/ código de barras SMS ou via e-mail. {2}";
                texto = string.Format(texto, dataHora, valor, nomeOperador);
            }

            return texto;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbTipoPromessa_SelectedIndexChanged(object sender, EventArgs e)
        {
            AjustaTela(cbTipoPromessa.Text);
        }

        private void cbTipoParcelamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            int totalParcela = cbTipoParcelamento.Text.ToUpper().Contains("NORMAL") ? 24 : 12;
            for (int parc = 2; parc <= totalParcela; parc++)
                cbParcelas.Properties.Items.Add(parc.ToString().PadLeft(2, '0'));
        }

    }


}