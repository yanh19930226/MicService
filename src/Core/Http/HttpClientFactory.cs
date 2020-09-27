using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Http
{
    public class HttpClientFactory: IHttpClientFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpClientFactory(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IHttpClient GetHttpClient()
        {
            return new StandardHttpClient();
        }

        public IHttpClient GetHttpClientWithPolly()
        {
            return new PollyHttpClient("", Policy[], _logger, _httpContextAccessor);
        }
       
        //private Policy[] CreatePolicy(string origin)
        //{
        //    return new Policy[] {
        //        Policy.Handle<HttpRequestException>()
        //        .WaitAndRetryAsync(
        //            _retryCount,
        //            retryAttempt=>TimeSpan.FromSeconds(Math.Pow(2,retryAttempt)),
        //            (exception,timespan,retrycount,context)=>{
        //                var msg=$"第{retrycount}次"+
        //                $"of {context.PolicyKey}"+
        //                $"at {context.OperationKey},"+
        //                $"due to:{exception}";
        //                //_logger.LogWarning(msg);
        //                _logger.LogDebug(msg);
        //            }),
        //        Policy.Handle<HttpRequestException>()
        //        .CircuitBreakerAsync(
        //            _exceptionsAllowedBeforeBreaking,
        //            TimeSpan.FromMinutes(1),
        //            (exception,duraton)=>{
        //                _logger.LogDebug("熔断器打开");
        //                //_logger.LogTrace("熔断器打开");
        //            },()=>{
        //                _logger.LogDebug("熔断器关闭");
        //                //_logger.LogTrace("熔断器关闭");
        //            })
        //        //扩展机制:仓壁隔离;回退 todo
        //    };
        //}
    }
}
