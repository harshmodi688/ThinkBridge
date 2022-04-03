using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Shopbridge_base.Domain.Models
{
    [ExcludeFromCodeCoverage]
    public class ProductDTO
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(15, ErrorMessage = "Description must be less than 150 characters.")]
        public string ProductName { get; set; }
        [MaxLength(1500, ErrorMessage = "Description must be less than 1500 characters.")]
        public string? ProductDescription { get; set; }
        [Range(0, double.MaxValue)]
        public decimal? Price { get; set; }
    }
}
