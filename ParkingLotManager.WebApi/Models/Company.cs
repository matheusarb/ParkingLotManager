using ParkingLotManager.WebApi.Models.ValueObjects;

namespace ParkingLotManager.WebApi.Models;

public class Company
{
    public int Id { get; }
    public string Name { get; private set; }
    public Cnpj Cnpj { get; private set; }
    public Address Address { get; private set; }
    public string Telephone { get; private set; }
    public ParkingSlots MotorcycleParkingSlots { get; private set; }
    public ParkingSlots CarParkingSlots { get; private set; }
    public IList<Vehicle> Vehicles { get; private set; }

}
