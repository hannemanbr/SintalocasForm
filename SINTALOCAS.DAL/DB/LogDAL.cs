using System;
using System.Collections.Generic;
using System.Data;
using SINTALOCAS.DAL.Context;
using SINTALOCAS.Modelo;

namespace SINTALOCAS.DAL.DB
{
    public static class LogDAL
    {
        private static ContextoMySqlDB _contexto = new ContextoMySqlDB();

        public static int RegistraLog(
            string tipo, 
            string ip, 
            string usuario, 
            string link, 
            string acao, 
            string valor, 
            string geolocation,
            int idacesso
            )
        {
            var result = 0;

            try
            {
                var query = "" +
                    " INSERT INTO Log(" +     
                    "Tipo, " +
                    "usuario, " +
                    "ip, " +
                    "acao, " +
                    "valores, " +
                    "Geolocation, " +
                    "IdAcesso, " +
                    "link" +
                    ") VALUES (";
                
                query += "'" + tipo + "'";
                query += ",'" + usuario + "'";
                query += ",'" + ip + "'";
                query += ",'" + acao + "'";
                query += ",'" + valor + "'";
                query += ",'" + geolocation + "'";
                query += ",'" + idacesso + "'";
                query += ",'" + link + "'";
                query += ")";

                result = _contexto.Transacao(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static void Consultar(DateTime data)
        {
            try
            {
                var query = "SELECT * FROM Log";

                if (data != null) query += " WHERE DataCadastro >= '" + data + "'";
                
                _contexto.Consultar(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<LogSistema> Lista(string local, DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                var lista = new List<LogSistema>();

                var query = "SELECT                     " +
                                "ID                     " +
                                ",DataCadastro          " +
                                ",IP                    " +
                                ",Link                  " +
                                ",Acao                  " +
                                ",Geolocation           " +
                             " FROM Log                 " +
                             " WHERE Tipo='UsuarioWeb'  ";
                
                if (local.Trim() != "") query += " AND Geolocation LIKE '%" + local + "%'";
                if (dataInicio != DateTime.Now) query += " AND DataCadastro >='" + dataInicio + "'";
                if (dataFim != DateTime.Now) query += " AND DataCadastro <='" + dataInicio + "'";

                var dataTable = _contexto.Consultar(query);

                foreach (DataRow linha in dataTable.Rows)
                {
                    var obj = new LogSistema
                    {
                        ID = Convert.ToInt32(linha["ID"]),
                        Acao = linha["Acao"].ToString(),
                        Geolocation = linha["Geolocation"].ToString(),
                        Link = linha["Link"].ToString(),
                        IP = linha["IP"].ToString(),
                        DataCadastro = Convert.ToDateTime(linha["DataCadastro"].ToString())
                    };
                    
                    lista.Add(obj);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<LogSistema> ListaAgrupado_Data_IP(string local, DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                var lista = new List<LogSistema>();

                var query = "SELECT                     " +
                                "DataCadastro           " +
                                ",IP                    " +
                                ",Acao                  " +
                                ",Geolocation           " +
                                ",Link                  " +
                             " FROM Log                 " +
                             " WHERE Tipo='UsuarioWeb'  ";

                if (local.Trim() != "") query += " AND Geolocation LIKE '%" + local + "%'";
                if (dataInicio != DateTime.Now) query += " AND DataCadastro >='" + dataInicio + "'";
                if (dataFim != DateTime.Now) query += " AND DataCadastro <='" + dataInicio + "'";

                query += " GROUP BY DataCadastro, IP, Acao, Geolocation, Link";

                var dataTable = _contexto.Consultar(query);

                foreach (DataRow linha in dataTable.Rows)
                {
                    var obj = new LogSistema
                    {
                        Acao = linha["Acao"].ToString(),
                        Geolocation = linha["Geolocation"].ToString(),
                        IP = linha["IP"].ToString(),
                        DataCadastro = Convert.ToDateTime(linha["DataCadastro"].ToString()),
                        Link = linha["Link"].ToString()
                    };

                    lista.Add(obj);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
