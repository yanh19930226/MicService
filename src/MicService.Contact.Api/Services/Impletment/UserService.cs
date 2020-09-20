using Core.ServiceDiscovery;
using Core.ServiceDiscovery.Impletment.LoadBalancer;
using Core.ServiceDiscovery.Impletment.Provider;
using Microsoft.Extensions.Logging;
using MicService.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MicService.Contact.Api.Services.Impletment
{
    public class UserService : IUserService
    {
        private HttpClient _httpClient;
        private readonly ILogger<UserService> _logger;
        public UserService(HttpClient httpClient, ILogger<UserService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<UserInfo> GetBaseUserInfoAsync(int userId)
        {
            #region ServicesDiscovery
            var serviceProvider = new ConsulServiceProvider(new Uri("http://127.0.0.1:8500"));

            var _userServiceUrl = serviceProvider.CreateServiceBuilder(builder =>
            {
                builder.ServiceName = "UserApi";
                builder.LoadBalancer = TypeLoadBalancer.RandomLoad;
                builder.UriScheme = Uri.UriSchemeHttp;
            });
            #endregion

            var client = new HttpClient();
            var response = client.GetStringAsync(_userServiceUrl + "/api/user/getuserinfo/" + userId).Result;
            if (!string.IsNullOrEmpty(response))
            {
                var userInfo = JsonConvert.DeserializeObject<UserInfo>(response);
                return userInfo;
            }
            return null;
        }
    }
}
