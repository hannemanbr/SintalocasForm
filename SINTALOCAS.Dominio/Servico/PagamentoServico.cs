using SINTALOCAS.DAL.DB;
using SINTALOCAS.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SINTALOCAS.Dominio.Servico
{
    public static class PagamentoServico
    {
        public static List<Pagamento> ConsultarPorCategoria(string categoria)
        {
            try
            {
                return PagamentoDAL.ConsultarPorCategoria(categoria);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<Pagamento> ConsultarTodos()
        {
            try
            {
                return PagamentoDAL.ConsultarPorCategoria("");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<Pagamento> ConsultarPorID(int id)
        {
            try
            {
                return PagamentoDAL.ConsultarPorID(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
