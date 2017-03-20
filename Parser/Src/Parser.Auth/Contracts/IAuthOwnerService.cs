using Parser.Auth.ViewModels;

namespace Parser.Auth.Contracts
{
    public interface IAuthOwnerService
    {
        void AddRoleAdmin(string username);

        OwnerViewModel GetAuthUsersOnPage(int pageNumber);
    }
}
