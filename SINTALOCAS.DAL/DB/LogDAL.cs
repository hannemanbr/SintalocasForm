using System;
using SINTALOCAS.DAL.Context;

namespace SINTALOCAS.DAL.DB
{
    public class LogDAL
    {
        ContextoMySqlDB _contexto = new ContextoMySqlDB();

        public void RegistraLog(string tipo, string ip, string usuario, string link, string acao, string valor)
        {
            try
            {

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

                _contexto.Transacao(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
