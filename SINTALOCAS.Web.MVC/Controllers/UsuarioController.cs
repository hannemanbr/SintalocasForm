using SINTALOCAS.Dominio.Servico;
using SINTALOCAS.Dominio.Util;
using SINTALOCAS.Modelo;
using SINTALOCAS.Web.MVC.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SINTALOCAS.Web.MVC.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            GeraView();            
            return View();
        }

        // GET: Usuario/Details/5
        public ActionResult Cadastro()
        {
            GeraView();            
            return View();
        }

        public ActionResult Senha(int id)
        {
            GeraView();
            var lista = UsuarioServico.Consultar("", id).OrderBy(x => x.Nome).ToList();

            if (lista.Count > 0)
                ViewBag.Usuario = lista[0];

            return View();
        }

        private void GeraView()
        {
            var linkRoot = Validacao.AnalisaLink(@Request.RawUrl.ToString(), true);
            ViewBag.Usuarios = UsuarioServico.Consultar("", 0).OrderBy(x => x.Nome).ToList();
            ViewBag.RootView = Validacao.AnalisaLink(@Request.RawUrl.ToString());

            ViewBag.LinkRelatorio01 = linkRoot + "Relatorio/PDFPadrao/4"; 
            ViewBag.LinkCadastro = linkRoot + "Usuario/Cadastro";
        }

        [HttpPost]
        public ActionResult Senha(FormCollection collection)
        {
            try
            {
                var valida = validacaoViewServico.ValidaCampoObrigatorio(collection);
                var listaCampos = validacaoViewServico.GeraListaCampos(collection);

                //validar campos
                var result = Validacao.FormUsuarioValidarPreenchimento(listaCampos, true);
                ViewBag.MensagemRetorno = result;

                if (result.Trim() == "")
                {
                    Usuario usuario = UsuarioServico.ConverteInfoFormEmObj(listaCampos, true);
                    var registro = UsuarioServico.AlteraSenha(usuario); //gravando informaçoes
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Senha");

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        // GET: Usuario/Create
        public ActionResult Cadastro(FormCollection collection)
        {
            var valida = validacaoViewServico.ValidaCampoObrigatorio(collection);
            var listaCampos = validacaoViewServico.GeraListaCampos(collection);

            //validar campos
            var result = Validacao.FormUsuarioValidarPreenchimento(listaCampos);

            if (result.Trim() == "")
            {
                Usuario usuario = UsuarioServico.ConverteInfoFormEmObj(listaCampos, false);
                var registro = UsuarioServico.Insere(usuario); //gravando informaçoes
            }

            ViewBag.MensagemRetorno = result;
            return View("Index");
        }

        // GET: Usuario/Edit/5
        public ActionResult Editar(int id)
        {
            return View();
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Editar(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            //nao permitir que o proprio usuario realize a sua exclusão
            var usuario = UsuarioServico.Consultar("", id)[0];
            var nomeLogin = Server.HtmlEncode(User.Identity.Name);

            if (usuario!=null)
            {
                if (usuario.Nome.ToUpper().Trim() == nomeLogin)
                {
                    ViewBag.MensagemRetorno =  MensagemUtil.ErroExcluirUsuarioLogado();
                    return View();
                } 
            }


            var reg = UsuarioServico.Delete(id);

            return RedirectToAction("Index");
        }
                
        //[HttpPost]
        //public JsonResult ValidarFormJSON(FormCollection collection)
        //{
        //    var retorno = false;
        //    var listaCampos = validacaoViewServico.GeraListaCampos(collection);

        //    //validar campos
        //    var result = Validacao.FormUsuarioValidarPreenchimento(listaCampos);
                        
        //    try
        //    {
        //        if (result.Trim() == "")
        //        {
        //            Usuario usuario = UsuarioServico.ConverteInfoFormEmObj(listaCampos, false);
        //            var registro = UsuarioServico.Insere(usuario); //gravando informaçoes
        //            retorno = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result = ex.Message;
        //        //throw;
        //    }

        //    return Json(new { success = retorno, msg = result }, JsonRequestBehavior.AllowGet); ;

        //}

    }
}
