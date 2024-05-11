using Api.Models;
using MongoDB.Bson.Serialization;
namespace Api.Mapper
{
    public class DepartmentMapper
    {
        public static void Map()
        {
            // Mapping should be done once. So, checking if the class has
            // already been registered
            if (!BsonClassMap.IsClassMapRegistered(typeof(Department)))
            {
                BsonClassMap.RegisterClassMap<Department>(x =>
                {
                    x.AutoMap();
                    x.MapProperty(e => e.CourseDistribution).SetElementName("courseDistribution");
                    x.MapProperty(e => e.Name).SetElementName("name");
                    x.MapProperty(e => e.Abbreviation).SetElementName("abbreviation");
                    x.MapProperty(e => e.TotalCredits).SetElementName("totalCredits");
                    x.MapProperty(e => e.MinimumCreditPerSem).SetElementName("minimumCreditPerSem");
                    x.MapProperty(e => e.DepartmentCode).SetElementName("departmentCode");
                });
            }
        }
    }
}
