using System;
using System.Runtime.Remoting;
using System.Threading.Tasks;
using WindowsPartyBase.Configuration;
using WindowsPartyBase.Interfaces;
using log4net;
using Newtonsoft.Json;
using RestSharp;

namespace WindowsPartyBase.Services
{
    public class RestClientBase : IRestClientBase
    {
        public RestClient RestClient { get; private set; }
        private readonly ILog _log;
        private readonly IUserService _userService;
        public RestClientBase(IUserService userService)
        {
            _userService = userService;
            _log = LogManager.GetLogger("ServerRequest");
            BaseInitialize(StaticConfigurations.ServerUrl);
        }
     
        public async Task<T> GetAsync<T>(string uri, bool noLogging = false) where T : class, new()
        {
            var request = new RestRequest(uri, Method.GET);
            return await ExecuteAsync<T>(request, noLogging);
        }

        public async Task<T> PostAsync<T>(string uri, object value, bool noLogging = false) where T : class, new()
        {
            var request = new RestRequest(uri, Method.POST);
            request.AddJsonBody(value);
            return await ExecuteAsync<T>(request, noLogging);
        }

        private void BaseInitialize(string server)
        {
            RestClient = new RestClient
            {
                BaseUrl = new Uri(server),
            };

            RestClient.UseSerializer(() => new CustomSerializer());
        }

        private T GetResponse<T>(IRestRequest request, IRestResponse<T> response, bool noLogging = false) where T : class
        {
            _log.Info($@"Request {request.Resource}");
            if (!noLogging)
                _log.Debug($"Request data: {JsonConvert.SerializeObject(JsonConvert.SerializeObject(request.Body))}");

            if (!response.IsSuccessful)
            {
                _log.Error($@"Status {response.StatusCode} : {response.StatusDescription} - {response.Content}");
                return null;
            }

            var responseData = response.Data;

            if(!noLogging)
                _log.Debug($"Response: {JsonConvert.SerializeObject(responseData)}");

            return responseData;
        }

        public async Task<T> ExecuteAsync<T>(RestRequest request, bool noLogging = false) where T : class, new()
        {
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-type", "application/json");
            
            if (_userService.IsLoggedIn())
                request.AddHeader("Authorization", $@"Bearer {_userService.GetToken()}");

            var taskCompletionSource = new TaskCompletionSource<T>();
            RestClient.ExecuteAsync<T>(request, response =>
            {
                var parsedResponse = GetResponse(request, response, noLogging);
                taskCompletionSource.SetResult(parsedResponse);
            });

            return await taskCompletionSource.Task;
        }


    }
}
