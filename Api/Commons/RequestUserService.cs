using Api.Models;
using Api.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Commons
{
    public sealed class RequestUserService : IRequestUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public RequestUserService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public async Task<User> GetUser()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault();
            if (userIdClaim == null)
            {
                throwUnAuthException();
            }
            var userId = userIdClaim.Value;
            var user = await _userRepository.GetSingle(x => x.Id == userId && x.IsActive == true);
            if (user == null)
            {
                throwUnAuthException();
            }
            return user;

            static void throwUnAuthException()
            {
                throw new Exception("user not found from token");
            }
        }

      
    }

}
