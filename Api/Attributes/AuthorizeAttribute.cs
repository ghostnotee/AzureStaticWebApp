namespace Api.Attributes;

public class AuthorizeAttribute: Attribute
{
    public string Role { get; set; } = "All";
    public AuthorizeAttribute(string role) => Role = role;
    public AuthorizeAttribute() { } 
}