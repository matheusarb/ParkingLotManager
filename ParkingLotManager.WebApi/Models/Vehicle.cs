using Microsoft.IdentityModel.Tokens;
using ParkingLotManager.WebApi.Enums;
using ParkingLotManager.WebApi.Models.Contracts;
using ParkingLotManager.WebApi.ViewModels.VehicleViewModels;

namespace ParkingLotManager.WebApi.Models;

public class Vehicle : IVehicle
{
    public string LicensePlate { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    public EVehicleType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdateDate { get; set; }

    public Company? Company { get; set; }
    public string CompanyName { get; set; }

    public virtual Vehicle Create(RegisterVehicleViewModel viewModel)
    {
        LicensePlate = viewModel.LicensePlate;
        Model = viewModel.Model;
        Brand = viewModel.Brand;
        Color = viewModel.Color;
        Type = viewModel.Type;
        CompanyName = viewModel.CompanyName;
        
        return this;
    }
    
    public virtual Vehicle Update(UpdateVehicleViewModel viewModel)
    {
        LicensePlate = viewModel.LicensePlate.IsNullOrEmpty() ? LicensePlate : viewModel.LicensePlate;
        Brand = viewModel.Brand.IsNullOrEmpty() ? Brand : viewModel.Brand;
        Model = viewModel.Model.IsNullOrEmpty() ? Model : viewModel.Model;
        Color = viewModel.Color.IsNullOrEmpty() ? Color : viewModel.Color;
        Type = viewModel.Type != Type ? viewModel.Type : Type;
        CompanyName = viewModel.CompanyName.IsNullOrEmpty() ? CompanyName : viewModel.CompanyName;

        return this;
    }

    static void ChangeLicensePlate(string licensePlate, Vehicle vehicle)
    {
        if (licensePlate == null)
            return;
        vehicle.LicensePlate = licensePlate;
    }
}
