using Api.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models
{
    public class User : AbstractDbEntity
    {
        [BsonElement("firstName")]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        public string LastName { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonIgnoreIfDefault(false)]
        [BsonElement("userType")]
        public UserTypeEnum UserType { get; set; }

       
        [BsonElement("isActive")]
        public bool IsActive { get; set; }
    }

}
