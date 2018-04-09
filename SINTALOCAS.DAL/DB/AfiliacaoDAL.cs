using System;
using SINTALOCAS.Modelo;
using SINTALOCAS.DAL.Context;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using SINTALOCAS.Modelo.Enumerator;

namespace SINTALOCAS.DAL.DB
{
    public class AfiliacaoDAL
    {
        ContextoMySqlDB _contexto = new ContextoMySqlDB();

        public int EditarAfiliado(Afiliado afiliado)
        {
            var idAfiliado = 0;

            try
            {
                int idResult = afiliado.ID;
                var dataNascTx = afiliado.DataNascimento.Year + "-" + afiliado.DataNascimento.Month + "-" + afiliado.DataNascimento.Day;

                string query = " UPDATE Afiliado " +
                    "Nome='" + afiliado.Nome + "'" +
                    ",Email='" + afiliado.Email + "'" + 
                    ",DataNascimento='" + dataNascTx + "'" +            
                    ",CPF='" + afiliado.CPF + "'" +
                    ",RG='" + afiliado.RG + "'" +
                    ",PIS='" + afiliado.CTPS.PIS + "'" +
                    ",Cargo='" + afiliado.Cargo + "'" +
                    ",CTPS_Num='" + afiliado.CTPS.Numero + "'" +      
                    ",CTPS_Serie='" + afiliado.CTPS.Serie + "'" +         
                    ",CONSIR='" + afiliado.Consir + "'" +
                    ",NomePai='" + afiliado.NomePai + "'" +     
                    ",NomeMae='" + afiliado.NomeMae + "'" +   
                    " WHERE ID="+ idResult;

                idAfiliado = _contexto.Transacao(query);

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
                        NomeMae = linha["NomeMae"].ToString(),
                        NomePai = linha["NomePai"].ToString()                        
                    };

                    if (idResult <= 0) return 0;

                    //GRAVAR ENDERECO
                    query = " UPDATE Afiliado_Endereco " +                        
                        "Rua='" + afiliado.Endereco.Logradouro + "'" +
                        ",Numero='" + afiliado.Endereco.Numero + "'" +
                        ",Complemento='" + afiliado.Endereco.Complemento + "'" +
                        ",Bairro='" + afiliado.Endereco.Bairro + "'" +
                        ",Cidade='" + afiliado.Endereco.Cidade + "'" +
                        ",UF='" + afiliado.Endereco.UF + "'" +
                        ",Pais='BRASIL'" +
                        ",CEP='" + afiliado.Endereco.CEP + "'" +
                        "WHERE IDAfiliado=" + afiliado.Endereco.ID + "";

                    _contexto.Transacao(query);
                        
                    //GRAVAR INFO EMPRESA
                    query = " UPDATE Afiliado_Empresa " +
                        ",CNPJ='" + afiliado.CNPJ + "'" +
                        ",Nome='" + afiliado.Empresa + "'" +
                        " WHERE IDAfiliado=" + idResult;
                    
                    _contexto.Transacao(query);

                }

