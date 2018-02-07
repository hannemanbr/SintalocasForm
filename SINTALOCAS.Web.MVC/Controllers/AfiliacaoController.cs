using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SINTALOCAS.Dominio.Util;
using SINTALOCAS.Web.MVC.Servico;

namespace SINTALOCAS.Web.MVC.Controllers
{
    public class AfiliacaoController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Create");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                //Convertendo informaçoes dos campos em uma lista
                var lista = AfiliacaoViewsServico.GeraListaCampos(collection);

                //validar cpf, cpn, pis, etc.
                var result = Validacao.FormAfiliacaoValidarPreenchimento(lista);

                if (result == "") AfiliacaoViewsServico.Insere(lista);

                ViewBag.MensagemRetorno = result;
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