using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AcessoBancoDados;
using ObjetoTransferencia;

namespace Negocios
{
    public class ClienteNegocios
    {
        // Instanciando o objeto baseado em um modelo
        AcessoDadosSqlServer acessoDadosSqlServer = new AcessoDadosSqlServer();

        public string Inserir(Cliente cliente)
        {
            try
            {
                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametro("@Nome", cliente.Nome);
                acessoDadosSqlServer.AdicionarParametro("@DataNascimento", cliente.DataNascimento);
                acessoDadosSqlServer.AdicionarParametro("Sexo", cliente.Sexo);
                acessoDadosSqlServer.AdicionarParametro("LimiteCompra", cliente.LimiteCompra);
                string idCliente = acessoDadosSqlServer.ExecutarManipulacao(CommandType.StoredProcedure, "uspClienteInserir").ToString();
                return idCliente;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }             
        }
        public string Alterar(Cliente cliente)
        {
            try
            { 
            acessoDadosSqlServer.LimparParametros();
            acessoDadosSqlServer.AdicionarParametro("@Nome", cliente.Nome);
            acessoDadosSqlServer.AdicionarParametro("DataNascimento", cliente.DataNascimento);
            acessoDadosSqlServer.AdicionarParametro("Sexo", cliente.Sexo);
            acessoDadosSqlServer.AdicionarParametro("LimiteCompra", cliente.LimiteCompra);
            string idCliente = acessoDadosSqlServer.ExecutarManipulacao(CommandType.StoredProcedure, "uspClienteALterar").ToString();

            return idCliente;
            }
            catch(Exception ex)
            {
                return ex.Message;

            }
        }

        public string Excluir(Cliente cliente)
        {
            try
            {
                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametro("IdCliente", cliente.IdCliente);
                string idCliente = acessoDadosSqlServer.ExecutarManipulacao(CommandType.StoredProcedure, "uspClienteExcluir").ToString();
                return idCliente;
            }
            catch(Exception ex)
            {
                return ex.Message;
             
            }

        }
        public ClienteColecao ConsultarPorNome (string Nome)
        {
            try
            {
                //criar uma coleção nova de clientes (aqui ela esta vazia)
                ClienteColecao clienteColecao = new ClienteColecao();

                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametro("@Nome", Nome);
                //DataTable = tabela de dados (irá retornar com tipo de datable)
                DataTable dataTableCliente = acessoDadosSqlServer.ExecutarConsulta(CommandType.StoredProcedure, "uspClienteConsultarPorNome");
                //percorrer o datable e transformar em coleção de cliente

                foreach(DataRow linha in dataTableCliente.Rows)
                {
                    //criar um cliente vazio | colocar os dados na linha e adicionar à coleção

                    Cliente cliente = new Cliente();
                    cliente.IdCliente = Convert.ToInt32(linha["IdCliente"]);
                    cliente.Nome = Convert.ToString(linha["Nome"]);
                    cliente.DataNascimento = Convert.ToDateTime(linha["DataNascimento"]);
                    cliente.Sexo = Convert.ToBoolean(linha["Sexo"]);
                    cliente.LimiteCompra = Convert.ToDecimal(linha["LimiteCompra"]);

                    clienteColecao.Add(cliente);
                }
                return clienteColecao;
            }
            catch(Exception ex)
            {
                throw new Exception("Não foi possível consultar o cliente por Nome"+ ex.Message);                          
            }
        }
            public ClienteColecao ConsultaPorId (int IdCliente)
        {
            try
            {
                ClienteColecao clienteColecao = new ClienteColecao();

                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametro("@IdCliente", IdCliente);

                DataTable dataTableCliente = acessoDadosSqlServer.ExecutarConsulta(CommandType.StoredProcedure, "uspClienteConsultarPorId");

                foreach (DataRow linha in dataTableCliente.Rows)
                {
                    Cliente cliente = new Cliente();
                    cliente.IdCliente = Convert.ToInt32(linha["IdCliente"]);
                    cliente.Nome = Convert.ToString(linha["Nome"]);
                    cliente.DataNascimento = Convert.ToDateTime(linha["DataNascimento"]);
                    cliente.Sexo = Convert.ToBoolean(linha["Sexo"]);
                    cliente.LimiteCompra = Convert.ToDecimal(linha["LimiteCompra"]);

                    clienteColecao.Add(cliente);
                }


                return clienteColecao;
            }
            catch(Exception ex)
            {
                throw new Exception("Não foi possível consultar por Id" + ex.Message);
            }
        }


            
            
          
          
        
               

        
    }
}
