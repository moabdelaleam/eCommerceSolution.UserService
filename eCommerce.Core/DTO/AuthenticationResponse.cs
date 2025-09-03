namespace eCommerce.Core.DTO;

public record AuthenticationResponse(
    Guid UserId,
    string? Email,
    string? Personname,
    string? Gender,
    string? Token,
    bool Sucess
)
{
    // Parameterless Constructure 
    public AuthenticationResponse(): this(default, default, default, default, default, default) { }
}