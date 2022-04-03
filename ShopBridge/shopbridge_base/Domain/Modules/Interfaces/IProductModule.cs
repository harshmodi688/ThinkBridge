using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Models.DTO;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Modules.Interfaces
{
    public interface IProductModule
    {
        IQueryable<Product> GetAllProducts();
        Task<ApiResponse<Product>> GetProductById(long id);
        Task<ApiResponse<Product>> AddAsync(Product product);
        Task<ApiResponse<Product>> UpdateAsync(Product product);
        Task<ApiResponse<bool>> DeleteAsync(Product product);
    }
}
