﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SINTALOCAS.Dominio.Util;
using SINTALOCAS.Web.MVC.Servico;
using SINTALOCAS.Dominio.Servico;
using SINTALOCAS.Web.MVC.Models;

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