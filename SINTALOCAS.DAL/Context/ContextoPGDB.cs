using System;
using System.Configuration;
using Npgsql;
using System.Data;

namespace SINTALOCAS.DAL.Context
{
    public class ContextoPGDB
    {
        private NpgsqlConnection conexao;

        public ContextoPGDB()
        {
            try
            {
                var conexaoString = ConfigurationManager.ConnectionStrings["PgSQLDB"].ConnectionString;
                conexao = new NpgsqlConnection(conexaoString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Consultar(string query)
        {
            DataTable dt = new DataTable();

            try
            {
                AbrirConexao();

                NpgsqlCommand cmd = CriarComando(query);
                NpgsqlDataReader dataReaderBD = cmd.ExecuteReader();

                dt.Load(dataReaderBD);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                FecharConexao();
            }

            return dt;
        }

        public void Transacao(string query)
        {

            try
            {
                AbrirConexao();

                NpgsqlCommand cmd = CriarComando(query);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                FecharConexao();
            }

        }

        private NpgsqlCommand CriarComando(string query)
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
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
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
