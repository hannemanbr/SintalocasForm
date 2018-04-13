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
    public class DependenteDAL : ContextoMySqlDB
    {
        public int Inserir(Dependentes dependentes)
        {
            var result = 0;

            try
            {

                var dataNascTx = dependentes.DataNascimento.Year + "-" + dependentes.DataNascimento.Month + "-" + dependentes.DataNascimento.Day;

                var query = "" +
                    " INSERT INTO Afiliado_Dependente (" +
                    "Nome," +
                    "DataNascimento," +
                    "Grau," +
                    "AcrescimoMensal," +
                    "IdAfiliado" +
                    ") VALUES (";
                query += "'" + dependentes.Nome + "',";
                query += "'" + dataNascTx + "',";
                query += "'" + dependentes.GrauParentescoID + "',";
                query += "'" + dependentes.AcrescimoMensal + "',";
                query += "'" + dependentes.IdAfiliado + "'";
                query += ")";

                result = Transacao(query);
            }
            catch (Exception)
            {
                throw;
            }

            return result;

        }

        public List<Dependentes> ConsultarPorAfiliado(int idAfiliado)
        {
            try
            {
                var lista = new List<Dependentes>();

                var query = "SELECT ";
                query += " D.ID, D.Nome, D.DataNascimento, D.AcrescimoMensal, D.Grau, P.Descricao GrauNome";
                query += " FROM Afiliado_Dependente D";
                query += " INNER JOIN Cfg_GrauParentesco P ON D.Grau = P.ID";
                query += " WHERE D.D_E_L_E_T_ = 0 AND D.idAfiliado=" + idAfiliado + "";

                var dataTable = Consultar(query);

                foreach (DataRow linha in dataTable.Rows)
                {
                    var obj = new Dependentes
                    {
                        ID = Convert.ToInt32(linha["ID"]),
                        Nome = linha["Nome"].ToString().ToUpper(),
                        DataNascimento = Convert.ToDateTime(linha["DataNascimento"]),
                        GrauParentescoID = Convert.ToInt32(linha["Grau"].ToString()),
                        GrauParentescoNome = linha["GrauNome"].ToString().ToUpper(),
                        AcrescimoMensal = Convert.ToInt32(linha["AcrescimoMensal"].ToString())
                    };

                    lista.Add(obj);
                }

                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Dictionary<int, int> QuantidadeDependentes(int idAfiliado)
        {
            try
            {
                var lista = new Dictionary<int, int>();

                var query = "SELECT count(0) Total," +
                    " Nome, DataNascimento, AcrescimoMensal, Grau" +
                    " FROM Afiliado_Dependente D" +
                    " WHERE idAfiliado=" + idAfiliado + "" +
                    " GROUP BY Nome, DataNascimento, AcrescimoMensal";

                var dataTable = Consultar(query);

                foreach (DataRow linha in dataTable.Rows)
                {
                    if (lista.ContainsKey(Convert.ToInt32(linha["Grau"])))
                        lista.Add(Convert.ToInt32(linha["Grau"]), Convert.ToInt32(linha["Total"]));
                }

                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
