using FinancialDocumentRetrieval.BL.Implementation;
using FluentAssertions;
using System.Text.RegularExpressions;

namespace FinancialDocumentRetrieval.Test.UnitTests.Services
{
    public class AnonymizationServiceTests
    {
        private readonly string _testJson =
            "{\"account_number\":\"95867648\",\"balance\":42331.12,\"currency\":\"EUR\",\"transactions\":[{\"transaction_id\":\"2913\",\"amount\":166.95,\"date\":\"1/4/2015\",\"description\":\"Grocery shopping\",\"category\":\"Food & Dining\"},{\"transaction_id\":\"3882\",\"amount\":6.58,\"date\":\"24/4/2016\",\"description\":\"Grocery shopping\",\"category\":\"Food & Dining\"},{\"transaction_id\":\"1143\",\"amount\":-241.07,\"date\":\"25/12/2019\",\"description\":\"Gas station purchase\",\"category\":\"Utilities\"}]}";

        private const string Mask = "#####";

        [Fact]
        public void AnonymizeFinancialDocument_Should_Mask_Fields()
        {
            // Arrange
            var productCode = "Code1";
            var anonymizationService = new AnonymizationService();
            var fieldsToAnonymize = new[] { "balance", "description" };

            // Act
            var anonymizedJson = anonymizationService.AnonymizeFinancialDocument(this._testJson, productCode);

            // Assert
            anonymizedJson.Should().NotBeNullOrEmpty();
            anonymizedJson.Should().BeOfType<string>();

            foreach (var field in fieldsToAnonymize)
            {
                anonymizedJson.Should().Contain($"\"{field}\": \"{Mask}\"");
            }
        }

        [Fact]
        public void AnonymizeFinancialDocument_AccountNumber_Should_Have_64_Character_Hash_Without_Special_Chars()
        {
            // Arrange
            var productCode = "Code1";
            var anonymizationService = new AnonymizationService();
            var fieldsToAnonymize = new[] { "account_number", "date" };
            var lengthOfSha256HashedString = 64;


            // Act
            var anonymizedJson = anonymizationService.AnonymizeFinancialDocument(_testJson, productCode);

            // Assert

            foreach (var field in fieldsToAnonymize)
            {
                var lengthOfFieldDeclarationInJson =
                    field.Length + 6; // six is number of special chars before and after json field declaration
                var pattern = $"\"{field}\":\\s*\"[0-9a-fA-F-]{{{lengthOfSha256HashedString}}}\"";
                var regex = new Regex(pattern);
                var match = regex.Match(anonymizedJson);

                match.Value.Should().NotBeNullOrEmpty();
                match.Value.Should().BeOfType<string>();
                match.Value.Should().HaveLength(lengthOfFieldDeclarationInJson + lengthOfSha256HashedString);
                match.Success.Should().BeTrue();
                match.Value.Should().MatchRegex($"\"{field}\":\\s*\"[0-9a-fA-F-]{{{lengthOfSha256HashedString}}}\"");
            }
        }
    }
}