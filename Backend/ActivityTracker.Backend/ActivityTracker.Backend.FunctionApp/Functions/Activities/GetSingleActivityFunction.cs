using ActivityTracker.Backend.FunctionApp.Models;
using ActivityTracker.Backend.Service.Inferfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace ActivityTracker.Backend.FunctionApp.Functions.Activities
{
    public class GetSingleActivityFunction
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
        public GetSingleActivityFunction(ILoggerFactory loggerFactory,
            IActivityService activityService)
        {
            _logger = loggerFactory.CreateLogger<StartActivityFunction>();
            _activityService = activityService;
        }

        #endregion

        /// <summary>
        /// Htpp trigger function - Get a single activity by id
        /// </summary>
        /// <param name="req"></param>
        /// <param name="id">Activity id</param>
        /// <returns>A single activity</returns>
        [Function("GetSingleActivityFunction")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/activity/single/{id}")] HttpRequestData req,
            string id)
        {
            HttpResponseData response = null;

            #region Input validation

            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogDebug($"Get single activity invalid input");

                response = req.CreateResponse(HttpStatusCode.BadRequest);
                response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

                await response.WriteStringAsync(JsonConvert.SerializeObject(new ErrorModel { Error = "Invalid input" }));
                return response;
            }

            #endregion

            var activity = await _activityService.GetAsync(id);


            if (activity == null)
            {
                response = req.CreateResponse(HttpStatusCode.NotFound);
                return response;
                
            }

            response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");

            var jsonToReturn = JsonConvert.SerializeObject(new ActivityModel
            {
                Id = activity.Id,
                StartTime = activity.Start,
                Description = activity.Description,
                EndTime = activity.End,
            });

            response.WriteString(jsonToReturn);
            _logger.LogDebug($"Get single activity - Response body: { jsonToReturn }");

            return response;
        }
    }
}
