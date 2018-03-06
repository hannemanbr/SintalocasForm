using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace SINTALOCAS.DAL.Context
{
    public class ContextoMySqlDB 
    {
        private MySqlConnection conexao;

        public ContextoMySqlDB()
        {
            try
            {
                var conexaoString = ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString;
                //var conexaoString = ConfigurationManager.ConnectionStrings["MySQLDBEXT"].ConnectionString;
                conexao = new MySqlConnection(conexaoString);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable Consultar(string query)
        {
            DataTable dt = new DataTable();

            try
            {
                AbrirConexao();

                MySqlCommand cmd = CriarComando(query);
                MySqlDataReader dataReaderBD = cmd.ExecuteReader();

                dt.Load(dataReaderBD);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                FecharConexao();
            }

            return dt;
        }

        public int Transacao(string query)
        {
            var result = 0;

            try
            {
                AbrirConexao();

                MySqlCommand cmd = CriarComando(query);
                result = cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                FecharConexao();
            }

            return result;

        }

        private MySqlCommand CriarComando(string query)
        {
            var cmd = conexao.CreateCommand();
            cmd.CommandText = query;
            return cmd;
        }

        private void AbrirConexao()
        {
            try
            {
                if (conexao.State == ConnectionState.Open) return;
                conexao.Open();
            }
            catch (MySqlException)
            {
                throw;
            }
            catch (Exception )
            {
                throw;
            }


        }

        private void FecharConexao()
        {
            if (conexao.State == ConnectionState.Open)
                conexao.Close();
        }

        private void Dispose()
        {
            if (conexao == null) return;
            conexao.Dispose();
            conexao = null;
        }
    }
}
