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
[TestCategory("VehicleController")]
public class VehicleControllerTests
{
    private Vehicle _vehicle = new Vehicle();
    private RegisterVehicleViewModel _registerVehicleViewModel = new RegisterVehicleViewModel("000-AAAA", "Ford", "Ranger", "Red", EVehicleType.Car, "WellPark Inc");
    private readonly FakeVehicleRepository _fakeVehicleRepository = new();

    [TestMethod]
    public void Should_return_success_when_creating_vehicle()
    {
        _vehicle.Create(_registerVehicleViewModel);
        Assert.IsTrue(_vehicle != null);
    }

    [TestMethod]
    public void Should_return_true_if_brand_is_ford()
    {        
        Assert.IsTrue(_fakeVehicleRepository.GetFord("ford"));
    }
}
