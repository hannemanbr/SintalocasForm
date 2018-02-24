using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SINTALOCAS.Dominio.Servico;

namespace SINTALOCAS.Web.MVC.Controllers
{
    public class FinalizarController : Controller
    {
        // GET: Finalizar
        public ActionResult Index()
        {
            
            ViewBag.TextoPrincipal = TextosServico.TextoDeAcordo().Replace(System.Environment.NewLine, "<br/>");
            return View();
        }

        // GET: Finalizar/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Finalizar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Finalizar/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Finalizar/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Finalizar/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Finalizar/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Finalizar/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
