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
        
        private void GeraViewBagRelatorio(int opcaoRelatorio)
        {
            LogAtivo();

            if (opcaoRelatorio == 1)
                ViewBag.Lista = AfiliacaoServico.Listar().Where(x => x.Nome.Trim() != "").ToList();

            if (opcaoRelatorio == 2)
                ViewBag.Lista = LogServico.ListaAgrupado();

        }

        private void LogAtivo()
        {
            var usuario = TempData["LogAtivo"];
            TempData["LogAtivo"] = usuario;
            ViewBag.UsuarioLogin = usuario;
        }

    }
}
