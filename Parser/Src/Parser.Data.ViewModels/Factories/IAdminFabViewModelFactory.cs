using Parser.Data.ViewModels.AdminFab;

namespace Parser.Data.ViewModels.Factories
{
    public interface IAdminFabViewModelFactory
    {
        AdminFabViewModel CreateAdminFabViewModel(bool isOwner, bool isAdmin);
    }
}
