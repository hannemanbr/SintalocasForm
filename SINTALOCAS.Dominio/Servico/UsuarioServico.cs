﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SINTALOCAS.DAL.DB;
using SINTALOCAS.Dominio.Util;
using SINTALOCAS.Modelo;

namespace SINTALOCAS.Dominio.Servico
{
    public static class UsuarioServico
    {
        public static List<Usuario> ConsultarLogin(string email, string senha)
        {
            try
            {
                //senha = GerarSenhaSHA1(senha);
                return UsuarioDAL.Consultar(email, senha);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<Usuario> Consultar(string email, int id)
        {
            try
            {
                //senha = GerarSenhaSHA1(senha);
                return UsuarioDAL.Consultar(email, "", id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static Usuario ConsultarPorEmail(string email)
        {
            try
            {
                var usuario = new Usuario() { ID = 0, Email = "", Nome = "" };
                //senha = GerarSenhaSHA1(senha);
                var lista = UsuarioDAL.Consultar(email, "", 0);

                if (lista.Count > 0) usuario = lista[0];

                return usuario;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static Usuario ConsultarPorID(int id)
        {
            try
            {
                var usuario = new Usuario() { ID = 0, Email = "", Nome = "" };
                //senha = GerarSenhaSHA1(senha);
                var lista = UsuarioDAL.Consultar("", "", id);

                if (lista.Count > 0) usuario = lista[0];

                return usuario;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static  bool ValidarLogin(string email, string senha)
        {
            try
            {
                if (ConsultarLogin(email, senha).Count > 0) return true;

                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string ValidarSenha(string senha)
        {
            var result = "";

            if (senha.Trim().Length < 8) return MensagemUtil.ErroTamanhoSenha();

            return result;
        }

        public static int Insere(Usuario usuario)
        {
            try
            {
                // VERIFICAR SE E-MAIL JA É CADASTRADO
                if (Consultar(usuario.Email, 0).Count == 0)
                {
                    //criptografar senha
                    usuario.Senha = GerarSenhaSHA1(usuario.Senha);
                    return UsuarioDAL.Insere(usuario);
                }

                return 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static Usuario ConverteInfoFormEmObj(Dictionary<string, string> lista, bool alteracaoSenha)
        {
            try
            {
                var senha = "";
                var nome = "";
                var email = "";
                var idUsuario = 0;

                if (lista.ContainsKey("EMAIL")) email = lista["EMAIL"];
                
                // VERIFICAR SE E-MAIL JA É CADASTRADO
                if (alteracaoSenha || Consultar(email, 0).Count == 0)
                {
                    if (lista.ContainsKey("SENHA")) senha = lista["SENHA"]; //GerarSenhaSHA1(lista["SENHA"]);
                    if (lista.ContainsKey("NOME")) nome = lista["NOME"];
                    if (lista.ContainsKey("ID")) idUsuario = Convert.ToInt32(lista["ID"]);
                }

                return new Usuario
                {
                    ID = idUsuario,
                    Senha = senha,
                    Email = email,
                    Nome = nome                    
                };

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Update(Usuario usuario)
        {
            try
            {
                return UsuarioDAL.Atualizar(usuario);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int AlteraSenha(Usuario usuario)
        {
            try
            {
                usuario.Senha = GerarSenhaSHA1(usuario.Senha);
                return UsuarioDAL.AlteraSenha(usuario);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Delete(int id)
        {
            try
            {
                return UsuarioDAL.Remove(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // ou se você não quiser usar a encoding como parâmetro.
        public static string GerarSenhaSHA1(string texto)
        {
            try
            {
                string hash = "";

                if (texto.Trim() != "")
                {
                    byte[] buffer = Encoding.Default.GetBytes(texto);
                    System.Security.Cryptography.SHA1CryptoServiceProvider cryptoTransformSHA1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
                    hash = BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
                    
                }

                return hash;

            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
        }
    }
}
