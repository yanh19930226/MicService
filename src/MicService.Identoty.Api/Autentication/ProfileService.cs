using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.Identoty.Api.Autentication
{
    public class ProfileService : IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));
            var subjectId = context.Subject.Claims.ToList().Find(s => s.Type == "sub").Value;

            if (!int.TryParse(subjectId, out int userId))
                throw new ArgumentNullException(nameof(context.Subject));
            context.IssuedClaims = context.Subject.Claims.ToList();
            return Task.CompletedTask;
        }
        public Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));
            var subjectId = context.Subject.Claims.ToList().Find(s => s.Type == "sub").Value;
            context.IsActive = int.TryParse(subjectId, out int userId);
            return Task.CompletedTask;
        }
    }
}
