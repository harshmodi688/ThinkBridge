using System.Diagnostics.CodeAnalysis;

namespace Shopbridge_base.Domain.Models.DTO
{
    [ExcludeFromCodeCoverage]
    public class ApiError
    {
        public string Message { get; set; }
        public dynamic AdditionalInfo { get; set; }
    }
}