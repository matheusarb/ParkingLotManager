using ParkingLotManager.WebApi.Models.ValueObjects;

namespace ParkingLotManager.WebApi.Models;

public class Company
{
    public Company(int id,
        string name,
        int cnpj,
        Address address,
        string telephone,
        ParkingSlots motorcycleParkingSlot,
        ParkingSlots carParkingSlot)
    {
        Id = id;
        Name = name;
        CNPJ = cnpj;
        Address = address;
        Telephone = telephone;
        MotorcycleParkingSlots = motorcycleParkingSlot;
        CarParkingSlots = carParkingSlot;
    }

    public int Id { get; }
    public string Name { get; private set; }
    public int CNPJ { get; private set; }
    public Address Address { get; private set; }
    public string Telephone { get; private set; }
    public ParkingSlots MotorcycleParkingSlots { get; private set; }
    public ParkingSlots CarParkingSlots { get; private set; }
}
