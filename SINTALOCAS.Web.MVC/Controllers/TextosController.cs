using SINTALOCAS.Dominio.Servico;
using SINTALOCAS.Dominio.Util;
using SINTALOCAS.Web.MVC.Servico;
using System.Web.Mvc;

namespace SINTALOCAS.Web.MVC.Controllers
{
    public class TextosController : Controller
    {
        // GET: Textos
        public ActionResult Index()
        {
            ViewBag.TxConcordar = TextosServico.TextoDeAcordo().Texto;
            ViewBag.Textos = TextosServico.ListarTextosSite();
            return View();
        }
        
        [HttpPost]
        public JsonResult ValidarFormJSON(FormCollection Collection)
        {
            var retorno = false;
            var mensagem = ValidarForm(Collection);

            if (mensagem.Trim() == "") retorno = true;

            return Json(new { success = retorno, msg = mensagem }, JsonRequestBehavior.AllowGet); ;

        }

        public string ValidarForm(FormCollection Collection)
        {
            var result = "";

            //Convertendo informaçoes dos campos em uma lista
            var lista = validacaoViewServico.GeraListaCampos(Collection);
            
            //validação específica cpf, cpn, pis, etc.
            result = Validacao.ValidarCodigos(lista);
            
            ViewBag.MensagemRetorno = result;

            //if (result.Trim() == "")
            //{
            //    InserirDados(lista); //gravando informaçoes
            //    GeraViewBag(idAfiliado);
            //    //CombosForm();
            //}

            return result;

        }

        private void GeraViewBag(int idAfiliado)
        {
            ViewBag.LinkSubmitAfilia = Validacao.AnalisaLink(@Request.RawUrl.ToString() + "/Textos");
        }
    }
}
