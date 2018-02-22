using System;
using System.Collections.Generic;
using System.Data;
using SINTALOCAS.DAL.Context;
using SINTALOCAS.Modelo;

namespace SINTALOCAS.DAL.DB
{
    public class TextosDAL
    {
        ContextoMySqlDB _contexto = new ContextoMySqlDB();

        public List<MensagemSistema> Consultar()
        {
            var lista = new List<MensagemSistema>();
            var query = "Select * From Cfg_Textos WHERE D_E_L_E_T_ = 0 ";
            var dataTable = _contexto.Consultar(query);

            try
            {

                foreach (DataRow linha in dataTable.Rows)
                {
                    var obj = new MensagemSistema
                    {
                        Texto = linha["Texto"].ToString(),
                        Categoria = linha["Categoria"].ToString(),
                        Alias = linha["Alias"].ToString(),
                        ID = Convert.ToInt32(linha["ID"])
                    };

                    lista.Add(obj);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lista;
        }

    }
}
