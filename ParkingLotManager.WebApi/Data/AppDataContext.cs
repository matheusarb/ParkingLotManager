using Microsoft.EntityFrameworkCore;
using ParkingLotManager.WebApi.Models;

namespace ParkingLotManager.WebApi.Data;

public class AppDataContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("Server=localhost,1433;Database=ParkingLotManager;Username=sa;Password=1q2w3e4r@#$");
}
