using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Domain.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		Task<bool> CommitAsync();
	}
}
