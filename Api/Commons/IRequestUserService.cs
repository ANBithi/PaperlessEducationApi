using Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Commons
{
    public interface IRequestUserService
    {        
        Task<User> GetUser();
    }

}
