using Core.Data.Domain.Interfaces;
using Core.Data.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Infra
{
	public class Repository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : Entity
	{
		protected readonly ZeusContext _context;

		protected readonly DbSet<TEntity> _dbSet;

		public Repository(ZeusContext context)
		{
			_context = context;
			_dbSet = _context.Set<TEntity>();
		}

		public virtual void Add(TEntity obj)
		{
			obj.CreateTime = DateTime.Now;
			obj.ModifyTime = DateTime.Now;
			_dbSet.Add(obj);
		}

		public virtual ValueTask<TEntity> GetByIdAsync(object id)
		{
			return _dbSet.FindAsync(id);
		}

		public virtual IQueryable<TEntity> GetAll()
		{
			return _dbSet;
		}

		public virtual void Update(TEntity obj)
		{
			obj.ModifyTime = DateTime.Now;
			_dbSet.Update(obj);
		}

		public virtual void Remove(object id)
		{
			_dbSet.Remove(_dbSet.Find(id));
		}

		public virtual IQueryable<TEntity> GetByPage<TKey>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TKey>> orderByLambda, out int total, bool isAsc = true)
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
