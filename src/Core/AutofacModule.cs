using Autofac;
using AutoMapper;
using Core.Data.SeedWork;
using Core.Http;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
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

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly()).Where(t => t.Name.EndsWith("Queries")).AsSelf()
                   .AsImplementedInterfaces().PropertiesAutowired().InstancePerLifetimeScope(); 

            #endregion

            #region Mediator
            //builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly()).AsClosedTypesOf(typeof(IRequestHandler<,>));
            //builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly()).AsClosedTypesOf(typeof(INotificationHandler<>));
            ////builder.RegisterType<InMemoryBus>().As<IMediatorHandler>().InstancePerLifetimeScope();
            //builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();
            //builder.Register((Func<IComponentContext, ServiceFactory>)delegate (IComponentContext context)
            //{
            //    IComponentContext c2 = context.Resolve<IComponentContext>();
            //    return (Type t) => c2.Resolve(t);
            //}); 
            #endregion
        }
    }
}
