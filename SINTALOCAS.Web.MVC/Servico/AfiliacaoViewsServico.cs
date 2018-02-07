using System;
using System.Collections.Generic;
using System.Globalization;
using SINTALOCAS.Dominio.Servico;
using SINTALOCAS.Modelo;
using SINTALOCAS.Modelo.Enumerator;
using SINTALOCAS.Dominio.Util;
using System.Web.Mvc;

namespace SINTALOCAS.Web.MVC.Servico
{
    public static class AfiliacaoViewsServico
    {
        
        public static int Insere(Dictionary<string, string> lista)
        {
            try
            {
                int result = 0;

                // criando objeto de afiliado
                var afiliado = new Afiliado();

                afiliado.Cargo = lista["CARGO"];
                afiliado.Consir = lista["CONSIR"];
                afiliado.CPF = lista["CPF"];
                afiliado.CNPJ = lista["CNPJ"];
                afiliado.Empresa = lista["EMPRESA"];
                afiliado.RG = lista["RG"];
                afiliado.Email = lista["EMAIL"];
                //afiliado.Matricula = lista["Matricula"];
                afiliado.Nome = lista["NOME"];
                afiliado.NomeMae = lista["NOMEMAE"];
                afiliado.NomePai = lista["NOMEPAI"];
                afiliado.PIS = lista["PIS"];

                // INFORMAÇOE CTPS
                afiliado.CTPS = new CTPS
                {
                    Numero = lista["CTPSNUM"],
                    Serie = lista["CTPSSERIE"]
                };

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

                var _afiliacaoServ = new AfiliacaoServico();

                _afiliacaoServ.Insere(afiliado);

                return result;

            } catch (Exception ex) {
                throw ex;
            }

        }

        public static Dictionary<string, string> GeraListaCampos(FormCollection collection){

            var lista = new Dictionary<string, string>();

            foreach (string formDados in collection)
            {
                lista.Add(formDados.ToUpper(), collection[formDados]);
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
