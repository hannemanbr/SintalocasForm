using System;
using Postmon4Net;
using SINTALOCAS.Modelo;

namespace SINTALOCAS.Dominio.Servico
{
    public class PostmonApi
    {
        
        public Endereco ConsultarEndereco(string cep)
        {
            EnderecoInfo infoEndereco = Postmon4Net.EncontrarEndereco.PorCEP(cep);
            var endereco = new Endereco
            {
                Bairro = infoEndereco.bairro,
                CEP = infoEndereco.cep,
                Logradouro = infoEndereco.logradouro,
                Numero = "",
                Complemento = "",
                Cidade = infoEndereco.cidade,
                ID = 0
            };

            return endereco;
        }
    }
}
