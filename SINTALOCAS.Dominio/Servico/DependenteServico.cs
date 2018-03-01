using System;
using System.Collections.Generic;
using System.Linq;
using SINTALOCAS.DAL.DB;
using SINTALOCAS.Dominio.Util;
using SINTALOCAS.Modelo;

namespace SINTALOCAS.Dominio.Servico
{
    public  static class DependenteServico
    {
        private static AfiliacaoDAL _afiliacaoDAL = new AfiliacaoDAL();

        public static Dictionary<int, string> DictionaryGrausParentesco()
        {
            try
            {
                var listaResult = new Dictionary<int, string>();
                var listaGrauParentesco = _afiliacaoDAL.ListaGrauPArenesco();

                foreach (var item in listaGrauParentesco.OrderBy(x => x.Descricao))
                {
                    listaResult.Add(item.ID, item.Descricao);
                }

                return listaResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static List<GrauParentesco> ListaGrausParentesco()
        {
            try
            {
                return _afiliacaoDAL.ListaGrauPArenesco();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static int Insere(Dependentes dependentes, int acrescimoMensal, int idAfiliado)
        {
            int result = 0;            
            result = _afiliacaoDAL.InserirDependente(dependentes);
            return result;
        }

        public static int Remove(int idDependente)
        {
            int result = 0;
            result = _afiliacaoDAL.RemoveDependente(idDependente);
            return result;
        }

        public static List<Dependentes> ListaDependentes(int idAfiliado)
        {
            return _afiliacaoDAL.ListaDependentes(idAfiliado);
        }

        public static string NomeGrauParentesco(int idGrau)
        { 
            var result = "";
            var lista = DictionaryGrausParentesco();

            if (lista.ContainsKey(idGrau))
            {
                result = lista[idGrau];
            }

            return result;
        }

        public static string ValidarCadastroDependente(int idAfiliado)
        {
            var result = "";
            var contParentesco = 0;            
            var listaGrauParentesco = ListaGrausParentesco();
            var lista = ListaDependentes(idAfiliado);

            foreach(var item in listaGrauParentesco)
            {
                contParentesco = lista.Where(x => x.GrauParentescoID == item.ID).Count();
                if (contParentesco > item.LimiteQuantidade) result += MensagemUtil.GrauParentescoAcimaPermitido(item.Descricao.Trim());
            }           

            return result;
        }

        public static Dictionary<int, string> ListaGrauParentescoDisponivel(int idAfiliado)
        {
            try
            {
                int cont = 0;
                var listaGrauParentesco = ListaGrausParentesco().OrderBy(x => x.Descricao);
                var listaDependentesCadastrado = ListaDependentes(idAfiliado);

                var listaResult = new Dictionary<int, string>();

                foreach (var item in listaGrauParentesco)
                {
                    cont = listaDependentesCadastrado.Where(x => x.GrauParentescoID == item.ID).Count();

                    if (cont <= item.LimiteQuantidade - 1) listaResult.Add(item.ID, item.Descricao);
                }

                return listaResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
