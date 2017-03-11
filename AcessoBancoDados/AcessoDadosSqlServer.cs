using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data; //organizar 
using System.Data.SqlClient; //contem as classes para trabalhar com o sql server
using AcessoBancoDados.Properties; //acessando as propriedades do sistema para acesso a dados


namespace AcessoBancoDados
{
    public class AcessoDadosSqlServer
    {

        //criar a conexão
        private SqlConnection CriarConexao()
        {
            return new SqlConnection(Settings.Default.stringConexao);
        }
        //parâmetros que vão para o banco
        private SqlParameterCollection sqlParameterCollection = new SqlCommand().Parameters;

        public void LimparParametros()
        {
            sqlParameterCollection.Clear();
        }
        public void AdicionarParametro(string nomeParametro, object valorParametro)
        {
            sqlParameterCollection.Add(new SqlParameter(nomeParametro, valorParametro));
        }
        //persistencia - Inserir - alterar - excluir

        public object ExecutarManipulacao(CommandType commandType, string nomeStoreProcedureOuTextoSql)
        {
            try
            {
                //cria a conexão
                SqlConnection sqlconnection = CriarConexao();
                // abrir a conexão
                sqlconnection.Open();
                //criar o comando que vai levar a informação para o banco
                SqlCommand sqlCommand = sqlconnection.CreateCommand();
                //colocando as coisas dentro do comando (dentro da caixa que vai trafegar na conexão)
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeStoreProcedureOuTextoSql;
                sqlCommand.CommandTimeout = 7200; // em segundos

                //adicionar os parametros no comando;
                foreach(SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }
                //ciar um adaptador
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                //DataTable = tabela de dados vazia onde vou colocar os dados que vem do banco
                DataTable datatable = new DataTable();

                //Mandar o comando ir até o banco buscar os dados e o adaptador preenche o datatable
                sqlDataAdapter.Fill(datatable);
                return datatable;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        // consultar os registros do banco de dados

        public DataTable ExecutarConsulta(CommandType commandType, string nomeStoreProcedureOuTextoSql)
        {
            try
            {
                //criando a conexao
                SqlConnection sqlconnection = CriarConexao();
                //Abrir conexão
                sqlconnection.Open();
                //criar o comando que vai levar a informação para o banco
                SqlCommand sqlcommand = sqlconnection.CreateCommand();
                //colocando as coisas dentro do comando (dentro da caixa que vai trafegar na conexao)
                sqlcommand.CommandType = commandType;
                sqlcommand.CommandText = nomeStoreProcedureOuTextoSql;
                sqlcommand.CommandTimeout = 7200; // em segundos

                //Adicionando os parametros no comando
                foreach(SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlcommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }
                //criando um adaptador
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlcommand);

                //DataTable = tabela de ddos vazia onde vou colocar os dados que vem do banco
                DataTable dataTable = new DataTable();
                //Mandar o comando ir até o banco buscar os dados e o adaptador preencher o dataTable
                //mandar o adapatador ir ao banco e preencher o dataTable
                sqlDataAdapter.Fill(dataTable);

                return dataTable;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }          
        }
    }
}
