using SINTALOCAS.DAL.Context;
using SINTALOCAS.Modelo;
using SINTALOCAS.Modelo.Enumerator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SINTALOCAS.DAL.DB
{
    public class ContribuicaoDAL : ContextoMySqlDB
    {
        public int Inserir(Afiliado afiliado)
        {
            var result = 0;

            try
            {
                var query = "";

                foreach (var item in afiliado.Contribuicoes)
                {
                    query += " INSERT INTO Afiliado_Contribuicao (" +
                        "IDAfiliado," +
                        "IDContribuicao" +
                        ") VALUES (";
                    query += "'" + afiliado.ID + "',";
                    query += "'" + item.ID + "'";
                    query += ");";
                }

                if(query.Trim()!="")
                    result = Transacao(query);
            }
            catch (Exception)
            {
                throw;
            }

            return result;

        }

        public List<Contribuicao> ConsultarPorAfiliado(int idAfiliado)
        {
            try
            {
                Pagamento pagamento;
                var listaContribuicao = PagamentoDAL.ConsultarPorCategoria(OpcaoPagamentoEnum.CONTRIBuICAO.ToString());
                var lista = new List<Contribuicao>();

                var query = "SELECT * FROM Afiliado_Contribuicao";
                query += " WHERE D_E_L_E_T_ = 0 AND idAfiliado=" + idAfiliado + "";

                var dataTable = Consultar(query);

                foreach (DataRow linha in dataTable.Rows)
                {
                    pagamento = listaContribuicao.Where(X => X.ID == Convert.ToInt32(linha["IDContribuicao"])).First();
                    lista.Add(new Contribuicao() { ID = pagamento.ID, Nome = pagamento.Texto });
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
