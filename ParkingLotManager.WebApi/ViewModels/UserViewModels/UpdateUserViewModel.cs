using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ParkingLotManager.WebApi.ViewModels.UserViewModels;

public class UpdateUserViewModel
{
    [JsonPropertyName("Email")]
    [EmailAddress]
    public string? Email { get; set; }
    [JsonPropertyName("Name")]
    public string? Name { get; set; }
    [JsonPropertyName("PasswordHash")]
    public string? PasswordHash { get; set; }
}
