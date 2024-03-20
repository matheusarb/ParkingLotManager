using Microsoft.AspNetCore.Mvc;
using Moq;
using ParkingLotManager.WebApi;
using ParkingLotManager.WebApi.Controllers;
using ParkingLotManager.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ParkingLotManager.XUnitTests.Controllers;

[Trait("Controllers", "Unit")]
public class VehicleControllerTests
{
    private Mock<VehicleController> _mockedController = new();

    [InlineData("AAA0000")]
    [Theory]
    public async void Get_ShouldGetVehicleByLicensePlate(string licensePlate)
    {
        var dbVehicle = new Vehicle();
        _mockedController.Setup(m => m.GetByLicensePlateAsync(It.IsAny<string>(), Configuration.ApiKeyName))
            .ReturnsAsync(new OkObjectResult(dbVehicle));

        var expected = await _mockedController.Object.GetByLicensePlateAsync(licensePlate, Configuration.ApiKeyName);

        Assert.Equal(200, ((OkObjectResult)expected).StatusCode);
    }

    [Fact]
    public async void GetAll_ShouldGetAllVehicles()
    {
        _mockedController.Setup(m => m.GetAsync(Configuration.ApiKeyName))
            .ReturnsAsync(new OkObjectResult(new List<Vehicle>()));

        var expected = await _mockedController.Object.GetAsync(Configuration.ApiKeyName);

        Assert.Equal(200, ((OkObjectResult)expected).StatusCode);
    }
}