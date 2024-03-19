using ParkingLotManager.WebApi.ViewModels.VehicleViewModels;

namespace ParkingLotManager.WebApi.Models.Contracts;

public interface IVehicle
{
    public void Create(RegisterVehicleViewModel viewModel);

    public void Update(UpdateVehicleViewModel viewModel);
}
