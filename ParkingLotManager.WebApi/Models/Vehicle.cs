using Microsoft.IdentityModel.Tokens;
using ParkingLotManager.WebApi.Enums;
using ParkingLotManager.WebApi.ViewModels.VehicleViewModels;

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

    public Company? Company { get; private set; }
    public string CompanyName { get; private set; }

    public void Create(RegisterVehicleViewModel viewModel)
    {
        LicensePlate = viewModel.LicensePlate;
        Model = viewModel.Model;
        Brand = viewModel.Brand;
        Color = viewModel.Color;
        Type = viewModel.Type;
        CompanyName = viewModel.CompanyName;
    }
    
    public void Update(UpdateVehicleViewModel viewModel)
    {
        LicensePlate = viewModel.LicensePlate.IsNullOrEmpty() ? LicensePlate : viewModel.LicensePlate;
        Brand = viewModel.Brand.IsNullOrEmpty() ? Brand : viewModel.Brand;
        Model = viewModel.Model.IsNullOrEmpty() ? Model : viewModel.Model;
        Color = viewModel.Color.IsNullOrEmpty() ? Color : viewModel.Color;
        Type = viewModel.Type != Type ? viewModel.Type : Type;
        CompanyName = viewModel.CompanyName.IsNullOrEmpty() ? CompanyName : viewModel.CompanyName;
    }

    static void ChangeLicensePlate(string licensePlate, Vehicle vehicle)
    {
        if (licensePlate == null)
            return;
        vehicle.LicensePlate = licensePlate;
    }
}
