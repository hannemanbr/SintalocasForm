using SINTALOCAS.Dominio.Servico;
using SINTALOCAS.Dominio.Util;
using SINTALOCAS.Web.MVC.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SINTALOCAS.Web.MVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            GeraViewBag();
            return View();
        }

        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            return View("Index");
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            //Convertendo informaçoes dos campos em uma lista
            var lista = validacaoViewServico.GeraListaCampos(collection);

            if (lista.Count > 0)
            {
                var email = lista["EMAIL"];
                var senha = lista["SENHA"];

                senha = UsuarioServico.GerarSenhaSHA1(senha);
                var validar = UsuarioServico.ConsultarLogin(email, senha);

                if (validar.Count == 0)
                {
                    ViewBag.MensagemRetorno = "Usuário ou senha inválido";
                    return View();
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(validar[0].Email, false);
                    TempData["LogAtivo"] = validar[0];
                    return Redirect("~/Relatorio");
                }
            }

            ViewBag.MensagemRetorno = "Usuário inválido";
            return View();
        }

        private void GeraViewBag()
        {
            ViewBag.RootView = "Index"; //Validacao.AnalisaLink(@Request.RawUrl.ToString());
            //ViewBag.LinkSubmitAfilia = Validacao.AnalisaLink(@Request.RawUrl.ToString() + "/Admin");
        }

        [ChildActionOnly]
        public void VerificarLogin()
        {
            var login = HttpContext.User.Identity.Name;

            if (login == "")
                RedirectToAction("Index", "Admin");
            
        }

        public void Logoff()
        {
            FormsAuthentication.SignOut();
        }
    }
}