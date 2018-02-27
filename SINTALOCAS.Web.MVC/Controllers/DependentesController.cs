using SINTALOCAS.Dominio.Servico;
using SINTALOCAS.Dominio.Util;
using SINTALOCAS.Web.MVC.Servico;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SINTALOCAS.Web.MVC.Controllers
{
    public class DependentesController : Controller
    {
        private DependenteServico _dependenteServ = new DependenteServico();

        public ActionResult Index()
        {
            int idAfiliado = 0;

            if (TempData["idAfiliadoForm"] != null)
            {
                if (!Int32.TryParse(TempData["idAfiliadoForm"].ToString(), out idAfiliado)) ViewBag.MensagemRetorno = MensagemUtil.ErroIDForm();

                TempData["idAfiliadoForm"] = idAfiliado; // renovando sessao
                GeraViewBag(idAfiliado);
                CombosForm();
            }
            else
            {
                ViewBag.MensagemRetorno = MensagemUtil.ErroGeneralizado();
                return View("Afiliacao");
            }

            return View();
        }

        private void GeraViewBag(int idAfiliado)
        {
            ViewBag.RootView = Validacao.AnalisaLink(@Request.RawUrl.ToString());
            ViewBag.LinkSubmitAfilia = Validacao.AnalisaLink(@Request.RawUrl.ToString() + "/Finaliza");
            ViewBag.GrauParentesco = _dependenteServ.DictionaryGrausParentesco();
            ViewBag.ListaDependentes = _dependenteServ.ListaDependentes(idAfiliado);
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

            int idAfiliado = 0;

            if (TempData["idAfiliadoForm"] != null)
            {
                if (!Int32.TryParse(TempData["idAfiliadoForm"].ToString(), out idAfiliado)) result = MensagemUtil.ErroIDForm();
                TempData["idAfiliadoForm"] = idAfiliado; // renovando sessao
            }
            else
            {
                result = MensagemUtil.ErroGeneralizado();
            }

            result = _dependenteServ.ValidarCadastroDependente(idAfiliado);

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
                int idAfiliado = 0;

                if (TempData["idAfiliadoForm"] != null)
                {
                    if (!Int32.TryParse(TempData["idAfiliadoForm"].ToString(), out idAfiliado))
                        ViewBag.MensagemRetorno = MensagemUtil.ErroIDForm();

                    TempData["idAfiliadoForm"] = idAfiliado; // renovando sessao                   
                }
                else
                {
                    ViewBag.MensagemRetorno = MensagemUtil.ErroGeneralizado();
                }

                validacaoViewServico.InsereDependente(lista, idAfiliado);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
