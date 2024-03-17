using ParkingLotManager.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotManager.Tests.Repositories;

public class FakeCompanyRepository
{
    public Company Get(string name)
    {
        if(name.Equals("companytest", StringComparison.OrdinalIgnoreCase))
            return new Company();
        return null;
    }
}