                return idResult;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public int InserirAfiliado(Afiliado afiliado)
        {
            var result = 0;

            try
            {
                int idResult = 0;
                var dataNascTx = afiliado.DataNascimento.Year + "-" + afiliado.DataNascimento.Month + "-" + afiliado.DataNascimento.Day;

                var query = "" +
                    " INSERT INTO Afiliado(" +
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
                query += ",'" + afiliado.CTPS.PIS + "'";
                query += ",'" + afiliado.Cargo + "'";
                query += ",'" + afiliado.CTPS.Numero + "'";
                query += ",'" + afiliado.CTPS.Serie + "'";
                query += ",'" + afiliado.Consir + "'";
                query += ",'" + afiliado.NomePai + "'";
                query += ",'" + afiliado.NomeMae + "'";

                query += ",'" + afiliado.Telefones.Where(t => t.TipoTelefone == TelefoneEnum.Celular01).Select(t => t.DDD).ToString() + "'";
                query += ",'" + afiliado.Telefones.Where(t => t.TipoTelefone == TelefoneEnum.Celular01).Select(t => t.Numero).ToString() + "'";
                query += ",'" + afiliado.Telefones.Where(t => t.TipoTelefone == TelefoneEnum.Residencia).Select(t => t.DDD).ToString() + "'";
                query += ",'" + afiliado.Telefones.Where(t => t.TipoTelefone == TelefoneEnum.Residencia).Select(t => t.Numero).ToString() + "'";

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

                    query += idResult;
                    query += ",'" + afiliado.CNPJ + "'";
                    query += ",'" + afiliado.Empresa + "'";
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
            catch (Exception)
            {
                throw;
            }

            return result;

        }

        public int Concordar(int Id, int opcaoPagamento, int opcaoContribuicao)
        {
            var result = 0;

            try
            {
                var query = "UPDATE Afiliado SET " +
                    " Concordar = 1" +
                    " ,Pagamento =" + opcaoPagamento + "" +
                    " ,Contribuicao =" + opcaoContribuicao + "" +
                    " WHERE Id=" + Id;

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
                                ",A.Pagamento                " +
                                ",A.Contribuicao             " +
                                ",A.CelDDD                   " +
                                ",A.CelNumero                " +
                                ",A.TelDDD                   " +
                                ",A.TelNumero                " +
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
                             " WHERE A.D_E_L_E_T_=0          ";

                if (cpf.Trim() != "") query += " AND A.CPF='" + cpf + "'";
                if (id > 0) query += " AND A.ID='" + id + "'";

                var dataTable = _contexto.Consultar(query);

                foreach (DataRow linha in dataTable.Rows)
                {
                    var obj = new Afiliado
                    {
                        ID = Convert.ToInt32(linha["ID"]),
                        Nome = linha["Nome"].ToString().ToUpper(),
                        RG = linha["RG"].ToString().ToUpper(),
                        CPF = linha["CPF"].ToString().ToUpper(),
                        Email = linha["Email"].ToString().ToUpper(),
                        Cargo = linha["Cargo"].ToString().ToUpper(),
                        Consir = linha["Consir"].ToString().ToUpper(),
                        CNPJ = linha["EmpresaCNPJ"].ToString().ToUpper(),
                        Empresa = linha["EmpresaNome"].ToString().ToUpper(),
                        DataNascimento = Convert.ToDateTime(linha["DataNascimento"].ToString()),
                        NomeMae = linha["NomeMae"].ToString().ToUpper(),
                        NomePai = linha["NomePai"].ToString().ToUpper(),
                        ContribuicaoID = Convert.ToInt32(linha["Contribuicao"].ToString()),
                        PagamentoID = Convert.ToInt32(linha["Pagamento"].ToString())
                    };

                    //TELEFONES
                    obj.Telefones = new List<Telefone>
                    {
                        new Telefone{
                            DDD =linha["CelDDD"].ToString(),
                            Numero =linha["CelNumero"].ToString()
                        },
                        new Telefone{
                            DDD =linha["TelDDD"].ToString(),
                            Numero =linha["TelNumero"].ToString()
                        }
                    };

                    //OBJETO CTPS
                    var ctps = new CTPS
                    {
                        Numero = linha["CTPS_Num"].ToString().ToUpper(),
                        Serie = linha["CTPS_Serie"].ToString().ToUpper(),
                        PIS = linha["PIS"].ToString().ToUpper()
                    };

                    obj.CTPS = ctps;

                    //OBJETO ENDEREÇO
                    var endereco = new Endereco
                    {
                        Logradouro = linha["RUA"].ToString().ToUpper(),
                        Numero = linha["Numero"].ToString().ToUpper(),
                        Complemento = linha["Complemento"].ToString().ToUpper(),
                        Bairro = linha["Bairro"].ToString().ToUpper(),
                        CEP = linha["CEP"].ToString(),
                        Cidade = linha["Cidade"].ToString().ToUpper(),
                        Pais = linha["Pais"].ToString().ToUpper(),
                        UF = linha["UF"].ToString().ToUpper()
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
                        Nome = linha["Nome"].ToString().ToUpper(),
                        DataNascimento = Convert.ToDateTime(linha["DataNascimento"].ToString()),
                        GrauParentescoID = Convert.ToInt32(linha["Grau"].ToString()),
                        GrauParentescoNome = linha["GrauNome"].ToString().ToUpper(),
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
