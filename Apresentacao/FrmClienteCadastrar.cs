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
    public partial class FrmClienteCadastrar : Form
    {
        private AcaoNaTela inserir;
        private object p;

        public FrmClienteCadastrar()
        {
            InitializeComponent();
        }

        public FrmClienteCadastrar(AcaoNaTela inserir, object p)
        {
            this.inserir = inserir;
            this.p = p;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
