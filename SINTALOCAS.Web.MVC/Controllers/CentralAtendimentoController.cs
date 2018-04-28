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

        public ActionResult Dependente()
        {
            GeraViewBag();
            CombosForm();

            return View();
        }

        [HttpPost]
        public ActionResult Dependente(FormCollection collection)
        {
            var result = ValidarForm(collection);

            ViewBag.MensagemRetorno = MensagemUtil.OperacaoRealizada();

            if (result.Trim() != "")
                ViewBag.MensagemRetorno = MensagemUtil.ErroCamposNaoPreenchidos();

            GeraViewBag();
            CombosForm();

            return View();
        }

        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("../Home/");
        }
                
        private Afiliado ConsultarPorCPF()
        {
            var idAfiliado = Convert.ToInt32(Server.HtmlEncode(User.Identity.Name));
            var afiliado = AfiliacaoServico.GetByID(idAfiliado);

            TempData["idAfiliado"] = afiliado.ID;

            ViewBag.DtNasc = afiliado.DataNascimento.ToString("dd/MM/yyyy");
            ViewBag.Dependentes = DependenteServico.ListaDependentes(idAfiliado);
            ViewBag.Enderecos = afiliado.Endereco;
            ViewBag.Telefone = afiliado.Telefones.Where(x => x.TipoTelefone == TelefoneEnum.Residencia).First();
            ViewBag.Celular = afiliado.Telefones.Where(x => x.TipoTelefone == TelefoneEnum.Celular01).First();

            ViewBag.Lista = new List<Afiliado>() { afiliado };

            return afiliado;
        }

        private void GeraViewBagDetalhe()
        {
            ViewBag.Pagamento = PagamentoServico.ConsultarPorCategoria(OpcaoPagamentoEnum.PAGTO.ToString());
            ViewBag.Contribuicao = PagamentoServico.ConsultarPorCategoria(OpcaoPagamentoEnum.CONTRIBuICAO.ToString());
            ViewBag.GrauParentesco = DependenteServico.ListaGrausParentesco();
        }

        private void GeraViewBag()
        {
            var afiliado = ConsultarPorCPF();
            ViewBag.RootView = Validacao.AnalisaLink(@Request.RawUrl.ToString());
            ViewBag.LinkSubmitAfilia = Validacao.AnalisaLink(@Request.RawUrl.ToString() + "/ValidarFormJSON");
            ViewBag.GrauParentesco = DependenteServico.DictionaryGrausParentesco();
            ViewBag.ListaDependentes = DependenteServico.ListaDependentes(afiliado.ID);
        }

        private void CombosForm()
        {

            try
            {
                ViewBag.GrauParentesco = DependenteServico.ListaGrauParentescoDisponivel(ConsultaIdAfiliado());
            }
            catch (Exception ex)
            {
                ViewBag.MensagemRetorno = ex.Message;
            }

        }

        private int ConsultaIdAfiliado()
        {
            var idAfiliado = 0;

            if (TempData["idAfiliado"] != null)
            {
                if (!Int32.TryParse(TempData["idAfiliado"].ToString(), out idAfiliado))
                    ViewBag.MensagemRetorno = MensagemUtil.ErroIDForm();
                TempData["idAfiliado"] = idAfiliado; // renovando sessao                
            }
            else
            {
                ViewBag.MensagemRetorno = MensagemUtil.ErroGeneralizado();
            }

            return idAfiliado;

        }
        
        public string ValidarForm(FormCollection Collection)
        {
            var result = "";

            //Convertendo informaçoes dos campos em uma lista
            var lista = validacaoViewServico.GeraListaCampos(Collection);

            //validar campos opcionais
            result = Validacao.FormAfiliacaoValidarPreenchimento(lista);

            //validação específica cpf, cpn, pis, etc.
            result = Validacao.ValidarCodigos(lista);

            int idAfiliado = ConsultaIdAfiliado();
            if (idAfiliado == 0) result += MensagemUtil.ErroGeneralizado(); ;

            result = DependenteServico.ValidarCadastroDependente(idAfiliado);

            ViewBag.MensagemRetorno = result;

            if (result.Trim() == "")
            {
                InserirDados(lista, idAfiliado); //gravando informaçoes
            }

            return result;

        }

        public ActionResult Delete(int id)
        {
            var result = DependenteServico.Remove(id);

            int idAfiliado = ConsultaIdAfiliado();
            if (idAfiliado == 0)
                return View("Afiliacao");

            GeraViewBag();
            CombosForm();

            return RedirectToAction("Dependente");
        }

        private void InserirDados(Dictionary<string, string> lista, int idAfiliado)
        {

            try
            {
                validacaoViewServico.InsereDependente(lista, idAfiliado);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
