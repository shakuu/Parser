using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

using Bytes2you.Validation;

using Parser.Auth.Contracts;
using Parser.Auth.Extended.Contracts;
using Parser.Data.Contracts;
using Parser.Data.ViewModels.Factories;

namespace Parser.Auth.Extended.Services
{
    public class ExtendedIdentityAuthAccountService : IExtendedIdentityAuthAccountService, IIdentityAuthAccountService
    {
        private readonly IIdentityAuthAccountService identityAuthAccountService;
        private readonly IParserUserDataProvider parserUserDataProvider;
        private readonly IRegisterParserUserViewModelFactory registerParserUserViewModelFactory;

        public ExtendedIdentityAuthAccountService(IIdentityAuthAccountService identityAuthAccountService, IParserUserDataProvider parserUserDataProvider, IRegisterParserUserViewModelFactory registerParserUserViewModelFactory)
        {
            Guard.WhenArgument(identityAuthAccountService, nameof(IIdentityAuthAccountService)).IsNull().Throw();
            Guard.WhenArgument(parserUserDataProvider, nameof(IParserUserDataProvider)).IsNull().Throw();
            Guard.WhenArgument(registerParserUserViewModelFactory, nameof(IRegisterParserUserViewModelFactory)).IsNull().Throw();

            this.identityAuthAccountService = identityAuthAccountService;
            this.parserUserDataProvider = parserUserDataProvider;
            this.registerParserUserViewModelFactory = registerParserUserViewModelFactory;
        }

        public async Task<IdentityResult> CreateAsync(AuthUser user, string password)
        {
            var result = await this.identityAuthAccountService.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var parserUser = this.registerParserUserViewModelFactory.CreateRegisterParserUserViewModel(user.UserName);
                this.parserUserDataProvider.CreateParserUser(parserUser);
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
