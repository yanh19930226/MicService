using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Redis
{
    public class RedisOptions
    {
        /// <summary>
        /// 连接
        /// </summary>
        public string Connection { get; set; }
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string InstanceName { get; set; }
        /// <summary>
        /// 大小
        /// </summary>
        public string DefaultDB { get; set; }
    }
}
