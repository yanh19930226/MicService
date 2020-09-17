using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.User.Api
{
    public class UserContextSeed
    {
        public async Task SeedAsync(UserContext context, IServiceProvider services)
        {
            if (!context.Users.Any())
            {
                var user = new MicService.User.Api.Models.Domain.User
                {
                    Name = "yanh",
                };
                await context.Users.AddAsync(user);
                var res=await context.SaveChangesAsync();
                if (res < 1)
                {
                    throw new Exception("初始化失败");
                }
            }
        }
    }
}
