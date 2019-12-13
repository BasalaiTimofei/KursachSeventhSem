using Backend.Context;
using Backend.Models.Database;

namespace Backend.Repository
{
    public class AssessmentRepository : EfCoreRepository<Assessment, ApplicationContext>
    {
        public AssessmentRepository(ApplicationContext context) : base(context)
        {
        }
    }
}