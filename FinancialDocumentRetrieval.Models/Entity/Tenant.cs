using System.ComponentModel.DataAnnotations;

namespace FinancialDocumentRetrieval.Models.Entity
{
    public class Tenant : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public virtual ICollection<TenantClient> TenantClients { get; set; }

        public virtual ICollection<FinancialDocument> FinancialDocuments { get; set; }
    }
}
