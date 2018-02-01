using System;
using System.Collections.Generic;
using SINTALOCAS.DAL.DATA;
using SINTALOCAS.Modelo;

namespace SINTALOCAS.Dominio.Servico
{
    public class UFServico
    {
        private UFDAL _ufData = new UFDAL();

        public List<UnidadeFederativa> Consultar()
        {
            return _ufData.Consultar();
        }
    }
}
