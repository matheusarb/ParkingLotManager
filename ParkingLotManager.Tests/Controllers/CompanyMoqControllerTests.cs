using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using ParkingLotManager.WebApi.Controllers;
using ParkingLotManager.WebApi.Data;
using ParkingLotManager.WebApi.Models;
using ParkingLotManager.WebApi.Models.Contracts;
using ParkingLotManager.WebApi.ValueObjects;
using ParkingLotManager.WebApi.ViewModels.CompanyViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotManager.Tests.Controllers;

[TestClass]
[TestCategory("MoqControllers")]
public class CompanyMoqControllerTests
{
    private readonly RegisterCompanyViewModel _registerCompanyViewModel = new(
        "CompanyName",
        new Cnpj("76336280000175"),
        new Address("StreetTest", "CityTest", "ZipCodeTest"),
        "71999999999",
        10,
        10);

    [TestMethod]
    public void ShouldReturnCreatedCompany()
    {
        var company = new Company();
        var companyMock = new Mock<ICompany>();
        companyMock.Object.Create(_registerCompanyViewModel);

        Assert.AreEqual(company,
            companyMock.Object);
    }
}