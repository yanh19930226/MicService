using Core.Data.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.Test.Api.Models
{
    public class Blog : Entity
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
