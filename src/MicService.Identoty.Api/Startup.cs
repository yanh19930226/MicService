using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Core;
using Core.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MicService.Identoty.Api.Autentication;
using MicService.Identoty.Api.Services;
using MicService.Identoty.Api.Services.Impletment;

namespace MicService.Identoty.Api
{
    public class Startup : CommonStartup
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void CommonServices(IServiceCollection services)
        {
            //services.AddDbContext<UserContext>(options =>
            //{
            //    options.UseMySql(Configuration.GetConnectionString("MysqlUser"), sql => sql.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name));
            //})

            services.AddIdentityServer()
               .AddExtensionGrantValidator<SmsAuthCodeValidator>()
              .AddDeveloperSigningCredential()
              .AddInMemoryClients(Config.GetClients())
              .AddInMemoryApiResources(Config.GetResource())
              .AddInMemoryIdentityResources(Config.GetIdentityResource());

            services
                         .AddSingleton(new HttpClient())
                         .AddScoped<IAuthCodeService, TestAuthCodeService>()
                         .AddScoped<IUserService, UserService>();
        }

        public override void CommonConfigure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseIdentityServer();
        }
    }
}
