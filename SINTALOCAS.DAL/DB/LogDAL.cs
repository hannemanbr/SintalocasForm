using System;
using SINTALOCAS.DAL.Context;

namespace SINTALOCAS.DAL.DB
{
    public static class LogDAL
    {
        
        public static void RegistraLog(string tipo, string ip, string usuario, string link, string acao, string valor)
        {
            try
            {
                ContextoMySqlDB contexto = new ContextoMySqlDB();

                var query = "" +
                    " INSERT INTO Log(" +     
                    "Tipo, " +
                    "usuario, " +
                    "ip, " +
                    "acao, " +
                    "valores, " +
                    "link" +
                    ") VALUES (";
                
                query += "'" + tipo + "'";
                query += ",'" + usuario + "'";
                query += ",'" + ip + "'";
                query += ",'" + acao + "'";
                query += ",'" + valor + "'";
                query += ",'" + link + "'";
                query += ")";

                contexto.Transacao(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
