using Rise.Service.OrderAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Rise.Services.OrderAPI.Models;

namespace Rise.Service.OrderAPI.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
       
    }
}
