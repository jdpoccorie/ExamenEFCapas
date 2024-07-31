using Infraestructure.Context;
using Domain.Models;
using APICodigoEFC.Response;
using APICodigoEFC.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Services.Services;

namespace APICodigoEFC.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DetailsController : ControllerBase
    {
        private readonly CodigoContext _context;
        private DetailsService _service;

        public DetailsController(CodigoContext context)
        {
            _context = context;
            _service = new DetailsService(_context);
        }

        [HttpPost]
        public void Insert([FromBody] Detail detail)
        {
            _service.Insert(detail);
        }

        [HttpGet]
        public List<Detail> Get()
        {
            var details = _service.Get();
            return details;
        }
        //Listar todos los detalles y buscar por nombre de cliente.
        [HttpGet]
        public List<Detail> GetByFilters(string? customerName, string? invoiceNumber)
        {
            var details = _service.GetByFilters(customerName, invoiceNumber);
            return details;
        }




        [HttpGet]
        public List<DetailResponseV1> GetByInvoiceNumber(string? invoiceNumber)
        {

            var details = _service.GetByInvoiceNumber(invoiceNumber);
            var response = details
                           .Select(x => new DetailResponseV1
                           {
                               InvoiceNumber = x.Invoice.Number,
                               ProductName = x.Product.Name,
                               SubTotal = x.SubTotal
                           }).ToList();
            return response;
        }

        [HttpGet]
        public List<DetailResponseV2> GetByInvoiceNumber2(string? invoiceNumber)
        {

            var details = _service.GetByInvoiceNumber(invoiceNumber);


            var response = details
                           .Select(x => new DetailResponseV2
                           {
                               InvoiceNumber = x.Invoice.Number,
                               ProductName = x.Product.Name,
                               Amount = x.Amount,
                               Price = x.Price,
                               IGV = x.Amount * x.Price * Constants.IGV
                           }).ToList();
            return response;
        }
    }
}
