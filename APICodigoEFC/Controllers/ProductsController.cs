using Infraestructure.Context;
using Domain.Models;
using APICodigoEFC.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Services;

namespace APICodigoEFC.Controllers
{
    [Route("api/[controller]/[action]")]
    //[Authorize]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly CodigoContext _context;
        private ProductsService _service;

        public ProductsController(CodigoContext context)
        {
            _context = context;
            _service = new ProductsService(_context);
        }

        [HttpGet]
        [AllowAnonymous]
        public List<Product> GetByFilters(string? name)
        {
           var products= _service.GetByFilters(name);
            return products;
        }

        [HttpPost]
        public void Insert([FromBody] ProductInsertRequest request)
        {
           
            Product product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            _service.Insert(product);
           
        }

        [HttpPut]
        public void Update([FromBody] Product Product)
        {
            _service.Update(Product);
        }

        [HttpPut]
        public void UpdatePrice([FromBody] ProductUpdateRequest request)
        {
            _service.UpdatePrice(request.Id, request.Price);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
