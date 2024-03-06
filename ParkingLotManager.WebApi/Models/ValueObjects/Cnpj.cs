namespace ParkingLotManager.WebApi.Models.ValueObjects;

public class Cnpj
{
    public Cnpj(string cnpjNumber)
    {
        CnpjNumber = cnpjNumber;
        
        if(string.IsNullOrEmpty(CnpjNumber))
            throw new Exception("Please type your CNPJ number");
        if (!IsValid)
            throw new Exception("Invalid CNPJ");
    }

    public string CnpjNumber { get; }

    public bool IsValid => CnpjNumber.Length == 14;
}
