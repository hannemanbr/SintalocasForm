using SINTALOCAS.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SINTALOCAS.Dominio.Servico;
using SINTALOCAS.Web.MVC.Servico;
using SINTALOCAS.Dominio.Util;
using Rotativa;
using System.Web.Security;
using SINTALOCAS.Modelo.Enumerator;

namespace SINTALOCAS.Web.MVC.Controllers
{
    [Authorize]
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
            ConsultarPorCPF(id);
            GeraViewBagDetalhe();

            return View();
        }
        
        private void GeraViewBag()
        {
            var id = Convert.ToInt32(Server.HtmlEncode(User.Identity.Name));
            ViewBag.UsuarioLogin = UsuarioServico.ConsultarPorID(id).Nome;
            ViewBag.Saudadacao = MensagemUtil.Saudacao();
            ViewBag.ListaRelatorio = RelatorioServico.ListarRelatorios();
        }

        [HttpPost]
        public ActionResult DetalheAfiliado(FormCollection collection)
        {
            if (ValidarForm(collection))
            {
                ViewBag.MensagemRetorno = MensagemUtil.OperacaoRealizada();
                ViewBag.ColorAlerta = "#149603";
            }
            else
            {
                ViewBag.ColorAlerta = "#FF0000";
            }

            var cpf = TempData["cpfAfiliado"].ToString();

            ConsultarPorCPF(cpf);
            GeraViewBagDetalhe();
            return View();
        }

        public bool ValidarForm(FormCollection Collection)
        {
            var validar = false;

            try
            {
                var result = "";

                //Convertendo informaçoes dos campos em uma lista
                var lista = validacaoViewServico.GeraListaCampos(Collection);

                //validar campos opcionais
                result = Validacao.FormAfiliacaoValidarPreenchimento(lista);

                //validação específica cpf, cpn, pis, etc.
                result = Validacao.ValidarCodigos(lista);

                if (result.Trim() == "")
                {
                    var reg = validacaoViewServico.Atualizar(lista, true); //gravando informaçoes

                    validar = true;
                }
                else
                {
                    ViewBag.MensagemRetorno = result;
                    validar = false;
                }
            }
            catch (Exception ex)
            {
                validar = false;
                ViewBag.MensagemRetorno = ex.Message;                
            }

            return validar;
        }

        private void GeraViewBagRelatorio(int opcaoRelatorio, string id = "")
        {
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

            if (opcaoRelatorio == 4)
                ViewBag.Lista = UsuarioServico.Consultar("", 0);

        }

        public ActionResult PDFPadrao(int id)
        {
            ViewBag.TituloRelatorio = "SINTALOCAS Relatoório";
            var viewRelatorio = "";

            if (id == 2)
            {
                GeraViewBagRelatorio(id);
                ViewBag.TituloRelatorio += " - Log de acessos";
                viewRelatorio = "RelLogPDF";
            }
            if (id == 1)
            {
                GeraViewBagRelatorio(id);
                ViewBag.TituloRelatorio += " - Cadastro de afiliados";
                viewRelatorio = "RelAfiliadosPDF";
            }
            if (id == 3)
            {
                if (TempData["cpfAfiliado"] != null)
                {
                    var cpfAfiliado = TempData["cpfAfiliado"].ToString();
                    ViewBag.TituloRelatorio += " - Afiliado";
                    GeraViewBagRelatorio(id, cpfAfiliado);
                    viewRelatorio = "DetalheAfiliadoPDF";
                }
            }
            if (id == 4)
            {
                GeraViewBagRelatorio(id);
                ViewBag.TituloRelatorio += " - Usuários";
                viewRelatorio = "RelUsuariosPDF";
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

        private void ConsultarPorCPF(string cpf)
        {
            if (cpf.Trim() == "") ViewBag.MensagemRetorno = MensagemUtil.ErroGeneralizado();

            var listaAfiliado = AfiliacaoServico.Listar(cpf.ToString());
            TempData["cpfAfiliado"] = cpf;
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

        public ActionResult Delete(int id)
        {
            var cpf = TempData["cpfAfiliado"].ToString();
            var result = DependenteServico.Remove(id);

            if (id == 0) ViewBag.MensagemRetorno = MensagemUtil.ErroGeneralizado();
            ConsultarPorCPF(cpf);

            return RedirectToAction("DetalheAfiliado/" + TempData["cpfAfiliado"]);
        }

        private void GeraViewBagDetalhe()
        {
            ViewBag.Pagamento = PagamentoServico.ConsultarPorCategoria(OpcaoPagamentoEnum.PAGTO.ToString());
            ViewBag.Contribuicao = PagamentoServico.ConsultarPorCategoria(OpcaoPagamentoEnum.CONTRIBuICAO.ToString());
            ViewBag.GrauParentesco = DependenteServico.ListaGrausParentesco();
        }


    }
}
