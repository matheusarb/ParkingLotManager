using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ParkingLotManager.Tests.Repositories;
using ParkingLotManager.WebApi.Models;
using ParkingLotManager.WebApi.ValueObjects;
using ParkingLotManager.WebApi.ViewModels.CompanyViewModels;

namespace ParkingLotManager.Tests.Entities;

[TestClass]
//[TestCategory("Company")]
public class CompanyTests
{
    private readonly Company _company = new();
    private readonly RegisterCompanyViewModel _registerCompanyViewModel = new(
        "CompanyName", new Cnpj("76336280000175"),
        new Address("StreetTest", "CityTest", "ZipCodeTest"),
        "71999999999", 10, 10);
    private readonly FakeCompanyRepository _fakeCompanyRepository = new();

    [TestMethod]
    public void Should_create_company_if_register_company_viewmodel_is_valid()
    {
        _company.Create(_registerCompanyViewModel);
        Assert.IsTrue(_company != null);
    }

    [TestMethod]
    public void Should_generate_number_10_if_created_company_has_10_MotorcycleSlots()
    {
        _company.Create(_registerCompanyViewModel);
        Assert.AreEqual(10, _company.MotorcycleSlots);
    }

    [TestMethod]
    public void Should_generate_number_10_if_created_company_has_10_CarSlots()
    {
        _company.Create(_registerCompanyViewModel);
        Assert.AreEqual(10, _company.CarSlots);
    }

    [TestMethod]
    public void Should_return_Company_if_name_is_CompanyTest()
    {
        var company = _fakeCompanyRepository.Get("COMPANYTEST");
        Assert.IsInstanceOfType<Company>(company);
    }
}
