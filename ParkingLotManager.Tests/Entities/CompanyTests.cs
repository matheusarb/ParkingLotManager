using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ParkingLotManager.WebApi.Models;
using ParkingLotManager.WebApi.ValueObjects;
using ParkingLotManager.WebApi.ViewModels.CompanyViewModels;

namespace ParkingLotManager.Tests.Entities;

[TestClass]
public class CompanyTests
{
    private readonly Company _company = new ();

    private readonly RegisterCompanyViewModel _registerCompanyViewModel = new(
        "CompanyName", new Cnpj("76336280000175"),
        new Address("StreetTest", "CityTest", "ZipCodeTest"),
        "71999999999", 10, 10);

    [TestMethod]
    [TestCategory("Entities")]
    public void Should_create_company_if_register_company_viewmodel_is_valid()
    {
       _company.Create(_registerCompanyViewModel);
       
       Assert.IsTrue(_company != null);
    }
}
