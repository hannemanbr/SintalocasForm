using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SINTALOCAS.DAL.DB;

namespace SINTALOCAS.Dominio.Servico
{
    public static class TextosServico
    {

        public static string TextoDeAcordo()
        {
            var textoDAL = new TextosDAL();
            var lista = textoDAL.Consultar("concordo", "concordar");

            if (lista.Count>0) return lista[0].Texto;

            return "";

        }
    }
}
