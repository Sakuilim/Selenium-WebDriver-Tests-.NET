using HelperLibrary.Models;

namespace HelperLibrary.VaultHelpers
{
    public interface IVaultDataService
    {
        UserModel GetUserData(UserModel user);
    }
}