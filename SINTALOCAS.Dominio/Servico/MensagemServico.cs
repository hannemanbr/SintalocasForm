using System;
using System.Collections.Generic;
using System.Linq;
using SINTALOCAS.DAL.DB;
using SINTALOCAS.Modelo;

namespace SINTALOCAS.Dominio.Servico
{
    public static class MensagemServico
    {
        public static List<MensagemSistema> Consultar(string categoria, string alias)
        {
            TextosDAL _textosDAL = new TextosDAL();
            var lista = new List<MensagemSistema>();

            if (categoria.Trim() != "" && alias.Trim() != "") 
            {
                lista = _textosDAL.Consultar()
                                  .Where(x => x.Categoria == categoria && x.Alias == alias)
                                  .OrderBy(x => x.Texto)
                                  .ToList();   
            }
            else if (categoria.Trim() != "")
            {
                lista = _textosDAL.Consultar()
                                  .Where(x => x.Categoria == categoria)
                                  .OrderBy(x => x.Texto)
                                  .ToList();
            }
            else if (alias.Trim() != "")
            {
                lista = _textosDAL.Consultar()
                                  .Where(x => x.Alias == alias)
                                  .OrderBy(x => x.Texto)
                                  .ToList();
            }
            else
            { 
                lista = _textosDAL.Consultar().OrderBy(x => x.Texto).ToList();
            }

            if (lista.Count() == 0) lista.Add(new MensagemSistema { Categoria = "", Texto = "", ID = -1 });

            return lista;

        }

    }
}
