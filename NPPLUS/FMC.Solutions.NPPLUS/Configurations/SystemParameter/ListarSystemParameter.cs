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
using System.Collections.ObjectModel;
using DevExpress.XtraGrid.Views.Grid;

namespace FMC.Solutions.NPPLUS.Configurations.SystemParameter
{
    public partial class ListarSystemParameter : DevExpress.XtraEditors.XtraForm
    {
        //public User user;

        public ListarSystemParameter()
        {
            InitializeComponent();
        }

        private void GetParameter()
        {
            /*
            var systemParameterBll = new SystemParameterBLL();
            ICollection<IBI.DAO.Model.SystemParameter> lista = systemParameterBll.GetAll();

            if (lista.Count() > 0)
            {
                gcSystemParameter.DataSource = ListaParameter(lista);
                gcSystemParameter.RefreshDataSource();
            }
            else
            {
                gcSystemParameter.DataSource = null;
                gcSystemParameter.RefreshDataSource();
            }
            */
        }


        /*
        private ICollection<SParameter> ListaParameter(ICollection<IBI.DAO.Model.SystemParameter> lista)
        {
            ICollection<SParameter> parametros = new Collection<SParameter>();
            foreach (IBI.DAO.Model.SystemParameter item in lista)
            {
                parametros.Add(new SParameter
                {
                    IdSystemParameter = item.IdSystemParameter,
                    Parameter = item.Parameter,
                    Value = item.Value
                });
            }
            return parametros;
        }
        */

        private partial class SParameter
        {
            public int IdSystemParameter { get; set; }
            public string Parameter { get; set; }
            public string Value { get; set; }
        }

        private void Perfil_Load(object sender, EventArgs e)
        {
            // this.user = ((Main)this.Owner).User;
            GetParameter();
        }

        private void btnEditParameter_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit editor = (ButtonEdit)sender;
            GridView gv = gcSystemParameter.MainView as GridView;
            SParameter parameter = (SParameter)gv.GetFocusedRow();


            /*
            FMC.IBI.DAO.Model.SystemParameter sParameter = new SystemParameterBLL().GetBykey(parameter.IdSystemParameter);
            Cadastrar cadastrar = new Cadastrar(sParameter);

            DialogResult drResult = cadastrar.ShowDialog(this);
            if (drResult == DialogResult.OK)
            {
                GetParameter();
                //Recarregar lista
            }
            */
        }
    }

}