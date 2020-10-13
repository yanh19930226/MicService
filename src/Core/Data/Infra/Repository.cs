using Core.Data.Domain.Interfaces;
using Core.Data.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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

		#region SQL语句
		public virtual void BulkInsert<T>(List<T> entities)
		{ }
		public int ExecuteSql(string sql)
		{
			return _context.Database.ExecuteSqlCommand(sql);
		}

		public Task<int> ExecuteSqlAsync(string sql)
		{
			return _context.Database.ExecuteSqlCommandAsync(sql);
		}

		public int ExecuteSql(string sql, List<DbParameter> spList)
		{
			return _context.Database.ExecuteSqlCommand(sql, spList.ToArray());
		}

		public async Task<int> ExecuteSqlAsync(string sql, List<DbParameter> spList)
		{
			return await _context.Database.ExecuteSqlCommandAsync(sql, spList.ToArray());
		}


		public virtual DataTable GetDataTableWithSql(string sql)
		{
			throw new NotImplementedException();
		}

		public virtual DataTable GetDataTableWithSql(string sql, List<DbParameter> spList)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
