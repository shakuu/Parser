using Microsoft.AspNet.Identity;

namespace Parser.Auth.Extended.Tests.Mocks
{
    public class MockIdentityResult : IdentityResult
    {
        public MockIdentityResult(bool success)
            : base(success)
        {

        }
    }
}
