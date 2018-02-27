using System;
using SINTALOCAS.DAL.Context;

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
    }
}
