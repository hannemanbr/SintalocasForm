using System;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Data.Odbc;
using SINTALOCAS.Modelo;

namespace SINTALOCAS.DAL.Context
{
    public class ContextoODBCDB
    {
        private OdbcConnection conexao;

        public ContextoODBCDB()
        {
            try
            {
                var conexaoString = ConfigurationManager.ConnectionStrings["ODBCLDB"].ConnectionString;
                conexao = new OdbcConnection(conexaoString);
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

                OdbcCommand cmd = CriarComando(query);
                OdbcDataReader dataReaderBD = cmd.ExecuteReader();
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
                OdbcCommand cmd = CriarComando(query);
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

        private OdbcCommand CriarComando(string query)
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
            catch (OdbcException ex)
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
