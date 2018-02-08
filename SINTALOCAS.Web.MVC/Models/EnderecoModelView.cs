using System;
namespace SINTALOCAS.Web.MVC.Models
{
    public class EnderecoModelView
    {
        public int ID { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public string MensagemConsulta { get; set; }
    }
}
