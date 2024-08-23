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
using FMC.Solutions.NPPLUS.Library.Class;
using DevExpress.XtraEditors.Controls;
using FMC.Solutions.NPPLUS.Library.APIDialer;
using FMC.Solutions.NPPLUS.Library.REST.Models;
using FMC.Solutions.NPPLUS.Library.REST;

namespace FMC.Solutions.NPPLUS
{
    public partial class CodificarPausa : DevExpress.XtraEditors.XtraForm
    {
        public CodificarPausa(ICollection<Pause> pausas)
        {
            InitializeComponent();
            PreencherPausas(pausas);
        }

        private void PreencherTela(ICollection<Pause> pausas)
        {
            if (pausas.Count > 0)
            {
                PreencherPausas(pausas);
            }
        }

        private void PreencherPausas(ICollection<Pause> pausas)
        {
            foreach (var pausa in pausas)
            {
                ImageComboBoxItem item = new ImageComboBoxItem();
                item.Description = pausa.DsPause;
                item.Value = pausa.IdAytyPause;
                cbPausa.Properties.Items.Add(item);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                string titulo = "Pausa";
                string destaque = "Tem certeza que deseja entrar para PAUSA?";
                string mensagem = "Ao confirmar, ficará armazenado em banco de dados. As ligações só serão liberadas após clicar em Liberar.";
                bool big = true;

                MessageBox concluir = new MessageBox(titulo, destaque, mensagem, big, "Não");
                DialogResult drConcluir = concluir.ShowDialog(this);
                if (drConcluir == DialogResult.OK)
                {
                    ImageComboBoxItem item = cbPausa.SelectedItem as ImageComboBoxItem;
                    DialerConnection.EnviarPausa(((Main)this.Owner).AgentDialer, item.Value.ToString());
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Aconteceu algum problema ao tentar codificar a pausa. Tente novamente.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cbPausa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Pausa_Load(object sender, EventArgs e)
        {
            
        }
    }
}