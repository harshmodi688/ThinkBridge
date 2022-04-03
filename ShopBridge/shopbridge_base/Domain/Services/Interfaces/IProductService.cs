using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Services.Interfaces
{
    public interface IProductService
    {
        //IAsyncEnumerable<Product> GetAllProducts();

        Task<ApiResponse<IEnumerable<ProductDTO>>> GetAllProducts();
        Task<ApiResponse<ProductDTO>> GetProductById(long id);
        Task<ApiResponse<ProductDTO>> AddProduct(ProductDTO productDTO);
        Task<ApiResponse<ProductDTO>> UpdateProduct(long id, ProductDTO productDTO);
        Task<ApiResponse<bool>> DeleteProduct(long id);
    }
}
