using Microsoft.AspNetCore.Mvc;
using StockAPI.Common.Result;
using StockAPI.Dtos.Product;
using StockAPI.Services.Product;

namespace StockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/<ProductController>
        [HttpGet]
        [ProducesResponseType<List<ProductDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponseDto>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllProductsAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
        {
            var result = await _productService.GetAllProductsAsync(page, pageSize, ct);

            return result.IsSuccess ? Ok(result) : this.ToErrorResult(result);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        [ProducesResponseType<ProductDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponseDto>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductById(long id, CancellationToken ct = default)
        {
            var result = await _productService.GetProductByIdAsync(id, ct);

            return result.IsSuccess ? Ok(result) : this.ToErrorResult(result);
        }

        // POST api/<ProductController>
        [HttpPost]
        [ProducesResponseType<ProductDto>(StatusCodes.Status201Created)]
        [ProducesResponseType<ApiErrorResponseDto>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponseDto>(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductDto dto, CancellationToken ct)
        {
            var result = await _productService.CreateProductAsync(dto, ct);

            return result.IsSuccess 
                ? CreatedAtAction(nameof(GetProductById), new {id = result.Data!.Id}, result.Data) 
                : this.ToErrorResult(result);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        [ProducesResponseType<ProductDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponseDto>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponseDto>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProductAsync(long id, [FromBody] UpdateProductDto dto, CancellationToken ct = default)
        {
            var result = await _productService.UpdateProductAsync(id, dto, ct);

            return result.IsSuccess ? Ok(result) : this.ToErrorResult(result);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProductByIdAsync(long id, CancellationToken ct = default)
        {
            var result = await _productService.DeleteProductAsync(id, ct);

            return result.IsSuccess ? NoContent() : this.ToErrorResult(result);
        }
    }
}
