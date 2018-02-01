using System;
using SINTALOCAS.Modelo.Enumerator;

namespace SINTALOCAS.Modelo
{
    public class Telefone
    {
        public TelefoneEnum TipoTelefone { get; set; }
        public string DDD { get; set; }
        public string Numero { get; set; }
        //public int Tipo { get; set; }
    }
}
