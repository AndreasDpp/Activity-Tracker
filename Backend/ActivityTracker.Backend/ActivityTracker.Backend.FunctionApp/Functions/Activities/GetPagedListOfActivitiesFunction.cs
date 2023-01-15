using ActivityTracker.Backend.FunctionApp.Models;
using ActivityTracker.Backend.Service.Inferfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace ActivityTracker.Backend.FunctionApp.Functions.Activities
{
    public class GetPagedListOfActivitiesFunction
    {

        #region Fields

        private readonly ILogger _logger;
        private readonly IActivityService _activityService;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="activityService"></param>
        public GetPagedListOfActivitiesFunction(ILoggerFactory loggerFactory,
            IActivityService activityService)
        {
            _logger = loggerFactory.CreateLogger<StartActivityFunction>();
            _activityService = activityService;
        }

        #endregion

        /// <summary>
        /// HTTP Trigger function - Gets a paged list of activities
        /// </summary>
        /// <param name="req"></param>
        /// <param name="pageNumber">The page number</param>
        /// <param name="pageSize">The page size</param>
        /// <returns>A list of activities</returns>
        [Function("GetPagedListOfActivitiesFunction")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/activity/paged/{pageNumber}/{pageSize}")] HttpRequestData req,
            int pageNumber,
            int pageSize)
        {
            HttpResponseData response = null;

            #region Input validation

            if (pageNumber <= 0)
            {
                response = req.CreateResponse(HttpStatusCode.BadRequest);
                await response.WriteStringAsync(JsonConvert.SerializeObject(new ErrorModel { Error = "Invalid page number, it can't be lower than 1." }));
                return response;
            }

            if (pageSize <= 0)
            {
                response = req.CreateResponse(HttpStatusCode.BadRequest);
                await response.WriteStringAsync(JsonConvert.SerializeObject(new ErrorModel { Error = "Invalid page size, it can't be lower than 1." }));
                return response;
            }

            #endregion

            var activities = await _activityService.GetPagedAsync(pageNumber, pageSize);

            response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");

            var metadata = new
            {
                activities.TotalCount,
                activities.PageSize,
                activities.CurrentPage,
                activities.TotalPages,
                activities.HasNext,
                activities.HasPrevious
            };

            response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            var jsonToReturn = JsonConvert.SerializeObject(activities.Select(x => new ActivityModel
                {
                    Id = x.Id,
                    StartTime = x.Start,
                    Description = x.Description,
                    EndTime = x.End
                }).ToList());

            response.WriteString(jsonToReturn);

            _logger.LogDebug($"Get paged list of activities - Response body: { jsonToReturn }");

            return response;
        }
    }
}
