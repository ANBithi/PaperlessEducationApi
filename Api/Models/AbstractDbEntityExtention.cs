using System;

namespace Api.Models
{
    public static class AbstractDbEntityExtention
    {
        public static void UpdateCreatedByFields(this AbstractDbEntity dbEntity, User user, DateTime dateTime)
        {
            dbEntity.CreatedById = user.Id; ;
            dbEntity.CreatedBy = $"{user.FirstName} {user.LastName}";
            dbEntity.CreatedAt = dateTime.ToUniversalTime();
            dbEntity.ModifiedById = user.Id; ;
            dbEntity.ModifiedBy = $"{user.FirstName} {user.LastName}";
            dbEntity.ModifiedAt = dateTime.ToUniversalTime();
        }
        public static void UpdateModifiedByFields(this AbstractDbEntity dbEntity, User user, DateTime dateTime)
        {
            dbEntity.ModifiedById = user.Id; ;
            dbEntity.ModifiedBy = $"{user.FirstName} {user.LastName}";
            dbEntity.ModifiedAt = dateTime.ToUniversalTime();
        }
    }
}
