using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Data.Domain.Events
{
	public interface IHandler<in T, T2> where T : Message<T2>
	{
		void Handle(T message);
	}
}
