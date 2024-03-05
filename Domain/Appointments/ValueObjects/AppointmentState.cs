// ReSharper disable All
using Domain.Shared;

namespace Domain.Appointments.ValueObjects;

public class AppointmentState(string value) : ValueObject
{
    public static readonly AppointmentState Approved = new(Constants.Approved);
    public static readonly AppointmentState Postponed = new(Constants.Postponed);
    public static readonly AppointmentState Realized = new(Constants.Realized);
    public static readonly AppointmentState Canceled = new(Constants.Canceled);

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
        public const string Approved = "Onaylandı";
        public const string Postponed = "Ertelendi";
        public const string Realized = "Gerçekleştirildi";
        public const string Canceled = "İptal Edildi";
    }
}