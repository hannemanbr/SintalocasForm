﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Web.Mvc;
using SINTALOCAS.Dominio.Util;
using SINTALOCAS.Web.MVC.Servico;
using SINTALOCAS.Dominio.Servico;
using SINTALOCAS.Web.MVC.Models;
using SINTALOCAS.Modelo;

namespace SINTALOCAS.Web.MVC.Controllers
{
    public class AfiliacaoController : Controller
    {
        private UFServico _ufServ = new UFServico();
        
        public ActionResult Index()
        {
            ViewBag.RootView = Validacao.AnalisaLink(@Request.RawUrl.ToString());

            CombosForm();
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            TempData["AfiliadoID"] = id;

            if (id != null)
            {
                var idAfiliado = Convert.ToInt32((int)id);
                GeraViewBag(idAfiliado);
            }

            return View();
        }
                
        private void GeraViewBag(int idAfiliado)
        {
            ViewBag.RootView = Validacao.AnalisaLink(@Request.RawUrl.ToString());
            ViewBag.LinkSubmitAfilia = Validacao.AnalisaLink(@Request.RawUrl.ToString() + "/ValidarFormJSON");
            ViewBag.Afiliado = AfiliacaoServico.GetByID(idAfiliado);
        }

        public bool ValidarForm(FormCollection Collection, bool editar)
        {
            var result = "";

            //Convertendo informaçoes dos campos em uma lista
            var lista = validacaoViewServico.GeraListaCampos(Collection);

            //validar campos opcionais
            result = Validacao.FormAfiliacaoValidarPreenchimento(lista);

            //validação específica cpf, cpn, pis, etc.
            result = Validacao.ValidarCodigos(lista);

            //result = "";
            
            if (result.Trim() == "")
            {
                TempData["idAfiliadoForm"] = AtualizarDados(lista, editar); //gravando informaçoes
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPost]
        public JsonResult ValidarFormEditaJSON(FormCollection Collection)
        {
            var retorno = ValidarForm(Collection, true);
            var mensagem = "";

            if (!retorno) mensagem = MensagemUtil.ErroCamposNaoPreenchidos();

            return Json(new { success = retorno, msg = mensagem }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ValidarFormJSON(FormCollection Collection)
        {
            var retorno = ValidarForm(Collection, false);
            var mensagem = "";

            if (!retorno) mensagem = MensagemUtil.ErroCamposNaoPreenchidos();
            
            return Json(new { success = retorno, msg = mensagem }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public string ValidarCPF(string Cpf)
        {
            var result = "";

            if (!ValidaCodigosUtil.ValidaCpf(Cpf))
            {
                result = MensagemUtil.ErroCPFInvalido();
            }
            else
            {
                //VALIDAR SE CPF EXISTE NA BASE DE DADOS
                result = AfiliacaoServico.AfiliadoExistente(Cpf);
            }

            return result;
        }

        [HttpGet]
        public string ValidarEMAIL(string emailtx)
        {
            var result = "";

            if (!ValidaCodigosUtil.ValidarEmail(emailtx)) result = MensagemUtil.ErroEMAILInvalido();

            return result;
        }

        [HttpGet]
        public string ValidarRG(string Rg)
        {
            var result = "";

            if (!ValidaCodigosUtil.ValidaRG(Rg)) result = MensagemUtil.ErroRGInvalido();

            return result;
        }

        [HttpGet]
        public string ValidarPIS(string Pis)
        {
            var result = "";

            if (!ValidaCodigosUtil.ValidaPis(Pis)) result = MensagemUtil.ErroPIsInvalido();

            return result;
        }

        [HttpGet]
        public string ValidarCNPJ(string Cnpj)
        {
            var result = "";

            if (!ValidaCodigosUtil.ValidaCnpj(Cnpj)) result = MensagemUtil.ErroCNPJInvalido();

            return result;
        }

        [HttpGet]
        public string ValidarCEP(string Cep)
        {    
            var result = new Endereco();

            if (!ValidaCodigosUtil.ValidaCep(Cep) || Cep.Trim().Length < 8)
            {
                ViewBag.MensagemCEPRetorno = MensagemUtil.ErroCEPInvalido();
                result = null; // MensagemUtil.ErroCEPInvalido();
            }
            else
            {
                //CONSULTAR ENDEREÇO COM CEP INFORMADO
                var endereco = EnderecoUtil.ConsultarEndereco(Cep);

                if (endereco.ID == -1) ViewBag.MensagemCEPRetorno = MensagemUtil.ErroCEPInvalido();

                result = endereco;
            }

            string json = JsonConvert.SerializeObject(result, Formatting.Indented);
            return json;

        }

        [HttpGet]
        public string ValidarUF(string Uf)
        {
            var result = "";
            var listaUFs = _ufServ.Consultar().Select(x => x.UF).ToList();

            if (!listaUFs.Contains(Uf.ToUpper())) result = MensagemUtil.ErroUFInvalido();

            return result;
        }

        [HttpGet]
        public string ValidarDtNasc(string dtnasc)
        {
            var result = "";

            if (!ValidaCodigosUtil.ValidaDtNasc(dtnasc)) result = MensagemUtil.ErroDTNASCInvalido();

            return result;
        }

        private void CombosForm()
        {

            try
            {
                ViewBag.UFLista = _ufServ.Consultar();
                ViewBag.DDDLista = _ufServ.DDDs();
                ViewBag.Afiliado = new AfiliacaoModelView();
            }
            catch (Exception ex)
            {
                ViewBag.MensagemRetorno = ex.Message;
            }

        }

        private int AtualizarDados(Dictionary<string, string> lista, bool editar) {

            int result = 0;

            try
            {
                result = validacaoViewServico.Atualizar(lista, editar);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

    }
}