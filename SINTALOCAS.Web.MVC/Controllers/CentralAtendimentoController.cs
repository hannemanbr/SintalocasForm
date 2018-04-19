using SINTALOCAS.Dominio.Servico;
using SINTALOCAS.Dominio.Util;
using SINTALOCAS.Modelo.Enumerator;
using SINTALOCAS.Web.MVC.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SINTALOCAS.Web.MVC.Controllers
{    
    [Authorize]
    public class CentralAtendimentoController : Controller
    {
        // GET: CentralAtendimento
        public ActionResult Index()
        {
            ConsultarPorCPF();
            return View();
        }

        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("../Home/");
        }
                
        private void ConsultarPorCPF()
        {
            var id = Convert.ToInt32(Server.HtmlEncode(User.Identity.Name));

            var listaAfiliado = AfiliacaoServico.GetByID(id);
            TempData["cpfAfiliado"] = id;
            ViewBag.Lista = listaAfiliado;

            if (listaAfiliado.ID > 0)
            {
                var idAfiliado = listaAfiliado.ID;
                ViewBag.DtNasc = listaAfiliado.DataNascimento.ToString("dd/MM/yyyy");
                ViewBag.Dependentes = DependenteServico.ListaDependentes(idAfiliado);
                ViewBag.Enderecos = listaAfiliado.Endereco;
                ViewBag.Telefone = listaAfiliado.Telefones.Where(x => x.TipoTelefone == TelefoneEnum.Residencia).First();
                ViewBag.Celular = listaAfiliado.Telefones.Where(x => x.TipoTelefone == TelefoneEnum.Celular01).First();

            }
        }

        private void GeraViewBagDetalhe()
        {
            ViewBag.Pagamento = PagamentoServico.ConsultarPorCategoria(OpcaoPagamentoEnum.PAGTO.ToString());
            ViewBag.Contribuicao = PagamentoServico.ConsultarPorCategoria(OpcaoPagamentoEnum.CONTRIBuICAO.ToString());
            ViewBag.GrauParentesco = DependenteServico.ListaGrausParentesco();
        }

    }
}
