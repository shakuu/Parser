namespace Parser.Auth.Contracts
{
    public interface IAuthUserManagerProvider
    {
        IAuthUserManager UserManager { get; }
    }
}
