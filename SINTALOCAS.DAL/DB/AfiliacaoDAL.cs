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
                query += ",'" + afiliado.Cargo + "'";
                query += ",'" + afiliado.CTPS.Numero + "'";
                query += ",'" + afiliado.CTPS.Serie + "'";
                query += ",'" + afiliado.CTPS.PIS + "'";
                //query += ",'" + afiliado.PIS + "'";
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
                        NomeMae = linha["NomeMae"].ToString(),
                        NomePai = linha["NomePai"].ToString()                        
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
            catch (Exception)
            {
                throw;
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
            catch (Exception )
            {
                throw;
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
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public int RemoveDependente(int Id)
        {
            var result = 0;

            try
            {
                var query = "UPDATE Afiliado_Dependente SET D_E_L_E_T_ = 1 WHERE Id=" + Id;
                result = _contexto.Transacao(query);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public int RemoveAfiliado(int Id)
        {
            var result = 0;

            try
            {
                var query = "UPDATE Afiliado SET D_E_L_E_T_ = 1 WHERE Id=" + Id;
                result = _contexto.Transacao(query);
            }
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
            }
        }

        public List<Afiliado> ListaAfiliado(string cpf, int id = 0)
        {
            try
            {
                var lista = new List<Afiliado>();

                var query = "SELECT                          " +
                                "A.Nome                      " +
                                ",A.Email                    " +
                                ",A.DataNascimento           " +
                                ",A.Cargo                    " +
                                ",A.CONSIR                   " +
                                ",A.CPF                      " +
                                ",A.CTPS_Num                 " +
                                ",A.CTPS_Serie               " +
                                ",A.ID                       " +
                                ",A.NomeMae                  " +
                                ",A.NomePai                  " +
                                ",A.PIS                      " +
                                ",A.RG                       " +
                                ",E.Rua                      " +
                                ",E.Numero                   " +
                                ",E.Complemento              " +
                                ",E.Bairro                   " +
                                ",E.Cidade                   " +
                                ",E.CEP                      " +
                                ",E.UF                       " +
                                ",E.Pais                     " +
                                ",EP.CNPJ EmpresaCNPJ        " +
                                ",EP.Nome EmpresaNome        " +
                             " FROM Afiliado A               " +
                             " LEFT JOIN Afiliado_Endereco E " +                             
                             " ON E.IdAfiliado = A.ID        " +
                             "      AND E.D_E_L_E_T_ = 0     " +
                             "      AND A.D_E_L_E_T_ = 0     " +
                             " LEFT JOIN Afiliado_Empresa EP " +                            
                             " ON EP.IdAfiliado = A.ID       " +
                             "      AND EP.D_E_L_E_T_ = 0    " +
                             " WHERE 1=1                     ";

                if (cpf.Trim() != "") query += " AND A.CPF='" + cpf + "'";
                if (id > 0) query += " AND A.ID='" + id + "'";

                var dataTable = _contexto.Consultar(query);

                foreach (DataRow linha in dataTable.Rows)
                {
                    var obj = new Afiliado
                    {
                        ID = Convert.ToInt32(linha["ID"]),
                        Nome = linha["Nome"].ToString(),
                        RG = linha["RG"].ToString(),
                        CPF = linha["CPF"].ToString(),
                        Email = linha["Email"].ToString(),
                        Cargo = linha["Cargo"].ToString(),
                        Consir = linha["Consir"].ToString(),
                        CNPJ = linha["EmpresaCNPJ"].ToString(),
                        Empresa = linha["EmpresaNome"].ToString(),
                        DataNascimento = Convert.ToDateTime(linha["DataNascimento"].ToString()),
                        NomeMae = linha["NomeMae"].ToString(),
                        NomePai = linha["NomePai"].ToString(),
                    };

                    //OBJETO CTPS
                    var ctps = new CTPS
                    {
                        Numero = linha["CTPS_Num"].ToString(),
                        Serie = linha["CTPS_Serie"].ToString(),
                        PIS = linha["PIS"].ToString()
                    };
                    
                    obj.CTPS = ctps;

                    //OBJETO ENDEREÇO
                    var endereco = new Endereco
                    {
                        Logradouro = linha["RUA"].ToString(),
                        Numero = linha["Numero"].ToString(),
                        Complemento = linha["Complemento"].ToString(),
                        Bairro = linha["Bairro"].ToString(),
                        CEP = linha["CEP"].ToString(),
                        Cidade = linha["Cidade"].ToString(),
                        Pais = linha["Pais"].ToString(),
                        UF = linha["UF"].ToString()
                    };

                    obj.Endereco = endereco;

                    lista.Add(obj);
                }

                return lista;
            }
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
            }
        }

        public Dictionary<int, int> QuantidadeDependentes(int idAfiliado)
        {
            try
            {
                var lista = new Dictionary<int, int>();

                var query = "SELECT count(0) Total," +
                    " Nome, DataNascimento, AcrescimoMensal, Grau" +
                    " FROM Afiliado_Dependente D" +
                    " WHERE idAfiliado=" + idAfiliado + "" +
                    " GROUP BY Nome, DataNascimento, AcrescimoMensal";

                var dataTable = _contexto.Consultar(query);

                foreach (DataRow linha in dataTable.Rows)
                {                    
                    if (lista.ContainsKey(Convert.ToInt32(linha["Grau"])))
                        lista.Add(Convert.ToInt32(linha["Grau"]), Convert.ToInt32(linha["Total"]));
                }

                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
