using SINTALOCAS.Dominio.Servico;
using SINTALOCAS.Dominio.Util;
using SINTALOCAS.Modelo;
using SINTALOCAS.Web.MVC.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SINTALOCAS.Web.MVC.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            try
            {
                var listaCampos = validacaoViewServico.GeraListaCampos(collection);
                var destino = listaCampos["DESTINO"];
                
                EmailServico.EnviarEmail(destino, "teste e-mail");

                return View("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Msg = ex.Message;
                return View();
            }
        }
    }
}