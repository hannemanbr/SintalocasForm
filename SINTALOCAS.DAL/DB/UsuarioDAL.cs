﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SINTALOCAS.DAL.Context;
using SINTALOCAS.Modelo;

namespace SINTALOCAS.DAL.DB
{    
    public static class UsuarioDAL
    {
        private static ContextoMySqlDB _contexto = new ContextoMySqlDB();

        public static List<Usuario> Consultar(string email  = "", string senha = "")
        {
            try
            {
                var lista = new List<Usuario>();
                var query = "SELECT ID, Nome, Email, Perfil"
                    + " FROM Admin_Login"
                    + " WHERE D_E_L_E_T_ = 0";

                if (email.Trim() != "") query += " AND Email = '" + email + "'";
                if (senha.Trim() != "") query += " AND Senha = '" + senha + "'";

                var dataTable = _contexto.Consultar(query);

                foreach (DataRow linha in dataTable.Rows)
                {
                    var obj = new Usuario
                    {
                        ID = Convert.ToInt32(linha["ID"]),
                        Nome = linha["Nome"].ToString(),
                        Email = linha["Email"].ToString(),
                        Perfil= Convert.ToInt32(linha["Perfil"])
                    };

                    lista.Add(obj);
                }

                dataTable.Dispose();

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int Insere(Usuario usuario)
        {
            try
            {
                var lista = new List<Usuario>();
                var query = "INSERT INTO Admin_Login " +
                    "(" +
                    " Nome, Email, Senha, Perfil" +
                    ") VALUES (" +
                    "'" + usuario.Nome + "'," +
                    "'" + usuario.Email + "'," +
                    "'" + usuario.Senha + "'," +
                    "'1'" +
                    ")";

                return _contexto.Transacao(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int Update(Usuario usuario)
        {
            try
            {
                var lista = new List<Usuario>();
                var query = "UPDATE Admin_Login" +
                    " SET" +
                    " NOME = '" + usuario.Nome + "'," +
                    " EMAIL = '" + usuario.Email + "'," +                    
                    " SENHA = '" + usuario.Senha + "'" +
                    " WHERE ID=" + usuario.ID + "";

                return _contexto.Transacao(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
