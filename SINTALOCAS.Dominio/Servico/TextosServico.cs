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
            try
            {
                var textoDAL = new TextosDAL();
                var lista = textoDAL.Consultar("concordo", "WEB");

                return lista[0];
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static  List<MensagemSistema> ListarTextosSite()
        {
            try
            {
                var textoDAL = new TextosDAL();
                var lista = textoDAL.Consultar("", "WEB");

                return lista;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static int Atualizar(List<MensagemSistema> listaTextos)
        {
            try
            {
                var textoDAL = new TextosDAL();
                return textoDAL.Atualizar(listaTextos);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
