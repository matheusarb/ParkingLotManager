using ParkingLotManager.WebApi.Enums;

namespace ParkingLotManager.WebApi.Models;

public class Vehicle
{
    public string LicensePlate { get; private set; }
    public string Brand { get; private set; }
    public string Model { get; private set; }
    public string Color { get; private set; }
    public EVehicleType Type { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime LastUpdateDate { get; private set; }

    public Company Company { get; private set; }
}
