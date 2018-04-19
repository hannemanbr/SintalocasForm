using SINTALOCAS.Dominio.Servico;
using SINTALOCAS.Dominio.Util;
using SINTALOCAS.Modelo;
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
            GeraViewBagDetalhe();
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
            var listaAfiliado = new List<Afiliado>();
            listaAfiliado.Add(AfiliacaoServico.GetByID(id));

            TempData["cpfAfiliado"] = id;
            ViewBag.Lista = listaAfiliado;

            if (listaAfiliado.Count > 0)
            {
                var idAfiliado = listaAfiliado[0].ID;
                ViewBag.DtNasc = listaAfiliado[0].DataNascimento.ToString("dd/MM/yyyy");
                ViewBag.Dependentes = DependenteServico.ListaDependentes(idAfiliado);
                ViewBag.Enderecos = listaAfiliado[0].Endereco;
                ViewBag.Telefone = listaAfiliado[0].Telefones.Where(x => x.TipoTelefone == TelefoneEnum.Residencia).First();
                ViewBag.Celular = listaAfiliado[0].Telefones.Where(x => x.TipoTelefone == TelefoneEnum.Celular01).First();

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
