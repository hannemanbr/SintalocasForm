using SINTALOCAS.Modelo;
using System;
using System.Linq;

namespace SINTALOCAS.DAL.DB
{
    public class AfiliacaoEFDAL
    {

        public int Inserir(Afiliado afiliado)
        {

            try
            {
                int idResult = 0;
                var dataNascTx = afiliado.DataNascimento.Year + "-" + afiliado.DataNascimento.Month + "-" + afiliado.DataNascimento.Day;

                //INFORMAÇÕES DO AFILIADO
                ContextoEF _contexto = new ContextoEF();

                _contexto.Afiliados.Add(afiliado);
                _contexto.SaveChanges();

                //GRAVANDO ENDEREÇOS
                _contexto.Enderecos.Add(afiliado.Endereco);
                _contexto.SaveChanges();

                //CAPTURANDO ID INSERIDO
                var lista = _contexto.Afiliados.Where(x => x.Email == afiliado.Email && x.CPF == afiliado.CPF).OrderByDescending(x => x.ID).ToList();

                foreach (var afiliadoItem in lista)
                {
                    idResult = afiliadoItem.ID;
                }

                return idResult;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //public void InserirDependente(Dependentes dependentes)
        //{

        //    try
        //    {

        //        var dataNascTx = dependentes.DataNascimento.Year + "-" + dependentes.DataNascimento.Month + "-" + dependentes.DataNascimento.Day;

        //        var query = "" +
        //            " INSERT INTO Afiliado(" +
        //            //"ID, " +                
        //            "Descricao" +
        //            ") VALUES (";
        //        query += "'" + dependentes.Nome + "'";
        //        query += ")";

        //        _contexto.Transacao(query);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        //public List<GrauParentesco> ListaGrauPArenesco()
        //{
        //    try
        //    {
        //        var lista = new List<GrauParentesco>();
        //        var query = "SELECT ID, Descricao FROM Cfg_GrauParentesco WHERE Grau=1 AND D_E_L_E_T_ = 0";
        //        var dataTable = _contexto.Consultar(query);

        //        foreach (DataRow linha in dataTable.Rows)
        //        {
        //            var obj = new GrauParentesco
        //            {
        //                ID = Convert.ToInt32(linha["ID"]),
        //                Descricao = linha["Descricao"].ToString(),
        //            };

        //            lista.Add(obj);
        //        }

        //        return lista;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
