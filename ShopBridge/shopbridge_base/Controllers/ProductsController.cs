using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Models.DTO;
using Shopbridge_base.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopbridge_base.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILoggerService _loggerService;
        public ProductsController(IProductService productService
            , ILoggerService loggerService)
        {
            _productService = productService;
            _loggerService = loggerService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<IEnumerable<ProductDTO>>>> GetProduct()
        {
            //logging entry and exit action also log exception using filter
            //try
            //{
            ApiResponse<IEnumerable<ProductDTO>> products = await _productService.GetAllProducts();
            return Ok(products);
            //}
            //catch (Exception ex)
            //{
            //    await _loggerService.MessageLoggerAsync(nameof(GetProduct), $"Something went wrong: {ex.Message}");
            //    return StatusCode(500, "Internal server error");
            //}
        }


        [HttpGet("{id:long}", Name = "GetProductById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<ProductDTO>>> GetProductById(long id)
        {
            var apiResponse = await _productService.GetProductById(id);
            //(bool isExists, ProductDTO productDTO) =await ProductExists(id);
            return apiResponse.Data != null ? Ok(apiResponse) : NotFound();
        }


        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<ProductDTO>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<ProductDTO>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutProduct(long id, ProductDTO productDTO)
        {
            if (id != productDTO.Id)
            {
                return BadRequest("Id and productId does not match.");
            }
            var response = await _productService.UpdateProduct(id, productDTO);

            if (response.Validation.ValidationStatus.Equals(ValidationStatus.Error))
            {
                return NotFound(response);
            }

            return NoContent();
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductDTO>> PostProduct([FromBody] ProductDTO product, ApiVersion apiVersion)
        {
            if (product == null)
            {
                await _loggerService.MessageLoggerAsync(nameof(PostProduct), "product object sent from client is null.");
                return BadRequest("product object sent from client is null");
            }

            var productObj = await _productService.AddProduct(product);
            if (productObj.Data == null)
                return BadRequest(productObj);
            return CreatedAtRoute(nameof(GetProductById), new { id = productObj.Data.Id, version = apiVersion.ToString() }, productObj);
        }


        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            var response = await _productService.DeleteProduct(id);

            if (response.Validation.ValidationStatus.Equals(ValidationStatus.Error))
            {
                return NotFound(response);
            }
            return NoContent();
        }

        //private async Task<(bool, ProductDTO)> ProductExists(long id)
        //{
        //    ProductDTO productDTO = await _productService.GetProductById(id);
        //    bool isExists = productDTO != null ? true : false;
        //    return (isExists, productDTO);
        //}
    }
}
