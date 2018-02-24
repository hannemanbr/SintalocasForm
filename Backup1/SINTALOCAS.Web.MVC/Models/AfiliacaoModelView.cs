using System;
using System.Collections.Generic;
using SINTALOCAS.Modelo;

namespace SINTALOCAS.Web.MVC.Models
{
    public class AfiliacaoModelView
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Consir { get; set; }
        public string Cargo { get; set; }
        public string CTPS_Numero { get; set; }
        public string CTPS_Serie { get; set; }
        public string RG { get; set; }
        public string CNPJ { get; set; }
        public string Empresa { get; set; }
        public string CPF { get; set; }
        public string CONSIR { get; set; }
        public string Email { get; set; }
        public string Logradoro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string NomePai { get; set; }
        public string NomeMae { get; set; }
        public string PIS { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public string DDDCel { get; set; }
        public string NumCel { get; set; }
        public string DDDTel { get; set; }
        public string NumTel { get; set; }
        public DateTime DataNascimento { get; set; }
    }

}
