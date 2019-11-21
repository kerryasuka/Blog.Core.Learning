using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Common.GlobalVar
{
    /// <summary>
    /// 权限变量配置
    /// </summary>
    public static class Permissions
    {
        public const string Name = "Permission";
    }

    /// <summary>
    /// 路由变量前缀配置
    /// </summary>
    public static class RoutePrefix
    {
        /// <summary>
        /// 前缀名
        /// 如果不需要，尽量留空，不要修改
        /// 除非一定要在所有的api钱同意加上特定的前缀
        /// </summary>
        public const string Name = "";
    }
}
