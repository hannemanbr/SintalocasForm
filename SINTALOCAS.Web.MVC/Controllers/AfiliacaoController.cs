using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Web.Mvc;
using SINTALOCAS.Dominio.Util;
using SINTALOCAS.Web.MVC.Servico;
using SINTALOCAS.Dominio.Servico;
using SINTALOCAS.Web.MVC.Models;
using SINTALOCAS.Modelo;

namespace SINTALOCAS.Web.MVC.Controllers
{
    public class AfiliacaoController : Controller
    {
        UFServico _ufServ = new UFServico();

        public ActionResult Index()
        {
            //return RedirectToAction("Create");
            CombosForm();
            return View();
        }

        public ActionResult Create()
        {
            //CombosForm();
            return View();
        }

        public ActionResult Dependente()
        {
            return View();
        }
               
        public bool ValidarForm(FormCollection Collection)
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

        [HttpPost]
        public ActionResult Index(FormCollection Collection) {
            return View("Create");
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
        public string ValidarCPF(string Cpf)
        {
            var result = "";

            if (!ValidaCodigosUtil.ValidaCpf(Cpf)) result = MensagemUtil.ErroCPFInvalido();

            return result;
        }

        [HttpGet]
        public string ValidarEMAIL(string emailtx)
        {
            var result = "";

            if (!ValidaCodigosUtil.ValidarEmail(emailtx)) result = MensagemUtil.ErroEMAILInvalido();

            return result;
        }

        [HttpGet]
        public string ValidarRG(string Rg)
        {
            var result = "";

            if (!ValidaCodigosUtil.ValidaRG(Rg)) result = MensagemUtil.ErroRGInvalido();

            return result;
        }

        [HttpGet]
        public string ValidarPIS(string Pis)
        {
            var result = "";

            if (!ValidaCodigosUtil.ValidaPis(Pis)) result = MensagemUtil.ErroPIsInvalido();

            return result;
        }

        [HttpGet]
        public string ValidarCNPJ(string Cnpj)
        {
            var result = "";

            if (!ValidaCodigosUtil.ValidaCnpj(Cnpj)) result = MensagemUtil.ErroCNPJInvalido();

            return result;
        }

        [HttpGet]
        public string ValidarCEP(string Cep)
        {    
            var result = new Endereco();


            if (!ValidaCodigosUtil.ValidaCep(Cep) || Cep.Trim().Length < 8)
            {
                result = null; // MensagemUtil.ErroCEPInvalido();
            }
            else
            {
                //CONSULTAR ENDEREÇO COM CEP INFORMADO
                var endereco = EnderecoUtil.ConsultarEndereco(Cep);

                if (endereco.ID == -1) ViewBag.MensagemCEPRetorno = MensagemUtil.ErroCEPInvalido();

                result = endereco;
            }

            string json = JsonConvert.SerializeObject(result, Formatting.Indented);
            return json;

        }

        [HttpGet]
        public string ValidarUF(string Uf)
        {
            var result = "";
            var listaUFs = _ufServ.Consultar().Select(x => x.UF).ToList();

            if (!listaUFs.Contains(Uf.ToUpper())) result = MensagemUtil.ErroUFInvalido();

            return result;
        }

        [HttpGet]
        public string ValidarDtNasc(string dtnasc)
        {
            var result = "";

            if (!ValidaCodigosUtil.ValidaDtNasc(dtnasc)) result = MensagemUtil.ErroDTNASCInvalido();

            return result;
        }

        private void CombosForm()
        {

            try
            {
                ViewBag.UFLista = _ufServ.Consultar();
                ViewBag.DDDLista = _ufServ.DDDs();
                ViewBag.Afiliado = new AfiliacaoModelView();
            }
            catch (Exception ex)
            {
                ViewBag.MensagemRetorno = ex.Message;
            }

        }

        private void InserirDados(Dictionary<string, string> lista) {

            try
            {
                AfiliacaoViewsServico.Insere(lista);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}