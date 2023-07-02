using Rise.Services.Identity.Models;

namespace Rise.Services.Identity.MainModule.User
{
	public interface IUserRepository
	{
		Task<IEnumerable<ApplicationUser>> GetAllUsers();
		Task<ApplicationUser> GetUserById(string id);
		bool Add(ApplicationUser user);
		bool Update(ApplicationUser user);
		bool Delete(ApplicationUser user);
		bool Save();
	}
}
