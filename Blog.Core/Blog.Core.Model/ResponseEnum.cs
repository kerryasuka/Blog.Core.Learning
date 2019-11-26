using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Blog.Core.Model
{
    public enum ResponseEnum
    {
        /// <summary>
        /// 无权限
        /// </summary>
        [Description("无权限")]
        NoPermission = 401,
        /// <summary>
        /// 找不到指定资源
        /// </summary>
        [Description("找不到指定资源")]
        NotFound = 404,
        /// <summary>
        /// 服务器错误
        /// </summary>
        [Description("服务器错误")]
        ServerError = 500,
    }
}
