using System;
//using Postmon4Net;
using SINTALOCAS.Modelo;
using SINTALOCAS.Dominio.CorreiosWebService;

namespace SINTALOCAS.Dominio.Util
{
    public static class EnderecoUtil
    {
        
        public static Endereco ConsultarEndereco(string cep)
        {
            Endereco endereco;

            try
            {
                //EnderecoInfo infoEndereco = Postmon4Net.EncontrarEndereco.PorCEP(cep);
                AtendeClienteService correiosWs = new AtendeClienteService();
                enderecoERP infoEndereco = correiosWs.consultaCEP(cep);

                if (infoEndereco != null)
                {
                    endereco = new Endereco
                    {
                        Bairro = infoEndereco.bairro,
                        CEP = infoEndereco.cep,
                        Logradouro = infoEndereco.end,
                        Numero = "",
                        Complemento = "",
                        Cidade = infoEndereco.cidade,
                        UF = infoEndereco.uf,
                        ID = 0
                    };
                }
                else
                {
                    endereco = new Endereco
                    {
                        Bairro = "",
                        CEP = "",
                        Logradouro = "",
                        Numero = "",
                        Complemento = "",
                        Cidade = "",
                        UF = "",
                        ID = -1
                    };
                }
            }
            catch (Exception ex)
            {
                endereco = new Endereco
                {
                    Bairro = "",
                    CEP = ex.Message,
                    Logradouro = "",
                    Numero = "",
                    Complemento = "",
                    Cidade = "",
                    UF = "",
                    ID = -1
                };
            }

            return endereco;
        }
    }
}
