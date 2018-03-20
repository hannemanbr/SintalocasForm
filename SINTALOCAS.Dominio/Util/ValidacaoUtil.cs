using System;
using System.Collections.Generic;
using System.Linq;
using SINTALOCAS.Dominio.Servico;

namespace SINTALOCAS.Dominio.Util
{
    public static class Validacao
    {
        
        public static List<string> FormAfiliacaoCampoOpcional()
        {
            var listaCampoOpcional = new List<string>();

            listaCampoOpcional.Add("NOMEPAI");
            listaCampoOpcional.Add("COMPLEMENTO");
            listaCampoOpcional.Add("TELRESDDD");
            listaCampoOpcional.Add("TELRESNUM");

            return listaCampoOpcional;
        }
              

        public static string AnalisaLink(string link, bool nivelRaiz = false)
        {
            link = link.Trim().Replace("//", "/"); // remove barras duplicadas

            if (nivelRaiz)
            {                
                var vetor = link.Split('/'); // envia link com nivel abaixo
                var indice = vetor.Count() - 2;
                link = ""; //reinicia variavel

                for(var i =0; i <= indice; i++)
                {
                    link += vetor[i] + "/";
                }
            }

            return link;
        }

        public static string FormAfiliacaoValidarPreenchimento(Dictionary<string, string> listaCampos){

            var result = "";

            // VALIDAR PREENCHIMENTO DE CAMPO OBRIGATORIO
            var camposOpcionais = FormAfiliacaoCampoOpcional();

            foreach(KeyValuePair<string, string> itens in listaCampos)
            {
                if (!camposOpcionais.Contains(itens.Key.ToUpper()))
                {
                    var valorCampo = itens.Value.Trim();
                    if (valorCampo == "") result += "<li>" + itens.Key + "</li>";
                }

            }

            if (result.Trim() != "") result = "<strong>Preencha os campos:</strong> <ul>" + result + "</ul>";

            return result;

        }
        
        public static string FormUsuarioValidarPreenchimento(Dictionary<string, string> listaCampos)
        {

            var result = "";

            // VALIDAR PREENCHIMENTO DE CAMPO OBRIGATORIO
            var camposOpcionais = FormAfiliacaoCampoOpcional();

            foreach (KeyValuePair<string, string> itens in listaCampos)
            {
                var valorCampo = itens.Value.Trim();
                if (valorCampo == "") result += "<li>" + itens.Key + "</li>";
            }

            //validar se existe usuario
            if (listaCampos.ContainsKey("EMAIL"))
            {
                if (UsuarioServico.Consultar(listaCampos["EMAIL"], 0).Count()>0)
                {
                    result += "<li>" + MensagemUtil.ErroEMAILExistente() + "</li>";
                }
            }
            
            if (listaCampos.ContainsKey("SENHA") && listaCampos.ContainsKey("CONFIRMAÇÃO DE SENHA"))
            {
                //VALIDANDO SENHA DIGITADA
                result += UsuarioServico.ValidarSenha(listaCampos["SENHA"].Trim());
                //VALIDAR CONFIRMAÇÃO DE SENHA
                if (listaCampos["SENHA"].Trim() != listaCampos["CONFIRMAÇÃO DE SENHA"].Trim()) result += "<li>" + MensagemUtil.ErroConfirmacaoSenha() + "</li>";
            }
            else
            {
                result += MensagemUtil.ErroCamposNaoPreenchidos();
            }

            if (result.Trim() != "") result = "<strong>Preencha os campos:</strong> <ul>" + result + "</ul>";

            return result;

        }

        public static string FormDependenteValidarPreenchimento(Dictionary<string, string> listaCampos)
        {

            var result = "";

            // VALIDAR PREENCHIMENTO DE CAMPO OBRIGATORIO
            var camposOpcionais = FormAfiliacaoCampoOpcional();

            foreach (KeyValuePair<string, string> itens in listaCampos)
            {
                if (!camposOpcionais.Contains(itens.Key.ToUpper()))
                {
                    var valorCampo = itens.Value.Trim();
                    if (valorCampo == "") result += "<li>" + itens.Key + "</li>";
                }

            }

            if (result.Trim() != "") result = "<strong>Preencha os campos:</strong> <ul>" + result + "</ul>";

            return result;

        }

        public static string ValidarCodigos(Dictionary<string, string> listaCampos)
        {
            var result = "";

            // VALIDAR CRP, CPF PIS
            if (listaCampos.Keys.Contains("CPF"))
            {
                if (!ValidaCodigosUtil.ValidaCpf(listaCampos["CPF"].ToString()))
                {
                    result += "<li>CPF inválido</li>";
                }
            }

            if (listaCampos.Keys.Contains("CNPJ"))
            {
                if (!ValidaCodigosUtil.ValidaCnpj(listaCampos["CNPJ"].ToString()))
                {
                    result += "<li>CNPJ inválido</li>";
                }
            }

            if (listaCampos.Keys.Contains("PIS"))
            {
                if (!ValidaCodigosUtil.ValidaPis(listaCampos["PIS"].ToString()))
                {
                    result += "<li>PIS inválido</li>";
                }
            }

            if (listaCampos.Keys.Contains("CEP"))
            {
                if (!ValidaCodigosUtil.ValidaCep(listaCampos["CEP"].ToString()))
                {
                    result += "<li>CEP inválido</li>";
                }
            }

            if (listaCampos.Keys.Contains("TelCelDDD") && listaCampos.Keys.Contains("TelCelNum"))
            {
                if (!ValidaCodigosUtil.ValidarDDD(listaCampos["TelCelDDD"].ToString()))
                {
                    result += "<li>Celular: DDD inválido</li>"; //
                }
            }

            return result;
        }

    }
}
