using SINTALOCAS.Dominio.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SINTALOCAS.Web.MVC.Controllers
{
    public class LogController : Controller
    {
        [ChildActionOnly]
        // GET: Log
        public void AcessoFormAfilia()
        {
            LogServico.Registrar("UsuarioWeb", Request.UserHostAddress, Request.UserHostName, Request.RawUrl, "Acesso", "", 0);
        }

        [ChildActionOnly]
        // GET: Log
        public void AcessoAdmin()
        {
            LogServico.Registrar("PainelControle", Request.UserHostAddress, Request.UserHostName, Request.RawUrl, "Acesso", "", 0);
        }

    }
}