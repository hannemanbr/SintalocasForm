using System;
namespace SINTALOCAS.Web.MVC.Models
{
    public class DependenteModelView
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public int GrauParentesco { get; set; }
        public int IDAfiliado { get; set; }
    }
}
