using eCommerce.Core.DTO;
using eCommerce.Core.Entities;

namespace eCommerce.Core.RepositoryContracts;

public interface IUserRepository
{
    Task<IEnumerable<ApplicationUser?>> GetAllUsersAsync();
    Task<ApplicationUser?> GetUserByEmailAndPassword(string email, string password);
    Task<ApplicationUser?> AddUser(ApplicationUser user);
    Task<string> UpdateUser(ApplicationUser user);
    Task<string> DeleteUser(Guid UserId);
}
