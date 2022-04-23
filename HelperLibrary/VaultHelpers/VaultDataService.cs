using HelperLibrary.Models;
using HelperLibrary.Shared;
using HelperLibrary.StringFormatHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary.VaultHelpers
{
    public class VaultDataService : IVaultDataService
    {
        public readonly IFileParser fileParser;

        public VaultDataService(IFileParser fileParser)
        {
            this.fileParser = fileParser;
        }
        public UserModel GetUserData(UserModel user)
        {
            var data = fileParser.ParseFile(ItemPathStrings.VaultPath);
            user.Username = data.First();
            user.Password = data.Skip(1).Last();
            return user;
        }
    }
}
