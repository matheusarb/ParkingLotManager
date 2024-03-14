namespace ParkingLotManager.WebApi.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Slug { get; set; }

    public Company? Company { get; private set; }
    public string CompanyName { get; private set; }
}