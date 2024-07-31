using Infraestructure.Context;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Services;

namespace APICodigoEFC.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {

        private readonly CodigoContext _context;
        private InvoicesService _service;

        public InvoicesController(CodigoContext context)
        {
            _context = context;
            _service = new InvoicesService(_context);
        }

        [HttpGet]
        public List<Invoice> GetByFilters(string? number)
        {
            var invoices = _service.GetByFilters(number);
            return invoices;
        }

        [HttpPost]
        public void Insert([FromBody] Invoice invoice)
        {
            _service.Insert(invoice);
        }

    }
}
