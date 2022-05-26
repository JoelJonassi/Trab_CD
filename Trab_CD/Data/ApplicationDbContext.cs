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


         



            builder.Entity<JobSimulation>()
             .HasKey(jp => new { jp.IdJob, jp.IdSimulation });

            //Cria a relação de muitos para muitos
            builder.Entity<JobSimulation>()
            .HasOne(j => j.Job)
            .WithMany(b => b.JobSimulation)
            .HasForeignKey(jp => jp.IdJob);

            builder.Entity<JobSimulation>()
              .HasOne(j => j.Simulation)
              .WithMany(b => b.JobSimulation)
              .HasForeignKey(jp => jp.IdSimulation);


            builder.Entity<MachineOperation>()
            .HasKey(jp => new { jp.IdMachine, jp.IdOperation });

            //Cria a relação de muitos para muitos
            builder.Entity<MachineOperation>()
            .HasOne(j => j.Machine)
            .WithMany(b => b.MachineOperation)
            .HasForeignKey(jp => jp.IdMachine);
         


            builder.Entity<MachineOperation>()
                .HasOne(j => j.Operation)
                .WithMany(b => b.MachineOperation)
                .HasForeignKey(jp => jp.IdOperation);

            builder.Entity<JobOperation>()
            .HasKey(jp => new { jp.IdJob, jp.IdOperation });




            /*builder.Entity<Simulation>()
             .HasOne(j => j.Job)
             .WithMany(b => b.)
             .HasForeignKey(jp => jp.IdJob);*/

            builder.Entity<JobOperation>()
             .HasOne(j => j.Job)
             .WithMany(b => b.JobOperation)
             .HasForeignKey(jp => jp.IdJob);

            builder.Entity<JobOperation>()
            .HasOne(j => j.Operation)
            .WithMany(b => b.JobOperation)
            .HasForeignKey(jp => jp.IdOperation);

         


        }

        //Models para acesso a base de dados
        public DbSet<User> Users { get; set; }
        public DbSet<Simulation> Simulations { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet <Machine> Machines { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<MachineOperation> MachineOperation { get; set; }
        public DbSet<JobOperation> JobOperation { get; set; }
        public DbSet<JobSimulation> JobSimulation { get; set; }
        public DbSet<Plan> Plan { get; set; }
    }
}
