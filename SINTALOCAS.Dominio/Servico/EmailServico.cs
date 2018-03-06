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
        public static void EnviarEmailAdmin()
        {
            var listaUsuario = UsuarioDAL.Consultar();            
            string to = "abrantes.thomas@gmail.com";
            string from = ConfigDAL.GetValor("EMAILFROM", "EMAIL");
            
            try
            {
                MailMessage message = new MailMessage();
                message.Subject = ConfigDAL.GetValor("EMAILTIT", "EMAIL");
                message.Body = @"Using this new feature, you can send an e-mail message from an application very easily.";

                SmtpClient client = new SmtpClient(ConfigDAL.GetValor("SMTP", "EMAIL"));
                NetworkCredential configEmail = new NetworkCredential("admin@thomasabrantes.com", "Thom@s2003");

                client.Credentials = configEmail;
                client.Send(message);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static void EnviarEmail(string destino, string mensagem)
        {   
            try
            {
         
                var listaUsuario = UsuarioDAL.Consultar();
                var from = ConfigDAL.GetValor("EMAILFROM", "EMAIL");
                var smtp = ConfigDAL.GetValor("SMTP", "EMAIL");
                var titulo = ConfigDAL.GetValor("EMAILTIT", "EMAIL");

                SmtpClient client = new SmtpClient(smtp);
                NetworkCredential configEmail = new NetworkCredential("admin@thomasabrantes.com", "Thom@s2003");
                client.Credentials = configEmail;
                client.UseDefaultCredentials = false;
                client.Send(from, destino, titulo, mensagem);
            }
            catch (Exception)
            {
                throw;
            }

        }


    }
}
