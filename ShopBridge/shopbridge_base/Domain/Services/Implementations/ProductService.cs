using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Models.DTO;
using Shopbridge_base.Domain.Modules.Interfaces;
using Shopbridge_base.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly ILoggerService _loggerService;
        private readonly IProductModule _productModule;
        private readonly IMapper _mapper;

        public ProductService(IProductModule productModule
            , ILoggerService loggerService
            , IMapper mapper)
        {
            _productModule = productModule;
            _loggerService = loggerService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<ProductDTO>> AddProduct(ProductDTO productDTO)
        {
            ApiResponse<ProductDTO> apiResponse = new ApiResponse<ProductDTO>();
            ProductDTO createdProduct = new ProductDTO();
            var productEntity = _mapper.Map<Product>(productDTO);
            var res = await _productModule.GetProductById(productDTO.Id);
            var product = res.Data;
            if (product == null)
            {
                var response = await _productModule.AddAsync(productEntity);
                createdProduct = _mapper.Map<ProductDTO>(response.Data);
                apiResponse.Data = createdProduct;
            }
            else
            {
                string message = $"Product with productid: {productDTO.Id} already exists.";
                apiResponse = CreateFailedResponse<ProductDTO>(message);
            }
            return apiResponse;
        }

        private ApiResponse<T> CreateFailedResponse<T>(string message)
        {
            ApiResponse<T> apiResponse = new ApiResponse<T>();
            apiResponse.Validation.ValidationStatus = ValidationStatus.Error;
            apiResponse.Validation.Messages.Add(message);
            return apiResponse;
        }

        public async Task<ApiResponse<bool>> DeleteProduct(long id)
        {
            ApiResponse<bool> apiResponse;
            var response = await _productModule.GetProductById(id);
            var product = response.Data;
            if (product != null)
            {
                apiResponse = await _productModule.DeleteAsync(product!);
            }
            else
            {
                string message = $"No product available with id : {id}";
                apiResponse = CreateFailedResponse<bool>(message);
            }
            return apiResponse;
        }

        //public async IAsyncEnumerable<Product> GetAllProducts()
        //{
        //    var products = _productModule.GetAllProducts().AsAsyncEnumerable<Product>();
        //    await foreach (var item in products)
        //    {
        //        yield return item;
        //    }
        //}

        public async Task<ApiResponse<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            var products = await _productModule.GetAllProducts().ToListAsync();
            List<ProductDTO> productDTOs = products.Count > 0 ? _mapper.Map<List<ProductDTO>>(products) : new List<ProductDTO>();
            ApiResponse<IEnumerable<ProductDTO>> response = new ApiResponse<IEnumerable<ProductDTO>>();
            response.Data = productDTOs;
            return response;
        }
        public async Task<ApiResponse<ProductDTO>> GetProductById(long id)
        {
            ApiResponse<ProductDTO> productDtO = new ApiResponse<ProductDTO>();
            var response = await _productModule.GetProductById(id);

            if (response.Data != null)
                productDtO.Data = _mapper.Map<ProductDTO>(response.Data);
            else
                productDtO.Data = null!;
            return productDtO;
        }
        public async Task<ApiResponse<ProductDTO>> UpdateProduct(long id, ProductDTO productDTO)
        {
            ApiResponse<ProductDTO> apiResponse = new ApiResponse<ProductDTO>();
            var response = await _productModule.GetProductById(id);
            var product = response.Data;
            if (product != null)
            {
                product.ProductName = productDTO.ProductName;
                product.Price = productDTO.Price;
                product.ProductDescription = productDTO.ProductDescription;
                ApiResponse<Product> res = await _productModule.UpdateAsync(product!);
                apiResponse.Data = _mapper.Map<ProductDTO>(res.Data);
            }
            else
            {
                string message = $"No product available with id : {id}";
                apiResponse = CreateFailedResponse<ProductDTO>(message);
            }
            return apiResponse;
        }
    }
}
