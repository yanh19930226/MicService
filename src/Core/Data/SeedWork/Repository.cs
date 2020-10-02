using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.SeedWork
{
	public class Repository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
	{
		protected readonly ZeusContext _context;

		protected readonly DbSet<TEntity> _dbSet;

		public IUnitOfWork UnitOfWork => _context;

		public Repository(ZeusContext context)
		{
			_context = context;
			_dbSet = _context.Set<TEntity>();
		}

		public virtual TEntity Add(TEntity obj)
		{
			return _dbSet.Add(obj).Entity;
		}
		public async Task<TEntity> GetAsync(int id)
		{
			return await _dbSet.FindAsync(id);
		}

		public virtual IQueryable<TEntity> GetAll()
		{
			return _dbSet;
		}

		public virtual TEntity Update(TEntity obj)
		{
			return _dbSet.Update(obj).Entity;
		}

		public virtual void Remove(object id)
		{
			_dbSet.Remove(_dbSet.Find(id));
		}

		public virtual IQueryable<TEntity> GetByPage<TKey>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TKey>> orderByLambda, bool isAsc, out int total)
		{
			var tempData = _dbSet.Where(whereLambda);
			total = tempData.Count();

			if (isAsc)
			{
				tempData = tempData.OrderBy(orderByLambda).
					  Skip(pageSize * (pageIndex - 1)).
					  Take(pageSize);
			}
			else
			{
				tempData = tempData.OrderByDescending(orderByLambda).
					 Skip(pageSize * (pageIndex - 1)).
					 Take(pageSize);
			}
			return tempData;
		}

		public void Dispose()
		{
			_context.Dispose();
			GC.SuppressFinalize(this);
		}

		
	}
}
