using System;
using System.Collections.Generic;
using System.Linq;

namespace SINTALOCAS.Dominio.Util
{
    public static class ValidacaoForm
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

        public static string GeraMensagemErroRetorno(Dictionary<string, bool> listaValidacao)
        {

            var result = "";

            if (listaValidacao.Any())
            {
                result = "<strong>Preencha os campos:</strong> ";

                foreach (KeyValuePair<string, bool> item in listaValidacao)
                {
                    result += "<br/>-" + item.Key + "";
                }

            }

            return result;
        }

    }
}
