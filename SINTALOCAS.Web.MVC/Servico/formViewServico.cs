using System;
namespace SINTALOCAS.Web.MVC.Servico
{
    public static class formViewServico
    {
        public static bool ValidarForm(FormCollection Collection)
        {
            var result = "";

            //Convertendo informaçoes dos campos em uma lista
            var lista = AfiliacaoViewsServico.GeraListaCampos(Collection);

            //validar campos opcionais
            result = Validacao.FormAfiliacaoValidarPreenchimento(lista);

            //validação específica cpf, cpn, pis, etc.
            result = Validacao.ValidarCodigos(lista);

            ViewBag.MensagemRetorno = result;

            if (result.Trim() == "")
            {
                InserirDados(lista); //gravando informaçoes
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
