using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SINTALOCAS.Dominio.Servico;
using SINTALOCAS.Dominio.Util;
using SINTALOCAS.Modelo;
using SINTALOCAS.Modelo.Enumerator;

namespace SINTALOCAS.Web.MVC.Controllers
{
    public class FinalizarController : Controller
    {
        // GET: Finalizar
        public ActionResult Index()
        {
            int idAfiliado = ConsultaIdAfiliado();
            
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
            var afiliado = AfiliacaoServico.GetByID(idAfiliado);

            ViewBag.Aviso = MensagemUtil.AvisoConcordo();
            ViewBag.RootView = Validacao.AnalisaLink(@Request.RawUrl.ToString());
            ViewBag.Pagamento = PagamentoServico.Consultar(OpcaoPagamentoEnum.PAGTO.ToString());
            ViewBag.Contribuicao = PagamentoServico.Consultar(OpcaoPagamentoEnum.CONTRIB.ToString());

            var mensagemSistema = TextosServico.TextoDeAcordo();

            ViewBag.Titulo = mensagemSistema.Titulo;
            ViewBag.TextoPrincipal = mensagemSistema.Texto.Replace(System.Environment.NewLine, "<br/>");
        }
    }
}
