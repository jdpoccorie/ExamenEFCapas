using Domain.Models;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class InvoicesService
    {
        private readonly CodigoContext _context;

        public InvoicesService(CodigoContext context)
        {
            _context = context;
        }

        public List<Invoice> GetByFilters(string? number)
        {
            IQueryable<Invoice> query = _context.Invoices.Include(x => x.Customer).Where(x => x.IsActive);
            if (!string.IsNullOrEmpty(number))
                query = query.Where(x => x.Number.Contains(number));
            return query.ToList();
        }

        public void Insert(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            _context.SaveChanges();
        }


    }
}
