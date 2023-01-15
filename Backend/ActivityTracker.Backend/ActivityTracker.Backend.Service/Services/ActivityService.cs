using ActivityTracker.Backend.Repository.Domain.Documents;
using ActivityTracker.Backend.Repository.Interfaces;
using ActivityTracker.Backend.Service.DTO;
using ActivityTracker.Backend.Service.Inferfaces;
using AutoMapper;

namespace ActivityTracker.Backend.Service.Services
{
    public class ActivityService : IActivityService
    {
        #region Fields

        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="activityRepository"></param>
        /// <param name="mapper"></param>
        public ActivityService(IActivityRepository activityRepository, IMapper mapper)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;

        }

        #endregion

        public async Task<ActivityDTO> StartAsync(StartActivityDTO startDTO)
        {
            var activity = _mapper.Map<ActivityDocument>(startDTO);

            activity.Start = startDTO.Start.ToUniversalTime();

            await _activityRepository.InsertOneAsync(activity);

            return _mapper.Map<ActivityDTO>(activity);
        }

        public async Task<ActivityDTO> EndAsync(EndActivityDTO endDTO)
        {
            var acitvityDTO = await GetAsync(endDTO.Id);

            if(acitvityDTO == null)
            {
                return null;
            }

            var acitvity = _mapper.Map<ActivityDocument>(acitvityDTO);
            acitvity.End = endDTO.End.ToUniversalTime();

            await _activityRepository.ReaplceOneAsync(acitvity);

            return _mapper.Map<ActivityDTO>(acitvity);
        }

        public async Task<ActivityDTO> GetAsync(string id)
        {
            var activity = await _activityRepository.FindByIdAsync(id);

            return _mapper.Map<ActivityDTO>(activity);
        }

        public async Task<PagedListDTO<ActivityDTO>> GetPagedAsync(int pageNumber, int pageSize = 20)
        {
            var pagedActivities = await _activityRepository.FindManyPagedAsync(a => true, pageNumber, pageSize);

            var items = _mapper.Map<List<ActivityDTO>>(pagedActivities.Item2);

            var pagedList = new PagedListDTO<ActivityDTO>(items, pagedActivities.Item1, pageNumber, pageSize);

            return pagedList;
        }


    }
}
