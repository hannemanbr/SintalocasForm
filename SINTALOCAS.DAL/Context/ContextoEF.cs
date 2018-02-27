using SINTALOCAS.Modelo;
using System.Data.Entity;

namespace SINTALOCAS.DAL.DB
{    
    public class ContextoEF : DbContext
    {
        //public ContextoEF(DbContextOptions<ContextoEF> options) : base(options) { }
        public ContextoEF() : base("MySQLConexao")
        {            
        }

        public DbSet<LogSistema> LogsSistema { get; set; }
        public DbSet<Afiliado> Afiliados { get; set; }
        public DbSet<Dependentes> Dependentes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

    }
}
