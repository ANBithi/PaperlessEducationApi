using Api.Models.UserSpecific;

namespace Api.Repositories
{
    public interface IPasswordMetadataRepository  : IRepository<PasswordMetadata>
    {
    }


    public class PasswordMetadataRepository : BaseRepository<PasswordMetadata>, IPasswordMetadataRepository
    {
        public PasswordMetadataRepository(IDbContext context) : base(context)
        {

        }
    }
}
