using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SINTALOCAS.Web.MVC.Servico;

namespace SINTALOCAS.Web.MVC.Controllers
{
    public class AfiliacaoController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction ("Create");
        }

        public ActionResult Details(int id)
        {
            return View ();
        }

        public ActionResult Create()
        {
            return View ();
        } 

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var lista = new Dictionary<string, string>();

            foreach(string formDados in collection)
            {   
                lista.Add(formDados.ToUpper(), collection[formDados]);
            }

            AfiliacaoViewsServico.Insere(lista);

            return View();

            //try {
            //    //return RedirectToAction ("Index");
            //} catch {
            //    return View ();
            //}
        }
        
        public ActionResult Edit(int id)
        {
            return View ();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try {
                return RedirectToAction ("Index");
            } catch {
                return View ();
            }
        }

        public ActionResult Delete(int id)
        {
            return View ();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try {
                return RedirectToAction ("Index");
            } catch {
                return View ();
            }
        }

        [HttpGet]
        public HttpGetAttribute BuscaEndereco(){
            return null;
        }
    }
}