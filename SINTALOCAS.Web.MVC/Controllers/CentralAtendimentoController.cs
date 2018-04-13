using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SINTALOCAS.Web.MVC.Controllers
{
    public class CentralAtendimentoController : Controller
    {
        // GET: CentralAtendimento
        public ActionResult Index()
        {
            return View();
        }

        // GET: CentralAtendimento/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CentralAtendimento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CentralAtendimento/Create
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

        // GET: CentralAtendimento/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CentralAtendimento/Edit/5
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

        // GET: CentralAtendimento/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CentralAtendimento/Delete/5
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
