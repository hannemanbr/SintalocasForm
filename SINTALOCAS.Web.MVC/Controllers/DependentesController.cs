using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SINTALOCAS.Dominio.Servico;
using SINTALOCAS.Dominio.Util;
using SINTALOCAS.Web.MVC.Servico;

namespace SINTALOCAS.Web.MVC.Controllers
{
    public class DependentesController : Controller
    {
        private DependenteServico _dependenteServ = new DependenteServico();

        public ActionResult Index()
        {
            CombosForm();
            ViewBag.RootView = Validacao.AnalisaLink(@Request.RawUrl.ToString());
            ViewBag.LinkSubmitAfilia = Validacao.AnalisaLink(@Request.RawUrl.ToString() + "/Finaliza");
            ViewBag.GrauParentesco = _dependenteServ.DictionaryGrausParentesco();
            return View();
        }

        [HttpPost]
        public JsonResult ValidarFormJSON(FormCollection Collection)
        {
            var retorno = ValidarForm(Collection);
            var mensagem = "";

            if (!retorno) mensagem = MensagemUtil.ErroCamposNaoPreenchidos();

            return Json(new { success = retorno, msg = mensagem }, JsonRequestBehavior.AllowGet); ;

        }

        [HttpGet]
        public string ValidarDtNasc(string dtnasc)
        {
            var result = "";

            if (!ValidaCodigosUtil.ValidaDtNasc(dtnasc)) result = MensagemUtil.ErroDTNASCInvalido();

            return result;
        }

        public bool ValidarForm(FormCollection Collection)
        {
            var result = "";

            //Convertendo informaçoes dos campos em uma lista
            var lista = validacaoViewServico.GeraListaCampos(Collection);

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


        private void CombosForm()
        {

            try
            {
                ViewBag.GrauParentesco = _dependenteServ.DictionaryGrausParentesco();
            }
            catch (Exception ex)
            {
                ViewBag.MensagemRetorno = ex.Message;
            }

        }

        private void InserirDados(Dictionary<string, string> lista)
        {

            try
            {
                validacaoViewServico.InsereDependente(lista);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
