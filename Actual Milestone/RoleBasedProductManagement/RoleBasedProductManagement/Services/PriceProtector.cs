using Microsoft.AspNetCore.DataProtection;
using System.Globalization;

namespace RoleBasedProductManagement.Services;

public class PriceProtector : IPriceProtector
{
    private readonly IDataProtector _protector;
    public PriceProtector(IDataProtectionProvider provider)
    {
        _protector = provider.CreateProtector("ProductPriceProtector.v1");
    }

    public string Protect(decimal value) =>
        _protector.Protect(value.ToString(CultureInfo.InvariantCulture));

    public bool TryUnprotect(string protectedValue, out decimal value)
    {
        value = 0m;
        try
        {
            var raw = _protector.Unprotect(protectedValue);
            return decimal.TryParse(raw, NumberStyles.Number, CultureInfo.InvariantCulture, out value);
        }
        catch { return false; }
    }
}
