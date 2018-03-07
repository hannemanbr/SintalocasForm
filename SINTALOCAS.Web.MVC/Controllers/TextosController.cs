using SINTALOCAS.Dominio.Servico;
using SINTALOCAS.Dominio.Util;
using SINTALOCAS.Modelo;
using SINTALOCAS.Web.MVC.Servico;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SINTALOCAS.Web.MVC.Controllers
{
    [Authorize]
    public class TextosController : Controller
    {
        // GET: Textos
        public ActionResult Index()
        {
            GeraViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection Collection)
        {
            var retorno = false;
            var mensagem = ValidarForm(Collection);

            if (mensagem.Trim() == "") retorno = true;

            return View();
        }

        public string ValidarForm(FormCollection Collection)
        {
            var result = "";

            //Convertendo informaçoes dos campos em uma lista
            var lista = validacaoViewServico.GeraListaCampos(Collection);

            //validação específica cpf, cpn, pis, etc.
            result = Validacao.ValidarCodigos(lista);

            ViewBag.MsgRetorno = result;

            if (result.Trim() == "")
            {
                AtualizarTextos(lista); //gravando informaçoes
            }

            return result;
        }

        private int AtualizarTextos(Dictionary<string, string> lista)
        {

            int result = 0;

            try
            {
                var texto = lista["CONCORDAR"];
                var id = Convert.ToInt32( lista["CONCORDARID"]);
                var listObj = new List<MensagemSistema>();

                listObj.Add(new MensagemSistema
                {
                    Alias = "CONCORDO",
                    Categoria = "WEB",
                    Texto = texto,
                    ID = id,
                    Titulo = "Termos e Condições"
                });

                result = TextosServico.Atualizar(listObj);

                if (result > 0) ViewBag.MsgRetorno = MensagemUtil.OperacaoRealizada();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            GeraViewBag();
            return result;
        }

        private void GeraViewBag()
        {
            var obj = TextosServico.TextoDeAcordo();
            ViewBag.ID = obj.ID;
            ViewBag.TxConcordar = obj.Texto;
            ViewBag.LinkSubmitAfilia = Validacao.AnalisaLink(@Request.RawUrl.ToString() + "/Textos");
        }
    }
}
