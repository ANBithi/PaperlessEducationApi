using System.Threading.Tasks;

namespace Api.Commons
{
    public interface IServiceSecretValidationService
    {
        Task<bool> ValidateService();
    }
}
