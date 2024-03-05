namespace Contracts.Auth;

public class UserInfo
{
    public UserInfo() { }
    public UserInfo(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; private set; }
    public required string Name { get; set; }
    public required string Token { get; set; }
    public void SetUserInformation(Guid userId, string name, string token)
    {
        Id = userId;
        Name = name;
        Token = token;
    }
}