using Microsoft.EntityFrameworkCore;

namespace encryption_p2Api
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Asistance> Asistance { get; set; }
        public DbSet<Log> Logs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Code)
                .HasMaxLength(10);

           

            modelBuilder.Entity<Employee>()
                .Property(e => e.UpdateDate)
                .HasMaxLength(10);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Department)
                .HasMaxLength(50);

            modelBuilder.Entity<Asistance>()
                .Property(a => a.Type)
                .HasMaxLength(40);

            modelBuilder.Entity<Log>()
                .Property(l => l.Action)
                .HasMaxLength(50);
        }
    }
}