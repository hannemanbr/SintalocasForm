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
    public class CentralAtendimentoController : Controller
    {
        [HttpPost]
        // GET: CentralAtendimento
        public ActionResult Index(FormCollection collection)
        {
            Login(collection);

            ConsultarPorCPF();
            GeraViewBagDetalhe();

            return View();
        }

        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("../Home/");
        }

        private bool Login(FormCollection collection)
        {
            var result = false;
            //Convertendo informaçoes dos campos em uma lista
            var lista = validacaoViewServico.GeraListaCampos(collection);

            if (lista.Count > 0)
            {
                var cpf = lista["CPF"];
                var email = lista["EMAIL"];
                var dtnascTx = lista["DTNASC"];
                var dtnasc = DataUtil.ConverterString(dtnascTx);

                var usuario = AfiliacaoServico.GetByCpfEmailDataNascimento(cpf, email, dtnasc);

                if (usuario.ID <= 0)
                {
                    ViewBag.MensagemRetorno = "E-mail, CPF ou Data de Nascimento inválida";
                    result = false;
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(usuario.ID.ToString(), false);
                    TempData["LogAtivo"] = usuario;
                    result = true;
                }
            }
            else
            {
                ViewBag.MensagemRetorno = "Usuário inválido";
            }
            

            return result;
        }

        private void ConsultarPorCPF()
        {
            var id = Convert.ToInt32(Server.HtmlEncode(User.Identity.Name));

            var listaAfiliado = AfiliacaoServico.Listar(id.ToString());
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
