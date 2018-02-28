using SINTALOCAS.DAL.DB;
using SINTALOCAS.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SINTALOCAS.Dominio.Servico
{
    public class LogServico
    {
        public static void Registrar(string tipo, string ip, string usuario, string link, string acao, string valor, int idacesso)
        {
            try
            {
                var geolocation = GetLocalizacao(ip);
                LogDAL.RegistraLog(tipo, ip, usuario, link, acao, valor, geolocation, idacesso);

                //ENVIAR EMAIL
                //EmailServico.EnviarEmailAdmin();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static string GetLocalizacao(string ip)
        {
            var result = "";

            try
            {
                var localizacaoXMl = new XmlDocument();

                localizacaoXMl.Load("http://freegeoip.net/xml/" + ip);
                var localizacaoXmlNode = localizacaoXMl.GetElementsByTagName("Response");

                for (var indice = 0; indice<= localizacaoXmlNode.Count - 1; indice++)
                {
                    if (localizacaoXmlNode[indice].ChildNodes.Item(5).InnerText.Trim() != "")
                        result += " | Cidade : " + localizacaoXmlNode[indice].ChildNodes.Item(5).InnerText.Trim();

                    if (localizacaoXmlNode[indice].ChildNodes.Item(2).InnerText.Trim() != "")
                        result += " | País : " + localizacaoXmlNode[indice].ChildNodes.Item(2).InnerText.Trim();

                    if (localizacaoXmlNode[indice].ChildNodes.Item(4).InnerText.Trim() != "")
                        result += " | Região : " + localizacaoXmlNode[indice].ChildNodes.Item(4).InnerText.Trim();

                    //lbResultado.Items.Add("End. IP : " & xmlnode(i).ChildNodes.Item(0).InnerText.Trim())
                    //lbResultado.Items.Add("Cód. País : " & xmlnode(i).ChildNodes.Item(1).InnerText.Trim())
                    //lbResultado.Items.Add("País : " & xmlnode(i).ChildNodes.Item(2).InnerText.Trim())
                    //lbResultado.Items.Add("Cod. Região : " & xmlnode(i).ChildNodes.Item(3).InnerText.Trim())
                    //lbResultado.Items.Add("Nome Região : " & xmlnode(i).ChildNodes.Item(4).InnerText.Trim())
                    //lbResultado.Items.Add("Cidade : " & xmlnode(i).ChildNodes.Item(5).InnerText.Trim())
                    //lbResultado.Items.Add("Cep : " & xmlnode(i).ChildNodes.Item(6).InnerText.Trim())
                    //lbResultado.Items.Add("Time Zone : " & xmlnode(i).ChildNodes.Item(7).InnerText.Trim())
                    //lbResultado.Items.Add("Latitude : " & xmlnode(i).ChildNodes.Item(8).InnerText.Trim())
                    //lbResultado.Items.Add("Longitude : " & xmlnode(i).ChildNodes.Item(9).InnerText.Trim())
                }
                
            }
            catch (Exception ex)
            {
                //throw;
                result = ex.Message;
            }

            if (result.Length > 400) result = result.Substring(0, 400);

            return result;
        }

        public static List<LogSistema> Lista(string local = "")
        {
            try
            {
                var dataInicio = DateTime.Now;
                var dataFim = DateTime.Now;

                return LogDAL.Lista(local, dataInicio, dataFim)
                    .Where(x => x.IP.Trim() != "::1")
                    .OrderByDescending(x => x.DataCadastro)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<LogSistema> ListaAgrupado(string local = "")
        {
            try
            {
                var dataInicio = DateTime.Now;
                var dataFim = DateTime.Now;

                return LogDAL.ListaAgrupado_Data_IP(local, dataInicio, dataFim)
                    .Where(x => x.IP.Trim() != "::1")
                    .OrderByDescending(x => x.DataCadastro)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
