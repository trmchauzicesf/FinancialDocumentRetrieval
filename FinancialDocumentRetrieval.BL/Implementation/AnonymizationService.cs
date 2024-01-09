using FinancialDocumentRetrieval.BL.Interface;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using static FinancialDocumentRetrieval.Models.Common.Enums.AppEnums;

namespace FinancialDocumentRetrieval.BL.Implementation
{
    public class AnonymizationService : IAnonymizationService
    {
        private readonly IDictionary<string, IDictionary<string, string[]>> _anonymizationConfig;
        private const string Mask = "#####";
        private const string MaskingTypeAnonymization = "MaskingTypeAnonymization";
        private const string MaskingTypeHash = "MaskingTypeHash";

        public AnonymizationService()
        {
            _anonymizationConfig = new Dictionary<string, IDictionary<string, string[]>>
            {
                {
                    nameof(ProductType.Code1), new Dictionary<string, string[]>
                    {
                        { MaskingTypeAnonymization, new[] { "balance", "description" } },
                        { MaskingTypeHash, new[] { "account_number", "transaction_id" } }
                    }
                },
                {
                    nameof(ProductType.Code2), new Dictionary<string, string[]>
                    {
                        { MaskingTypeAnonymization, new[] { "name", "transaction_id" } },
                        { MaskingTypeHash, new[] { "account_number", "balance" } }
                    }
                },
                {
                    nameof(ProductType.Code3), new Dictionary<string, string[]>
                    {
                        { MaskingTypeAnonymization, new[] { "balance", "transaction_id" } },
                        { MaskingTypeHash, new[] { "account_number", "last_update_date" } }
                    }
                }
            };
        }

        public string AnonymizeFinancialDocument(string json, string productCode)
        {
            if (!_anonymizationConfig.TryGetValue(productCode, out var anonymizationCategories))
            {
                throw new ArgumentException($"Configuration for Product Code: {productCode} not found or is invalid.");
            }

            var fieldsToAnonymize = anonymizationCategories.TryGetValue(MaskingTypeAnonymization, out var anon1)
                ? anon1.ToArray()
                : Array.Empty<string>();
            var fieldsToHash = anonymizationCategories.TryGetValue(MaskingTypeHash, out var anon2)
                ? anon2.ToArray()
                : Array.Empty<string>();

            json = MaskFields(json, fieldsToAnonymize);

            json = HashFields(json, fieldsToHash);
            return json;
        }

        #region Private

        private static string MaskFields(string json, string[] fieldsToAnonymize)
        {
            foreach (var field in fieldsToAnonymize)
            {
                var pattern = $"\"{field}\":\\s*(\"([^\"]+)\"|([^,\"]+))";
                var replacement = $"\"{field}\": \"{Mask}\"";
                json = Regex.Replace(json, pattern, replacement);
            }

            return json;
        }

        private string HashFields(string json, string[] fieldsToHash)
        {
            foreach (var field in fieldsToHash)
            {
                var pattern = $"\"{field}\":\\s*(\"([^\"]+)\"|([^,\"]+))";
                var regex = new Regex(pattern);
                var matches = regex.Matches(json);

                foreach (Match match in matches)
                {
                    var valueToHash = match.Groups[1].Value;
                    var hashedValue = HashString(valueToHash);

                    json = json.Substring(0, match.Groups[1].Index) +
                           $"\"{field}\": \"{hashedValue}\"" +
                           json.Substring(match.Groups[1].Index + match.Groups[1].Length);
                }
            }

            return json;
        }

        private string HashString(string input)
        {
            using var sha256Hash = SHA256.Create();
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            var builder = new StringBuilder();
            foreach (var t in bytes)
            {
                builder.Append(t.ToString("x2"));
            }

            return builder.ToString();
        }

        #endregion
    }
}