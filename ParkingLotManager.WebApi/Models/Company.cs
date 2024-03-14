﻿using Microsoft.IdentityModel.Tokens;
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

    public void Update(UpdateCompanyViewModel viewModel)
    {
        this.Name = viewModel.Name.IsNullOrEmpty() ? this.Name : viewModel.Name;
        this.Cnpj = !viewModel.Cnpj.IsValid ? this.Cnpj : viewModel.Cnpj;
        this.Address = viewModel.Address ?? this.Address;
        this.Telephone = viewModel.Telephone.IsNullOrEmpty() ? this.Telephone : viewModel.Telephone;
        this.CarSlots = viewModel.CarSlots == 0 || viewModel.CarSlots == null ? this.CarSlots : viewModel.CarSlots;
        this.MotorcycleSlots = viewModel.MotorcycleSlots == 0 || viewModel.MotorcycleSlots == null ? this.MotorcycleSlots : viewModel.MotorcycleSlots;
    }
}
