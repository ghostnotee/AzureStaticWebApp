using Domain.Shared;

namespace Domain.Users;

public static class UserErrors
{
    public static readonly Error Failure = Error.NotFound("Users.Failure", "Verilen email yada parola hatalı.");
    public static Error NotFoundByEmail(string email) => Error.NotFound("Users.NotFoundByEmail", $"Sağlanan email '{email}' ile kullanıcı bulunamadı.");
    public static readonly Error EmailNotUnique = Error.Conflict("Users.EmailNotUnique", "Verilen email sistemde kayıtlı.");
}