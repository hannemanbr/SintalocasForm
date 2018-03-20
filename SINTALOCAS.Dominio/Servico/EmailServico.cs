using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SINTALOCAS.DAL.DB;

namespace SINTALOCAS.Dominio.Servico
{
    public  static class EmailServico
    { 
        public static void EnviarEmail(string destino, string mensagem)
        {   
            try
            {
         
                var listaUsuario = UsuarioDAL.Consultar();
                var from = ConfigDAL.GetValor("EMAILFROM", "EMAIL");
                var pass = ConfigDAL.GetValor("EMAILSENHA", "EMAIL");
                var smtp = ConfigDAL.GetValor("SMTP", "EMAIL");
                var titulo = ConfigDAL.GetValor("EMAILTIT", "EMAIL");

                SmtpClient client = new SmtpClient(smtp);

                client.Credentials = new NetworkCredential(from, pass);
                client.Send(from, destino, titulo, mensagem);
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
