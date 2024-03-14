using Microsoft.IdentityModel.Tokens;

namespace ParkingLotManager.WebApi.Models.ValueObjects;

public class Cnpj : ValueObject
{
    private Cnpj()
    { }

    public Cnpj(string cnpjNumber)
    {
        CnpjNumber = cnpjNumber.Replace(".", "").Replace("/", "").Replace("-", "");

        if (!IsValid)
            throw new Exception("Invalid CNPJ");
    }

    public string CnpjNumber { get; }

    public bool IsValid => CnpjNumber.Length == 14;
}
