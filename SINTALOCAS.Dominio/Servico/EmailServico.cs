using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SINTALOCAS.DAL.DB;
using SINTALOCAS.Modelo;

namespace SINTALOCAS.Dominio.Servico
{
    public  static class EmailServico
    { 
        public static void EnviarEmail(string destino, string mensagem, string titulo)
        {
            var smtp = ConfigDAL.GetValor("SMTP", "EMAIL");
            SmtpClient client = new SmtpClient(smtp);

            try
            {         
                var listaUsuario = UsuarioDAL.Consultar();
                var from = ConfigDAL.GetValor("EMAILFROM", "EMAIL");
                var pass = ConfigDAL.GetValor("EMAILSENHA", "EMAIL");

                var msg = new MailMessage(from, destino, titulo, mensagem);
                msg.IsBodyHtml = true;

                client.Credentials = new NetworkCredential(from, pass);
                //client.Send(from, destino, titulo, mensagem);                
                client.Send(msg);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                client.Dispose();
            }

        }

        public static string montarMensagem(string texto, string titulo)
        {
            var result = "";
            var banner = "<h1>SINTALOCAS - " + titulo + "</h1>";
            var corpo = "<br/><br/>" + texto + "<br/><br/>";
            var rodape = "<br/><h3>Este é um e-mail automático, nâo responder</h3>";

            result += banner + corpo + rodape;

            return result;
        }

        public static void NotificarCadastro(Afiliado afiliado, List<Dependentes> dependentes)
        {
            try
            {
                var texto = "Foi realizado um cadastro eletrônico, veja abaixo as informações do cadastro";

                texto += "<br/><br/>";
                texto += "<h2>Informações Gerais:</h2><br/>";
                texto += "<strong>Nome:</strong> " + afiliado.Nome + "<br/>";
                texto += "<strong>E-mail:</strong> " + afiliado.Email + "<br/>";
                texto += "<strong>Empresa:</strong> " + afiliado.Empresa + "<br/>";
                texto += "<strong>CNPJ:</strong> " + afiliado.CNPJ + "<br/>";
                texto += "<strong>CPF:</strong> " + afiliado.CPF + "<br/>";
                texto += "<strong>CTPS:</strong> " + afiliado.CTPS.Numero + " <strong>Série:</strong> " + afiliado.CTPS.Serie + "<br/>";
                texto += "<strong>PIS:</strong> " + afiliado.CTPS.PIS + "<br/>";
                texto += "<strong>Data de Nascimento:</strong> " + afiliado.DataNascimento + "<br/>";
                texto += "<h2>Endereço:</h2><br/>";
                texto += "<strong>Logradouto:</strong> " + afiliado.Endereco.Logradouro + "<br/>";
                texto += "<strong>Número:</strong> " + afiliado.Endereco.Numero + " ";
                texto += "<strong>Complemento:</strong> " + afiliado.Endereco.Complemento + "<br/>";
                texto += "<strong>Bairro:</strong> " + afiliado.Endereco.Bairro + "<br/>";
                texto += "<strong>Cidade:</strong> " + afiliado.Endereco.Cidade + "-" + afiliado.Endereco.UF + "<br/>";
                texto += "<strong>CEP:</strong> " + afiliado.Endereco.CEP + "<br/>";
                texto += "<h2>Dependentes:</h2><br/>";

                foreach (var item in dependentes)
                {
                    texto += "<strong>- Nome:</strong> " + item.Nome + " | " +
                        "<strong>Dt.Nasc:</strong> " + item.DataNascimento + " | " +
                        "<strong>Grau:</strong> " + item.GrauParentescoNome + "<br/>";
                }

                var titulo = ConfigDAL.GetValor("EMAILTIT", "EMAIL");
                var listaDestino = UsuarioServico.Consultar("", 0);
                var destino = "";            

                foreach(var item in listaDestino)
                {
                    destino += "" + item.Email.Trim() + ",";
                }

                //removendo utlima virgula
                if (destino.Trim().Length > 0)
                    destino = destino.Substring(0, destino.Trim().Length - 1);

                EnviarEmail(
                    destino,
                    montarMensagem(texto, titulo),
                    titulo);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
