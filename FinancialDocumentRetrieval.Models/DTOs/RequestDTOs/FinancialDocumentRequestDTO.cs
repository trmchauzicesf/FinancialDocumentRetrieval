﻿using System.ComponentModel.DataAnnotations;

namespace FinancialDocumentRetrieval.Models.DTOs.RequestDTOs
{
    public record FinancialDocumentRequestDTO
    {
        [Required]
        public string ProductCode { get; set; }

        [Required]
        public Guid TenantId { get; set; }

        [Required]
        public Guid DocumentId { get; set; }
    }
}
