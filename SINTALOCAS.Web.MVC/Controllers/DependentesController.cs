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
            int idAfiliado = ConsultaIdAfiliado();
            if (idAfiliado == 0) return View("Afiliacao");

            GeraViewBag(idAfiliado);
            CombosForm();

            return View();
        }

        public ActionResult Delete(int id)
        {
            var result = _dependenteServ.Remove(id);

            int idAfiliado = ConsultaIdAfiliado();
            if (idAfiliado == 0) return View("Afiliacao");

            GeraViewBag(idAfiliado);
            CombosForm();

            return View("Index");
        }

        private int ConsultaIdAfiliado()
        {
            var idAfiliado = 0;

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

            return idAfiliado;

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

            int idAfiliado = ConsultaIdAfiliado();
            if (idAfiliado == 0) return false;

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
                int idAfiliado = ConsultaIdAfiliado();
                validacaoViewServico.InsereDependente(lista, idAfiliado);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
