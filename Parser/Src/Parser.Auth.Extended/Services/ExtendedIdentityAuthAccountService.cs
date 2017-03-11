using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

using Bytes2you.Validation;

using Parser.Auth.Contracts;
using Parser.Auth.Extended.Contracts;
using Parser.Data.ViewModels.Factories;
using Parser.Data.Services.Contracts;

namespace Parser.Auth.Extended.Services
{
    public class ExtendedIdentityAuthAccountService : IExtendedIdentityAuthAccountService, IIdentityAuthAccountService
    {
        private readonly IIdentityAuthAccountService identityAuthAccountService;
        private readonly IRegisterParserUserViewModelFactory registerParserUserViewModelFactory;
        private readonly ICreateParserUserService createParserUserService;

        public ExtendedIdentityAuthAccountService(IIdentityAuthAccountService identityAuthAccountService, IRegisterParserUserViewModelFactory registerParserUserViewModelFactory, ICreateParserUserService createParserUserService)
        {
            Guard.WhenArgument(identityAuthAccountService, nameof(IIdentityAuthAccountService)).IsNull().Throw();
            Guard.WhenArgument(registerParserUserViewModelFactory, nameof(IRegisterParserUserViewModelFactory)).IsNull().Throw();
            Guard.WhenArgument(createParserUserService, nameof(ICreateParserUserService)).IsNull().Throw();

            this.identityAuthAccountService = identityAuthAccountService;
            this.registerParserUserViewModelFactory = registerParserUserViewModelFactory;
            this.createParserUserService = createParserUserService;
        }

        public async Task<IdentityResult> CreateAsync(AuthUser user, string password)
        {
            var result = await this.identityAuthAccountService.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var parserUser = this.registerParserUserViewModelFactory.CreateRegisterParserUserViewModel(user.UserName);
                this.createParserUserService.CreateParserUser(parserUser);
            }

            return result;
        }

        public async Task<SignInStatus> PasswordSignInAsync(string email, string password, bool rememberMe)
        {
            return await this.identityAuthAccountService.PasswordSignInAsync(email, password, rememberMe);
        }

        public async Task SignInAsync(AuthUser user)
        {
            await this.identityAuthAccountService.SignInAsync(user);
        }
    }
}
