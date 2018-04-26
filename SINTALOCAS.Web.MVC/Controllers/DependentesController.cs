using SINTALOCAS.Dominio.Servico;
using SINTALOCAS.Dominio.Util;
using SINTALOCAS.Web.MVC.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SINTALOCAS.Web.MVC.Controllers
{
    public class DependentesController : Controller
    {

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
            var result = DependenteServico.Remove(id);

            int idAfiliado = ConsultaIdAfiliado();
            if (idAfiliado == 0) return View("Afiliacao");

            GeraViewBag(idAfiliado);
            //CombosForm();

            return RedirectToAction("Index");
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
            ViewBag.LinkSubmitAfilia = Validacao.AnalisaLink(@Request.RawUrl.ToString() + "/ValidarFormJSON");
            ViewBag.GrauParentesco = DependenteServico.DictionaryGrausParentesco();
            ViewBag.ListaDependentes = DependenteServico.ListaDependentes(idAfiliado);
        }

        [HttpPost]
        public JsonResult ValidarFormJSON(FormCollection Collection)
        {
            var retorno = false;
            var mensagem = ValidarForm(Collection);

            if (mensagem.Trim() == "") retorno = true;

            return Json(new { success = retorno, msg = mensagem }, JsonRequestBehavior.AllowGet); ;

        }

        [HttpGet]
        public string ValidarDtNasc(string dtnasc)
        {
            var result = "";

            if (!ValidaCodigosUtil.ValidaDtNasc(dtnasc)) result = MensagemUtil.ErroDTNASCInvalido();

            return result;
        }

        public string ValidarForm(FormCollection Collection)
        {
            var result = "";

            //Convertendo informaçoes dos campos em uma lista
            var lista = validacaoViewServico.GeraListaCampos(Collection);

            //validar campos opcionais
            result = Validacao.FormAfiliacaoValidarPreenchimento(lista);

            //validação específica cpf, cpn, pis, etc.
            result = Validacao.ValidarCodigos(lista);

            int idAfiliado = ConsultaIdAfiliado();
            if (idAfiliado == 0) result += MensagemUtil.ErroGeneralizado(); ;

            result = DependenteServico.ValidarCadastroDependente(idAfiliado);

            ViewBag.MensagemRetorno = result;

            if (result.Trim() == "")
            {
                InserirDados(lista, idAfiliado); //gravando informaçoes
                GeraViewBag(idAfiliado);
                //CombosForm();
            }

            return result;

        }
        
        private void CombosForm()
        {

            try
            {
                ViewBag.GrauParentesco = DependenteServico.ListaGrauParentescoDisponivel(ConsultaIdAfiliado());
            }
            catch (Exception ex)
            {
                ViewBag.MensagemRetorno = ex.Message;
            }

        }

        private void InserirDados(Dictionary<string, string> lista, int idAfiliado)
        {

            try
            {
                validacaoViewServico.InsereDependente(lista, idAfiliado);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
