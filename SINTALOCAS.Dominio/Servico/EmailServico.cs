using System;
using System.Collections.Generic;
using System.Linq;
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

            MailMessage message = new MailMessage(from, to);

            message.Subject = ConfigDAL.GetValor("EMAILTIT", "EMAIL");
            message.Body = @"Using this new feature, you can send an e-mail message from an application very easily.";

            SmtpClient client = new SmtpClient(ConfigDAL.GetValor("SMTP", "EMAIL"));
            // Credentials are necessary if the server requires the client 
            // to authenticate before it will send e-mail on the client's behalf.
            client.UseDefaultCredentials = true;

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
