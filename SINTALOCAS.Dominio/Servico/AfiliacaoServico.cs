using System;
using SINTALOCAS.DAL.DB;
using SINTALOCAS.Modelo;
using SINTALOCAS.DAL;

namespace SINTALOCAS.Dominio.Servico
{
    public class AfiliacaoServico
    {
        private AfiliacaoDAL _afiliacaoDAL = new AfiliacaoDAL();

        public int Insere(Afiliado afiliado){

            int result = 0;

            _afiliacaoDAL.InserirAfiliado(afiliado);

            return result;
        }
    }
}
