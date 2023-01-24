using Api.Models.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface IExamMetadataRepository : IRepository<ExamMetadata>
    {
    }
    public class ExamMetadataRepository : BaseRepository<ExamMetadata>, IExamMetadataRepository
    {
        public ExamMetadataRepository(IDbContext context) : base(context)
        {
        }
    }
}
