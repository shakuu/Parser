using System.Collections.Generic;

using Parser.Auth.ViewModels;

namespace Parser.Auth.Contracts
{
    public interface IAuthOwnerService
    {
        void AddRoleAdmin(string username);

        IEnumerable<AuthUserViewModel> GetAuthUsersOnPage(int pageNumber);
    }
}
