using System;
using SINTALOCAS.Modelo;
using SINTALOCAS.DAL.Context;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using SINTALOCAS.Modelo.Enumerator;

namespace SINTALOCAS.DAL.DB
{
    public class AfiliacaoDAL : ContextoMySqlDB
    {
        private DependenteDAL _dependenteDAL = new DependenteDAL();

        public int EditarAfiliado(Afiliado afiliado)
        {
            var idAfiliado = 0;

            try
            {
                int idResult = afiliado.ID;
                var dataNascTx = afiliado.DataNascimento.Year + "-" + afiliado.DataNascimento.Month + "-" + afiliado.DataNascimento.Day;

                string query = " UPDATE Afiliado SET " +
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

                idAfiliado = Transacao(query);

                var dataTable = Consultar(query);

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
                    query = " UPDATE Afiliado_Endereco SET " +                        
                        "Rua='" + afiliado.Endereco.Logradouro + "'" +
                        ",Numero='" + afiliado.Endereco.Numero + "'" +
                        ",Complemento='" + afiliado.Endereco.Complemento + "'" +
                        ",Bairro='" + afiliado.Endereco.Bairro + "'" +
                        ",Cidade='" + afiliado.Endereco.Cidade + "'" +
                        ",UF='" + afiliado.Endereco.UF + "'" +
                        ",Pais='BRASIL'" +
                        ",CEP='" + afiliado.Endereco.CEP + "'" +
                        "WHERE IDAfiliado=" + afiliado.Endereco.ID + "";

                    Transacao(query);
                        
                    //GRAVAR INFO EMPRESA
                    query = " UPDATE Afiliado_Empresa SET " +
                        "CNPJ='" + afiliado.CNPJ + "'" +
                        ",Nome='" + afiliado.Empresa + "'" +
                        " WHERE IDAfiliado=" + idResult;
                    
                    Transacao(query);

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
                    "NomeMae, " +
                    "CelDDD," +
                    "CelNumero," +
                    "TelDDD," + 
                    "TelNumero" +
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

                query += ",'" + afiliado.Telefones.Where(t => t.TipoTelefone == TelefoneEnum.Celular01).Select(t => t.DDD).First() + "'";
                query += ",'" + afiliado.Telefones.Where(t => t.TipoTelefone == TelefoneEnum.Celular01).Select(t => t.Numero).First() + "'";
                query += ",'" + afiliado.Telefones.Where(t => t.TipoTelefone == TelefoneEnum.Residencia).Select(t => t.DDD).First() + "'";
                query += ",'" + afiliado.Telefones.Where(t => t.TipoTelefone == TelefoneEnum.Residencia).Select(t => t.Numero).First() + "'";

                query += ")";

                result = Transacao(query);

                //CAPTURANDO ID INSERIDO
                query = "SELECT * FROM Afiliado WHERE CPF='" + afiliado.CPF + "' AND Email='" + afiliado.Email + "' ORDER by ID DESC LIMIT 1";

                var dataTable = Consultar(query);

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

                    Transacao(query);

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

                    Transacao(query);

                }

                return idResult;

            }
            catch (Exception)
            {
                throw;
            }

        }
        
        public int ConcordaOpcaoPagamento(int Id, int opcaoPagamento)
        {
            var result = 0;

            try
            {
                var query = "UPDATE Afiliado SET " +
                    " Concordar = 1" +
                    " ,Pagamento =" + opcaoPagamento + "" +
                    " WHERE Id=" + Id;

                result = Transacao(query);
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
                result = Transacao(query);
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
                result = Transacao(query);
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
                var dataTable = Consultar(query);

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

                var dataTable = Consultar(query);

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
                        DataNascimento = Convert.ToDateTime(linha["DataNascimento"]),
                        NomeMae = linha["NomeMae"].ToString().ToUpper(),
                        NomePai = linha["NomePai"].ToString().ToUpper(),                        
                        PagamentoID = Convert.ToInt32(linha["Pagamento"].ToString())
                    };

                    //TELEFONES
                    obj.Telefones = new List<Telefone>
                    {
                        new Telefone{
                            DDD =linha["CelDDD"].ToString(),
                            Numero =linha["CelNumero"].ToString(),
                            TipoTelefone = TelefoneEnum.Celular01
                        },
                        new Telefone{
                            DDD =linha["TelDDD"].ToString(),
                            Numero =linha["TelNumero"].ToString(),
                            TipoTelefone = TelefoneEnum.Residencia
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

    }
}
