using Microsoft.AspNetCore.Mvc.ModelBinding;
using ParkingLotManager.WebApi.Models;
using ParkingLotManager.WebApi.Models.Contracts;
using ParkingLotManager.WebApi.ViewModels.VehicleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotManager.Tests.Repositories;

public class FakeVehicleRepository : IVehicle
{
    public bool Get(string licensePlate)
    {
        if (licensePlate.Length != 7)
            return false;
        if (licensePlate == "000AAAA")
            return true;
        return false;
    }

    public List<Vehicle> GetAll(string route)
    {
        var list = new List<Vehicle>();
        if (route == "v1/vehicles")
            return list;
        return null;
    }

    public bool GetFord(string brand)
    {
        if (brand.Equals("Ford", StringComparison.OrdinalIgnoreCase))
            return true;
        return false;
    }

    public void Create(RegisterVehicleViewModel viewModel)
    {
    }

    public void Update(UpdateVehicleViewModel viewModel)
    {
    }
}