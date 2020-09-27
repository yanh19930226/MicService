using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Collections.Generic;
using System.Net.Http;
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

        public IHttpClient GetHttpClientWithPolly(string applicationName, Func<string, ISyncPolicy> pollices)
        {
            return new PollyHttpClient(applicationName, pollices, _httpContextAccessor);
        }


        
        private Policy[] CreatePolicy(string origin)
        {
            return new Policy[] {
                Policy.Handle<HttpRequestException>()
                .WaitAndRetryAsync(
                    _retryCount,
                    retryAttempt=>TimeSpan.FromSeconds(Math.Pow(2,retryAttempt)),
                    (exception,timespan,retrycount,context)=>{
                        var msg=$"第{retrycount}次"+
                        $"of {context.PolicyKey}"+
                        $"at {context.OperationKey},"+
                        $"due to:{exception}";
                        //_logger.LogWarning(msg);
                        _logger.LogDebug(msg);
                    }),
                Policy.Handle<HttpRequestException>()
                .CircuitBreakerAsync(
                    _exceptionsAllowedBeforeBreaking,
                    TimeSpan.FromMinutes(1),
                    (exception,duraton)=>{
                        _logger.LogDebug("熔断器打开");
                        //_logger.LogTrace("熔断器打开");
                    },()=>{
                        _logger.LogDebug("熔断器关闭");
                        //_logger.LogTrace("熔断器关闭");
                    })
                //扩展机制:仓壁隔离;回退 todo
            };
        }


        public static ISyncPolicy CreatePolly()
        {
            // 超时1秒
            var timeoutPolicy = Policy.Timeout(1, (context, timespan, task) =>
            {
                Debug.WriteLine("执行超时，抛出TimeoutRejectedException异常");
            });


            // 重试2次
            var retryPolicy = Policy.Handle<Exception>()
                .WaitAndRetry(
                    2,
                    retryAttempt => TimeSpan.FromSeconds(2),
                    (exception, timespan, retryCount, context) =>
                    {
                        Debug.WriteLine($"{DateTime.Now} - 重试 {retryCount} 次 - 抛出{exception.GetType()}");
                    });

            // 连续发生两次故障，就熔断3秒
            var circuitBreakerPolicy = Policy.Handle<Exception>()
                .CircuitBreaker(
                    // 熔断前允许出现几次错误
                    2,
                    // 熔断时间
                    TimeSpan.FromSeconds(5),
                    // 熔断时触发 OPEN
                    onBreak: (ex, breakDelay) =>
                    {
                        Debug.WriteLine($"{DateTime.Now} - 断路器：开启状态（熔断时触发）");
                    },
                    // 熔断恢复时触发 // CLOSE
                    onReset: () =>
                    {
                        Debug.WriteLine($"{DateTime.Now} - 断路器：关闭状态（熔断恢复时触发）");
                    },
                    // 熔断时间到了之后触发，尝试放行少量（1次）的请求，
                    onHalfOpen: () =>
                    {
                        Debug.WriteLine($"{DateTime.Now} - 断路器：半开启状态（熔断时间到了之后触发）");
                    }
                );

            // 回退策略，降级！
            var fallbackPolicy = Policy.Handle<Exception>()
                .Fallback(() =>
                {
                    Debug.WriteLine("这是一个Fallback");
                }, exception =>
                {
                    Debug.WriteLine($"Fallback异常：{exception.GetType()}");
                });

            // 策略从右到左依次进行调用
            // 首先判断调用是否超时，如果超时就会触发异常，发生超时故障，然后就触发重试策略；
            // 如果重试两次中只要成功一次，就直接返回调用结果
            // 如果重试两次都失败，第三次再次失败，就会发生故障
            // 重试之后是断路器策略，所以这个故障会被断路器接收，当断路器收到两次故障，就会触发熔断，也就是说断路器开启
            // 断路器开启的3秒内，任何故障或者操作，都会通过断路器到达回退策略，触发降级操作
            // 3秒后，断路器进入到半开启状态，操作可以正常执行
            return Policy.Wrap(fallbackPolicy, circuitBreakerPolicy, retryPolicy, timeoutPolicy);
        }
    }
}
