using Api.Models;

namespace Api.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
    }


    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(IDbContext context) : base(context)
        {

        }
    }
}
