using System;
namespace SINTALOCAS.Modelo
{
    public class Dependentes
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public int GrauParentescoID { get; set; }
        public string GrauParentescoNome { get; set; }
        public int AcrescimoMensal { get; set; }
        public int IdAfiliado { get; set; }
    }
}
