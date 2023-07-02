using Microsoft.EntityFrameworkCore;
using Rise.Services.Identity.DbContexts;
using Rise.Services.Identity.Models;

namespace Rise.Services.Identity.MainModule.User
{
	public class UserRepository : IUserRepository
	{
		private readonly ApplicationDbContext _context;

		public UserRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public bool Add(ApplicationUser user)
		{
			throw new NotImplementedException();
		}

		public bool Delete(ApplicationUser user)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
		{
			return await _context.Users.ToListAsync();
		}

		public async Task<ApplicationUser> GetUserById(string id)
		{
			return await _context.Users.Where(x=>x.Id == id).FirstOrDefaultAsync();
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool Update(ApplicationUser user)
		{
			_context.Update(user);
			return Save();
		}
	}
}
