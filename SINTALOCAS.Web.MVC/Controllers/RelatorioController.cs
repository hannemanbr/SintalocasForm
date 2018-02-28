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

        public ActionResult RelAfiliados()
        {
            GeraViewBagRelatorio();
            return View();
        }

        public ActionResult DeleteAfiliado(int id)
        {            
            AfiliacaoServico.Delete(id);
            GeraViewBagRelatorio();
            return View("RelAfiliados");
        }

        public ActionResult DetalheAfiliado(string id)
        {
            var listaAfiliado = AfiliacaoServico.Listar(id.ToString());
            
            ViewBag.Afiliados = listaAfiliado;

            if (listaAfiliado.Count > 0)
            {
                var idAfiliado = listaAfiliado[0].ID;
                ViewBag.Dependentes = DependenteServico.ListaDependentes(idAfiliado);
            }

            return View();
        }

        private void GeraViewBag()
        {
            LogAtivo();            
            ViewBag.ListaRelatorio = RelatorioServico.ListarRelatorios();
        }
        
        private void GeraViewBagRelatorio()
        {
            LogAtivo();
            ViewBag.Afiliados = AfiliacaoServico.Listar().Where(x => x.Nome.Trim() != "").ToList();
        }

        private void LogAtivo()
        {
            var usuario = TempData["LogAtivo"];
            TempData["LogAtivo"] = usuario;
            ViewBag.UsuarioLogin = usuario;
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
