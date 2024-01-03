using FinancialDocumentRetrieval.DAL.Configurations;
using FinancialDocumentRetrieval.DAL.Identity;
using FinancialDocumentRetrieval.Models.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinancialDocumentRetrieval.DAL.Contexts
{
    public class DatabaseContext : IdentityDbContext<ApiUser>
    {

        public DatabaseContext()
        { }

        public DatabaseContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}

