using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.EventBus.Abstractions;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using MicService.Abstractions.IntegrationEvents;
using MicService.Abstractions.IntegrationEvents.Model;
using MicService.User.Api.Integration.Event;

namespace MicService.User.Api.Controllers
{
    /// <summary>
    /// 事件服务
    /// </summary>
    [Route("api/event")]
    [ApiController]
    public class EventController : Controller
    {
        private readonly IEventBus _eventBus;
        private readonly ICapPublisher _capBus;
        public EventController(IEventBus eventBus, ICapPublisher capBus)
        {
            _eventBus = eventBus;
            _capBus = capBus;
        }
        /// <summary>
        /// 集成事件发送
        /// </summary>
        /// <returns></returns>
        [Route("rabbit")]
        [HttpGet]
        public IActionResult Index()
        {
            var eventModel = new TestIntegrationEventModel
            {
                Id="1",
                Title ="集成事件"
            };
            var @event = new TestIntegrationEvent(eventModel);
            _eventBus.Publish(@event);
            return Ok();
        }
        /// <summary>
        /// CAP事件发送
        /// </summary>
        /// <returns></returns>
        [Route("cap")]
        [HttpGet]
        public IActionResult Cap()
        {
            var @event = new UserProfileChangedIntegrationEvent()
            {
                UserId =1,
                Name = "Test",
                Company ="Test"
            };
            _capBus.Publish("UserProfileChanged", @event);
            return Ok();
        }
    }
}
