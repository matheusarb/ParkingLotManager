using ParkingLotManager.WebApi.ViewModels.UserViewModels;
using SecureIdentity.Password;

namespace ParkingLotManager.WebApi.Models;

public class User
{
    public int Id { get; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }    
    public string Slug { get; private set; }

    public Company? Company { get; private set; }
    public string CompanyName { get; private set; }

    public IList<Role> Roles { get; set; }

    public void Create(CreateUserViewModel viewModel, string password)
    {
        Name = viewModel.Name;
        Email = viewModel.Email;
        PasswordHash = PasswordHasher.Hash(password);

        Slug = viewModel.Email.Replace("@", "-").Replace(".", "-");
        CompanyName = viewModel.CompanyName;
    }

    public void Update(UpdateUserViewModel viewModel, User user)
    {
        throw new NotImplementedException();
    }


}