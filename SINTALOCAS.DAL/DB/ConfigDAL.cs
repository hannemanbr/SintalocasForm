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
    public static class ConfigDAL
    {
        private static ContextoMySqlDB _contexto = new ContextoMySqlDB();

        public static List<ConfigSistema> Consultar(string nome = "", string categoria = "")
        {
            try
            {
                var lista = new List<ConfigSistema>();
                var query = "SELECT ID, Nome, Valor"
                    + " FROM Cfg_Sistema"
                    + " WHERE D_E_L_E_T_ = 0";

                if (nome.Trim() != "") query += " AND Nome='" + nome + "'";
                if (categoria.Trim() != "") query += " AND Categoria='" + categoria + "'";

                var dataTable = _contexto.Consultar(query);

                foreach (DataRow linha in dataTable.Rows)
                {
                    var obj = new ConfigSistema
                    {
                        ID = Convert.ToInt32(linha["ID"]),
                        Nome = linha["Nome"].ToString(),
                        Valor = linha["Valor"].ToString()
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

        public static string GetValor(string nome, string categoria)
        {
            var result = "";
            var lista = Consultar(nome, categoria);

            if (lista.Count > 0) result = lista[0].Valor;

            return result;
        }
    }
}
