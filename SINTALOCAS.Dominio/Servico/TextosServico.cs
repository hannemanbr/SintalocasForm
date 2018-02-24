using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SINTALOCAS.DAL.DB;
using SINTALOCAS.Modelo;

namespace SINTALOCAS.Dominio.Servico
{
    public static class TextosServico
    {

        public static MensagemSistema TextoDeAcordo()
        {
            var textoDAL = new TextosDAL();
            var lista = textoDAL.Consultar("concordo", "concordar");

            return lista[0];
            
        }
    }
}
