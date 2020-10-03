using Core.Data.Domain.Interfaces;
using Core.Data.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Infra
{
	public class UnitOfWork : IUnitOfWork, IDisposable
	{
		private readonly DbContext _context;

		private readonly IMediator _mediator;

		public UnitOfWork(ZeusContext context, IMediator mediator)
		{
			_context = context;
			_mediator = mediator;
		}

		public async Task<bool> CommitAsync()
		{
			bool isSuccess = await _context.SaveChangesAsync() > 0;
			if (isSuccess)
			{
				var domainEntities = _context.ChangeTracker
			   .Entries<Entity>()
			   .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

				var domainEvents = domainEntities
					.SelectMany(x => x.Entity.DomainEvents)
					.ToList();

				domainEntities.ToList()
					.ForEach(entity => entity.Entity.ClearDomainEvents());

				var tasks = domainEvents
					.Select(async (domainEvent) => {
						await _mediator.Publish(domainEvent);
					});

				await Task.WhenAll(tasks);
			}
			return isSuccess;
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
