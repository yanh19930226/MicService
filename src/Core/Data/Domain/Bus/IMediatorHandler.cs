using Core.Data.Domain.Commands;
using Core.Data.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Domain.Bus
{
	public interface IMediatorHandler
	{
		Task RaiseEvent<T>(T @event) where T : Event;

		Task<T> SendCommandAsync<T>(Command<T> command);
	}
}
