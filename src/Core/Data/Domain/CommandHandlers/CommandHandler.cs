using Core.Data.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Domain.CommandHandlers
{
	public class CommandHandler
	{
		private readonly IUnitOfWork _uow;

		public CommandHandler(IUnitOfWork uow)
		{
			_uow = uow;
		}

		public async Task<bool> CommitAsync()
		{
			if (await _uow.CommitAsync())
			{
				return true;
			}
			throw new Exception("We had a problem during saving your data.");
		}
	}
}
