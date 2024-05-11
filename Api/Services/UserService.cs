using Api.Commons;
using Api.Enums;
using Api.Exceptions;
using Api.IServices;
using Api.Models;
using Api.Models.UserSpecific;
using Api.Repositories;
using Api.Requests.StudentRequests;
using Api.Requests.UserRequests;
using Api.Responses;
using Api.Responses.UserResponses;
using Api.Validator;
using Api.Validators;
using Api.ViewModels;
using AutoMapper;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        private readonly IRequestUserService _requestUserService;
        private readonly IDateTime _dateTime;
        private readonly IMapper _mapper;
        private readonly IPasswordMetadataRepository _passwordMetadataRepository;

        public UserService(IUserRepository userRepository, IStudentRepository studentRepository,
            IFacultyRepository facultyRepository, IDepartmentRepository departmentRepository,
            IInstituteRepository instituteRepository, IUserInteractionRepository userInteractionRepository,
            IMapper mapper, IRequestUserService requestUserService, IDateTime dateTime, IPasswordMetadataRepository passwordMetadataRepository)
        {
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _facultyRepository = facultyRepository;
            _departmentRepository = departmentRepository;
            _instituteRepository = instituteRepository;
            _userInteractionRepository = userInteractionRepository;
            _mapper = mapper;
            _requestUserService = requestUserService;
            _dateTime = dateTime;
            _passwordMetadataRepository = passwordMetadataRepository;
        }

        public async Task<bool> UpdateUserInteraction(InteractionType type)
        {
            var user = await _requestUserService.GetUser();
          try
            {
                var found = await _userInteractionRepository.GetSingle(x => x.CreatedById == user.Id && x.Type == type); 
                if(found is null)
                {
                    var interaction = new UserInteraction
                    {
                        Type = type,
                    };
                    interaction.UpdateCreatedByFields(user, _dateTime.NowUTC);
                    interaction.UpdateModifiedByFields(user, _dateTime.NowUTC);
                    _userInteractionRepository.Add(interaction);
                }
                else
                {
                    //TODO: Change the model of interaction -- >  changed it!
                    found.UpdateModifiedByFields(user, _dateTime.NowUTC);
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

        public async Task<PasswordChangeStatus> ChangePassword(string newPassword, int otp)
        {
            var passwordStatus = new PasswordChangeStatus
            {
                ResponseStatus = false,
                Message = ""
            };
            var user = await _requestUserService.GetUser();

            var metadata = await _passwordMetadataRepository.GetSingle(x => x.CreatedById == user.Id);

            if(metadata == null)
            {
                passwordStatus.Message = "Invalid Attempt!";
                return passwordStatus;
            }
            else
            {
                if(_dateTime.NowUTC.Subtract(metadata.ModifiedAt).TotalMinutes < 2)
                {
                    // old password incorrect
                    if (BCrypt.Net.BCrypt.Verify(newPassword, user.Password))
                    {
                        passwordStatus.Message = "Password matches the old password. Try something different.";
                        return passwordStatus;
                    }
                    user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
                    user.UpdateModifiedByFields(user, _dateTime.NowUTC);
                    _userRepository.Update(user);
                    await _userRepository.Commit();
                    passwordStatus.Message = "Password Changed.";
                    passwordStatus.ResponseStatus = true;
                    return passwordStatus;
                }
                else
                {
                    passwordStatus.Message = "Request Timeout. Try Again.";
                    passwordStatus.ResponseStatus = false;
                    return passwordStatus;
                }
            }


          
        }


        public async Task CreateUser(CreateUserRequest newUser)
        {
            var creator = await _requestUserService.GetUser();
            var institute = await _instituteRepository.GetSingle(x => true);
            newUser.FirstName = newUser.FirstName.Trim();
            newUser.LastName = newUser.LastName.Trim();
   
            List<IValidator> validators = new List<IValidator>()
            {
                new NameValidator(newUser.FirstName, newUser.LastName),
                new RoleValidator(newUser.UserType),
            };
            try
            {
                validators.ForEach(x => x.Validate());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var users = await _userRepository.GetAllAsync(x => x.FirstName == newUser.FirstName && x.LastName == newUser.LastName);

             string email;
            if (users.Count > 0)
            {
               email = GenerateEmail(newUser.FirstName, newUser.LastName, institute.Domain, $"{users.Count}");
            }
            else
            {
               email = GenerateEmail(newUser.FirstName, newUser.LastName, institute.Domain, "");
            }
            var addUser = new User
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Email = email,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Password = BCrypt.Net.BCrypt.HashPassword("1234"),
                UserType = (UserTypeEnum) Enum.Parse(typeof(UserTypeEnum), newUser.UserType),
            };


            addUser.UpdateCreatedByFields(creator, _dateTime.NowUTC);
            addUser.UpdateModifiedByFields(creator, _dateTime.NowUTC);
            addUser.IsActive = true;

            _userRepository.Add(addUser);
            if (addUser.UserType == UserTypeEnum.Faculty)
            {
                AddFaculty(addUser, newUser.Department, newUser.Designation, creator);
            }
            else
            {

            }
            await _userRepository.Commit();
        }

        private string GenerateEmail(string firstName, string lastName, string domain, string suffix)
        {
            var email = $"{firstName.ToLower()}.{lastName.ToLower()}{suffix}@{domain}";
            return email;
        }

        public async Task AddStudents(AddStudentsRequest request)
        {
            var user = await _requestUserService.GetUser();
            if(string.IsNullOrEmpty(request.Batch))
            {
                throw new InvalidRequestException("Batch not found.");
            }
            var departments = await _departmentRepository.GetAllAsync(x=> true);


            var currentBatchStudent = await _studentRepository.GetAllAsync(x => x.Batch == request.Batch && x.DepartmentId == request.Department);
            var names = request.Students.Split(",");
            var department = await _departmentRepository.GetById(request.Department);
            var institute = await _instituteRepository.GetSingle(x => true);
            var studentsCount = currentBatchStudent.Count;
            foreach (string name in names)
            {
                var newUser = new User
                {
                    Id = ObjectId.GenerateNewId().ToString(),
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
                List<IValidator> validators = new List<IValidator>()
            {
                new NameValidator(newUser.FirstName, newUser.LastName),
            };
                try
                {
                    validators.ForEach(x => x.Validate());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                newUser.Email = $"{nameParts[0].ToLower()}.{nameParts[1].ToLower()}.{department.Abbreviation}@{institute.Domain}";
                newUser.UpdateCreatedByFields(user, _dateTime.NowUTC);
                newUser.UpdateModifiedByFields(user, _dateTime.NowUTC);
                
                _userRepository.Add(newUser);

                var id = GetStudentId(request.Batch, department.DepartmentCode, studentsCount);
                AddStudent(newUser, request.AdvisorId, department.Id, request.Batch, user, id);
                studentsCount++;
            }
            await _userRepository.Commit();
        }

        public void AddStudent(User user, string advisorId, string departmentId, string batch, User creator, string studentId)
        {
            var student = new Student
            {
                BelongsTo = user.Id,
                Advisor = advisorId,
                DepartmentId = departmentId,
                Batch = batch,

            };
            student.AdmissionYear = DateTime.Now.Year;
            student.Name = $"{user.FirstName} {user.LastName}";
            student.Email = user.Email;
            student.StudentId = studentId ;
            student.CurrentClasses = new List<string>();

            student.UpdateCreatedByFields(creator, _dateTime.NowUTC);
            student.UpdateModifiedByFields(creator, _dateTime.NowUTC);

            _studentRepository.Add(student);
        }

        private string GetStudentId(string batch, int departmentCode, int count)
        {
            var pref = "";
           if(count == 0)
            {
              pref = "001";
            }
           else
            {
                pref = (count+1).ToString("D3");
            }

            var id = $"{batch}0{departmentCode}{pref}";
            return id;
        }

        public void AddFaculty(User user, string department, string designation, User creator)
        {
            var faculty = new Faculty
            {
                BelongsTo = user.Id,
                Department = department,
                Designation = designation
            };
            faculty.Name = $"{user.FirstName} {user.LastName}";
            faculty.Email = user.Email;
            faculty.UpdateCreatedByFields(creator, _dateTime.NowUTC);
            faculty.UpdateModifiedByFields(creator, _dateTime.NowUTC);
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
                UserType = user.UserType,
                IsActive = user.IsActive,
                LastLogin = DateTime.UtcNow
            };

            loginStatus.IsAuthorized = true;
            loginStatus.User = userView;
            loginStatus.Message = "Login Successfull!";

            return loginStatus;
        }

        public async Task<UserProfile> UserProfile()
        {
            UserProfile response = null;
           var user =  await _requestUserService.GetUser();

            if(user.UserType == UserTypeEnum.Faculty)
            {
                var fac = await _facultyRepository.GetSingle(x => x.BelongsTo == user.Id);
                var department = await _departmentRepository.GetSingle(x=> x.Id == fac.Department);
                 response = _mapper.Map<FacultyProfileViewModel>(fac);
                response.UserType = user.UserType;
                response.Department = department.Name;
            }
            if(user.UserType == UserTypeEnum.Student)
            {
                var student = await _studentRepository.GetSingle(x => x.BelongsTo == user.Id);
                response = await GetStudentProfile(student);
                response.UserType = user.UserType;
            }

            return response;
        }

        public async Task<string> InititateChangePassword()
        {
            string url = "";
            Random rand = new Random();
            var user = await _requestUserService.GetUser();
            var otp = rand.Next();
            url = $"paperless.local:3000/changePassword/{user.Id}/{otp}";

            var metadata = await _passwordMetadataRepository.GetSingle(x => x.CreatedById == user.Id);

            if(metadata == null)
            {
                metadata = new PasswordMetadata();
                metadata.CurrentOtp = otp;
                metadata.UpdateCreatedByFields(user, _dateTime.NowUTC);
                metadata.UpdateModifiedByFields(user, _dateTime.NowUTC);
                _passwordMetadataRepository.Add(metadata);
            }
            else
            {
                metadata.CurrentOtp = otp;
                metadata.UpdateModifiedByFields(user, _dateTime.NowUTC);
                _passwordMetadataRepository.Update(metadata);
            }
            await _passwordMetadataRepository.Commit();
            Console.WriteLine($"PASS RESET :{url}");
            return "";

        }


        public async Task<StudentProfileViewModel> GetStudentProfile(Student student)
        {
            var advisor = await _facultyRepository.GetSingle(x => x.Id == student.Advisor);
            var department = await _departmentRepository.GetSingle(x => x.Id == student.DepartmentId);
            var studentProfile = _mapper.Map<StudentProfileViewModel>(student);
            studentProfile.Department = department.Name;
            studentProfile.Advisor = _mapper.Map<FacultyProfileViewModel>(advisor);
            return studentProfile;
        }

    }

}

