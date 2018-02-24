using SINTALOCAS.DAL.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SINTALOCAS.Dominio.Servico
{
    public class LogServico
    {
        public static void Registrar(string tipo, string ip, string usuario, string link, string acao, string valor)
        {
            try
            {
                LogDAL.RegistraLog(tipo, ip, usuario, link, acao, valor);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
