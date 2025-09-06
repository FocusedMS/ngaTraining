namespace RoleBasedProductManagement.Services;

public interface IPriceProtector
{
    string Protect(decimal value);
    bool TryUnprotect(string protectedValue, out decimal value);
}
