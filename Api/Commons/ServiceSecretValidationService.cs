using Api.DTOs;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Api.Commons
{
    public sealed class ServiceSecretValidationService : IServiceSecretValidationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LiveUpdateConfigDTO _liveUpdateConfig;

        public ServiceSecretValidationService(IHttpContextAccessor httpContextAccessor, LiveUpdateConfigDTO liveUpdateConfig)
        {
            _httpContextAccessor = httpContextAccessor;
            _liveUpdateConfig = liveUpdateConfig;
        }

        public async Task<bool> ValidateService()
        {
            var serviceSecret = _httpContextAccessor.HttpContext.Request.Headers["Service-Secret"].ToString();

            return serviceSecret ==_liveUpdateConfig.ServiceSecret;
        }
    }

}
