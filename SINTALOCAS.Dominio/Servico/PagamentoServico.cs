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
        public static List<Pagamento> Consultar()
        {
            try
            {
                return PagamentoDAL.Consultar();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
