namespace Parser.Data.ViewModels.AdminFab
{
    public class AdminFabViewModel
    {
        public AdminFabViewModel(bool isOwner, bool isAdmin)
        {
            this.IsOwner = isOwner;
            this.IsAdmin = isAdmin;
        }

        public bool IsOwner { get; private set; }

        public bool IsAdmin { get; private set; }
    }
}
