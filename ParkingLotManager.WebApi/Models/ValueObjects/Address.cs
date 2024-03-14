using Microsoft.IdentityModel.Tokens;
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

    public Address Update(Address newAddress)
    {
        var address = new Address();
        address.Street = newAddress.Street.IsNullOrEmpty() ? this.Street : newAddress.Street;
        address.City = newAddress.City.IsNullOrEmpty() ? this.City : newAddress.City;
        address.ZipCode = newAddress.City.IsNullOrEmpty() ? this.ZipCode : newAddress.ZipCode;

        return address;
    }
}
