using System;
using Api.Models;
using Api.Repositories;
using System.Threading.Tasks;
using Api.ViewModels;
using System.Collections.Generic;
using System.Globalization;
using Api.Enums;
using Api.IServices;
using Api.Responses.UserResponses;
using Api.Requests.UserRequests;
using Api.Requests.StudentRequests;
using AutoMapper;
using Api.Models.UserInteraction;

namespace Api.Services
{

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserInteractionRepository _userInteractionRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IFacultyRepository _facultyRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IInstituteRepository _instituteRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IStudentRepository studentRepository,
            IFacultyRepository facultyRepository, IDepartmentRepository departmentRepository,
            IInstituteRepository instituteRepository, IUserInteractionRepository userInteractionRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _facultyRepository = facultyRepository;
            _departmentRepository = departmentRepository;
            _instituteRepository = instituteRepository;
            _userInteractionRepository = userInteractionRepository;
            _mapper = mapper;
        }

        public async Task<bool> UpdateUserInteraction(string createdById, InteractionType type)
        {
          try
            {
                var found = await _userInteractionRepository.GetSingle(x => x.CreatedById == createdById && x.Type == type); 
                if(found is null)
                {
                    var interaction = new UserInteraction
                    {
                        Time = DateTime.Now,
                        Type = type,
                        CreatedById = createdById,
                    };
                    _userInteractionRepository.Add(interaction);
                }
                else
                {
                    found.Time = DateTime.Now;
                    _userInteractionRepository.Update(found);
                }
                await _userInteractionRepository.Commit();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<PasswordChangeStatus> ChangePassword(string oldPassword, string newPassword, string id)
        {
            var passwordStatus = new PasswordChangeStatus
            {
                ResponseStatus = false,
                Message = ""
            };
            var user = await _userRepository.GetById(id);
            // old password incorrect
            if (!BCrypt.Net.BCrypt.Verify(oldPassword, user.Password))
            {
                passwordStatus.Message = "Old password is incorrect";
                return passwordStatus;
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _userRepository.Update(user);
            await _userRepository.Commit();
            passwordStatus.Message = "Password Changed.";
            passwordStatus.ResponseStatus = true;
            return passwordStatus;
        }

        //public async Task<List<SupervisorViewModel>> GetAllUsers()
        //{

        //    var allUsers = new List<SupervisorViewModel>();
        //    var users = await _userRepository.GetAll();

        //    foreach (User user in users)
        //    {
        //        var userView = new SupervisorViewModel
        //        {
        //            Id = user.Id,
        //            FullName = $"{user.FirstName} {user.LastName}"

        //        };
        //        allUsers.Add(userView);
        //    }

        //    return allUsers;
        //}


        public async Task<UserAddStatus> CreateUser(CreateUserRequest newUser)
        {

            var userAddStatus = new UserAddStatus
            {
                IsCreated = false,
                Message = ""
            };
            var user = await _userRepository.GetSingle(x => x.Email == newUser.Email);

            if (user is null)
            {
                var addUser = new User
                {
                    Email = newUser.Email,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password),
                    UserType = (UserTypeEnum)newUser.UserType,
                };


                addUser.CreatedAt = DateTime.UtcNow;
                addUser.CreatedBy = newUser.CreatedBy;
                addUser.CreatedById = newUser.CreatedById;
                addUser.IsActive = true;

                _userRepository.Add(addUser);
                if (addUser.UserType == UserTypeEnum.Faculty)
                {
                    AddFaculty(addUser, newUser.Department, newUser.Designation);
                }
                else
                {

                }
                await _userRepository.Commit();

                userAddStatus.IsCreated = true;
                userAddStatus.Message = "User Added Succesfully!";
                return userAddStatus;
            }

            userAddStatus.Message = "User Already Exist.";
            return userAddStatus;

        }


        public async Task<UserAddStatus> AddStudents(AddStudentsRequest request)
        {

            var userAddStatus = new UserAddStatus
            {
                IsCreated = false,
                Message = ""
            };

            var names = request.Students.Split(",");
            var department = await _departmentRepository.GetById(request.Department);
            var institute = await _instituteRepository.GetSingle(x => true);

            foreach (string name in names)
            {
                var newUser = new User
                {
                    CreatedBy = request.CreatedBy,
                    CreatedById = request.CreatedById,
                    CreatedAt = DateTime.Now,
                    Password = BCrypt.Net.BCrypt.HashPassword("1234"),
                    UserType = UserTypeEnum.Student,
                    IsActive = true
                };
                var nameParts = name.Trim().Split(" ");
                var firstName = "";
                for (var i = 0; i <= nameParts.Length - 2; i++)
                {

                    var space = i == 0 ? "" : " ";
                    firstName = $"{space}{nameParts[i]}";
                }
                newUser.FirstName = firstName;
                newUser.LastName = nameParts[nameParts.Length - 1];
                newUser.Email = $"{nameParts[0].ToLower()}.{nameParts[1].ToLower()}.{department.Abbreviation}@{institute.Domain}";
                _userRepository.Add(newUser);
                AddStudent(newUser, request.AdvisorId, request.Department, request.Batch);

            }
            await _userRepository.Commit();

            userAddStatus.IsCreated = true;
            userAddStatus.Message = "User Added Succesfully!";
            return userAddStatus;
        }

        public void AddStudent(User user, string advisorId, string department, string batch)
        {

            var student = new Student
            {
                BelongsTo = user.Id,
                Advisor = advisorId,
                Department = department,
                Batch = batch,

            };
            student.AdmissionYear = DateTime.Now.Year;
            student.Name = $"{user.FirstName} {user.LastName}";
            student.Email = user.Email;
            student.StudentId = $"{batch}014001";
            student.Payments = new List<string>();
            student.CurrentClasses = new List<string>();

            _studentRepository.Add(student);
        }

        public void AddFaculty(User user, string department, string designation)
        {
            var faculty = new Faculty
            {
                BelongsTo = user.Id,
                Department = department,
                Designation = designation
            };
            faculty.Name = $"{user.FirstName} {user.LastName}";
            faculty.Email = user.Email;
            _facultyRepository.Add(faculty);
        }

        public async Task<LoginStatus> LogInUser(string email, string password)
        {

            var loginStatus = new LoginStatus
            {
                IsAuthorized = false,
                User = null,
                Message = ""
            };

            var user = await _userRepository.GetSingle(x => x.Email == email);

            // verify valid user
            if (user is null)
            {
                loginStatus.Message = "User Doesn't Exit!";
                return loginStatus;
            }


            //Verfiy pass
            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                loginStatus.Message = "Password Incorrect.";
                loginStatus.User = null;
                return loginStatus;

            }


            var userView = new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserType = (int)user.UserType,
                IsActive = user.IsActive,
                LastLogin = DateTime.UtcNow
            };

            loginStatus.IsAuthorized = true;
            loginStatus.User = userView;
            loginStatus.Message = "Login Successfull!";

            return loginStatus;
        }

    }

}

