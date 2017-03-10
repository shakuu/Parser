namespace Parser.Auth.Contracts
{
    public interface IAuthSignInManagerProvider
    {
        IAuthSignInManager SignInManager { get; }
    }
}
