using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SINTALOCAS.DAL.DB;
using SINTALOCAS.Modelo;

namespace SINTALOCAS.Dominio.Servico
{
    public static class AdminServico
    {
        public static List<Usuario> Consultar(string email, string senha)
        {
            try
            {
                //senha = GerarSenhaSHA1(senha);
                return UsuarioDAL.Consultar(email, senha);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static  bool ValidarLogin(string email, string senha)
        {
            var lista = Consultar(email, senha);

            if (lista.Count > 0) return true;

            return false;
        }

        // ou se você não quiser usar a encoding como parâmetro.
        public static string GerarSenhaSHA1(string texto)
        {
            try
            {
                byte[] buffer = Encoding.Default.GetBytes(texto);
                System.Security.Cryptography.SHA1CryptoServiceProvider cryptoTransformSHA1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
                string hash = BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
                return hash;
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
        }
    }
}
