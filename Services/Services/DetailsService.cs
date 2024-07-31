using Domain.Models;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class DetailsService
    {
        private readonly CodigoContext _context;

        public DetailsService(CodigoContext context)
        {
            _context = context;
        }

        public void Insert(Detail detail)
        {
            _context.Details.Add(detail);
            _context.SaveChanges();
        }

        public List<Detail> Get()
        {
            IQueryable<Detail> query = _context.Details
                .Include(x => x.Product)
                .Include(x => x.Invoice).ThenInclude(y => y.Customer)
                .Where(x => x.IsActive);


            return query.ToList();
        }

        //Listar todos los detalles y buscar por nombre de cliente.
        public List<Detail> GetByFilters(string? customerName, string? invoiceNumber)
        {
            IQueryable<Detail> query = _context.Details
               .Include(x => x.Product)
               .Include(x => x.Invoice).ThenInclude(y => y.Customer)
               .Where(x => x.IsActive);

            if (!string.IsNullOrEmpty(customerName))
                query = query.Where(x => x.Invoice.Customer.Name.Contains(customerName));
            if (!string.IsNullOrEmpty(invoiceNumber))
                query = query.Where(x => x.Invoice.Number.Contains(invoiceNumber));


            return query.ToList();
        }


        //public List<DetailResponseV1> GetByInvoiceNumber(string? invoiceNumber)
        public List<Detail> GetByInvoiceNumber(string? invoiceNumber)
        {

            IQueryable<Detail> query = _context.Details
                .Include(x => x.Product)
                .Include(x => x.Invoice)
                .Where(x => x.IsActive);
            if (!string.IsNullOrEmpty(invoiceNumber))
                query = query.Where(x => x.Invoice.Number.Contains(invoiceNumber));

            //Todos los detalles del modelo
            var details = query.ToList();


            //Convertir modelo al response
            /*var response = details
                           .Select(x => new Detail
                           {
                               InvoiceNumber = x.Invoice.Number,
                               ProductName = x.Product.Name,
                               SubTotal = x.SubTotal
                           }).ToList();*/

            return details;
        }

        public List<Detail> GetByInvoiceNumber2(string? invoiceNumber)
        {

            IQueryable<Detail> query = _context.Details
                .Include(x => x.Product)
                .Include(x => x.Invoice)
                .Where(x => x.IsActive);
            if (!string.IsNullOrEmpty(invoiceNumber))
                query = query.Where(x => x.Invoice.Number.Contains(invoiceNumber));

            //Todos los detalles del modelo
            var details = query.ToList();


            //Convertir modelo al response
            /*var response = details
                           .Select(x => new DetailResponseV2
                           {
                               InvoiceNumber = x.Invoice.Number,
                               ProductName = x.Product.Name,
                               Amount = x.Amount,
                               Price = x.Price,
                               IGV = x.Amount * x.Price * Constants.IGV
                           }).ToList();*/

            return details;
        }

    }
}
