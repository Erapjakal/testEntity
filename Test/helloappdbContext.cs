using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Test
{
    public partial class helloappdbContext : DbContext
    {
        public helloappdbContext()
        {
        }

        public helloappdbContext(DbContextOptions<helloappdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Продукты> Продуктыs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Продукты>(entity =>
            {
                entity.ToTable("Продукты");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
