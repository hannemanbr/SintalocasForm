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
        public void Index()
        {
            LogServico.Registrar("UsuarioWeb", Request.UserHostAddress, Request.UserHostName, Request.RawUrl, "Acesso", "", 0);
        }
    }
}