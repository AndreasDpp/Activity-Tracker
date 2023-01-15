using ActivityTracker.Backend.Repository.Domain.Documents;
using ActivityTracker.Backend.Service.DTO;
using AutoMapper;

namespace ActivityTracker.Backend.Repository.Mappers
{
    public class ActivityMapper : Profile
    {
        public ActivityMapper()
        {
            CreateMap<ActivityDocument, ActivityDTO>().ReverseMap();
            CreateMap<ActivityDocument, StartActivityDTO>().ReverseMap();
            CreateMap<ActivityDocument, EndActivityDTO>().ReverseMap();
        }
    }
}
