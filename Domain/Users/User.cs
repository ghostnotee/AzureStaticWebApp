using Domain.Users.Entities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Users;

public sealed class User : IdentityUser<Guid>
{
    public User()
    {
    }

    public User(string firstName, string? middleName, string lastName, string email)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Email = email;
        UserName = email;
    }

    public string FirstName { get; private set; } = null!;
    public string? MiddleName { get; private set; }
    public string LastName { get; private set; } = null!;
    public string? RefreshToken { get; private set; }
    public DateTime? RefreshTokenExpiry { get; private set; }
    private readonly List<Client> _clients = [];
    public IEnumerable<Client> Clients => _clients.AsReadOnly();

    public void SetRefreshToken(string refreshToken)
    {
        RefreshToken = refreshToken;
        RefreshTokenExpiry = DateTime.UtcNow.AddHours(8);
    }

    public void RevokeRefreshToken()
    {
        RefreshToken = null;
    }
}