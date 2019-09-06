using Microsoft.EntityFrameworkCore;

namespace KairanTool.Models
{
    public class KairanToolDbContext  : DbContext  
    {         
        public KairanToolDbContext(DbContextOptions<KairanToolDbContext> options) : base(options)         
        {         
            
        }       
        public DbSet<Kairan> Kairans { get; set; } 
        public DbSet<Role> Roles { get; set; } 
        public DbSet<Type> Type { get; set; } 
  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new {Id = 1, Name = "SV" },
                new {Id = 2,  Name = "Com" },
                new {Id = 3, Name = "LD/SL" },
                new {Id = 4, Name = "OP" }
            );
            modelBuilder.Entity<Type>().HasData(
                new {Id = 1, Name = "Feedback" },
                new {Id = 2,  Name = "Update" },
                new {Id = 3, Name = "Information" }
            );
            
        }
    } 
}
