using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using SINTALOCAS.Dominio.Servico;
using SINTALOCAS.Dominio.Util;
using SINTALOCAS.Modelo;
using SINTALOCAS.Web.MVC.Servico;

namespace SINTALOCAS.Web.MVC.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            if (!ValidarForm(collection))
            {
                ViewBag.MensagemRetorno += "<br/>" + MensagemUtil.ErroCamposNaoPreenchidos();
                ViewBag.ColorAlerta = "#FF0000";
            }

            //Convertendo informaçoes dos campos em uma lista
            var Cpf = "";
            var Email = "";
            var DtNasc = DateTime.Now;
            var lista = validacaoViewServico.GeraListaCampos(collection);

            //enviar informações
            if (lista.ContainsKey("EMAIL"))
                Email = lista["EMAIL"];
            if (lista.ContainsKey("CPF"))
                Cpf = lista["CPF"];
            if (lista.ContainsKey("DTNASC"))
            {
                DateTime data;

                if (DateTime.TryParse(lista["DTNASC"], out data))
                {
                    var reg = AfiliacaoServico.GetByCpfEmailDataNascimento(Cpf, Email, data);

                    if (reg.ID > 0)
                    {
                        return RedirectToAction("../CentralAtendimento");
                    }

                }
                else
                {
                    ViewBag.MensagemRetorno += "<br/>" + MensagemUtil.ErroDTNASCInvalido();
                }

            }

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

    }
}
