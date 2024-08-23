using DevExpress.XtraEditors;
using FMC.Solutions.NPPLUS.Library.Class;
using System;
using System.Windows.Forms;

namespace FMC.Solutions.NPPLUS.Configurations.SystemParameter
{
    public partial class Cadastrar : DevExpress.XtraEditors.XtraForm
    {
        //private IBI.DAO.Model.SystemParameter systemParameter;
        private DialogResult msg;

        public Cadastrar()
        {
            InitializeComponent();
        }

        /*
        public Cadastrar(IBI.DAO.Model.SystemParameter _systemParameter)
        {
            this.systemParameter = _systemParameter;
            InitializeComponent();
        }
        */


        private void CadastrarParametro(string parametro, string valor)
        {
            //SystemParameterBLL systemParameterBll = new SystemParameterBLL();
            //systemParameterBll.Add(new IBI.DAO.Model.SystemParameter { Parameter = parametro, Value = valor });
        }

        private void Atualizar(string parametro, string valor)
        {
            /*
            SystemParameterBLL systemParameterBll = new SystemParameterBLL();
            var sParameter = systemParameterBll.GetBykey(systemParameter.IdSystemParameter);

            sParameter.Parameter = parametro;
            sParameter.Value = valor;

            systemParameterBll.Update(sParameter);
            */
        }


        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                string parametro = txtParameter.Text.Trim();
                string valor = txtValueParameter.Text.Trim();

                if (string.IsNullOrEmpty(parametro) && string.IsNullOrEmpty(valor))
                {
                    XtraMessageBox.Show("É obrigatório preencher o Nome do Parâmetro e o Valor do Parâmetro.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.None;
                }
                else
                {
                    Splash.Visible(splashScreenManager1, true);
                    splashScreenManager1.SetWaitFormDescription("Registrando informações...");

                    /*
                    if (systemParameter == null)
                        CadastrarParametro(parametro, valor);
                    else
                        Atualizar(parametro, valor);
                    */

                    Splash.Visible(splashScreenManager1, false);
                }
            }
            catch (Exception ex)
            {
                Splash.Visible(splashScreenManager1, false);
                XtraMessageBox.Show("Erro ao inserir/atualizar parâmetro.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cadastrar_Load(object sender, EventArgs e)
        {
            /*
            if (systemParameter != null)
            {
                txtParameter.Text = systemParameter.Parameter;
                txtValueParameter.Text = systemParameter.Value;
                if (!Permission.Usuario.Atualizar)
                    btnConfirmar.Enabled = false;
            }
            */
        }
    }


}