using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Core;
using Core.Result;
using Core.ServiceDiscovery;
using Core.ServiceDiscovery.Impletment.LoadBalancer;
using Core.ServiceDiscovery.Impletment.Provider;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MicService.Identoty.Api.Services.Impletment;

namespace MicService.Identoty.Api.Controllers
{
    /// <summary>
    /// 用户服务
    /// </summary>
    [Route("api/home")]
    [ApiController]
    public class HomeController : Controller
    {
        private HttpClient _httpClient;
        private readonly ILogger<HomeController> _logger;
        public HomeController(HttpClient httpClient, ILogger<HomeController> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        [HttpGet]
        public async  Task<IActionResult> Index()
        {
            CoreResult coreResult = new CoreResult();

            #region ServiceDiscovery

            var serviceProvider = new ConsulServiceProvider(new Uri("http://127.0.0.1:8500"));

            var _userServiceUrl = serviceProvider.CreateServiceBuilder(builder =>
            {
                builder.ServiceName = "UserApi";
                builder.LoadBalancer = TypeLoadBalancer.RoundRobin;
                builder.UriScheme = Uri.UriSchemeHttp;
            }).BuildAsync("/api/user/servicesdiscovery").Result;

            #endregion

            var polly = PolicyBuilder.CreatePolly();
            polly.Execute(()=> {
                try
                {
                    var form = new Dictionary<string, string>() { { "phone", "18650482503" } };
                    var response =  _httpClient.PostAsync(_userServiceUrl.ToString(), new FormUrlEncodedContent(form)).Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        coreResult.Success(response.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        coreResult.Failed(response.Content.ReadAsStringAsync().Result);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("在重试之后失败");
                    throw new Exception(ex.Message);
                }
            });

            return Content(coreResult.Message);
        }
    }
}
