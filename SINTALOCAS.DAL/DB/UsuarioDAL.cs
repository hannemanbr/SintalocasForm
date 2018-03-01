using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SINTALOCAS.DAL.Context;
using SINTALOCAS.Modelo;

namespace SINTALOCAS.DAL.DB
{    
    public static class UsuarioDAL
    {
        private static ContextoMySqlDB _contexto = new ContextoMySqlDB();

        public static List<Usuario> Consultar(string email  = "", string senha = "")
        {
            try
            {
                var lista = new List<Usuario>();
                var query = "SELECT ID, Nome, Email, Perfil"
                    + " FROM Admin_Login"
                    + " WHERE D_E_L_E_T_ = 0";

                if (email.Trim() != "") query += " AND Email = '" + email + "' AND Senha = '" + senha + "'";

                var dataTable = _contexto.Consultar(query);

                foreach (DataRow linha in dataTable.Rows)
                {
                    var obj = new Usuario
                    {
                        ID = Convert.ToInt32(linha["ID"]),
                        Nome = linha["Nome"].ToString(),
                        Email = linha["Email"].ToString(),
                        Perfil= Convert.ToInt32(linha["Perfil"])
                    };

                    lista.Add(obj);
                }

                dataTable.Dispose();

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
