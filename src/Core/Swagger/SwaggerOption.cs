using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Swagger
{
    /// <summary>
    /// SwaggerOption
    /// </summary>
    public class SwaggerOption
    {
        /// <summary>
        /// Api名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// MiniProfiler
        /// </summary>
        public bool MiniProfiler { get; set; }
        /// <summary>
        /// 是否开启
        /// </summary>
        public bool Enabled { get; set; }
    }
}
