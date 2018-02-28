using SINTALOCAS.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SINTALOCAS.Dominio.Servico;
using SINTALOCAS.Web.MVC.Servico;

namespace SINTALOCAS.Web.MVC.Controllers
{
    public class RelatorioController : Controller
    {
        // GET: Relatorio
        public ActionResult Index()
        {
            GeraViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            var lista = validacaoViewServico.GeraListaCampos(collection);
            return View();
        }

        private void GeraViewBag()
        {
            var usuario = TempData["LogAtivo"];
            TempData["LogAtivo"] = usuario;
            ViewBag.UsuarioLogin = usuario;
            ViewBag.ListaRelatorio = RelatorioServico.ListarRelatorios();
        }

        // GET: Relatorio/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Relatorio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Relatorio/Create
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

        // GET: Relatorio/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Relatorio/Edit/5
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

        // GET: Relatorio/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Relatorio/Delete/5
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
