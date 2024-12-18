using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace NYS_ERP.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<MaterialType> MaterialTypes { get; set; }
        public DbSet<CostCenter> CostCenters { get; set; }
        public DbSet<BOM> BOMs { get; set; }
        public DbSet<Rota> Rotas { get; set; }
        public DbSet<WorkCenter> WorkCenters { get; set; }
        public DbSet<Operation> Operations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Company
            modelBuilder.Entity<Company>().HasData(
                new Company { COMCODE = "001", COMTEXT = "Company A", ADDRESS1 = "123 Main St", ADDRESS2 = "Suite 100" },
                new Company { COMCODE = "002", COMTEXT = "Company B", ADDRESS1 = "456 Elm St", ADDRESS2 = "Apt 5" },
                new Company { COMCODE = "003", COMTEXT = "Company C", ADDRESS1 = "789 Oak St", ADDRESS2 = "Floor 2" },
                new Company { COMCODE = "004", COMTEXT = "Company D", ADDRESS1 = "101 Pine St", ADDRESS2 = "Building B" }
            );

            modelBuilder.Entity<Language>()
            .HasOne(y => y.Company)
            .WithMany() 
            .HasForeignKey(y => y.COMCODE)
            .HasPrincipalKey(c => c.COMCODE);

            modelBuilder.Entity<Language>()
            .Property(l => l.RowVersion)
            .IsRowVersion();

            // Seed Language
            modelBuilder.Entity<Language>().HasData(
                new Language { LANCODE = "001", LANTEXT = "English", COMCODE = "001" },
                new Language { LANCODE = "002", LANTEXT = "French", COMCODE = "001" },
                new Language { LANCODE = "003", LANTEXT = "Spanish", COMCODE = "002" },
                new Language { LANCODE = "004", LANTEXT = "German", COMCODE = "002" }
            );

            modelBuilder.Entity<Country>()
            .HasOne(y => y.Company)
            .WithMany()
            .HasForeignKey(y => y.COMCODE)
            .HasPrincipalKey(c => c.COMCODE);

            modelBuilder.Entity<Country>()
            .Property(l => l.RowVersion)
            .IsRowVersion();

            modelBuilder.Entity<Country>().HasData(
               new { COUNTRYCODE = "FRA", COUNTRYTEXT = "France", COMCODE = "001" },
               new { COUNTRYCODE = "USA", COUNTRYTEXT = "United States", COMCODE = "002" },
               new { COUNTRYCODE = "TUR", COUNTRYTEXT = "Turkey", COMCODE = "003" },
               new { COUNTRYCODE = "DEU", COUNTRYTEXT = "Germany", COMCODE = "004" }
           );

            modelBuilder.Entity<City>()
            .HasOne(y => y.Company)
            .WithMany()
            .HasForeignKey(y => y.COMCODE)
            .HasPrincipalKey(c => c.COMCODE);

            modelBuilder.Entity<City>()
            .Property(l => l.RowVersion)
            .IsRowVersion();

            modelBuilder.Entity<City>()
            .HasOne(c => c.Company)
            .WithMany()  
            .HasForeignKey(c => c.COMCODE)
            .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<City>()
                .HasOne(c => c.Country)
                .WithMany()  
                .HasForeignKey(c => c.COUNTRYCODE)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Unit>()
            .HasOne(y => y.Company)
            .WithMany()
            .HasForeignKey(y => y.COMCODE)
            .HasPrincipalKey(c => c.COMCODE);

            modelBuilder.Entity<Unit>()
            .Property(l => l.RowVersion)
            .IsRowVersion();

            modelBuilder.Entity<MaterialType>()
            .HasOne(y => y.Company)
            .WithMany()
            .HasForeignKey(y => y.COMCODE)
            .HasPrincipalKey(c => c.COMCODE);

            modelBuilder.Entity<MaterialType>()
            .Property(l => l.RowVersion)
            .IsRowVersion();

            modelBuilder.Entity<CostCenter>()
            .HasOne(y => y.Company)
            .WithMany()
            .HasForeignKey(y => y.COMCODE)
            .HasPrincipalKey(c => c.COMCODE);

            modelBuilder.Entity<CostCenter>()
            .Property(l => l.RowVersion)
            .IsRowVersion();

            modelBuilder.Entity<BOM>()
           .HasOne(y => y.Company)
           .WithMany()
           .HasForeignKey(y => y.COMCODE)
           .HasPrincipalKey(c => c.COMCODE);

            modelBuilder.Entity<BOM>()
            .Property(l => l.RowVersion)
            .IsRowVersion();

            modelBuilder.Entity<Rota>()
           .HasOne(y => y.Company)
           .WithMany()
           .HasForeignKey(y => y.COMCODE)
           .HasPrincipalKey(c => c.COMCODE);

            modelBuilder.Entity<Rota>()
            .Property(l => l.RowVersion)
            .IsRowVersion();

            modelBuilder.Entity<WorkCenter>()
            .HasOne(y => y.Company)
            .WithMany()
            .HasForeignKey(y => y.COMCODE)
            .HasPrincipalKey(c => c.COMCODE);

            modelBuilder.Entity<WorkCenter>()
            .Property(l => l.RowVersion)
            .IsRowVersion();

            modelBuilder.Entity<Operation>()
            .HasOne(y => y.Company)
            .WithMany()
            .HasForeignKey(y => y.COMCODE)
            .HasPrincipalKey(c => c.COMCODE);

            modelBuilder.Entity<Operation>()
            .Property(l => l.RowVersion)
            .IsRowVersion();
        }
    
    }
}
