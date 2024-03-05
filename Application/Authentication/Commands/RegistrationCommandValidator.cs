using System.Text.RegularExpressions;
using FluentValidation;

namespace Application.Authentication.Commands;

public class RegistrationCommandValidator : AbstractValidator<RegistrationCommand>
{
    public RegistrationCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("İsim alanı boş geçilemez").NotNull().WithMessage("İsim alanı boş geçilemez");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyisim alanı boş geçilemez").NotNull().WithMessage("Soyisim alanı boş geçilemez");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Parola alanı boş geçilemez.")
            .Must(IsPasswordValid).WithMessage("Parolanız en az sekiz karakter, en az bir harf ve bir sayı içermelidir.");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Geçerli bir mail adresi giriniz.");
    }

    private bool IsPasswordValid(string arg)
    {
        var regex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d!@#$%^&*()_+{}:;<>,.?~[\]|\-/\\]{8,}$");
        return regex.IsMatch(arg);
    }
}