using Api.Models;
using Api.Repositories;
using Api.Services;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class AddContactStatus
    {
        public bool IsContactAdded { get; set; }
        public string Message { get; set; }

    }


    public class GetContactResponse
    {
        public List<ContactViewModel> Contacts { get; set; }
        public bool ContactResponse { get; set; }

    }

    public class GetAcademicResponse
    {
        public List<AcademicViewModel> Academics { get; set; }
        public bool Response { get; set; }

    }

    public class GetProfQualificationResponse
    {
        public List<ProfQualificationViewModel> Profs { get; set; }
        public bool Response { get; set; }

    }
    public class GetExperienceResponse
    {
        public List<ExperienceViewModel> Experiences { get; set; }
        public bool Response { get; set; }

    }

    public class GetInfoResponse
    {
        public PersonalInfoViewModel ProfileInfo { get; set; }
        public bool Response { get; set; }

    }

    public class AddAddressResponse
    {
        public bool IsAddressAdded { get; set; }
        public string Message { get; set; }

    }

    public class GetAddressResponse
    {
        public PermanentAddressViewmodel PermanentAddress { get; set; }

        public AddressViewModel PresentAddress { get; set; }
        public bool AddressFound { get; set; }

    }



    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        private readonly IEmployeeInfoRepository _employeeInfoRepository;
        private readonly IAcademicsRepository _acdemicsRepository;
        private readonly ISectionRepository _profQualificationRepository;
        private readonly ICourseRepository _experienceRepository;

        public EmployeeController(IContactRepository contactRepository, IEmployeeInfoRepository employeeInfoRepository, IAcademicsRepository academicsRepository, ISectionRepository profQualificationRepository, ICourseRepository experienceRepository )
        {
            _contactRepository = contactRepository;
            _employeeInfoRepository = employeeInfoRepository;
            _acdemicsRepository = academicsRepository;
            _profQualificationRepository = profQualificationRepository;
            _experienceRepository = experienceRepository;

        }

        [HttpPost("AddContact")]
    public async Task<ActionResult<AddContactStatus>> AddContact(AddContactRequest request)
    {
            var newContact = new Contact
            {
                BelongsTo = request.BelongsTo,
                Type = request.Type,
                Name = request.Name,
                Relation = request.Relation,
                Address = request.Address,
                Mobile = request.Mobile,
                Email = request.Email,
            };

            _contactRepository.Add(newContact);
           await  _contactRepository.Commit();

            var contactStatus = new AddContactStatus
            {
                IsContactAdded = true,
                Message = "Contact Added Succesfully."
            };
            return contactStatus;

        }


        [HttpGet("GetContact")]
        public async Task<ActionResult<GetContactResponse>> GetContact([FromQuery]GetContactRequest request)
        {
            var contactResponse = new GetContactResponse
            {
                Contacts = new List<ContactViewModel>(),
                ContactResponse = false
            };

            var contacts = await _contactRepository.GetAllAsync(x=> x.BelongsTo == request.BelongsTo && x.Type == request.Type);
                
            if(contacts is null)
            {
                return contactResponse;
            }
            foreach(Contact contact in contacts)
            {
                var contactView = new ContactViewModel
                {
                    Name = contact.Name,
                    Relation = contact.Relation,
                    Mobile = contact.Mobile,
                    Address = contact.Address,
                    Email = contact.Email

                };
                contactResponse.Contacts.Add(contactView);
            }
            contactResponse.ContactResponse = true;


            return contactResponse;
        }


        //[HttpPost("AddAddress")]
        //public async Task<ActionResult<AddAddressResponse>> AddAddress(AddAddressRequest request)
        //{
        //    var addressResponse = new AddAddressResponse
        //    {
        //        IsAddressAdded = false,
        //        Message = ""
        //    };
        //    if (request.Type == "permanent")
        //    {
        //        var newAddress = new EmployeeAddress
        //        {
        //            BelongsTo = request.BelongsTo,
        //            Type = request.Type,
        //            Address =request.Address,
        //            Upazilla = request.Upazilla,
        //            District = request.District,
        //            Phone = request.Phone
        //        };

        //        _employeeAddressRepository.Add(newAddress);
        //        await _employeeAddressRepository.Commit();

        //        addressResponse.IsAddressAdded = true;
        //        addressResponse.Message = "Permanent Address Added.";

        //        return addressResponse;

        //    }
        //    else
        //    {
        //        var newAddress = new EmployeeAddress
        //        {
        //            BelongsTo = request.BelongsTo,
        //            Type = request.Type,
        //            Address = request.Address,
        //            City = request.City,
        //            Zip = request.Zip,
        //            Phone = request.Phone,
        //            Mobile = request.Mobile,
        //            Email = request.Email,
        //            AlternateEmail = request.AlternateEmail
        //        };

        //        _employeeAddressRepository.Add(newAddress);
        //        await _employeeAddressRepository.Commit();

        //        addressResponse.IsAddressAdded = true;
        //        addressResponse.Message = "Present Address Added.";

        //        return addressResponse;

        //    }
        //}

        //[HttpGet("GetAddress")]
        //public async Task<ActionResult<GetAddressResponse>> GetAddress ([FromQuery]GetAddressRequest request)
        //{
        //    var addressResponse = new GetAddressResponse
        //    {
        //        PermanentAddress = null,
        //        PresentAddress = null,
        //        AddressFound = false

        //    };

        //    var address = await _employeeAddressRepository.GetSingle(x => x.BelongsTo == request.BelongsTo && x.Type == request.Type);

        //    if (address is null)
        //        return addressResponse;

        //    if(request.Type == "permanent")
        //    {
        //        var permanentView = new PermanentAddressViewmodel
        //        {
        //            Address = address.Address,
        //            Upazilla = address.Upazilla,
        //            District = address.District,
        //            Phone = address.Phone
        //        };
        //        addressResponse.PermanentAddress = permanentView;
        //        addressResponse.AddressFound = true;

        //        return addressResponse;
        //    }
        //    else
        //    {
        //        var presentView = new AddressViewModel
        //        {
        //            Address = address.Address,
        //            City = address.City,
        //            Zip = address.Zip,
        //            Phone = address.Phone,
        //            Mobile = address.Mobile,
        //            Email = address.Email,
        //            AlternateEmail = address.AlternateEmail
        //        };
        //        addressResponse.PresentAddress = presentView;
        //        addressResponse.AddressFound = true;

        //        return addressResponse;
        //    }

        //}

        [HttpPost("CreateInfo")]
        public async Task<ActionResult<bool>> CreateInfo(AddInfoRequest request)
        {
           try
            {
                var employeeInfo = new EmployeeInfo
                {
                    BelongsTo = request.BelongsTo,
                    Pin = request.Pin,
                    Salutation = request.Salutation,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    FatherName = request.FatherName,
                    MotherName = request.MotherName,
                    Gender = request.Gender,
                    BirthDate = DateTime.Parse(request.BirthDate),
                    BirthPlace = request.BirthPlace,
                    BloodGroup = request.BloodGroup,
                    Nationality = request.Nationality,
                    Religion = request.Religion,
                    MaritalStatus = request.MaritalStatus,
                    NumberOfSons = request.NumberOfSons,
                    NumberOfDaughters = request.NumberOfDaughters,
                    CardNo = request.CardNo,
                    TinNo = request.TinNo,
                    PassportNo = request.PassportNo,
                    NidNumber = request.NidNumber,
                    DrivingLicense = request.DrivingLicense,
                    ExtraCurriculum = request.ExtraCurriculum,
                    Remarks = request.Remarks
                };

                if(request.MaritalStatus == "married")
                {
                    employeeInfo.SpouseName = request.SpouseName;
                }

                _employeeInfoRepository.Add(employeeInfo);
                await _employeeInfoRepository.Commit();

                return true;
            }
            catch
            {
                return false;
            }
        }


        [HttpPost("UpdateInfo")]
        public async Task<ActionResult<bool>> UpdateInfo(AddInfoRequest request)
        {
            try
            {
                var employeeInfo = await _employeeInfoRepository.GetSingle(x=>x.BelongsTo == request.BelongsTo);
                
                if(request.Pin != 0)
                {
                    employeeInfo.Pin = request.Pin;
                }
                if (request.Salutation != null)
                {
                    employeeInfo.Salutation = request.Salutation;
                }
                if (request.FirstName != null)
                {
                    employeeInfo.FirstName = request.FirstName;
                }
                if (request.LastName != null)
                {
                    employeeInfo.LastName = request.LastName;
                }
                if (request.FatherName != null)
                {
                    employeeInfo.FatherName = request.FatherName;
                }
                if (request.MotherName != null)
                {
                    employeeInfo.MotherName = request.MotherName;
                }
                if (request.Gender != null)
                {
                    employeeInfo.Gender = request.Gender;
                }
                if (request.BirthDate != null)
                {
                    employeeInfo.BirthDate = DateTime.Parse(request.BirthDate);
                }
                if (request.BirthPlace != null)
                {
                    employeeInfo.BirthPlace = request.BirthPlace;
                }
                if (request.BloodGroup != null)
                {
                    employeeInfo.BloodGroup = request.BloodGroup;
                }

                if (request.Nationality != null)
                {
                    employeeInfo.Nationality = request.Nationality;
                }
                if (request.Religion != null)
                {
                    employeeInfo.Religion = request.Religion;
                }

                if (request.MaritalStatus != null)
                {
                    employeeInfo.MaritalStatus = request.MaritalStatus;
                }
                if (request.NumberOfSons != 0)
                {
                    employeeInfo.NumberOfSons = request.NumberOfSons;
                }
                if (request.NumberOfDaughters != 0)
                {
                    employeeInfo.NumberOfDaughters = request.NumberOfDaughters;
                }

                if (request.CardNo != 0)
                {
                    employeeInfo.CardNo = request.CardNo;
                }
                if (request.TinNo != 0)
                {
                    employeeInfo.TinNo = request.TinNo;
                }
                if (request.PassportNo != 0)
                {
                    employeeInfo.PassportNo = request.PassportNo;
                }

                if (request.NidNumber != 0)
                {
                    employeeInfo.NidNumber = request.NidNumber;
                }

                if (request.DrivingLicense != 0)
                {
                    employeeInfo.DrivingLicense = request.DrivingLicense;
                }
                if (request.ExtraCurriculum != null)
                {
                    employeeInfo.ExtraCurriculum = request.ExtraCurriculum;
                }
                if (request.Remarks != null)
                {
                    employeeInfo.Remarks = request.Remarks;
                }

                if (request.MaritalStatus == "married")
                {
                    employeeInfo.SpouseName = request.SpouseName;
                }

                _employeeInfoRepository.Update(employeeInfo);
                await _employeeInfoRepository.Commit();

                return true;
            }
            catch
            {
                return false;
            }
        }



        [HttpGet("GetInfo")]
        public async Task<ActionResult<GetInfoResponse>> GetInfo([FromQuery] string belongsTo)
        {
            var infoResponse = new GetInfoResponse
            {
                ProfileInfo = null,
                Response =  false,
            };
            var profile = await _employeeInfoRepository.GetSingle(x => x.BelongsTo == belongsTo);

            if (profile is null)
            {
                return infoResponse;
            }

            var infoViewModel = new PersonalInfoViewModel
                {
                    BelongsTo = profile.BelongsTo,
                    Pin = profile.Pin,
                    Salutation = profile.Salutation,
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    FatherName = profile.FatherName,
                    MotherName = profile.MotherName,
                    Gender = profile.Gender,
                    BirthDate = profile.BirthDate.ToString("yyyy-MM-dd"),
                    BirthPlace = profile.BirthPlace,
                    BloodGroup = profile.BloodGroup,
                    Nationality = profile.Nationality,
                    Religion = profile.Religion,
                    MaritalStatus = profile.MaritalStatus,
                    NumberOfSons = profile.NumberOfSons,
                    NumberOfDaughters = profile.NumberOfDaughters,
                    CardNo = profile.CardNo,
                    TinNo = profile.TinNo,
                    PassportNo = profile.PassportNo,
                    NidNumber = profile.NidNumber,
                    DrivingLicense = profile.DrivingLicense,
                    ExtraCurriculum = profile.ExtraCurriculum,
                    Remarks = profile.Remarks
                };

                if (profile.MaritalStatus == "married")
                {
                    infoViewModel.SpouseName = profile.SpouseName;
                }

            infoResponse.ProfileInfo = infoViewModel;
            infoResponse.Response = true;
                

            return infoResponse;
        }


        [HttpPost("AddAcademic")]
        public async Task<ActionResult<bool>> AddAcademic(AddAcademicRequest request)
        {

            try
            {
                var academic = new Academic
                {
                    BelongsTo = request.BelongsTo,
                    Degree = request.Degree,
                    ExamTitle = request.ExamTitle,
                    Institute = request.Institute,
                    BoardOrCountry = request.BoardOrCountry,
                    MajorOrGroup = request.MajorOrGroup,
                    Result = request.Result,
                    CgpaOrMarks = request.CgpaOrMarks,
                    Scale = request.Scale,
                    PassedYear = request.PassedYear,
                    Duration = request.Duration,
                    Remarks = request.Remarks,
                    Achievement = request.Achievement,
                };

                _acdemicsRepository.Add(academic);
                await _acdemicsRepository.Commit();
                return true;
            }
            catch
            {
                return false;
            }
        }


        [HttpGet("GetAcademic")]
        public async Task<ActionResult<GetAcademicResponse>> GetAcademic([FromQuery] string belongsTo)
        {
            var academicResponse = new GetAcademicResponse
            {
                Academics = new List<AcademicViewModel>(),
                Response = false,
            };
            var academics = await _acdemicsRepository.GetAllAsync(x => x.BelongsTo == belongsTo);
            if (academics is null)
            {
                return academicResponse;
            }

            foreach (Academic academic in academics)
            {
                var academicView = new AcademicViewModel
                {
                    Id = academic.Id,
                    Degree = academic.Degree,
                    ExamTitle = academic.ExamTitle,
                    Institute = academic.Institute,
                    BoardOrCountry = academic.BoardOrCountry,
                    MajorOrGroup = academic.MajorOrGroup,
                    Result = academic.Result,
                    CgpaOrMarks = academic.CgpaOrMarks,
                    Scale = academic.Scale,
                    PassedYear = academic.PassedYear,
                    Duration = academic.Duration,
                    Remarks = academic.Remarks,
                    Achievement = academic.Achievement,
                };
                academicResponse.Academics.Add(academicView);
            }
            academicResponse.Response = true;


            return academicResponse;

        }



        //[HttpPost("AddProfQualification")]
        //public async Task<ActionResult<bool>> AddProfQualification(AddProfRequest request)
        //{

        //    try
        //    {
        //        var ProfQualification = new Section
        //        {
        //            BelongsTo = request.BelongsTo,
        //            CourseType = request.CourseType,
        //            CourseTitle = request.CourseTitle,
        //            Institute = request.Institute,
        //            Address = request.Address,
        //            Duration = request.Duration,
        //            Result = request.Result,
        //            Year = request.Year,
        //            Achievement = request.Achievement,
        //        };

        //        _profQualificationRepository.Add(ProfQualification);
        //        await _profQualificationRepository.Commit();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //[HttpGet("GetProfQualification")]
        //public async Task<ActionResult<GetProfQualificationResponse>> GetProfQualification([FromQuery] string belongsTo)
        //{
        //    var profResponse = new GetProfQualificationResponse
        //    {
        //        Profs = new List<ProfQualificationViewModel>(),
        //        Response = false,
        //    };
        //    var profs = await _profQualificationRepository.GetAllAsync(x => x.BelongsTo == belongsTo);
        //    if (profs is null)
        //    {
        //        return profResponse;
        //    }

        //    foreach (Section prof in profs)
        //    {
        //        var profView = new ProfQualificationViewModel
        //        {
        //            Id = prof.Id,
        //            CourseType = prof.CourseType,
        //            CourseTitle = prof.CourseTitle,
        //            Institute = prof.Institute,
        //            Address = prof.Address,
        //            Duration = prof.Duration,
        //            Result = prof.Result,
        //            Year = prof.Year,
        //            Achievement = prof.Achievement,
        //        };
        //        profResponse.Profs.Add(profView);
        //    }
        //    profResponse.Response = true;


        //    return profResponse;

        //}


        //[HttpPost("AddExperience")]
        //public async Task<ActionResult<bool>> AddExperience(AddExperienceRequest request)
        //{

        //    try
        //    {
        //        var experiene = new Course
        //        {
        //            BelongsTo = request.BelongsTo,
        //            Company = request.Company,
        //            Business = request.Business,
        //            Location = request.Location,
        //            PositionHeld = request.PositionHeld,
        //            JobNature = request.JobNature,
        //            Responsibilities = request.Responsibilities,
        //            FromDate = request.FromDate,
        //            ToDate = request.ToDate,
        //            Department = request.Department,
        //            LastSalary = request.LastSalary,
        //            Remarks = request.Remarks
        //        };

        //        _experienceRepository.Add(experiene);
        //        await _experienceRepository.Commit();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //[HttpGet("GetExperience")]
        //public async Task<ActionResult<GetExperienceResponse>> GetExperience([FromQuery] string belongsTo)
        //{
        //    var response = new GetExperienceResponse
        //    {
        //        Experiences = new List<ExperienceViewModel>(),
        //        Response = false,
        //    };
        //    var experiences = await _experienceRepository.GetAllAsync(x => x.BelongsTo == belongsTo);
        //    if (experiences is null)
        //    {
        //        return response;
        //    }

        //    foreach (Course experience in experiences)
        //    {
        //        var experienceView = new ExperienceViewModel
        //        {
        //            Id = experience.Id,
        //            Company = experience.Company,
        //            Business = experience.Business,
        //            Location = experience.Location,
        //            PositionHeld = experience.PositionHeld,
        //            JobNature = experience.JobNature,
        //            Responsibilities = experience.Responsibilities,
        //            FromDate = experience.FromDate.ToString("MM/dd/yyyy"),
        //            ToDate = experience.ToDate.ToString("MM/dd/yyyy"),
        //            Department = experience.Department,
        //            LastSalary = experience.LastSalary,
        //            Remarks = experience.Remarks
        //        };
        //        response.Experiences.Add(experienceView);
        //    }
        //    response.Response = true;


        //    return response;

        //}


        [HttpGet("Remove")]
        public async Task<ActionResult<bool>> Remove([FromQuery] string id, string repoType)
        {
            //var leave = await _leaveRepository.GetById(id);
            //var userLeaveCount = await _leaveCountRepository.GetSingle(x => x.BelongsTo == leave.BelongsTo);

            try
            {
                switch (repoType)
                {
                    case "academic":
                        {
                        _acdemicsRepository.Remove(id);
                        await _acdemicsRepository.Commit();
                        break;
                        }

                    case "prof":
                        {
                        _profQualificationRepository.Remove(id);
                        await _profQualificationRepository.Commit();
                        break;
                    }
                    case "experience":
                        {
                        _experienceRepository.Remove(id);
                        await _experienceRepository.Commit();
                        break;
                    }
                };

            }
            catch (Exception)
            {

                return false;
            }

            return true;
        }

    }

}
