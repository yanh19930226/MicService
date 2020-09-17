using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Logger
{
    /// <summary>
    /// LoggerOption
    /// </summary>
    public class LoggerOption
    {
        /// <summary>
        /// 日志级别
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// es地址
        /// </summary>
        public string EsUri { get; set; }
    }
}
