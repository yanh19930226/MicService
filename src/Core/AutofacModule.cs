using Autofac;
using AutoMapper;
using AutoMapper.Configuration;
using Core.Data;
using Core.Data.Domain.Bus;
using Core.Data.Domain.Interfaces;
using Core.Data.Infra;
using Core.Http;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Core
{
    public class AutofacModule : Autofac.Module
    {

        public AutofacModule(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        protected override void Load(ContainerBuilder builder)
        {
            ZeusOptions zeusOptions = ConfigurationBinder.Get<ZeusOptions>((IConfiguration)(object)Configuration.GetSection("Zeus"));

            #region AutoMapper
            List<Profile> autoMapperProfiles = (Assembly.GetEntryAssembly()!.GetTypes()).Where(p => p.BaseType == typeof(Profile)).Select(p => (Profile)Activator.CreateInstance(p)).ToList();

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                foreach (Profile item in autoMapperProfiles)
                {
                    cfg.AddProfile(item);
                }
            }));
            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().InstancePerLifetimeScope();
            #endregion

            #region IHttpClientFactory
            //builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().InstancePerLifetimeScope();
            //builder.RegisterType<HttpClientFactory>().As<IHttpClientFactory>().InstancePerLifetimeScope();
            #endregion

            #region Data
            builder.Register(c =>
               {
                   var optionsBuilder = new DbContextOptionsBuilder<ZeusContext>();
                   optionsBuilder.UseMySql(zeusOptions.Connection);
                   return optionsBuilder.Options;
               }).InstancePerLifetimeScope();

            builder.RegisterType<ZeusContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
             builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly()).Where(t => t.Name.EndsWith("Queries")).AsSelf()
                   .AsImplementedInterfaces().PropertiesAutowired().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly()).AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly()).AsClosedTypesOf(typeof(INotificationHandler<>));
            builder.RegisterType<InMemoryBus>().As<IMediatorHandler>().InstancePerLifetimeScope();
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();

            builder.Register((Func<IComponentContext, ServiceFactory>)delegate (IComponentContext context)
            {
                IComponentContext c2 = context.Resolve<IComponentContext>();
                return (Type t) => c2.Resolve(t);

            }); 
            #endregion
        }
    }
}
