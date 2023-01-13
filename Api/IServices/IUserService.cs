using Api.Requests.StudentRequests;
using Api.Requests.UserRequests;
using Api.Responses.UserResponses;
using Api.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.IServices
{
    public interface IUserService
    {
        Task<LoginStatus> LogInUser(string email, string password);
        Task<UserAddStatus> CreateUser(CreateUserRequest newUser);
        Task<PasswordChangeStatus> ChangePassword(string oldPassword, string newPassword, string id);
        Task<UserAddStatus> AddStudents(AddStudentsRequest request);
    }
}
