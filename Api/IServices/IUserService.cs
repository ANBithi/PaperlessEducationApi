using Api.Models;
using Api.Requests.StudentRequests;
using Api.Requests.UserRequests;
using Api.Responses.UserResponses;
using Api.ViewModels;
using System.Threading.Tasks;

namespace Api.IServices
{
    public interface IUserService
    {
        Task<LoginStatus> LogInUser(string email, string password);
        Task CreateUser(CreateUserRequest newUser);
        Task<PasswordChangeStatus> ChangePassword(string newPassword, int otp);
        Task AddStudents(AddStudentsRequest request);
        Task<bool> UpdateUserInteraction(InteractionType type);
        Task<UserProfile> UserProfile();
        Task<string> InititateChangePassword();
        Task<StudentProfileViewModel> GetStudentProfile(Student student);
    }
}
