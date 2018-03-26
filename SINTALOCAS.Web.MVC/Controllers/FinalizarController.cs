using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SINTALOCAS.Dominio.Servico;
using SINTALOCAS.Dominio.Util;
using SINTALOCAS.Modelo;

namespace SINTALOCAS.Web.MVC.Controllers
{
    public class FinalizarController : Controller
    {
        // GET: Finalizar
        public ActionResult Index()
        {
            int idAfiliado = ConsultaIdAfiliado();
            var mensagemSistema = TextosServico.TextoDeAcordo();
            GeraViewBag(idAfiliado);

            if (TempData["idAfiliadoForm"] != null)
            {
                if (!Int32.TryParse(TempData["idAfiliadoForm"].ToString(), out idAfiliado)) ViewBag.MensagemRetorno = MensagemUtil.ErroIDForm();

                TempData["idAfiliadoForm"] = idAfiliado; // renovando sessao
            }
            else
            {
                ViewBag.MensagemRetorno = MensagemUtil.ErroGeneralizado();
            }

            ViewBag.Titulo = mensagemSistema.Titulo;
            ViewBag.TextoPrincipal = mensagemSistema.Texto.Replace(System.Environment.NewLine, "<br/>");
            return View();
        }

        [HttpPost]
        public ActionResult Realizado(FormCollection collection)
        {
            try
            {
                int idAfiliado = ConsultaIdAfiliado();
                AfiliacaoServico.Concordar(idAfiliado);
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private int ConsultaIdAfiliado()
        {
            var idAfiliado = 0;

            if (TempData["idAfiliadoForm"] != null)
            {
                if (!Int32.TryParse(TempData["idAfiliadoForm"].ToString(), out idAfiliado))
                    ViewBag.MensagemRetorno = MensagemUtil.ErroIDForm();
                TempData["idAfiliadoForm"] = idAfiliado; // renovando sessao                
            }
            else
            {
                ViewBag.MensagemRetorno = MensagemUtil.ErroGeneralizado();
            }

            return idAfiliado;

        }

        private void GeraViewBag(int idAfiliado)
        {
            ViewBag.Aviso = MensagemUtil.AvisoConcordo();
            ViewBag.RootView = Validacao.AnalisaLink(@Request.RawUrl.ToString());
            var afiliado = AfiliacaoServico.GetByID(idAfiliado);
            var dependentes = new List<Dependentes>();

            if (afiliado!=null)
                dependentes = DependenteServico.ListaDependentes(idAfiliado);
        }
    }
}
