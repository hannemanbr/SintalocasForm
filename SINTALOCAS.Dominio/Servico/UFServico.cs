using System;
using System.Linq;
using System.Collections.Generic;
using SINTALOCAS.DAL.DB;
using SINTALOCAS.Modelo;

namespace SINTALOCAS.Dominio.Servico
{
    public class UFServico
    {
        private UFDAL _ufData = new UFDAL();

        public List<UnidadeFederativa> Consultar()
        {
            return _ufData.Consultar().OrderBy(x=>x.UF).ToList();
        }

        public List<UnidadeFederativa> UF_DDDs()
        {
            return _ufData.ListarDDDs();
        }

        public List<string> DDDs()
        {
            try
            {
                var listaResult = new List<string>();
                var listaUFs = _ufData.ListarDDDs();

                foreach (var item in listaUFs.OrderBy(x=>x.DDD))
                {
                    listaResult.Add(item.DDD.ToString());
                }

                return listaResult;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
