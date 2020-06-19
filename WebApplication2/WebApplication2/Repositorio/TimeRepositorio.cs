using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication2.Models;

//Repositório para a String de conexão
namespace WebApplication2.Repositorio 
{
    public class TimeRepositorio
    {
        private SqlConnection _con;
        private void Connection() // Objeto de conexão
        {
            string connstr = ConfigurationManager.ConnectionStrings["stringConexao"].ToString(); // Conversão para String do endereço no banco vindo do "stringConection" em web.config
            _con = new SqlConnection(connstr); // Instância do novo objeto de conexão
        }
        private static ConnectionStringSettingsCollection GetConnectionStrings()
        {
            return ConfigurationManager.ConnectionStrings;
        }

        //Adicionar um time
        public bool AdicionarTime(Times timeObj) 
        {
            Connection();

            int i;

            using (SqlCommand command = new SqlCommand("IncluirTime", _con))// usa a conexão _con para acessar uma procedue feita no sql server
            {
                command.CommandType = CommandType.StoredProcedure; // busca a procedure armazenada
                command.Parameters.AddWithValue("@Time", timeObj.Time); // adiciona o id informado ao @TimeId e encontra o elemento
                command.Parameters.AddWithValue("@Estado", timeObj.Estado);
                command.Parameters.AddWithValue("@Cores", timeObj.Cores);
                _con.Open(); //aciona a abertura da conexão já tendo os dados
                i = command.ExecuteNonQuery();  //Executa a instrução na conexão, retorna o número de linhas afetadas e armazena em i
            }
            _con.Close(); // fecha a conexão após a ação
            return i >= 1; //return condicional caso a variável 'i' não seja null
        }

        //Obter todos os times
        public List<Times> ObterTimes() 
        {
            Connection();

            List<Times> timesList = new List<Times>(); // instância de uma lista para aguardar as informações obtidas

            using (SqlCommand command = new SqlCommand("ObterTimes", _con)) // instância do SQL Command para obtenção dos dados
            {
                command.CommandType = CommandType.StoredProcedure; // busca a procedure armazenada
                _con.Open();// abre a conexão 
                SqlDataReader reader = command.ExecuteReader(); // executa o reader para obter os dados do banco

                while (reader.Read()) // loop do READ, até que não obtenha mais informações e retorne nulo
                {
                    timesList.Add(new Times
                    {
                        TimeId = Convert.ToInt32(reader["TimeId"]),
                        Time = reader["Time"].ToString(),
                        Estado = reader["Estado"].ToString(),
                        Cores = reader["Cores"].ToString()
                    });// adiciona cada um dos elementos do banco à lista criada anteriormente
                }
            }
            _con.Close(); // fecha a conexão sempre após o uso do banco
            return timesList; // retorna ao final a lista de times informados um a um no laço while
        }
        
        // Atualizar times
        public bool AtualizarTime(Times timeObj) // entra na função com os dados do time a atualizar
        {
            Connection();
            int i;
            using (SqlCommand command = new SqlCommand("AtualizarTime", _con))// instância do SQL Command para obtenção dos dados
            {
                command.CommandType = CommandType.StoredProcedure;// busca a procedure armazenada
                command.Parameters.AddWithValue("@TimeId", timeObj.TimeId);// adiciona o id informado ao @TimeId e encontra o elemento
                command.Parameters.AddWithValue("@Time", timeObj.Time);
                command.Parameters.AddWithValue("@Estado", timeObj.Estado);
                command.Parameters.AddWithValue("@Cores", timeObj.Cores);
                _con.Open(); // abre a conexão do banco para atualizar
                i = command.ExecuteNonQuery(); //Executa a instrução na conexão, retorna o número de linhas afetadas e armazena em i
            }
            _con.Close(); // fecha a conexão
            return i >= 1; // retorna o valor obtido caso não seja null
        }
            
        //Excluir Time
        public bool ExcluirTime(int id)// entra na função com o id do time escolhido
        {
            Connection();
            int i;
            using (SqlCommand command = new SqlCommand("ExcluirTimePorId", _con))// instância do SQL Command para obtenção dos dados
            {
                command.CommandType = CommandType.StoredProcedure;// busca a procedure armazenada
                command.Parameters.AddWithValue("@TimeId", id);// adiciona o id informado ao @TimeId e encontra o elemento

                _con.Open();// Abre a Conexão
                i = command.ExecuteNonQuery();//Executa a instrução na conexão, retorna o número de linhas afetadas e armazena em i
            }

            _con.Close();// Fecha a Conexão
            if (i >= 1)
            {
                return true;
            }
            return false;
        }
    }
}
