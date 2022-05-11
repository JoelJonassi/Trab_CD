    using JobShopAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobShopAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
       // https://stackoverflow.com/questions/63306882/specify-on-delete-no-action-or-on-update-no-action-or-modify-other-foreign-key
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

            builder.Entity<Simulation>()
            .HasOne(e => e.Machine)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Simulation>()
            .HasOne(e => e.Operation)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

            //Permite uma ligação de um para muitos
            builder.Entity<Simulation>()
            .HasOne(e => e.Job)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);   
            


        }

        //Models para acesso a base de dados
        public DbSet<User> Users { get; set; }
        public DbSet<Simulation> Simulations { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet <Machine> Machines { get; set; }
        public DbSet<Operation> Operations { get; set; }
    }
}
