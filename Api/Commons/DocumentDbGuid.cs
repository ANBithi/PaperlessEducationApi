using MongoDB.Bson;

namespace Api.Commons
{
    public class DocumentDbGuid : IEntityId
    {
        public string GetNewId()
        {
            return ObjectId.GenerateNewId().ToString();
        }
    }
}
