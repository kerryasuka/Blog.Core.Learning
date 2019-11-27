using System;

namespace Blog.Core.AuthHelper.Policys
{
    /// <summary>
    /// 用户或角色或其他凭据实体，就像订单详情一样
    /// </summary>
    public class PermissionItem
    {
        /// <summary>
        /// 用户或角色或其他凭据
        /// </summary>
        public virtual string Role { get; set; }
        /// <summary>
        /// 请求Url
        /// </summary>
        public virtual string Url { get; set; }
    }
}
