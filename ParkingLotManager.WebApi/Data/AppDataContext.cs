﻿using Microsoft.EntityFrameworkCore;
using ParkingLotManager.WebApi.Data.Mappings;
using ParkingLotManager.WebApi.Models;
using System.Diagnostics;

namespace ParkingLotManager.WebApi.Data;

public class AppDataContext : DbContext
{
    public AppDataContext(DbContextOptions options)
        : base(options)
    {        
    }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CompanyMap());
        modelBuilder.ApplyConfiguration(new VehicleMap());
        modelBuilder.ApplyConfiguration(new UserMap());
    }
}