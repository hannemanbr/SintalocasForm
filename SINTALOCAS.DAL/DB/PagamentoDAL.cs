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

        public static List<Pagamento> Consultar(string categoria)
        {
            try
            {
                var lista = new List<Pagamento>();

                var query = "SELECT ID, Texto"
                    + " FROM Cfg_OpcaoPagto"
                    + " WHERE D_E_L_E_T_ = 0"
                    + " AND Categoria='" + categoria.ToUpper().Trim() + "'";

                var dataTable = _contexto.Consultar(query);

                foreach (DataRow linha in dataTable.Rows)
                {
                    var obj = new Pagamento
                    {
                        ID = Convert.ToInt32(linha["ID"]),
                        Texto = linha["Texto"].ToString()
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
