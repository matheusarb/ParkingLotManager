using ParkingLotManager.WebApi.ValueObjects;
using ParkingLotManager.WebApi.ViewModels.CompanyViewModels;

namespace ParkingLotManager.WebApi.Models.Contracts;

public interface ICompany
{
    public void Create(RegisterCompanyViewModel viewModel);

    public void Update(UpdateCompanyViewModel viewModel, Address address);
}
