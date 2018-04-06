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
    public static class PagamentoDAL
    {
        private static ContextoMySqlDB _contexto = new ContextoMySqlDB();

        public static List<Pagamento> ConsultarPorCategoria(string categoria)
        {
            return query(0, categoria);
        }

        public static List<Pagamento> ConsultarPorID(int id)
        {
            return query(id, "");
        }

        public static List<Pagamento> query(int id, string categoria)
        {
            try
            {
                var lista = new List<Pagamento>();

                var query = "SELECT ID, Texto, Categoria"
                    + " FROM Cfg_OpcaoPagto"
                    + " WHERE D_E_L_E_T_ = 0";

                if (categoria.Trim() != "") query += " AND Categoria='" + categoria.ToUpper().Trim() + "'";
                if (id > 0) query += " AND ID='" + id + "'";

                var dataTable = _contexto.Consultar(query);

                foreach (DataRow linha in dataTable.Rows)
                {
                    var obj = new Pagamento
                    {
                        ID = Convert.ToInt32(linha["ID"]),
                        Texto = linha["Texto"].ToString(),
                        Categoria = linha["Categoria"].ToString()
                    };

                    lista.Add(obj);
                }

                dataTable.Dispose();

                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
