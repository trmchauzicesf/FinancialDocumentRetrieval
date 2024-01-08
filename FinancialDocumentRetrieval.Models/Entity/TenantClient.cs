using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialDocumentRetrieval.Models.Entity
{
    public class TenantClient : BaseEntity
    {
        [ForeignKey(nameof(Tenant))]
        public Guid TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }

        [ForeignKey(nameof(Client))]
        public Guid ClientId { get; set; }
        public virtual Client Client { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
