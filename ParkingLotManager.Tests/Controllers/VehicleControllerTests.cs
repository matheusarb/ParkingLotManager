using Moq;
using ParkingLotManager.Tests.Repositories;
using ParkingLotManager.WebApi.Controllers;
using ParkingLotManager.WebApi.Data;
using ParkingLotManager.WebApi.Enums;
using ParkingLotManager.WebApi.Models;
using ParkingLotManager.WebApi.ViewModels.VehicleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotManager.Tests.Controllers;

[TestClass]
[TestCategory("ControllersMSTest")]
public class VehicleControllerTests
{
    private Vehicle _vehicle = new Vehicle();
    private RegisterVehicleViewModel _registerVehicleViewModel = new RegisterVehicleViewModel("000-AAAA", "Ford", "Ranger", "Red", EVehicleType.Car, "WellPark Inc");
    private UpdateVehicleViewModel _updateVehicleViewModel = new("000-AAAA", "Ford", "Ranger", "Red", EVehicleType.Car, "WellPark Inc");
    private readonly FakeVehicleRepository _fakeVehicleRepository = new();

    [TestMethod]
    public void Should_return_success_when_creating_vehicle()
    {
        _vehicle.Create(_registerVehicleViewModel);
        Assert.IsInstanceOfType<Vehicle>(_vehicle);
    }

    [TestMethod]
    public void Should_return_vehicle_if_licensePlate_is_000AAAA()
    {        
        Assert.IsTrue(_fakeVehicleRepository.Get("000AAAA"));
    }
    
    [TestMethod]
    public void Should_return_list_of_vehicles_if_route_is_correct()
    {        
        Assert.IsInstanceOfType<List<Vehicle>>(_fakeVehicleRepository.GetAll("v1/vehicles"));
    }

    [TestMethod]
    public void Should_return_true_if_brand_is_ford()
    {        
        Assert.IsTrue(_fakeVehicleRepository.GetFord("ford"));
    }
    
    [TestMethod]
    public void Should_return_object_if_update_is_correct()
    {        
        _vehicle.Update(_updateVehicleViewModel);
        Assert.IsInstanceOfType<Vehicle>(_vehicle);
    }
}
