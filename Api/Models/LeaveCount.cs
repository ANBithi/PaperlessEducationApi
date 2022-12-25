using Api.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models
{
    public class LeaveCount : AbstractDbEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("belongsTo")]
        public string BelongsTo { get; set; }

        [BsonElement("leftAnnualLeave")]
        [BsonIgnoreIfDefault(false)]
        public int LeftAnnualLeave { get; set; } = 26;

        [BsonElement("leftSickLeave")]
        [BsonIgnoreIfDefault(false)]
        public int LeftSickLeave { get; set; } = 10;

        [BsonElement("leftChildSickLeave")]
        [BsonIgnoreIfDefault(false)]
        public int LeftChildSickLeave { get; set; } = 5;

        [BsonElement("leftCompassionateLeave")]
        [BsonIgnoreIfDefault(false)]
        public int LeftCompassionateLeave { get; set; } = 3;

        [BsonElement("leftMaternityLeave")]
        [BsonIgnoreIfDefault(false)]
        public int LeftMaternityLeave { get; set; } = 180;

        [BsonElement("leftPaternityLeave")]
        [BsonIgnoreIfDefault(false)]
        public int LeftPaternityLeave { get; set; } = 10;

        [BsonElement("leftPilgrimageLeave")]
        [BsonIgnoreIfDefault(false)]
        public int LeftPilgrimageLeave { get; set; } = 40;

        [BsonElement("leftMovingHouseLeave")]
        [BsonIgnoreIfDefault(false)]
        public int LeftMovingHouseLeave { get; set; } = 1;

    }
}
