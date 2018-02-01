using System;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SINTALOCAS.Modelo;
using MySql.Data.MySqlClient;

namespace SINTALOCAS.DAL.Context
{
    public class ContextoDB 
    {
        private MySqlConnection conexao;

        public ContextoDB()
        {
            var conexaoString = ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString;
            conexao = new MySqlConnection(conexaoString);
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

                MySqlCommand cmd = CriarComando(query);
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
            catch (MySqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {

                throw ex;
                //}
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
