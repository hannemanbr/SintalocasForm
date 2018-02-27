using System;
using SINTALOCAS.DAL.DB;
using SINTALOCAS.Modelo;

namespace SINTALOCAS.Dominio.Servico
{
    public static class AfiliacaoServico
    {
        private static AfiliacaoDAL _afiliacaoDAL = new AfiliacaoDAL();
        //private static AfiliacaoEFDAL _afiliacaoEFDAL = new AfiliacaoEFDAL();

        public static int Insere(Afiliado afiliado){

            try
            {
                int result = _afiliacaoDAL.InserirAfiliado(afiliado);
                //_afiliacaoEFDAL.Inserir(afiliado);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool Concordar(int id)
        {
            var result = false;

            try
            {
                var registros = _afiliacaoDAL.Concordar(id);
                if (registros > 0) result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result; // qualquer excessao retorna false
        }
    }
}
