using System;
using System.Collections.Generic;
using SINTALOCAS.Modelo;

namespace SINTALOCAS.Web.MVC.Models
{
    public class AfiliacaoModelView
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public int CTPS_Numero { get; set; }
        public string CTPS_Serie { get; set; }
        public string CPF { get; set; }
        public string CONSIR { get; set; }
        public string Email { get; set; }
        public string Logradoro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        //public string UF { get; set; }
        public List<UnidadeFederativa> UnidadeFederativa { get; set; }
        public string CEP { get; set; }


    }

}
