using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialDocumentRetrieval.Models.Entity
{
    public class FinancialDocument : BaseEntity
    {
        [Required]
        public string Data { get; set; }

        [MaxLength]
        public string Name { get; set; }

        [ForeignKey(nameof(Tenant))]
        public Guid TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }

        [ForeignKey(nameof(Client))]
        public Guid ClientId { get; set; }
        public virtual Client Client { get; set; }

        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
