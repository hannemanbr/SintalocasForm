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

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var validar = ValidarForm(collection);

                //if (!validar)
                //{
                //    //return Json(new { success = false, responseText = "falhou." }, JsonRequestBehavior.AllowGet);
                     
                //    return this.Json(new
                //    {
                //        EnableSuccess = false,
                //        SuccessTitle = "Success",
                //        SuccessMsg = "Erro"
                //    });
                //}
                //else
                //{
                //    //Convertendo informaçoes dos campos em uma lista
                //    var lista = AfiliacaoViewsServico.GeraListaCampos(collection);

                //    //validar cpf, cpn, pis, etc.
                //    var result = Validacao.FormAfiliacaoValidarPreenchimento(lista);
                //    ViewBag.MensagemRetorno = result;

                //    if (result == "")
                //    {
                //        AfiliacaoViewsServico.Insere(lista);
                //    }

                //    //return Json(new { success = true, responseText = View() }, JsonRequestBehavior.AllowGet);

                //}

                //Convertendo informaçoes dos campos em uma lista
                var lista = AfiliacaoViewsServico.GeraListaCampos(collection);

                //validar cpf, cpn, pis, etc.
                var result = Validacao.FormAfiliacaoValidarPreenchimento(lista);
                ViewBag.MensagemRetorno = result;

                if (result == "")
                {
                    AfiliacaoViewsServico.Insere(lista);
                }

                return RedirectToAction("Dependente");

            }
            catch (Exception ex)
            {
                ViewBag.MensagemRetorno = "Ocorreu um problema durante a operação, tente novamente -  " + ex.Message;
                //return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
                return this.Json(new
                {
                    EnableError = true,
                    ErrorTitle = "Error",
                    ErrorMsg = "Something goes wrong, please try again later"
                });
            }

        }
       
        public bool ValidarForm(FormCollection Collection)
        {
            var result = "";

            //Convertendo informaçoes dos campos em uma lista
            var lista = AfiliacaoViewsServico.GeraListaCampos(Collection);

            //validar cpf, cpn, pis, etc.
            result = Validacao.FormAfiliacaoValidarPreenchimento(lista);
            ViewBag.MensagemRetorno = result;

            if (result.Trim() == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPost]
        public JsonResult ValidarFormJSON(FormCollection Collection)
        {
            var retorno = ValidarForm(Collection);
            var mensagem = "";

            if (!retorno) mensagem = MensagemUtil.ErroCamposNaoPreenchidos();

            return Json(new { success = retorno, msg = mensagem }, JsonRequestBehavior.AllowGet); ;

        }

        public string DataHoraAtual()
        {
            return DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
        }

        [HttpGet]
        public string ValidarCPF(string Cpf)
        {
            var result = "";

            if (!ValidaCodigosUtil.ValidaCpf(Cpf)) result = MensagemUtil.ErroCPFInvalido();

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

            if (!ValidaCodigosUtil.ValidaCep(Cep))
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

        //public JsonResult BuscaEndereco(string cep)
        //{
        //    var endereco = EnderecoUtil.ConsultarEndereco(cep);
        //    return Json(endereco, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}


    }
}