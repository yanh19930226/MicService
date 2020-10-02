using Core.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Data.SeedWork
{
    public class ZeusContext : DbContext, IUnitOfWork
    {
        private IMediator _mediator;
        public ZeusContext(DbContextOptions<ZeusContext> options, IMediator mediator)
            : base(options)
        {
            _mediator = mediator;
        }
      

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(false);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region MyRegion
            //因为模型程序集被这个类所在的程序集引用了所以可以使用数据迁移
            //var entityTypes = (Assembly.GetEntryAssembly()!.GetTypes())
            //    .Where(type => !string.IsNullOrWhiteSpace(type.Namespace))
            //    .Where(type => type.GetTypeInfo().IsClass)
            //    .Where(type => type.GetTypeInfo().BaseType != null)
            //    .Where(p => p.BaseType == typeof(Entity)).ToList();

            ////实体
            //foreach (var entityType in entityTypes)
            //{
            //    if (modelBuilder.Model.FindEntityType(entityType) != null)
            //        continue;
            //    modelBuilder.Model.AddEntityType(entityType);
            //} 
            #endregion

            //配置文件
            foreach (Type item in (Assembly.GetEntryAssembly()!.GetTypes()).Where(type => type.HasImplementedRawGeneric(typeof(IEntityTypeConfiguration<>))))
            {
                dynamic val = Activator.CreateInstance(item);
                modelBuilder.ApplyConfigurationsFromAssembly(val);
            }
            base.OnModelCreating(modelBuilder);
        }
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);
            await base.SaveChangesAsync();
            return true;
        }
    }
}
