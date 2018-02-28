using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SINTALOCAS.Modelo
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int Perfil { get; set; }
        
    }
}
