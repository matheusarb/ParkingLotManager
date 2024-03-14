using ParkingLotManager.WebApi.Models.ValueObjects;
using ParkingLotManager.WebApi.ViewModels.CompanyViewModels;

namespace ParkingLotManager.WebApi.Models;

public class Company
{
    public string Name { get; private set; }
    public Cnpj Cnpj { get; private set; }
    public Address Address { get; private set; }
    public string Telephone { get; private set; }
    public int CarSlots { get; private set; }
    public int MotorcycleSlots { get; private set; }

    public IList<Vehicle>? Vehicles { get; private set; }

    public void Create(RegisterCompanyViewModel viewModel)
    {
        Name = viewModel.Name;
        Cnpj = viewModel.Cnpj;
        Address = viewModel.Address;
        Telephone = viewModel.Telephone;
        CarSlots = viewModel.CarSlots;
        MotorcycleSlots = viewModel.MotorcycleSlots;
    }
}
