using System.Collections.Generic;
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

        public override IDictionary<string, string> GetData()
        {
            var isAuthorized = CheckAuthorization();
            if (isAuthorized)
            {
                return base.GetData();
            }

            return new Dictionary<string, string>();
        }

        private bool CheckAuthorization()
        {
            return _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, Constants.VERSIONINFO_ADMIN_POLICY).Result.Succeeded;
        }
    }
}
