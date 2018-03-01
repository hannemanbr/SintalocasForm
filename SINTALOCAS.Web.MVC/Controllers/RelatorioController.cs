using SINTALOCAS.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SINTALOCAS.Dominio.Servico;
using SINTALOCAS.Web.MVC.Servico;
using Rotativa;
using SINTALOCAS.Dominio.Util;

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
            GeraViewBagRelatorio(1);
            return View();
        }

        public ActionResult RelLog()
        {
            GeraViewBagRelatorio(2);
            return View();
        }

        public ActionResult DeleteAfiliado(int id)
        {            
            AfiliacaoServico.Delete(id);
            GeraViewBagRelatorio(1);
            return View("RelAfiliados");
        }

        public ActionResult DetalheAfiliado(string id)
        {
            var listaAfiliado = AfiliacaoServico.Listar(id.ToString());
            TempData["cpfAfiliado"] = id;
            ViewBag.Lista = listaAfiliado;

            if (listaAfiliado.Count > 0)
            {
                var idAfiliado = listaAfiliado[0].ID;
                
                ViewBag.Dependentes = DependenteServico.ListaDependentes(idAfiliado);
                ViewBag.Enderecos = listaAfiliado[0].Endereco;
            }

            return View();
        }

        private void GeraViewBag()
        {
            LogAtivo();            
            ViewBag.ListaRelatorio = RelatorioServico.ListarRelatorios();
        }
        
        private void GeraViewBagRelatorio(int opcaoRelatorio, string id = "")
        {
            LogAtivo();

            if (opcaoRelatorio == 1)
                ViewBag.Lista = AfiliacaoServico.Listar().Where(x => x.Nome.Trim() != "").ToList();

            if (opcaoRelatorio == 2)
                ViewBag.Lista = LogServico.ListaAgrupado();

            if (opcaoRelatorio == 3)
            {
                var listaAfiliado = AfiliacaoServico.Listar(id.ToString());

                ViewBag.Lista = listaAfiliado;

                if (listaAfiliado.Count > 0)
                {
                    var idAfiliado = listaAfiliado[0].ID;
                    ViewBag.Dependentes = DependenteServico.ListaDependentes(idAfiliado);

                    ViewBag.Enderecos = listaAfiliado[0].Endereco;
                }
            }

        }

        public ActionResult PDFPadrao(int id)
        {
            var viewRelatorio = "";

            if (id == 2)
            {
                GeraViewBagRelatorio(id);
                viewRelatorio = "RelLogPDF";
            }
            if (id == 1)
            {
                GeraViewBagRelatorio(id);
                viewRelatorio = "RelAfiliadosPDF";
            }
            if (id == 3)
            {
                if (TempData["cpfAfiliado"] != null)
                {
                    var cpfAfiliado = TempData["cpfAfiliado"].ToString();
                    GeraViewBagRelatorio(id, cpfAfiliado);
                    viewRelatorio = "DetalheAfiliadoPDF";
                }
            }

            if (viewRelatorio.Trim() != "")
            {
                var pdf = new ViewAsPdf
                {
                    ViewName = viewRelatorio
                };
                return pdf;
            }
            else
            {
                ViewBag.MensagemRetorn0 = MensagemUtil.ErroGeneralizado();
                return View();
            }
        }

        private void LogAtivo()
        {
            var usuario = TempData["LogAtivo"];
            TempData["LogAtivo"] = usuario;
            ViewBag.UsuarioLogin = usuario;
        }

    }
}
