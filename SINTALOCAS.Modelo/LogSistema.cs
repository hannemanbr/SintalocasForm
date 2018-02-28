using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SINTALOCAS.Modelo
{
    public class LogSistema
    {
        public int ID { set; get; }
        public string Tipo { get; set; }
        public string IP { get; set; }
        public string Usuario { get; set; }
        public string Acao { get; set; }
        public string Valores { get; set; }
        public string Link { get; set; }
        public string Geolocation { get; set; }
        public int IDAcesso { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
