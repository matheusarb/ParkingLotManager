using System.ComponentModel.DataAnnotations;

namespace ParkingLotManager.WebApi.Models.ValueObjects;

public class Address : ValueObject
{
    private Address()
    { }

    public Address(string street, string city, string zipCode)
    {
        Street = street;
        City = city;
        ZipCode = zipCode;
    }
        
    public string Street { get; private set; }
    public string City { get; private set; }
    public string ZipCode { get; private set; }
}
