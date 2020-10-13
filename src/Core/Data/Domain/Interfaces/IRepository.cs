using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Domain.Interfaces
{
	public interface IRepository<TEntity> : IDisposable where TEntity : class
	{
		void Add(TEntity obj);

		ValueTask<TEntity> GetByIdAsync(object id);

		IQueryable<TEntity> GetAll();

		void Update(TEntity obj);

		void Remove(object id);

		IQueryable<TEntity> GetByPage<TKey>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TKey>> orderByLambda, out int total, bool isAsc = true);


		#region 执行Sql语句
		void BulkInsert<T>(List<T> entities);
		int ExecuteSql(string sql);
		Task<int> ExecuteSqlAsync(string sql);
		int ExecuteSql(string sql, List<DbParameter> spList);
		Task<int> ExecuteSqlAsync(string sql, List<DbParameter> spList);
		DataTable GetDataTableWithSql(string sql);

		DataTable GetDataTableWithSql(string sql, List<DbParameter> spList);

		#endregion
	}
}
