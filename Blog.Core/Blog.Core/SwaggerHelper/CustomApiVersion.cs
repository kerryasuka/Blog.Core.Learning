using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.SwaggerHelper
{
    /// <summary>
    /// 自定义Api版本
    /// </summary>
    public class CustomApiVersion
    {
        /// <summary>
        /// Api接口版本 自定义
        /// </summary>
        public enum ApiVersions
        {
            /// <summary>
            /// V1版本
            /// </summary>
            V1 = 1,
            /// <summary>
            /// V2版本
            /// </summary>
            V2 = 2,
        }
    }
}
