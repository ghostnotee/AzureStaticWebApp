using System.Text;
using Domain.Shared;

namespace Domain.Users.Entities;

public class Address : Entity
{
    public Address()
    {
    }

    public Address(Guid clientId, string addressTitle, string streetAddress, string district, string province,
        string city, string country, string zipCode, double latitude, double longitude)
    {
        ClientId = clientId;
        AddressTitle = addressTitle;
        StreetAddress = streetAddress;
        District = district;
        Province = province;
        City = city;
        Country = country;
        ZipCode = zipCode;
        Latitude = latitude;
        Longitude = longitude;
    }

    public Guid? ClientId { get; private set; }
    public string AddressTitle { get; private set; } = null!;
    public string StreetAddress { get; private set;} = null!;
    public string District { get; private set;} = null!;
    public string Province { get; private set;} = null!;
    public string City { get; private set;} = null!;
    public string Country { get; private set;} = null!;
    public string ZipCode { get; private set;} = null!;
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }

    public override string ToString()
    {
        var sb = new StringBuilder(StreetAddress);
        if (!string.IsNullOrWhiteSpace(District)) sb.Append(", ").Append(District);
        if (!string.IsNullOrWhiteSpace(Province)) sb.Append(", ").Append(Province);
        if (!string.IsNullOrWhiteSpace(City)) sb.Append(", ").Append(City);
        if (!string.IsNullOrWhiteSpace(Country)) sb.Append(", ").Append(Country);
        if (!string.IsNullOrWhiteSpace(ZipCode)) sb.Append(' ').Append(ZipCode);

        return sb.ToString();
    }
}