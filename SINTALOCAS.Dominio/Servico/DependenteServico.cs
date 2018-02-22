using System;
using System.Collections.Generic;
using System.Linq;
using SINTALOCAS.DAL.DB;
using SINTALOCAS.Modelo;

namespace SINTALOCAS.Dominio.Servico
{
    public class DependenteServico
    {
        private AfiliacaoDAL _afiliacaoDAL = new AfiliacaoDAL();

        public Dictionary<int, string> DictionaryGrausParentesco()
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

        public List<string> ListaGrausParentesco()
        {
            try
            {
                var listaResult = new List<string>();
                var listaGrauParentesco = _afiliacaoDAL.ListaGrauPArenesco();

                foreach (var item in listaGrauParentesco.OrderBy(x => x.Descricao))
                {
                    listaResult.Add(item.Descricao);
                }

                return listaResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int Insere(Dependentes dependentes)
        {

            int result = 0;

            _afiliacaoDAL.InserirDependente(dependentes);

            return result;
        }
    }
}
