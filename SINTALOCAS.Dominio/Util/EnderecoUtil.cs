using System;
using Postmon4Net;
using SINTALOCAS.Modelo;

namespace SINTALOCAS.Dominio.Util
{
    public static class EnderecoUtil
    {
        
        public static Endereco ConsultarEndereco(string cep)
        {
            EnderecoInfo infoEndereco = Postmon4Net.EncontrarEndereco.PorCEP(cep);
            Endereco endereco;

            if (infoEndereco != null)
            {
                endereco = new Endereco
                {
                    Bairro = infoEndereco.bairro,
                    CEP = infoEndereco.cep,
                    Logradouro = infoEndereco.logradouro,
                    Numero = "",
                    Complemento = "",
                    Cidade = infoEndereco.cidade,
                    ID = 0
                };
            } else{
                endereco = new Endereco
                {
                    Bairro = "",
                    CEP = "",
                    Logradouro = "",
                    Numero = "",
                    Complemento = "",
                    Cidade = "",
                    ID = 0
                };
            }

            return endereco;
        }
    }
}
