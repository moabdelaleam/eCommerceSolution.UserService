using AutoMapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Core.ServiceContracts;

namespace eCommerce.Core.Services;

internal class UsersService : IUsersService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UsersService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<AuthenticationResponse?> Login(LoginRequest loginRequest)
    {
        ApplicationUser? user = await _userRepository.GetUserByEmailAndPassword(loginRequest.Email, loginRequest.Password);
         
        if (user == null)
            return null;

        //return new AuthenticationResponse(user.UserID, user.Email, user.PersonName, user.Gender, "token", Sucess: true);
        return _mapper.Map<AuthenticationResponse>(user) with { Sucess = true, Token = "token" };
    }

    public async Task<AuthenticationResponse?> Register(RegisterRequest registerRequest)
    {
        ApplicationUser user = new ApplicationUser
        {
            Email = registerRequest.Email,
            Password = registerRequest.Password,
            PersonName = registerRequest.PersonName,
            Gender = registerRequest.Gender.ToString()
        };

        ApplicationUser? registeredUser = await _userRepository.AddUser(user);

        if(registeredUser == null) return null;

        //return new AuthenticationResponse(registeredUser.UserID, registeredUser.Email, registeredUser.PersonName, registeredUser.Gender, "token", Sucess: true);
        return _mapper.Map<AuthenticationResponse>(registeredUser) with { Sucess = true, Token = "token" };
    }

    public async Task<IEnumerable<AuthenticationResponse?>> GetAllUsers()
    { 
        IEnumerable<ApplicationUser?> users = await _userRepository.GetAllUsersAsync();
        if(users == null)
            return Enumerable.Empty<AuthenticationResponse>();

        return _mapper.Map<IEnumerable<AuthenticationResponse>>(users);
    }

    public async Task<string> UpdateUser(UpdateRequest user)
    {
        ApplicationUser appUser = new ApplicationUser
        {
            UserID = user.UserID,
            Email = user.Email,
            Password = user.Password,
            PersonName = user.PersonName,
            Gender = user.Gender.ToString()
        };

        return await _userRepository.UpdateUser(appUser);
    }

    public async Task<string> DeleteUser(Guid userId)
    {
        return await _userRepository.DeleteUser(userId);
    }
}
