using Microsoft.EntityFrameworkCore;
using SorteioTimes.Model;

namespace SorteioTimes.Context
{
    public class SorteioTimesContext : DbContext
    {
        public SorteioTimesContext()
        {

        }
        public SorteioTimesContext(DbContextOptions<SorteioTimesContext> options)
            : base(options)
        {

        }        

        public virtual DbSet<Jogador> Jogadores { get; set; }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=SorteioTimes;User Id=postgres;Password=123456;");
            }
        }
    }
}