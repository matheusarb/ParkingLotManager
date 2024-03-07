using Microsoft.EntityFrameworkCore;
using ParkingLotManager.WebApi.Data.Mappings;
using ParkingLotManager.WebApi.Models;

namespace ParkingLotManager.WebApi.Data;

public class AppDataContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ParkingLotManager;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CompanyMap());
        modelBuilder.ApplyConfiguration(new VehicleMap());
    }
}
