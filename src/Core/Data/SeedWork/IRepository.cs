using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.SeedWork
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class/*,IAggregateRoot*/
    {
        Task<TEntity> GetAsync(int id);

        TEntity Add(TEntity t);

        TEntity Update(TEntity t);
        IUnitOfWork UnitOfWork { get; }
    }
}
