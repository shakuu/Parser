using System;

namespace Parser.Auth.Contracts
{
    public interface IAuthOwnerService
    {
        void AddRoleAdmin(string username);
    }
}
