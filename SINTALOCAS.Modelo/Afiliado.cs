using System;

namespace SINTALOCAS.Modelo
{
    public class Afiliado
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public DateTime DataNascimento { get; set; }
        public string NomePai { get; set; }
        public string NomeMae { get; set; }
        public string Cargo { get; set; }
        public string Empresa { get; set; }
        public string CNPJ { get; set; }
        public string Consir { get; set; }
        public CTPS CTPS { get; set; }

    }
}
