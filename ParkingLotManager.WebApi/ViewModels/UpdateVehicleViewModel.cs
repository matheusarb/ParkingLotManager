﻿using ParkingLotManager.WebApi.Enums;
using ParkingLotManager.WebApi.Models;
using System.ComponentModel;

namespace ParkingLotManager.WebApi.ViewModels;

public class UpdateVehicleViewModel
{
    public string? LicensePlate { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public string? Color { get; set; }
    public EVehicleType Type { get; set; }
    public string? CompanyName { get; set; }

    public bool CheckIfAllEmpty(UpdateVehicleViewModel viewModel)
    {
        Type type = viewModel.GetType();
        var props = type.GetProperties();
        var count = 0;

        foreach (var prop in props)
        {
            var value = prop.GetValue(viewModel);
            if (value == "" || (prop.PropertyType.IsValueType && value.Equals(Activator.CreateInstance(prop.PropertyType))))
                count++;
        }

        return count == props.Length;
    }
}
