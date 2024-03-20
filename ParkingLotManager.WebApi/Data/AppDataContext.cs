using Microsoft.EntityFrameworkCore;
using ParkingLotManager.WebApi.Data.Mappings;
using ParkingLotManager.WebApi.Models;
using System.Diagnostics;

namespace ParkingLotManager.WebApi.Data;

public class AppDataContext : DbContext
{
    protected AppDataContext()
    {        
    }

    public AppDataContext(DbContextOptions options)
        : base(options)
    {        
    }

    public virtual DbSet<Company> Companies { get; set; }
    public virtual DbSet<Vehicle> Vehicles { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CompanyMap());
        modelBuilder.ApplyConfiguration(new VehicleMap());
        modelBuilder.ApplyConfiguration(new UserMap());
    }
}