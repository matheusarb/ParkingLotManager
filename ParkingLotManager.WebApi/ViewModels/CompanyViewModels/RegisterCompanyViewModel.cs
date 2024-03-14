﻿using ParkingLotManager.WebApi.Models;
using ParkingLotManager.WebApi.Models.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace ParkingLotManager.WebApi.ViewModels.CompanyViewModels;

public class RegisterCompanyViewModel : Company
{
    public RegisterCompanyViewModel(
        string name,
        Cnpj cnpj,
        Address address,
        string telephone,
        int carSlots,
        int motorcycleSlots)
    {
        Name = name;
        Cnpj = cnpj;
        Address = address;
        Telephone = telephone;
        CarSlots = carSlots;
        MotorcycleSlots = motorcycleSlots;
    }

    [Required(ErrorMessage = "Company name is required")]
    [MinLength(3, ErrorMessage ="Company name must have at least 3 characters")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Company CNPJ is required")]
    public Cnpj Cnpj { get; set; }

    [Required(ErrorMessage = "Company address is required")]
    public Address Address { get; set; }

    [Required(ErrorMessage = "Company telephone is required")]
    public string Telephone { get; set; }

    [Required(ErrorMessage = "Company car slots is required")]
    public int CarSlots { get; set; }

    [Required(ErrorMessage = "Company motorcycle slots is required")]
    public int MotorcycleSlots { get; set; }
}

