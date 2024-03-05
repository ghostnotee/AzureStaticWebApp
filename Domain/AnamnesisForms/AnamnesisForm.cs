// ReSharper disable CollectionNeverUpdated.Local
using Domain.AnamnesisForms.Entities;
using Domain.Shared;

namespace Domain.AnamnesisForms;

public class AnamnesisForm : Entity
{
    private readonly List<AnamnesisFormQuestion> _anamnesisFormQuestions = [];
    private readonly List<AnamnesisFormNote> _notes = [];

    public AnamnesisForm()
    {
    }

    public AnamnesisForm(string title, string? description, Guid? clientId, bool isFormFilled)
    {
        Title = title;
        Description = description;
        ClientId = clientId;
        IsFormFilled = isFormFilled;
    }

    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public Guid? ClientId { get; private set; }
    public bool IsFormFilled { get; private set; }
    public IReadOnlyCollection<AnamnesisFormQuestion> Questions => _anamnesisFormQuestions.AsReadOnly();
    public IReadOnlyCollection<AnamnesisFormNote> Notes => _notes.AsReadOnly();
}