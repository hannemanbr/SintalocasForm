using System;
using System.Collections.Generic;
using SINTALOCAS.DAL.DB;
using SINTALOCAS.Dominio.Util;
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
            catch (Exception)
            {
                throw;
            }
        }

        public static int Delete(int id)
        {
            try
            {
                int result = _afiliacaoDAL.RemoveAfiliado(id);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool Concordar(int id)
        {
            var result = false;

            try
            {
                var registros = _afiliacaoDAL.Concordar(id);
                var dependente = new List<Dependentes>();              
                var afiliado = GetByID(id);

                if (afiliado != null)
                {
                    dependente = DependenteServico.ListaDependentes(id);
                    EmailServico.NotificarCadastro(afiliado, dependente);
                }

                if (registros > 0) result = true;
            }
            catch (Exception)
            {
                throw;
            }

            return result; // qualquer excessao retorna false
        }

        public static string AfiliadoExistente(string cpf)
        {
            var result = "";

            try
            {
                var lista = _afiliacaoDAL.ListaAfiliado(cpf);
                if (lista.Count > 0) result = MensagemUtil.ErroCPFExistente();
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public static List<Afiliado> Listar(string cpf = "")
        {
            List<Afiliado> result = new List<Afiliado>();

            try
            {
                result = _afiliacaoDAL.ListaAfiliado(cpf);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public static Afiliado GetByID(int id)
        {
            var result = new Afiliado();

            try
            {
                if(_afiliacaoDAL.ListaAfiliado("", id).Count>0)
                    result = _afiliacaoDAL.ListaAfiliado("", id)[0];
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
    }
}
