using System;
using SINTALOCAS.Modelo;
using SINTALOCAS.DAL.Context;
using System.Data;
using System.Collections.Generic;

namespace SINTALOCAS.DAL.DATA
{
    public class AfiliacaoDAL
    {
        ContextoMySqlDB _contexto = new ContextoMySqlDB();

        public void InserirAfiliado(Afiliado afiliado)
        {

            try
            {

                var dataNascTx = afiliado.DataNascimento.Year + "-" + afiliado.DataNascimento.Month + "-" + afiliado.DataNascimento.Day;

                var query = "" +
                    " INSERT INTO Afiliado(" +                
                    //"ID, " +                
                    "Nome, " +                
                    "Email, " +                
                    "DataNascimento, " +                
                    "CPF, " +                
                    "CTPS_Num, " +                
                    "CTPS_Serie, " +                
                    "CONSIR, " +                
                    "NomePai, " +                
                    "NomeMae " +                
                    ") VALUES (";

                //query += "'1',";
                query += "'" + afiliado.Nome + "'";
                query += ",'" + afiliado.Email + "'";
                query += ",'" + dataNascTx + "'";
                query += ",'" + afiliado.CPF + "'";
                query += ",'" + afiliado.CTPS.Numero + "'";
                query += ",'" + afiliado.CTPS.Serie + "'";
                query += ",'" + afiliado.Consir + "'";
                query += ",'" + afiliado.NomePai + "'";
                query += ",'" + afiliado.NomeMae + "'";
                query += ")";

                _contexto.Transacao(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void InserirDependente(Dependentes dependentes)
        {

            try
            {

                var dataNascTx = dependentes.DataNascimento.Year + "-" + dependentes.DataNascimento.Month + "-" + dependentes.DataNascimento.Day;

                var query = "" +
                    " INSERT INTO Afiliado(" +
                    //"ID, " +                
                    "Descricao" +
                    ") VALUES (";
                query += "'" + dependentes.Nome + "'";
                query += ")";

                _contexto.Transacao(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<GrauParentesco> ListaGrauPArenesco() 
        {
            try
            {
                var lista = new List<GrauParentesco>();
                var query = "SELECT ID, Descricao FROM Cfg_GrauParentesco";
                var dataTable = _contexto.Consultar(query);

                foreach (DataRow linha in dataTable.Rows)
                {
                    var obj = new GrauParentesco
                    {
                        ID = Convert.ToInt32(linha["ID"]),
                        Descricao = linha["Descricao"].ToString(),
                    };

                    lista.Add(obj);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
