using Microsoft.EntityFrameworkCore;
using MicService.User.Api.Models.Domain;
using MicService.User.Api.Models.EntityConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.User.Api
{
    public class UserContext: DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLazyLoadingProxies(false);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserCfg());
            modelBuilder.ApplyConfiguration(new UserPropertyCfg());
            modelBuilder.ApplyConfiguration(new UserTagCfg());
        }

        public DbSet<Models.Domain.User> Users { get; set; }
        public DbSet<UserProperty> UserProperties { get; set; }
        public DbSet<UserTag> UserTags { get; set; }
    }
}
