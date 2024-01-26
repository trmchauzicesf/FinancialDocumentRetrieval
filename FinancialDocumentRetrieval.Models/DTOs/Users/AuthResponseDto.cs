namespace FinancialDocumentRetrieval.Models.Users
{
    public record AuthResponseDto
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
