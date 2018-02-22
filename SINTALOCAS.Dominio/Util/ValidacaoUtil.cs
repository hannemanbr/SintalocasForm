using System;
using System.Collections.Generic;
using System.Linq;

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

        public static string AnalisaLink(string link)
        {
            link = link.Trim().Replace("//", "/"); // remove barras duplicadas

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
