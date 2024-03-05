using Domain.Shared;

namespace Domain.Appointments.Entities;

public class Opinion : Entity
{
    public Opinion()
    {
    }

    public Opinion(Guid appointmentId, string title, string? opinionDescription)
    {
        AppointmentId = appointmentId;
        Title = title;
        OpinionDescription = opinionDescription;
    }


    public Guid AppointmentId { get; private set; }
    public string Title { get; private set; } = null!;
    public string? OpinionDescription { get; private set; }
}