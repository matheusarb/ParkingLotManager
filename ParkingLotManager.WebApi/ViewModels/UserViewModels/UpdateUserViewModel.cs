namespace ParkingLotManager.WebApi.ViewModels.UserViewModels;

public class UpdateUserViewModel
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
}
