using System.Threading.Tasks;

namespace Parser.Auth.Remote
{
    public interface IRemoteUserLoginService
    {
        Task Login(string username, string password);
    }
}
