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
            return RedirectToAction("Create");
        }

        public ActionResult Create()
        {
            try
            {
                ViewBag.UFLista = _ufServ.Consultar();
                ViewBag.DDDLista = _ufServ.DDDs();
                ViewBag.Afiliado = new AfiliacaoModelView();

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.MensagemRetorno = ex.Message;
                return View(ViewBag.MensagemRetorno);
            }
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                
                //Convertendo informaçoes dos campos em uma lista
                var lista = AfiliacaoViewsServico.GeraListaCampos(collection);

                //Gerando modelo para preencher form se recarregado
                var afiliadoModelView = AfiliacaoViewsServico.GeraAfiliacaoModelView(lista);
                ViewBag.Afiliado = afiliadoModelView;

                //validar cpf, cpn, pis, etc.
                var result = Validacao.FormAfiliacaoValidarPreenchimento(lista);
                ViewBag.MensagemRetorno = result;

                if (result == "")
                { 
                    AfiliacaoViewsServico.Insere(lista); 
                }

                return View();

            }
            catch (Exception ex)
            {
                ViewBag.MensagemRetorno = "Ocorreu um problema durante a operação, tente novamente -  " + ex.Message;
                return View();
            }

        }

        public JsonResult BuscaEndereco(string cep)
        {
            var endereco = EnderecoUtil.ConsultarEndereco(cep);
            return Json(endereco, JsonRequestBehavior.AllowGet);
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
            var listaUFs = _ufServ.Consultar().Select(x=>x.UF).ToList();

            if (!listaUFs.Contains(Uf.ToUpper())) result = MensagemUtil.ErroUFInvalido();

            return result;
        }

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