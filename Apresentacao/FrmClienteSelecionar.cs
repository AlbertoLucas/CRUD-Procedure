using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Negocios;
using ObjetoTransferencia;

namespace Apresentacao
{
    public partial class FrmClienteSelecionar : Form
    {
        public FrmClienteSelecionar()
        {
            InitializeComponent();
            dataGridViewPrincipal.AutoGenerateColumns = false;
        }
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            ClienteNegocios clienteNegocios = new ClienteNegocios();

            // Instanciando a classe  e Cliente Coleção para fazer a consulta e retornar uma coleção de clientes para o grid
            ClienteColecao clienteColecao = new ClienteColecao();
            clienteColecao = clienteNegocios.ConsultarPorNome(textBoxPesquisa.Text);

            // preenchendo o grid
            dataGridViewPrincipal.DataSource = null;
            dataGridViewPrincipal.DataSource = clienteColecao;

            //atualizar o grid
            dataGridViewPrincipal.Update();
            dataGridViewPrincipal.Refresh();
        }
        private void AtualizarGrid()
        {
            // Método para atualizar o grid - Instanciando a classe cliente negocios
            ClienteNegocios clienteNegocios = new ClienteNegocios();

            // Instanciando a classe  e Cliente Coleção para fazer a consulta e retornar uma coleção de clientes para o grid
            ClienteColecao clienteColecao = new ClienteColecao();
            clienteColecao = clienteNegocios.ConsultarPorNome(textBoxPesquisa.Text);

            // preenchendo o grid
            dataGridViewPrincipal.DataSource = null;
            dataGridViewPrincipal.DataSource = clienteColecao;

            //atualizar o grid
            dataGridViewPrincipal.Update();
            dataGridViewPrincipal.Refresh();
               
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            //Instanciar o formulário de cadastro
            FrmClienteCadastrar frmClienteCadastrar = new FrmClienteCadastrar(AcaoNaTela.Inserir,null);
            DialogResult dialogResult = frmClienteCadastrar.ShowDialog();
            if(dialogResult == DialogResult.Yes)
            {
                AtualizarGrid();
            }               
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            //validação para verificar se tem algum cliente selecionado para ser excluido
            if(dataGridViewPrincipal.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum cliente Selecionado");
                return;
            }
            //Pegar o Cliente selecionado no grid
            Cliente clienteSelecionado = (dataGridViewPrincipal.SelectedRows[0].DataBoundItem as Cliente);
            //instanciando o formulario de cadastro
            FrmClienteCadastrar frmClienteCadastrar = new FrmClienteCadastrar(AcaoNaTela.Alterar, clienteSelecionado);

            DialogResult resultado = frmClienteCadastrar.ShowDialog();
            if (resultado == DialogResult.Yes)
            {
                AtualizarGrid();
            }

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            //validação para verificar se existe algum cliente selecionado
            if(dataGridViewPrincipal.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum cliente foi selecionado");
                return;
            }
            //Confirmação se gostaria realmente excluir
            DialogResult resultado = MessageBox.Show("Tem certeza que deseja excluir? ", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(resultado == DialogResult.No)
            {
                return;
            }
            //Pegar o cliente selecionado no grid
            Cliente clienteSelecionado = (dataGridViewPrincipal.SelectedRows[0].DataBoundItem as Cliente);

            //Instancia a regra de negócio
            ClienteNegocios clienteNegocios = new ClienteNegocios();

            //Chamando o metodo de exclusão da regra  de negocio
            string retorno = clienteNegocios.Excluir(clienteSelecionado);
            
            //Verificação para ver se excluiu corretamente a informação
            try
            {
                int IdCliente = Convert.ToInt32(retorno);
                MessageBox.Show("Cliente excluido com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);

                AtualizarGrid();
            }
            catch
            {
                MessageBox.Show("Não foi possível excluir. Detalhes: " + retorno, "Atenção",MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
                        
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            //validação para confirmar se tem cliente selecionado
            if(dataGridViewPrincipal.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum cliente selecionado");
                return;
            }
            // Pegar o cliente selecionado no Grid
            Cliente clienteSelecionado = (dataGridViewPrincipal.SelectedRows[0].DataBoundItem as Cliente);

            //Instanciar o Formulario de cadastro
            FrmClienteCadastrar frmClienteCadastrar = new FrmClienteCadastrar(AcaoNaTela.Consultar, clienteSelecionado);
            frmClienteCadastrar.ShowDialog();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
