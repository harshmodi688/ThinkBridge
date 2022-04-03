using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Shopbridge_base.Domain.Models
{
    [ExcludeFromCodeCoverage]
    public class Product
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal? Price { get; set; }
    }
}
