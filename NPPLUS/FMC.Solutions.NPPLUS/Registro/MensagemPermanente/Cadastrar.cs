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
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using System.Collections.ObjectModel;

namespace FMC.Solutions.NPPLUS.Registro.MensagemPermanente
{
    public partial class Cadastrar : DevExpress.XtraEditors.XtraForm
    {
        //private AccessProfile accessProfile;
        private DialogResult msg;

        public Cadastrar()
        {
            InitializeComponent();
        }

        /*
        public Cadastrar(AccessProfile _accessProfile)
        {
            this.accessProfile = _accessProfile;
            InitializeComponent();
        }
        */

        private void Cadastrar_Load(object sender, EventArgs e)
        {
            /*
            if (accessProfile != null)
            {
                if (!Permission.Perfil.Atualizar)
                    btnConfirmar.Enabled = false;
            }
            */
        }

        private void treeList_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            var node = e.Node;
            if (node.Level == -1 || node.Level == 0)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                string action = cbAction.SelectedItem.ToString();
                string reference = cbRef.SelectedItem.ToString();
                string note = memo.Text.Replace("\n", "").Replace("\r", "").Trim();

                if (string.IsNullOrEmpty(action) || string.IsNullOrEmpty(reference) || string.IsNullOrEmpty(note))
                {
                    XtraMessageBox.Show("É necessário preencher todos os campos para inserir a mensagem.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox insert = new MessageBox("Confirmar Inserção?", "Tem certeza que deseja inserir esta mensagem?", "Ao confirmar, ela será mostrada em mensagens permanentes.", false, "Não", "Sim");
                    DialogResult msg = insert.ShowDialog(); //XtraMessageBox.Show("Tem certeza que deseja sair da aplicação?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (msg == DialogResult.OK)
                    {
                        Splash.Visible(splashScreenManager1, true);
                        splashScreenManager1.SetWaitFormDescription("Inserindo novo registro...");
                        string[] sAction = action.Split('-');
                        string[] sReference = reference.Split('-');
                        note = Util.RemoverAcentos(note);
                        /*
                         * ASMW asmw = SessionP2.Session.ASMW(((Listar)this.Owner).account);
                        if (!asmw.SetAction(sAction[0].Trim(), sReference[0].Trim(), "", note, "", "", "", "", "", ""))
                        {
                            XtraMessageBox.Show("É necessário preencher todos os campos para inserir a mensagem.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            this.DialogResult = DialogResult.None;
                        }
                        */
                        Splash.Visible(splashScreenManager1, false);
                    }
                    else
                    {
                        this.DialogResult = DialogResult.None;
                    }
                }
            }
            catch (Exception ex)
            {
                this.DialogResult = DialogResult.None;
            }
            finally
            {
                Splash.Visible(splashScreenManager1, false);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }


}