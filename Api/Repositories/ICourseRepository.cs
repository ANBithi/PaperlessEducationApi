using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
