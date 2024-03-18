using ParkingLotManager.WebApi.Enums;
using ParkingLotManager.WebApi.Models;
using ParkingLotManager.WebApi.ViewModels.VehicleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotManager.Tests.Entities;

[TestClass]
[TestCategory("Vehicle")]
public class VehicleTests
{
    private Vehicle _vehicle = new();
    private readonly RegisterVehicleViewModel _registerVehicleViewModel = new RegisterVehicleViewModel("AAA0000", "Ferrari", "f-50", "red", EVehicleType.Car, "WellPark Inc");
    private readonly UpdateVehicleViewModel _updateVehicleViewModel = new UpdateVehicleViewModel("AAA0000", "Ferrari", "f-50", "red", EVehicleType.Motorcycle, "WellPark Inc");

    [TestMethod]
    public void Should_create_vehicle_if_registerModel_is_valid()
    {
        _vehicle.Create(_registerVehicleViewModel);
        Assert.IsTrue(!string.IsNullOrEmpty(_vehicle.Brand));
    }

    [TestMethod]
    public void Should_generate_number_1_if_vehicleType_is_car()
    {
        Assert.AreEqual(1, (int)_registerVehicleViewModel.Type);
    }
    
    [TestMethod]
    public void Should_generate_number_2_if_vehicleType_is_motorcycle()
    {
        _vehicle.Update(_updateVehicleViewModel);
        Assert.AreEqual(2, (int)_registerVehicleViewModel.Type);
    }
}
