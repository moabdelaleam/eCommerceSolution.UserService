using eCommerce.Core.DTO;
using eCommerce.Core.Entities;

namespace eCommerce.Core.ServiceContracts;

public interface IUsersService
{
    Task<AuthenticationResponse?> Login(LoginRequest loginRequest);
    Task<AuthenticationResponse?> Register(RegisterRequest registerRequest);
    Task<IEnumerable<AuthenticationResponse?>> GetAllUsers();
    Task<string> UpdateUser(UpdateRequest user);
    Task<string> DeleteUser(Guid userId);
}
