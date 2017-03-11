using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apresentacao
{
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //instancia o form cliente selecionar
            FrmClienteSelecionar frmClienteSelecionar = new FrmClienteSelecionar();
            //Colocar o form cliente selecionar como form mdi parent do frmMenu
            frmClienteSelecionar.MdiParent = this;
            //comando ara mostrar o form cliente selecionar
            frmClienteSelecionar.Show();
        }

    }
}
