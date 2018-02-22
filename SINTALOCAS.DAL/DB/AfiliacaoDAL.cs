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

                _contexto.Transacao(query);

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

        public void InserirDependente(Dependentes dependentes)
        {

            try
            {

                var dataNascTx = dependentes.DataNascimento.Year + "-" + dependentes.DataNascimento.Month + "-" + dependentes.DataNascimento.Day;

                var query = "" +
                    " INSERT INTO Afiliado(" +
                    //"ID, " +                
                    "Descricao" +
                    ") VALUES (";
                query += "'" + dependentes.Nome + "'";
                query += ")";

                _contexto.Transacao(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<GrauParentesco> ListaGrauPArenesco() 
        {
            try
            {
                var lista = new List<GrauParentesco>();
                var query = "SELECT ID, Descricao FROM Cfg_GrauParentesco WHERE Grau=1 AND D_E_L_E_T_ = 0";
                var dataTable = _contexto.Consultar(query);

                foreach (DataRow linha in dataTable.Rows)
                {
                    var obj = new GrauParentesco
                    {
                        ID = Convert.ToInt32(linha["ID"]),
                        Descricao = linha["Descricao"].ToString(),
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
