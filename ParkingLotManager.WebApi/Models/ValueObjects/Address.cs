using System.ComponentModel.DataAnnotations;

namespace ParkingLotManager.WebApi.Models.ValueObjects;

public class Address : ValueObject
{
    public Address(string street, string city, string zipCode)
    {
        if (string.IsNullOrEmpty(street))
            throw new Exception("Type Street name");
        
        if (string.IsNullOrEmpty(city))
            throw new Exception("Type City name");
        
        if (string.IsNullOrEmpty(zipCode))
            throw new Exception("Type Zip Code");

        Street = street;
        City = city;
        ZipCode = zipCode;
    }
        
    public string Street { get; private set; }
    public string City { get; private set; }
    public string ZipCode { get; private set; }
}
