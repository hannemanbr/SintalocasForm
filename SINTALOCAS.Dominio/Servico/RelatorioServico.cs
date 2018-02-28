using SINTALOCAS.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SINTALOCAS.DAL.DB;

namespace SINTALOCAS.Dominio.Servico
{
    public static class RelatorioServico
    {
        public static List<Relatorio> ListarRelatorios()
        {
            return RelatorioDAL.Consultar();
        }
    }
}
