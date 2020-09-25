using Microsoft.EntityFrameworkCore;
using MicService.Recommend.Api.EntityConfiguration;
using MicService.Recommend.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.Recommend.Api.Data
{
    public class RecommendContext : DbContext
    {
        public RecommendContext(DbContextOptions<RecommendContext> options) : base(options)
        {
        }
        public DbSet<ProjectRecommend> ProjectRecommends { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProjectRecommendConfiguration());
        }
    }
}
