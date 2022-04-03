using Shopbridge_base.Core.Interfaces;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Models.DTO;
using Shopbridge_base.Domain.Modules.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Modules.Implementation
{

    /// <summary>
    /// Class will have methods related to pulling and pushing data from product domain(joins on tables related to product will come here)
    /// </summary>
    public class ProductModule : IProductModule
    {
        private IEFService<Product> _efProduct;
        public ProductModule(IEFService<Product> efProduct)
        {
            _efProduct = efProduct;
        }

        public async Task<ApiResponse<Product>> AddAsync(Product product)
        {
            return await _efProduct.AddAsync(product);
        }

        public IQueryable<Product> GetAllProducts()
        {
            return _efProduct.GetAllAsync();
        }

        public async Task<ApiResponse<Product>> GetProductById(long id)
        {
            ApiResponse<Product> product = new ApiResponse<Product>();
            product.Data = await _efProduct.GetByIdAsync(id);
            return product;
        }

        public async Task<ApiResponse<Product>> UpdateAsync(Product product)
        {
            return await _efProduct.UpdateAsync(product);
        }

        public async Task<ApiResponse<bool>> DeleteAsync(Product product)
        {
            return await _efProduct.DeleteAsync(product);
        }
    }
}
