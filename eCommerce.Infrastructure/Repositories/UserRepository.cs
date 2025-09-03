using Dapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Infrastructure.DbContext;

namespace eCommerce.Infrastructure.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly DapperDbContext _dbContext;

    public UserRepository(DapperDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApplicationUser?> AddUser(ApplicationUser user)
    {
        // Generate a new unique user ID for the user 
        user.UserID = Guid.NewGuid();

        string query = "INSERT INTO public.\"Users\"" +
            "(\"UserID\", \"Email\", \" \", \"PersonName\", \"Gender\") " +
            "VALUES(@UserId, @Email, @Password, @Personname, @Gender)";

        int rowCountAffected = await _dbContext.DbConnection.ExecuteAsync(query, user);
        return rowCountAffected > 0 ? user : null;
    }

    public async Task<ApplicationUser?> GetUserByEmailAndPassword(string email, string password)
    {
        // SQL query to select a user by Email and Password 3
        string query = "SELECT * FROM public.\"Users\" " +
            "WHERE \"Email\"=@Email AND \"Password\"=@Password";

        var parameters = new { Email = email, Password = password };

        ApplicationUser? user = await _dbContext.DbConnection.QueryFirstOrDefaultAsync<ApplicationUser>(query, parameters);
        return user;
    }

    public async Task<IEnumerable<ApplicationUser?>> GetAllUsersAsync()
    {
        string query = "SELECT * FROM public.\"Users\"";

        var users = await _dbContext.DbConnection.QueryAsync<ApplicationUser>(query);

        return users.ToList().Count > 0 ? users : null;
    }

    public async Task<string> UpdateUser(ApplicationUser user)
    {
        var parameters = new { UserID = user.UserID };
        var dbUser = await _dbContext.DbConnection
            .QueryFirstOrDefaultAsync("SELECT * FROM public.\"Users\" " +
            "WHERE \"UserID\"=@UserID", parameters);

        if (dbUser == null)
            return "Usre not found!";

        var result = await _dbContext.DbConnection.ExecuteAsync("UPDATE public.\"Users\" " +
            "SET \"Email\"=@Email, \"Password\"=@Password, \"PersonName\"=@PersonName, \"Gender\"=@Gender " +
            "WHERE \"UserID\"=@UserID", user);

        return result > 0 ? "Updated Successfully!" : "Something went wrong!";
    }

    public async Task<string> DeleteUser(Guid userId)
    {
        var parameters = new { UserID = userId };
        var user = await _dbContext.DbConnection.QueryFirstOrDefaultAsync("SELECT * FROM public.\"Users\" " +
            "WHERE \"UserID\"=@UserID", parameters);

        if (user == null)
            return "User not exists!";

        var result = await _dbContext.DbConnection.ExecuteAsync("DELETE FROM public.\"Users\" " +
            "WHERE \"UserID\"=@UserID", parameters);

        return result > 0 ? "Deleted Successfully!" : "Something went wrong!";
    }
}