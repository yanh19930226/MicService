using Core.EventBus.Abstractions;
using Core.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.EventBus
{
	public class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
	{
		public class SubscriptionInfo
		{
			public bool IsDynamic
			{
				get;
			}

			public Type HandlerType
			{
				get;
			}

			private SubscriptionInfo(bool isDynamic, Type handlerType)
			{
				IsDynamic = isDynamic;
				HandlerType = handlerType;
			}

			public static SubscriptionInfo Dynamic(Type handlerType)
			{
				return new SubscriptionInfo(isDynamic: true, handlerType);
			}

			public static SubscriptionInfo Typed(Type handlerType)
			{
				return new SubscriptionInfo(isDynamic: false, handlerType);
			}
		}

		private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;

		private readonly List<Type> _eventTypes;

		public bool IsEmpty => !_handlers.Keys.Any();

		public event EventHandler<string> OnEventRemoved;

		public InMemoryEventBusSubscriptionsManager()
		{
			_handlers = new Dictionary<string, List<SubscriptionInfo>>();
			_eventTypes = new List<Type>();
		}

		public void Clear()
		{
			_handlers.Clear();
		}

		public void AddDynamicSubscription<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
		{
			DoAddSubscription(typeof(TH), eventName, isDynamic: true);
		}

		public void AddSubscription<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
		{
			string eventKey = GetEventKey<T>();
			DoAddSubscription(typeof(TH), eventKey, isDynamic: false);
			if (!_eventTypes.Contains(typeof(T)))
			{
				_eventTypes.Add(typeof(T));
			}
		}

		private void DoAddSubscription(Type handlerType, string eventName, bool isDynamic)
		{
			if (!HasSubscriptionsForEvent(eventName))
			{
				_handlers.Add(eventName, new List<SubscriptionInfo>());
			}
			if (((IEnumerable<SubscriptionInfo>)_handlers[eventName]).Any(s => s.HandlerType == handlerType))
			{
				throw new ArgumentException("Handler Type " + handlerType.Name + " already registered for '" + eventName + "'", "handlerType");
			}
			if (isDynamic)
			{
				_handlers[eventName].Add(SubscriptionInfo.Dynamic(handlerType));
			}
			else
			{
				_handlers[eventName].Add(SubscriptionInfo.Typed(handlerType));
			}
		}

		public void RemoveDynamicSubscription<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
		{
			SubscriptionInfo subsToRemove = FindDynamicSubscriptionToRemove<TH>(eventName);
			DoRemoveHandler(eventName, subsToRemove);
		}

		public void RemoveSubscription<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
		{
			SubscriptionInfo subsToRemove = FindSubscriptionToRemove<T, TH>();
			string eventKey = GetEventKey<T>();
			DoRemoveHandler(eventKey, subsToRemove);
		}

		private void DoRemoveHandler(string eventName, SubscriptionInfo subsToRemove)
		{
			if (subsToRemove == null)
			{
				return;
			}
			_handlers[eventName].Remove(subsToRemove);
			if (!_handlers[eventName].Any())
			{
				_handlers.Remove(eventName);
				Type type = ((IEnumerable<Type>)_eventTypes).SingleOrDefault(e => e.Name == eventName);
				if (type != null)
				{
					_eventTypes.Remove(type);
				}
				RaiseOnEventRemoved(eventName);
			}
		}

		public IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent
		{
			string eventKey = GetEventKey<T>();
			return GetHandlersForEvent(eventKey);
		}

		public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName)
		{
			return _handlers[eventName];
		}

		private void RaiseOnEventRemoved(string eventName)
		{
			if (this.OnEventRemoved != null)
			{
				this.OnEventRemoved(this, eventName);
			}
		}

		private SubscriptionInfo FindDynamicSubscriptionToRemove<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
		{
			return DoFindSubscriptionToRemove(eventName, typeof(TH));
		}

		private SubscriptionInfo FindSubscriptionToRemove<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
		{
			string eventKey = GetEventKey<T>();
			return DoFindSubscriptionToRemove(eventKey, typeof(TH));
		}

		private SubscriptionInfo DoFindSubscriptionToRemove(string eventName, Type handlerType)
		{
			if (!HasSubscriptionsForEvent(eventName))
			{
				return null;
			}
			return ((IEnumerable<SubscriptionInfo>)_handlers[eventName]).SingleOrDefault(s => s.HandlerType == handlerType);
		}

		public bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent
		{
			string eventKey = GetEventKey<T>();
			return HasSubscriptionsForEvent(eventKey);
		}

		public bool HasSubscriptionsForEvent(string eventName)
		{
			return _handlers.ContainsKey(eventName);
		}

		public Type GetEventTypeByName(string eventName)
		{
			return ((IEnumerable<Type>)_eventTypes).SingleOrDefault(t => t.Name == eventName);
		}

		public string GetEventKey<T>()
		{
			return typeof(T).Name;
		}
	}
}
