using ParkingLotManager.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotManager.Tests.Repositories;

public class FakeCompanyRepository
{
    public string Get(string name)
    {
        if(name.Equals("companytest", StringComparison.OrdinalIgnoreCase))
            return name;
        return null;
    }
}
