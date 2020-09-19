using Core.ServiceDiscovery;
using Core.ServiceDiscovery.Impletment.LoadBalancer;
using Core.ServiceDiscovery.Impletment.Provider;
using Microsoft.Extensions.Logging;
using MicService.Abstractions;
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
        public async Task<UserIdentity> GetBaseUserInfoAsync(int userId)
        {
            #region ServicesDiscovery
            var serviceProvider = new ConsulServiceProvider(new Uri("http://127.0.0.1:8500"));

            var _userServiceUrl = serviceProvider.CreateServiceBuilder(builder =>
            {
                builder.ServiceName = "UserApi";
                builder.LoadBalancer = TypeLoadBalancer.RandomLoad;
                builder.UriScheme = Uri.UriSchemeHttp;
            }).BuildAsync("/api/user/checkcreate");
            #endregion

            //var form = new Dictionary<string, string>() { { "phone", phone } };
            //try
            //{
            //    var response = await _httpClient.PostAsync(_userServiceUrl.ToString(), new FormUrlEncodedContent(form));
            //    if (response.StatusCode == HttpStatusCode.OK)
            //    {
            //        var userId = await response.Content.ReadAsStringAsync();
            //        int.TryParse(userId, out int result);
            //        return result;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError("在重试之后失败");
            //    throw new Exception(ex.Message);
            //}
            //return 0;
            return null;
        }
    }
}
