using ParkingLotManager.Tests.Repositories;
using ParkingLotManager.WebApi.Models;
using ParkingLotManager.WebApi.ValueObjects;
using ParkingLotManager.WebApi.ViewModels.CompanyViewModels;
using ParkingLotManager.WebApi.ViewModels.VehicleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotManager.Tests.Controllers;

[TestClass]
[TestCategory("CompanyController")]
public class CompanyControllerTests
{
    private readonly Company _company = new();
    private readonly RegisterCompanyViewModel _registerCompanyViewModel = new(
        "CompanyName", new Cnpj("76336280000175"),
        new Address("StreetTest", "CityTest", "ZipCodeTest"),
        "71999999999", 10, 10);

    private readonly FakeCompanyRepository _fakeCompanyRepository = new();

    [TestMethod]
    public void Should_return_success_when_creating_company()
    {
        _company.Create(_registerCompanyViewModel);
        Assert.IsTrue(!string.IsNullOrEmpty(_company.Name));
    }
}
