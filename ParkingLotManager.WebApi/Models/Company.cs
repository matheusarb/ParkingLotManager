using ParkingLotManager.WebApi.Models.ValueObjects;

namespace ParkingLotManager.WebApi.Models;

public class Company
{
    private Company() {}

    public Company(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }
    public Cnpj Cnpj { get; private set; }
    public Address Address { get; private set; }
    public string Telephone { get; private set; }
    
    public ICollection<Vehicle> Vehicles { get; private set; }
}
