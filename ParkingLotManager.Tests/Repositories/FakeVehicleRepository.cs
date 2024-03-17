using ParkingLotManager.WebApi.Models;
using ParkingLotManager.WebApi.ViewModels.VehicleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotManager.Tests.Repositories;

public class FakeVehicleRepository
{
    public bool GetFord(string brand)
    {
        if (brand.Equals("Ford", StringComparison.OrdinalIgnoreCase))
            return true;
        return false;
    }
}