using Api.Enums;

namespace Api.ViewModels
{
    public class UserProfile
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public UserTypeEnum UserType { get; set; }
    }
    public class StudentProfileViewModel : UserProfile
    {
        public string StudentId { get; set; }
        public string Batch { get; set; }
        public int AdmissionYear { get; set; }
        public FacultyProfileViewModel Advisor { get; set; }
    }
    public class FacultyProfileViewModel : UserProfile
    {
        public string Designation { get; set; }
    }
}