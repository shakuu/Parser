﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using Bytes2you.Validation;

using Parser.Auth.Contracts;
using Parser.Auth.ViewModels;

namespace Parser.Auth.Services
{
    public class AuthOwnerService : IAuthOwnerService
    {
        private const int DefaultPageSize = 50;

        private const string AdminRole = "Admin";

        private readonly IAuthUserManagerProvider authUserManagerProvider;

        public AuthOwnerService(IAuthUserManagerProvider authUserManagerProvider)
        {
            Guard.WhenArgument(authUserManagerProvider, nameof(IAuthUserManagerProvider)).IsNull().Throw();

            this.authUserManagerProvider = authUserManagerProvider;
        }

        public void AddRoleAdmin(string username)
        {
            var user = this.authUserManagerProvider.UserManager.FindByName(username);
            this.authUserManagerProvider.UserManager.AddToRole(user.Id, AuthOwnerService.AdminRole);
        }

        public IEnumerable<AuthUserViewModel> GetAuthUsersOnPage(int pageNumber)
        {
            return this.authUserManagerProvider.UserManager.AuthUsers
                .Skip(pageNumber * AuthOwnerService.DefaultPageSize)
                .Take(AuthOwnerService.DefaultPageSize)
                .Select(u => new AuthUserViewModel() { Username = u.UserName })
                .ToList();
        }
    }
}
