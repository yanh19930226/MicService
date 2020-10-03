using Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Data.Infra
{
	public class ZeusContext : DbContext
	{
		public ZeusContext(DbContextOptions<ZeusContext> options)
			: base(options)
		{

		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseLazyLoadingProxies(false);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			foreach (Type item in (Assembly.GetEntryAssembly()!.GetTypes()).Where(type => type.HasImplementedRawGeneric(typeof(IEntityTypeConfiguration<>))))
			{
				dynamic val = Activator.CreateInstance(item);
				modelBuilder.ApplyConfiguration(val);
			}
			base.OnModelCreating(modelBuilder);
		}
	}
}
