using System.ComponentModel.DataAnnotations;

namespace ParkingLotManager.WebApi.ViewModels.UserViewModels;

public class CreateUserViewModel
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Company name is required")]
    public string CompanyName { get; set; }
}