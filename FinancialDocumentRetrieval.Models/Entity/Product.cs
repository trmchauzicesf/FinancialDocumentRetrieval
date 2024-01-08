using System.ComponentModel.DataAnnotations;

namespace FinancialDocumentRetrieval.Models.Entity
{
    public class Product : BaseEntity
    {
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public virtual ICollection<FinancialDocument> FinancialDocuments { get; set; }
    }
}
