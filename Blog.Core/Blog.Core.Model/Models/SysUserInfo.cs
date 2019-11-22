using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.Models
{
    /// <summary>
    /// 用户信息表
    /// </summary>
    public class SysUserInfo
    {
        public SysUserInfo() { }
        public SysUserInfo(string loginName, string loginPwd)
        {

        }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UId { get; set; }
        /// <summary>
        /// 登陆账户
        /// </summary>
        public string ULoginName { get; set; }
        /// <summary>
        /// 登陆密码
        /// </summary>
        public string ULoginPwd { get; set; }

    }
}
