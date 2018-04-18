using System;
using System.Collections.Generic;
using System.Globalization;
using SINTALOCAS.Dominio.Servico;
using SINTALOCAS.Modelo;
using SINTALOCAS.Modelo.Enumerator;
using SINTALOCAS.Dominio.Util;
using System.Web.Mvc;
using SINTALOCAS.Web.MVC.Models;
using SINTALOCAS.DAL.DB;

namespace SINTALOCAS.Web.MVC.Servico
{
    public static class validacaoViewServico
    {
        public static AfiliacaoModelView GeraAfiliacaoModelView(Dictionary<string, string> lista){

            // criando objeto de afiliado
            var afiliado = new AfiliacaoModelView();

            afiliado.Cargo = lista["CARGO"];
            afiliado.Consir = lista["CONSIR"];
            afiliado.CPF = lista["CPF"];
            afiliado.CNPJ = lista["CNPJ"];
            afiliado.Empresa = lista["EMPRESA"];
            afiliado.RG = lista["RG"];
            afiliado.Email = lista["EMAIL"];
            afiliado.Nome = lista["NOME"];
            afiliado.NomeMae = lista["NOMEMAE"];
            afiliado.NomePai = lista["NOMEPAI"];
            afiliado.PIS = lista["PIS"];
            afiliado.CTPS_Serie = lista["CTPSSERIE"];
            afiliado.CTPS_Numero = lista["CTPSNUM"];
            afiliado.DDDTel = lista["TELRESDDD"];
            afiliado.NumTel = lista["TELRESNUM"];
            afiliado.DDDCel = lista["TELCELDDD"];

            //Datas
            DateTime dataNascimento = DataUtil.ConverterString(lista["DTNASC"]);
            afiliado.DataNascimento = dataNascimento;

            ////Endereco
            //Endereco endereco = new Endereco()
            //{
            //    Logradouro = lista["RUA"],
            //    Bairro = lista["BAIRRO"],
            //    CEP = lista["CEP"],
            //    Cidade = lista["CIDADE"],
            //    Complemento = lista["COMPLEMENTO"],
            //    Numero = lista["NUMERO"],
            //    UF = lista["UF"]
            //};

            //Endereco
            afiliado.Logradoro = lista["RUA"];
            afiliado.Bairro = lista["BAIRRO"];
            afiliado.CEP = lista["CEP"];
            afiliado.Cidade = lista["CIDADE"];
            afiliado.Complemento = lista["COMPLEMENTO"];
            afiliado.Numero = lista["NUMERO"];
            afiliado.UF = lista["UF"];

            return afiliado;
        }

        public static int Atualizar(Dictionary<string, string> lista, bool editar)
        {
            try
            {
                int result = 0;
                int idEndereco = 0;

                // criando objeto de afiliado
                var afiliado = new Afiliado();

                afiliado.Cargo = lista["CARGO"];                
                afiliado.CPF = lista["CPF"];
                afiliado.CNPJ = lista["CNPJ"];
                afiliado.Empresa = lista["EMPRESA"];
                afiliado.RG = lista["RG"];
                afiliado.Email = lista["EMAIL"];
                afiliado.Nome = lista["NOME"];
                afiliado.NomeMae = lista["NOMEMAE"];
                afiliado.NomePai = lista["NOMEPAI"];

                if (lista.ContainsKey("CONSIR"))
                    afiliado.Consir = lista["CONSIR"];

                if (lista.ContainsKey("IDAFILIADO"))
                    afiliado.ID = Convert.ToInt32(lista["IDAFILIADO"]);

                // INFORMAÇOE CTPS
                afiliado.CTPS = new CTPS
                {
                    Numero = lista["CTPSNUM"],
                    Serie = lista["CTPSSERIE"],
                    PIS = lista["PIS"]
                };

                if (lista.ContainsKey("CONTRIBUICAO"))
                    afiliado.Contribuicoes = AfiliacaoServico.GerarListaContribuicao(lista["CONTRIBUICAO"]);
                
                //TELEFONES
                var telefones = new List<Telefone>();

                telefones.Add(new Telefone
                {
                    DDD = lista["TELRESDDD"],
                    Numero = lista["TELRESNUM"],
                    TipoTelefone = TelefoneEnum.Residencia
                });

                telefones.Add(new Telefone
                {
                    DDD = lista["TELCELDDD"],
                    Numero = lista["TELCELNUM"],
                    TipoTelefone = TelefoneEnum.Celular01
                });

                afiliado.Telefones = telefones;

                //Datas
                DateTime dataNascimento = DataUtil.ConverterString(lista["DTNASC"]);
                afiliado.DataNascimento = dataNascimento;

                if (lista.ContainsKey("IDENDERECO")) idEndereco = Convert.ToInt32(lista["IDENDERECO"]);

                //Endereco
                Endereco endereco = new Endereco()
                {
                    ID = idEndereco,
                    Logradouro = lista["RUA"],
                    Bairro = lista["BAIRRO"],
                    CEP = lista["CEP"],
                    Cidade = lista["CIDADE"],
                    Complemento = lista["COMPLEMENTO"],
                    Numero = lista["NUMERO"],
                    UF = lista["UF"],
                    Pais = "BRASIL"
                };

                afiliado.Endereco = endereco;

                if (editar)
                {
                    result = AfiliacaoServico.Editar(afiliado);                    
                }
                else
                {
                    result = AfiliacaoServico.Insere(afiliado);
                }

                return result;

            } catch (Exception) {
                throw;
            }

        }

        public static int InsereDependente(Dictionary<string, string> lista, int idAfiliado)
        {
            try
            {
                int result = 0;
                int acrescimo = 0;

                // criando objeto de afiliado
                var dependente = new Dependentes();

                dependente.Nome = lista["NOME"];
                dependente.GrauParentescoID = Convert.ToInt32(lista["GRAUPARENTE"]);

                if (lista.ContainsKey("ACRESCIMO"))
                    acrescimo = Convert.ToInt32(lista["ACRESCIMO"]);
                                
                //Datas
                DateTime dataNascimento = DataUtil.ConverterString(lista["DTNASC"]);

                dependente.DataNascimento = dataNascimento;
                dependente.AcrescimoMensal = acrescimo;
                dependente.IdAfiliado = idAfiliado;
                
                DependenteServico.Insere(dependente, 1, idAfiliado);

                return result;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public static Dictionary<string, string> GeraListaCampos(FormCollection collection){

            var nomeCampo = "";
            var lista = new Dictionary<string, string>();

            foreach (string formDados in collection)
            {
                nomeCampo = formDados.ToUpper().Trim();
                if (nomeCampo.ToUpper().Trim() == "SENHA2") nomeCampo = "CONFIRMAÇÃO DE SENHA";

                lista.Add(nomeCampo, collection[formDados]);
            }

            return lista;

        }

        public static Dictionary<string, bool> ValidaCampoObrigatorio(FormCollection collection)
        {

            var listaCampoOpcional = Validacao.FormAfiliacaoCampoOpcional();
            var result = new Dictionary<string, bool>();

            foreach (string formDados in collection)
            {
                if (!listaCampoOpcional.Contains(formDados.ToUpper()))
                {
                    var valorCampo = collection[formDados].Trim();
                    if (valorCampo == "") result.Add(formDados.ToUpper(), false);
                }

            }

            return result;
        }
    }
}
