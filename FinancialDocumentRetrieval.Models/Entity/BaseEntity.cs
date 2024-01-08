using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialDocumentRetrieval.Models.Entity
{
    public abstract class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public DateTime ModifiedOn { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
