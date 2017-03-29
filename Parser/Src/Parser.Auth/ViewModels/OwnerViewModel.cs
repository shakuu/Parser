using System.Collections.Generic;

namespace Parser.Auth.ViewModels
{
    public class OwnerViewModel
    {
        public IEnumerable<AuthUserViewModel> AuthUsers { get; set; }

        public int PageNumber { get; set; }
    }
}
