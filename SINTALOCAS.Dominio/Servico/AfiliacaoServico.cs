using System;
using System.Collections.Generic;
using System.Linq;
using SINTALOCAS.DAL.DB;
using SINTALOCAS.Dominio.Util;
using SINTALOCAS.Modelo;

namespace SINTALOCAS.Dominio.Servico
{
    public static class AfiliacaoServico
    {
        private static AfiliacaoDAL _afiliacaoDAL = new AfiliacaoDAL();
        //private static AfiliacaoEFDAL _afiliacaoEFDAL = new AfiliacaoEFDAL();

        public static int Insere(Afiliado afiliado)
        {

            try
            {
                int result = _afiliacaoDAL.InserirAfiliado(afiliado);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Editar(Afiliado afiliado)
        {

            try
            {
                int result = _afiliacaoDAL.EditarAfiliado(afiliado);

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

        public static bool Concordar(int id, int opcaoPagamento, int opcaoContribuicao)
        {
            var result = false;

            try
            {
                var registros = _afiliacaoDAL.Concordar(id, opcaoPagamento, opcaoContribuicao);
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
            var result = new List<Afiliado>();

            try
            {
                var pagamento = PagamentoServico.ConsultarTodos();
                var pagamentoTx = "";
                var contribuicaoTx = "";
                var listaDependente = new List<Dependentes>();
                var listaAfiliado = _afiliacaoDAL.ListaAfiliado(cpf);

                foreach (var item in listaAfiliado)
                {
                    if (item.PagamentoID > 0)
                        pagamentoTx = pagamento.Where(p => p.ID == item.PagamentoID).First().Texto;
                    if (item.ContribuicaoID > 0)
                        contribuicaoTx = pagamento.Where(p => p.ID == item.ContribuicaoID).First().Texto;

                    item.PagamentoTx = pagamentoTx;
                    item.ContribuicaoTx = contribuicaoTx;

                    // CONSULTAR DEPENDENTE
                    item.dependentes = DependenteServico.ListaDependentes(item.ID);

                    result.Add(item);
                }
                
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
                var lista = _afiliacaoDAL.ListaAfiliado("", id);
                if (lista.Count > 0)
                    result = lista[0];

                var pagamento = PagamentoServico.ConsultarTodos();

                if (result.PagamentoID > 0)
                    result.PagamentoTx = pagamento.Where(p => p.ID == result.PagamentoID).First().Texto;
                if (result.ContribuicaoID > 0)
                    result.ContribuicaoTx = pagamento.Where(p => p.ID == result.ContribuicaoID).First().Texto;

                // CONSULTAR DEPENDENTE
                result.dependentes = DependenteServico.ListaDependentes(result.ID);

            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

    }
}
