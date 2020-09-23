﻿using MicService.Project.Api.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicService.Project.Api.Domain.AggregatesModel
{
    public class ProjectContributor : Entity
    {
        public int ProjectId { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Avatar { get; set; }

        public DateTime CreateTime { get; set; }

        public bool IsCloser { get; set; }

        public int ContributorType { get; set; }

        public virtual Project Project { get; set; }
    }
}
