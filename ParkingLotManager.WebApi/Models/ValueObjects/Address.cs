﻿namespace ParkingLotManager.WebApi.Models.ValueObjects;

public class Address
{
    public string Street { get; private set; }
    public string City { get; private set; }
    public string ZipCode { get; private set; }
}
