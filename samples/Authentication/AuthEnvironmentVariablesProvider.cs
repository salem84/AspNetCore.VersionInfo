using System.Threading.Tasks;
using AspNetCore.VersionInfo.Models.Providers;
using AspNetCore.VersionInfo.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace AspNetCore.VersionInfo.Samples.Authentication
{
    public class AuthEnvironmentVariablesProvider : EnvironmentVariablesProvider
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public override string Name => nameof(AuthEnvironmentVariablesProvider);

        public AuthEnvironmentVariablesProvider(IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor)
        {
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task<InfoProviderResult> GetDataAsync()
        {
            var isAuthorized = await CheckAuthorization();
            if (isAuthorized)
            {
                return await base.GetDataAsync();
            }

            return null;
        }

        private async Task<bool> CheckAuthorization() => (await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, Constants.VERSIONINFO_ADMIN_POLICY)).Succeeded;
    }
}
