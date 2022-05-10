using JobShopAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobShopAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Cria a relação de muitos para muitos
            builder.Entity<Operation>()
               .HasMany(b => b.Machines)
                .WithMany(c => c.Operations);

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Simulation> Simulations { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet <Machine> Machines { get; set; }
        public DbSet<Operation> Operations { get; set; }
    }
}
