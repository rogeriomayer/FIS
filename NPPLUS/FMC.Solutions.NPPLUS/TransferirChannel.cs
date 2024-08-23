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
using FMC.Solutions.NPPLUS.Library.APIDialer;
using DevExpress.XtraEditors.Controls;



namespace FMC.Solutions.NPPLUS
{
    public partial class TransferirChannel : DevExpress.XtraEditors.XtraForm
    {
        public TransferirChannel()
        {
            InitializeComponent();
        }

        /*
        private void PreencherChannel(ICollection<TransferChannel> transferencias)
        {
            foreach (var transferencia in transferencias)
            {
                ImageComboBoxItem item = new ImageComboBoxItem();
                item.Description = transferencia.DsTransferChannel;
                item.Value = transferencia.Channel;
                cbChannel.Properties.Items.Add(item);
            }
        }
        */


        private void TransferirChannel_Load(object sender, EventArgs e)
        {
            /*
            TransferChannelBLL transferChannelBll = new TransferChannelBLL();
            ICollection<TransferChannel> transfer = transferChannelBll.GetAll();
            if (transfer.Count > 0)
            {
                PreencherChannel(transfer);
            }
            */
        }


        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                string titulo = "Transferência";
                string destaque = "Tem certeza que deseja transferir esta ligação?";
                string mensagem = "Ao confirmar, após a conclusão, o cliente será redirecionado para o canal selecionado.";
                bool big = true;

                MessageBox concluir = new MessageBox(titulo, destaque, mensagem, big, "Não");
                DialogResult drConcluir = concluir.ShowDialog(this);
                if (drConcluir == DialogResult.OK)
                {
                    ImageComboBoxItem item = cbChannel.SelectedItem as ImageComboBoxItem;
                    //          ((Main)this.Owner).transferChannel = new TransferChannel { DsTransferChannel = item.Description, Channel = item.Value.ToString() };
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Aconteceu algum problema ao tentar codificar a pausa. Tente novamente.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}