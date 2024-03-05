#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
using Domain.Shared;

namespace Domain.AnamnesisForms.Entities;

public class AnamnesisFormNote : Entity
{
    public AnamnesisFormNote(Guid anamnesisFormId, Guid expertId, string title, string formNote)
    {
        AnamnesisFormId = anamnesisFormId;
        ExpertId = expertId;
        Title = title;
        FormNote = formNote;
    }

    public AnamnesisFormNote()
    {
    }

    public Guid AnamnesisFormId { get; private set; }
    public Guid ExpertId { get; private set; }
    public string Title { get; private set; }
    public string FormNote { get; private set; }
}