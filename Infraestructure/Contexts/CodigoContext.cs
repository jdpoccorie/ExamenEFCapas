using Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Infraestructure.Context
{
    public class CodigoContext : DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Detail> Details { get; set; }



        public CodigoContext(DbContextOptions<CodigoContext> options) : base(options)
        {
        }
    }
}
