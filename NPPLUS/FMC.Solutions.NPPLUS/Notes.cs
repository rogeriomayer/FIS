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

namespace FMC.Solutions.NPPLUS
{
    public partial class Notes : DevExpress.XtraEditors.XtraForm
    {
        public Notes()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {

        }

        private void cbPausa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Pausa_Load(object sender, EventArgs e)
        {
            this.Location = new Point(968,36);
        }
    }
}