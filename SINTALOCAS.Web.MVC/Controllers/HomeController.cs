using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using SINTALOCAS.Dominio.Servico;
using SINTALOCAS.Modelo;

namespace SINTALOCAS.Web.MVC.Controllers
{
    public class HomeController : Controller
    {
        //private ContextoDBMVC _contexto = new ContextoDBMVC();
        private UFServico _ufServ = new UFServico();

        public ActionResult Index()
        {
            var lista = _ufServ.Consultar();

            foreach(UnidadeFederativa ufItem in lista){
                ViewBag.Retorno += ufItem.Descricao;
            }

            var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;

            ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
            ViewData["Runtime"] = isMono ? "Mono" : ".NET";

            return View();
        }
    }
}
