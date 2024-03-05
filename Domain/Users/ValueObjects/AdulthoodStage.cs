using Domain.Shared;

namespace Domain.Users.ValueObjects;

public class AdulthoodStage(string value) : ValueObject
{
    public static readonly AdulthoodStage Active = new(Constants.Adult);
    public static readonly AdulthoodStage NotExist = new(Constants.Adolescence);
    public static readonly AdulthoodStage NotActive = new(Constants.Child);

    private string Value { get; } = value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value;
    }

    private static class Constants
    {
        public const string Adult = "Yetişkin";
        public const string Adolescence = "Ergenlik Dönemi";
        public const string Child = "Çocuk";
    }
}