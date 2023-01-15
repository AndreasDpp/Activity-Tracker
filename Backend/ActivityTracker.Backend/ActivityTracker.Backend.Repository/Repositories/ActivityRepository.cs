using ActivityTracker.Backend.Repository.Domain.Documents;
using ActivityTracker.Backend.Repository.Domain.Settings;
using ActivityTracker.Backend.Repository.Interfaces;

namespace ActivityTracker.Backend.Repository.Repositories
{
    public class ActivityRepository : GenericMongoRepository<ActivityDocument>, IActivityRepository
    {
        public ActivityRepository(MongoDbSettings settings) : base(settings)
        {
        }
    }
}
