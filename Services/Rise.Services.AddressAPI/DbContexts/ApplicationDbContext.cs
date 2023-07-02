using Microsoft.EntityFrameworkCore;
using Rise.Services.AddressAPI.Models;

namespace Rise.Services.AddressAPI.DbContexts
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		public DbSet<Address> Address { get; set; }
		public DbSet<City> City { get; set; }
	}
}
