using Infraestructure.Context;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Azure.Core;
using Azure;

namespace Services.Services
{
    public class CustomersService
    {
        private readonly CodigoContext _context;

        public CustomersService(CodigoContext context)
        {
            _context = context;
        }
        public List<Customer> GetByFilters(string? name, string? documentNumber)
        {
            IQueryable<Customer> query = _context.Customers.Where(x => x.IsActive);

            if (!string.IsNullOrEmpty(name))
                query = query.Where(x => x.Name.Contains(name));

            if (!string.IsNullOrEmpty(documentNumber))
                query = query.Where(x => x.DocumentNumber.Contains(documentNumber));

            return query.ToList();
        }
      
        public void Insert( Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void Update(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            _context.SaveChanges();
        }

    
        public void Delete(int id)
        {           
            var customer = _context.Customers.Find(id);
            customer.IsActive = false;
            _context.Entry(customer).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public int UpdateName(int id,string name)
        {
            int code = 0;

            Customer customer = _context.Customers.Find(id);
            if (customer == null)
            {
                code = -1001;
                //response.Message = Validations.ExistCustomer;
                return code;
            }
            customer.Name = name;
            _context.Entry(customer).State = EntityState.Modified;
            _context.SaveChanges();
            return code;

        }
    }
}
