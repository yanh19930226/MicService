using Core.Data.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Data.Domain.Commands
{
	public abstract class Command<T> : Message<T>
	{
		public DateTime Timestamp
		{
			get;
			private set;
		}
		//public bool Result { get; set; } = false;
		
		protected Command()
		{
			Timestamp = DateTime.Now;
		}
	}
	public abstract class Command : Command<bool>
	{
	}
}
