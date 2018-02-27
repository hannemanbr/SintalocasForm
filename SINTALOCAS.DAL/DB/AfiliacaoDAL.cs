using System;
using SINTALOCAS.Modelo;
using SINTALOCAS.DAL.Context;
using System.Data;
using System.Collections.Generic;

namespace SINTALOCAS.DAL.DB
{
    public class AfiliacaoDAL
    {
        ContextoMySqlDB _contexto = new ContextoMySqlDB();

        public int InserirAfiliado(Afiliado afiliado)
        {
            var result = 0;

            try
            {
                int idResult = 0;
                var dataNascTx = afiliado.DataNascimento.Year + "-" + afiliado.DataNascimento.Month + "-" + afiliado.DataNascimento.Day;

                var query = "" +
                    " INSERT INTO Afiliado(" +                
                    //"ID, " +                
                    "Nome, " +                
                    "Email, " +                
                    "DataNascimento, " +                
                    "CPF, " + 
                    "RG, " +
                    "PIS, " +
                    "Cargo, " + 
                    "CTPS_Num, " +                
                    "CTPS_Serie, " +                
                    "CONSIR, " +                
                    "NomePai, " +                
                    "NomeMae " +                
                    ") VALUES (";

                //query += "'1',";
                query += "'" + afiliado.Nome + "'";
                query += ",'" + afiliado.Email + "'";
                query += ",'" + dataNascTx + "'";
                query += ",'" + afiliado.CPF + "'";
                query += ",'" + afiliado.RG + "'";
                query += ",'" + afiliado.PIS + "'";
                query += ",'" + afiliado.Cargo + "'";
                query += ",'" + afiliado.CTPS.Numero + "'";
                query += ",'" + afiliado.CTPS.Serie + "'";
                query += ",'" + afiliado.Consir + "'";
                query += ",'" + afiliado.NomePai + "'";
                query += ",'" + afiliado.NomeMae + "'";
                query += ")";

                result = _contexto.Transacao(query);

                //CAPTURANDO ID INSERIDO
                query = "SELECT * FROM Afiliado WHERE CPF='" + afiliado.CPF + "' AND Email='" + afiliado.Email + "' ORDER by ID DESC LIMIT 1";

                var dataTable = _contexto.Consultar(query);

                foreach (DataRow linha in dataTable.Rows)
                {
                    idResult = Convert.ToInt32(linha["ID"]);

                    var obj = new Afiliado
                    {
                        ID = Convert.ToInt32(linha["ID"]),
                        Nome = linha["Nome"].ToString(),
                        RG = linha["RG"].ToString(),
                        CPF = linha["CPF"].ToString(),
                        Email = linha["Email"].ToString(),
                        Cargo = linha["Cargo"].ToString(),
                        Consir = linha["Consir"].ToString(),
                        //CNPJ = linha["CNPJ"].ToString(),
                        //CTPS = linha["Descricao"].ToString(),
                        //DataNascimento = linha["Descricao"].ToString(),
                        //Empresa = linha["Empresa"].ToString(),
                        //Matricula = linha["Matricula"].ToString(),
                        NomeMae = linha["NomeMae"].ToString(),
                        NomePai = linha["NomePai"].ToString(),
                        PIS = linha["PIS"].ToString(),
                    };

                    if (idResult <= 0) return 0;

                    //GRAVAR ENDERECO
                    query = "" +
                        " INSERT INTO Afiliado_Endereco (" +
                        "IDAfiliado, " +                
                        "Rua, " +
                        "Numero, " +
                        "Complemento, " +
                        "Bairro, " +
                        "Cidade, " +
                        "UF, " +
                        "Pais, " +
                        "CEP" +
                        ") VALUES (";

                        query += idResult + ",";
                        query += "'" + afiliado.Endereco.Logradouro + "'";
                        query += ",'" + afiliado.Endereco.Numero + "'";
                        query += ",'" + afiliado.Endereco.Complemento + "'";
                        query += ",'" + afiliado.Endereco.Bairro + "'";
                        query += ",'" + afiliado.Endereco.Cidade + "'";
                        query += ",'" + afiliado.Endereco.UF + "'";
                        query += ",'BRASIL'";
                        query += ",'" + afiliado.Endereco.CEP + "'";
                        query += ")";

                    _contexto.Transacao(query);
                        
                    //GRAVAR INFO EMPRESA
                    query = "" +
                        " INSERT INTO Afiliado_Empresa (" +
                        "IDAfiliado, " +
                        "CNPJ, " +
                        "Nome" +
                        ") VALUES (";

                    query += idResult + ",";
                    query += "'" + afiliado.Empresa + "'";
                    query += ",'" + afiliado.CNPJ + "'";
                    query += ")";

                    _contexto.Transacao(query);

                }

                return idResult;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int InserirDependente(Dependentes dependentes)
        {
            var result = 0;

            try
            {

                var dataNascTx = dependentes.DataNascimento.Year + "-" + dependentes.DataNascimento.Month + "-" + dependentes.DataNascimento.Day;

                var query = "" +
                    " INSERT INTO Afiliado_Dependente (" +
                    "Nome," +
                    "DataNascimento," +
                    "Grau," +
                    "AcrescimoMensal," +
                    "IdAfiliado" +
                    ") VALUES (";
                query += "'" + dependentes.Nome + "',";
                query += "'" + dataNascTx + "',";
                query += "'" + dependentes.GrauParentescoID + "',";
                query += "'" + dependentes.AcrescimoMensal + "',";
                query += "'" + dependentes.IdAfiliado + "'";
                query += ")";

                result = _contexto.Transacao(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;

        }

        public int Concordar(int Id)
        {
            var result = 0;

            try
            {
                var query = "UPDATE Afiliado SET Concordar = 1 WHERE Id=" + Id;
                result = _contexto.Transacao(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public List<GrauParentesco> ListaGrauPArenesco() 
        {
            try
            {
                var lista = new List<GrauParentesco>();
                var query = "SELECT ID, Descricao, LimiteQuantidade FROM Cfg_GrauParentesco WHERE Grau=1 AND D_E_L_E_T_ = 0";
                var dataTable = _contexto.Consultar(query);

                foreach (DataRow linha in dataTable.Rows)
                {
                    var obj = new GrauParentesco
                    {
                        ID = Convert.ToInt32(linha["ID"]),
                        Descricao = linha["Descricao"].ToString(),
                        LimiteQuantidade = Convert.ToInt32(linha["LimiteQuantidade"].ToString())
                    };

                    lista.Add(obj);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Dependentes> ListaDependentes(int idAfiliado)
        {
            try
            {
                var lista = new List<Dependentes>();

                var query = "SELECT ";
                query += " D.ID, D.Nome, D.DataNascimento, D.AcrescimoMensal, D.Grau, P.Descricao GrauNome";
                query += " FROM Afiliado_Dependente D";
                query += " INNER JOIN Cfg_GrauParentesco P ON D.Grau = P.ID";
                query += " WHERE D.D_E_L_E_T_ = 0 AND D.idAfiliado=" + idAfiliado + "";

                var dataTable = _contexto.Consultar(query);

                foreach (DataRow linha in dataTable.Rows)
                {
                    var obj = new Dependentes
                    {
                        ID = Convert.ToInt32(linha["ID"]),
                        Nome = linha["Nome"].ToString(),
                        DataNascimento = Convert.ToDateTime(linha["DataNascimento"].ToString()),
                        GrauParentescoID = Convert.ToInt32(linha["Grau"].ToString()),
                        GrauParentescoNome = linha["GrauNome"].ToString(),
                        AcrescimoMensal = Convert.ToInt32(linha["AcrescimoMensal"].ToString())
                    };

                    lista.Add(obj);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
