using Domain.Appointments.Entities;
using Domain.Appointments.ValueObjects;
using Domain.Shared;

namespace Domain.Appointments;

public sealed class Appointment : Entity
{
    private readonly List<Opinion> _opinions = [];

    public Appointment()
    {
    }

    public Appointment(Guid expertId, Guid clientId, Guid addressId, AppointmentState appointmentState, DateTime time,
        DateTime sessionDuration, string? description)
    {
        ExpertId = expertId;
        ClientId = clientId;
        AddressId = addressId;
        AppointmentState = appointmentState;
        Time = time;
        SessionDuration = sessionDuration;
        Description = description;
    }

    public Guid ExpertId { get; private set; }
    public Guid ClientId { get; private set; }
    public Guid AddressId { get; private set; }
    public AppointmentState AppointmentState { get; private set; } = null!;
    public DateTime Time { get; private set; }
    public DateTime SessionDuration { get; private set; }
    public string? Description { get; private set; }
    public IReadOnlyCollection<Opinion> Opinions => _opinions.AsReadOnly();

    public void AddOpinion(Guid apponintmentId, string title, string opinionDescription)
    {
        Opinion opinion = new(apponintmentId, title, opinionDescription);
        _opinions.Add(opinion);
    }
}