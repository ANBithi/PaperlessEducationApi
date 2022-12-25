using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
    }

    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(IDbContext context) : base(context)
        {

        }
    }
}
