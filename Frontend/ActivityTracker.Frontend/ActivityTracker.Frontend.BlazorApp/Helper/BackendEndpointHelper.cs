using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityTracker.Frontend.BlazorApp.Helper
{
    public static class BackendEndpointHelper
    {
        private static readonly string _backendEndpointBaseURL = "http://localhost:7296/";

        private static readonly string _backendEndpointPrefix = "api/v1/";

        private static readonly string _backendEndpointUrlForActivityFunctions = _backendEndpointPrefix + "activity/";

        public static string Base()
        {
            return _backendEndpointBaseURL;
        }

        public static string StartingActivityPOSTEndpoint()
        {
            return _backendEndpointUrlForActivityFunctions + "start";
        }

        public static string EndingActivityPOSTEndpoint()
        {
            return _backendEndpointUrlForActivityFunctions + "end";
        } 

        public static string SingleAcitvityGETEndpoint(string id)
        {
           return  _backendEndpointUrlForActivityFunctions + "single/" + id;
        }

        public static string PagedAcitvitiesGETEndpoint(int pageNumber, int pageSize)
        {
            return _backendEndpointUrlForActivityFunctions + "paged/" + pageNumber + "/" + pageSize;
        }

    }
}
