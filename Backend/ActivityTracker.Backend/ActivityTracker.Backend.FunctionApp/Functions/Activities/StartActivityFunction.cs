using System.Net;
using ActivityTracker.Backend.FunctionApp.Models;
using ActivityTracker.Backend.FunctionApp.Validation;
using ActivityTracker.Backend.Service.Inferfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ActivityTracker.Backend.FunctionApp.Functions.Activities
{
    public class StartActivityFunction
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
        public StartActivityFunction(ILoggerFactory loggerFactory,
            IActivityService activityService)
        {
            _logger = loggerFactory.CreateLogger<StartActivityFunction>();
            _activityService = activityService;
        }

        #endregion


        /// <summary>
        /// HTTP Trigger function - Starts a new activity
        /// </summary>
        /// <param name="req">Body with start time of the activity</param>
        /// <returns>The added activity</returns>
        [Function("StartActivityFunction")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/activity/start")] HttpRequestData req)
        {
            HttpResponseData response = null;

            var json = await req.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json))
            {
                response = req.CreateResponse(HttpStatusCode.BadRequest);
                response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

                await response.WriteStringAsync(JsonConvert.SerializeObject(new ErrorModel { Error = "Empty input" }));
                return response;
            }

            var input = JsonConvert.DeserializeObject<StartActivityModel>(json);

            #region Input validation

            var validator = new StartActivityValidator();

            var validationResult = validator.Validate(input);

            if (!validationResult.IsValid)
            {
                var errorMessage = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage).ToArray());
                _logger.LogDebug($"Start activity invalid input error: { errorMessage }");

                response = req.CreateResponse(HttpStatusCode.BadRequest);
                response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

                await response.WriteStringAsync(JsonConvert.SerializeObject(new ErrorModel { Error = errorMessage }));
                return response;
            }

            #endregion

            var activity = await _activityService.StartAsync(new Service.DTO.StartActivityDTO
            {
                Start = input.Time,
                Description = input.Description
            });

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
            _logger.LogDebug($"Start activity - Response body: { jsonToReturn }");

            return response;
        }
    }
}
