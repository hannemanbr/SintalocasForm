using SINTALOCAS.DAL.Context;
using SINTALOCAS.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SINTALOCAS.DAL.DB
{
    public static class RelatorioDAL
    {
        private static ContextoMySqlDB _contexto = new ContextoMySqlDB();
        public static List<Relatorio> Consultar()
        {
            try
            {
                var lista = new List<Relatorio>();
                var query = "SELECT ID, Nome, Link, Ordem, Categoria FROM Relatorio WHERE D_E_L_E_T_ = 0";
                var dataTable = _contexto.Consultar(query);

                foreach (DataRow linha in dataTable.Rows)
                {
                    var obj = new Relatorio
                    {
                        ID = Convert.ToInt32(linha["ID"]),
                        Nome = linha["Nome"].ToString(),
                        Ordem = Convert.ToInt32(linha["Ordem"].ToString()),
                        Link = linha["Link"].ToString(),
                        Categoria = linha["Categoria"].ToString()
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
