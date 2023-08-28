using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practice.ApiKeyAuthentication.Models;

namespace Practice.ApiKeyAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly List<Product> _products = new()
        {
            new Product
            {
                Id = 1,
                Category = "Electronic",
                Brand = "Sony",
                Name = "Play Station",
                WarrantyYears = 2,
                IsAvailable = true
            },
            new Product
            {
                Id = 7,
                Category = "Electronic",
                Brand = "Apple",
                Name = "Mobile",
                WarrantyYears = 2,
                IsAvailable = true
            }
        };

        private readonly IMapper _mapper;

        public ProductController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetProductsByCategoryAndBrand(string category, string brand)
        {
            List<Product> result = _products
                .Where(x => x.Category == category && x.Brand == brand)
                .ToList();

            return Ok(_mapper.Map<List<ProductDto>>(result));
        }

        [HttpGet("type-manufacturer")]
        public IActionResult GetProductsByCategoryAndBrandUsingFromQuery(
            [FromQuery(Name = "type")] string category,
            [FromQuery(Name = "manufacturer")] string brand)
        {
            List<Product> result = _products
                .Where(x => x.Category == category && x.Brand == brand)
                .ToList();

            return Ok(_mapper.Map<List<ProductDto>>(result));
        }
    }
}
