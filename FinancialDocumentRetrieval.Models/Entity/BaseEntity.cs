using System.ComponentModel.DataAnnotations;

namespace FinancialDocumentRetrieval.Models.Entity
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
