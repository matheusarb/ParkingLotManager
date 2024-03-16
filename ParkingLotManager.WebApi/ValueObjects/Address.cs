using Microsoft.IdentityModel.Tokens;

namespace ParkingLotManager.WebApi.ValueObjects;

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

    public Address Update(Address newAddress)
    {
        var address = new Address();
        address.Street = newAddress.Street.IsNullOrEmpty() ? Street : newAddress.Street;
        address.City = newAddress.City.IsNullOrEmpty() ? City : newAddress.City;
        address.ZipCode = newAddress.City.IsNullOrEmpty() ? ZipCode : newAddress.ZipCode;

        return address;
    }
}
