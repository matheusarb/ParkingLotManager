using ParkingLotManager.WebApi.ViewModels.UserViewModels;

namespace ParkingLotManager.WebApi.Models.Contracts;

public interface IUser
{
    public void Create(CreateUserViewModel viewModel, string password);

    public void Update(UpdateUserViewModel viewModel);
}
