using System;
using SINTALOCAS.Modelo;
using SINTALOCAS.DAL.Context;
using System.Data;
using System.Collections.Generic;

namespace SINTALOCAS.DAL.DATA
{
    public class AfiliacaoDAL
    {
        ContextoDB _contexto = new ContextoDB();

        public void Inserir(Afiliado afiliado){

            var query = " INSERT INTO Afiliado";
            query += "(";
            query += "";
            query += "";
            query += "";
            query += "";
            query += "";
            query += "";
            query += "";
            query += "";
            query += "";
            query += "";
            query += "";
            query += "";
            query += "";
            query += ")";

            _contexto.Transacao(query);
            
        }
    }
}
