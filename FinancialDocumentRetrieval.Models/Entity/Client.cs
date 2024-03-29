﻿using System.ComponentModel.DataAnnotations;

namespace FinancialDocumentRetrieval.Models.Entity
{
    public class Client : BaseEntity
    {
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Vat { get; set; }

        [Required]
        [MaxLength(50)]
        public string RegistrationNumber { get; set; }

        [Required]
        [MaxLength(6)]
        public string CompanyType { get; set; }

        public virtual ICollection<TenantClient> TenantClients { get; set; }

        public virtual ICollection<FinancialDocument> FinancialDocuments { get; set; }
    }
}

