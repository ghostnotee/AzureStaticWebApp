// ReSharper disable All

using Domain.Appointments;
using Domain.Shared;
using Domain.Users.ValueObjects;

namespace Domain.Users.Entities;

public class Client : Entity
{
    private readonly List<Address> _addresses = [];
    private readonly List<Appointment> _appointments = [];

    public Client()
    {
    }

    public Client(Guid? expertId, Guid? anamnesisFormId, string firstName, string? middleName, string lastName, string? parentName,
        string? parentMiddleName, string? parentLastName, string? identityNumber, DateTime dateOfBirth, string? phoneNumber,
        AdulthoodStage? adulthoodStage)
    {
        ExpertId = expertId;
        AnamnesisFormId = anamnesisFormId;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        ParentName = parentName;
        ParentMiddleName = parentMiddleName;
        ParentLastName = parentLastName;
        IdentityNumber = identityNumber;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber;
        AdulthoodStage = adulthoodStage;
    }

    public Guid? ExpertId { get; private set; }
    public Guid? ParentId { get; private set; }
    public Guid? AnamnesisFormId { get; private set; }
    public string FirstName { get; private set; } = null!;
    public string? MiddleName { get; private set; }
    public string LastName { get; private set; } = null!;
    public string? ParentName { get; private set; }
    public string? ParentMiddleName { get; private set; }
    public string? ParentLastName { get; private set; }
    public string? IdentityNumber { get; private set; }
    public DateTime? DateOfBirth { get; private set; }
    public string? PhoneNumber { get; private set; }
    public AdulthoodStage? AdulthoodStage { get; private set; }
    public IEnumerable<Address> Addreses => _addresses.AsReadOnly();
    public IEnumerable<Appointment> Appointments => _appointments.AsReadOnly();
}