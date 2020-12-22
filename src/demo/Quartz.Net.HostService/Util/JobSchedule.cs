using Core.AutoDI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quartz.Net.HostService.Util
{
    public class JobSchedule
    {
        public JobSchedule(Type jobType, Action<SimpleScheduleBuilder> actionBuilder, string cronExpression = null, bool isStartAtNow = false)
        {
            JobType = jobType;
            CronExpression = cronExpression;
            action = actionBuilder;
            IsStartAtNow = isStartAtNow;
        }
        public JobSchedule(Type jobType, string cronExpression = null, bool isStartAtNow = false)
        {
            JobType = jobType;
            CronExpression = cronExpression;
            IsStartAtNow = isStartAtNow;
        }


        public bool IsStartAtNow { get; set; }
        public Type JobType { get; }
        public string CronExpression { get; }
        public Action<SimpleScheduleBuilder> action { get; }
    }
}
