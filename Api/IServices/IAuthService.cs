using Api.Models;
using Api.ViewModels;

namespace Api.IServices
{
    public interface IAuthService
    {
        AuthData GetAuthData(UserViewModel user);
    }
}
