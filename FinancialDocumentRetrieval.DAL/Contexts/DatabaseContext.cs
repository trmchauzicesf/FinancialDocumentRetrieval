using FinancialDocumentRetrieval.DAL.Configurations;
using FinancialDocumentRetrieval.DAL.Identity;
using FinancialDocumentRetrieval.Models.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FinancialDocumentRetrieval.DAL.Contexts
{
    public class DatabaseContext : IdentityDbContext<ApiUser>
    {

        public DatabaseContext()
        { }

        public DatabaseContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<FinancialDocument> FinancialDocuments { get; set; }
        public DbSet<TenantClient> TenantClients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetTableName(entityType.DisplayName());
            }

            // Added non a nonclustred indexed because Guid as Primary key with Clustred index decrease DB performance
            modelBuilder.Entity<Client>()
                .HasKey(e => e.Id)
                .IsClustered(false);

            modelBuilder.Entity<Tenant>()
                .HasKey(e => e.Id)
                .IsClustered(false);

            modelBuilder.Entity<Product>()
                .HasKey(e => e.Id)
                .IsClustered(false);

            modelBuilder.Entity<FinancialDocument>()
                .HasKey(e => e.Id)
                .IsClustered(false);

            modelBuilder.Entity<TenantClient>()
                 .HasKey(e => e.Id)
                 .IsClustered(false);

            modelBuilder.Entity<Client>()
                .Property(p => p.Id)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Tenant>()
                .Property(p => p.Id)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Product>()
                .Property(p => p.Id)
                .HasDefaultValueSql("NEWID()")
                ;
            modelBuilder.Entity<FinancialDocument>()
                .Property(p => p.Id)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<TenantClient>()
                .Property(p => p.Id)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<TenantClient>()
                .HasOne(tc => tc.Tenant)
                .WithMany(t => t.TenantClients)
                .HasForeignKey(tc => tc.TenantId);

            modelBuilder.Entity<TenantClient>()
                .HasOne(tc => tc.Client)
                .WithMany(c => c.TenantClients)
                .HasForeignKey(tc => tc.ClientId);

            modelBuilder.Entity<FinancialDocument>()
                .HasOne(fd => fd.Client)
                .WithMany(c => c.FinancialDocuments)
                .HasForeignKey(fd => fd.ClientId);

            modelBuilder.Entity<FinancialDocument>()
                .HasOne(fd => fd.Tenant)
                .WithMany(t => t.FinancialDocuments)
                .HasForeignKey(fd => fd.TenantId);

            modelBuilder.Entity<FinancialDocument>()
                .HasOne(fd => fd.Product)
                .WithMany(p => p.FinancialDocuments)
                .HasForeignKey(fd => fd.ProductId);
        }
    }
}

