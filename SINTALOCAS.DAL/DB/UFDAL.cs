using System;
using System.Collections.Generic;
using System.Data;
using SINTALOCAS.DAL.Context;
using SINTALOCAS.Modelo;

namespace SINTALOCAS.DAL.DB
{
    public class UFDAL
    {
        ContextoMySqlDB _contexto = new ContextoMySqlDB();

        public List<UnidadeFederativa> Consultar()
        {
            var lista = new List<UnidadeFederativa>();
            var query = "Select * From Cfg_UF WHERE D_E_L_E_T_ = 0";
            var dataTable = _contexto.Consultar(query);

            try
            {

                foreach(DataRow linha in dataTable.Rows)
                {
                    var obj = new UnidadeFederativa
                    {
                        Descricao = linha["Descricao"].ToString(),
                        UF = linha["UF"].ToString(),
                        ID = Convert.ToInt32(linha["ID"])
                    };

                    lista.Add(obj);
                }

                dataTable.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lista;
        }

        public List<UnidadeFederativa> ListarDDDs()
        {
            var lista = new List<UnidadeFederativa>();
            var query = "Select * From Cfg_DDD WHERE D_E_L_E_T_ = 0";
            var dataTable = _contexto.Consultar(query);

            try
            {

                foreach (DataRow linha in dataTable.Rows)
                {
                    var obj = new UnidadeFederativa
                    {
                        DDD = Convert.ToInt32(linha["DDD"]),
                        UF = linha["UF"].ToString(),
                        Descricao = linha["Estado"].ToString(),
                        Regiao = linha["Regiao"].ToString(),
                    };

                    lista.Add(obj);
                }

                dataTable.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lista;

        }

    }
}
