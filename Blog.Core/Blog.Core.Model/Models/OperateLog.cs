using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.Models
{
    /// <summary>
    /// 日志记录
    /// </summary>
    public class OperateLog : RootEntity
    {
        /// <summary>
        /// 获取或设置是否禁用，逻辑上的删除，非物理删除
        /// </summary>
        public bool? IsDeleted { get; set; }
        /// <summary>
        /// 区域名
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 区域控制器名称
        /// </summary>
        public string Controller { get; set; }
        /// <summary>
        /// Action名称
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// Ip地址
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 登陆时间
        /// </summary>
        public DateTime? LogTime { get; set; }
        /// <summary>
        /// 登陆名称
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 系统用户信息
        /// </summary>
        public virtual SysUserInfo User { get; set; }
    }
}
