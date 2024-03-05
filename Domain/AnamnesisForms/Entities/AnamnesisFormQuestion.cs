using Domain.Shared;

namespace Domain.AnamnesisForms.Entities;

public class AnamnesisFormQuestion : Entity
{
    public AnamnesisFormQuestion(Guid anamnesisFormId, string question, string? answer)
    {
        AnamnesisFormId = anamnesisFormId;
        Question = question;
        Answer = answer;
    }

    public AnamnesisFormQuestion()
    {
    }

    public Guid AnamnesisFormId { get; private set; }
    public string Question { get; private set; } = null!;
    public string? Answer { get; private set; }
}