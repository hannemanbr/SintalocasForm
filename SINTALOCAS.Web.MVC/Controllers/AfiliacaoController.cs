using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SINTALOCAS.Dominio.Util;
using SINTALOCAS.Web.MVC.Servico;

namespace SINTALOCAS.Web.MVC.Controllers
{
    public class AfiliacaoController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Create");
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            // VALIDANDO PREENCHIMENTO
            Dictionary<string, bool> listaValidacao = ValidarForm(collection);

            if (listaValidacao.Where(x => x.Value == false).Any())
            {
                ViewBag.MensagemRetorno = ValidacaoForm.GeraMensagemErroRetorno(listaValidacao);
                return View();
            }

            //GERANDO INFORMAÇAO PARA ENVIAR PARA o BD
            var lista = new Dictionary<string, string>();

            foreach (string formDados in collection)
            {
                lista.Add(formDados.ToUpper(), collection[formDados]);
            }

            AfiliacaoViewsServico.Insere(lista);

            return View();

        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public JsonResult BuscaEndereco(string cep)
        {

            var endereco = EnderecoUtil.ConsultarEndereco(cep);
            return Json(endereco, JsonRequestBehavior.AllowGet);

        }

        private Dictionary<string, bool> ValidarForm(FormCollection collection)
        {

            var listaCampoOpcional = ValidacaoForm.FormAfiliacaoCampoOpcional();
            var result = new Dictionary<string, bool>();

            foreach (string formDados in collection)
            {
                if (!listaCampoOpcional.Contains(formDados.ToUpper()))
                {
                    var valorCampo = collection[formDados].Trim();
                    if (valorCampo == "") result.Add(formDados.ToUpper(), false);
                }

            }

            return result;
        }
    }
}