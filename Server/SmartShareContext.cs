using Microsoft.EntityFrameworkCore;

namespace Server
{
    public class SmartShareContext : DbContext
    {
        // TODO define context and models

        public DbSet<SmartShareFile> SmartShareFileTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server=127.0.0.1;port=5432;database=smartshare;userid=postgres;password=bondstone");
        }
    }
}