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
        //ContextoPGDB _contexto = new ContextoPGDB();

        public void Inserir(Afiliado afiliado)
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
    }
}
