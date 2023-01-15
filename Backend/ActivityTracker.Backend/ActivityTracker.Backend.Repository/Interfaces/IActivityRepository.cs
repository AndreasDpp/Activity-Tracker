using ActivityTracker.Backend.Repository.Domain.Documents;

namespace ActivityTracker.Backend.Repository.Interfaces
{
    public interface IActivityRepository: IGenericMongoRepository<ActivityDocument>
    {

    }
}
