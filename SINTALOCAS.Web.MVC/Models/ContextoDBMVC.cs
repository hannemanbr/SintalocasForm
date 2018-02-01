using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace SINTALOCAS.Web.MVC.Models
{
    public class ContextoDBMVC 
    {
        private MySqlConnection conexao;

        public ContextoDBMVC()
        {
            var conexaoString = ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString;
            conexao = new MySqlConnection(conexaoString);
        }

    }
}
