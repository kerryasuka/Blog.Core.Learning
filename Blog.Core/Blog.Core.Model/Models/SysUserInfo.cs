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
            ULoginName = loginName;
            ULoginPwd = loginPwd;
            URealName = ULoginName;
            UStatus = 0;
            UCreateTime = DateTime.Now;
            UUpdateTime = DateTime.Now;
            ULastErrTime = DateTime.Now;
            UErrorCount = 0;
            Name = "";
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
        /// <summary>
        /// 真是姓名
        /// </summary>
        public string URealName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int UStatus { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string URemark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime UCreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UUpdateTime { get; set; }
        /// <summary>
        /// 最后错误时间
        /// </summary>
        public DateTime ULastErrTime { get; set; }
        /// <summary>
        /// 错误次数
        /// </summary>
        public int UErrorCount { get; set; }
        
        /// <summary>
        /// 登陆账号
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birth { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 删除状态
        /// </summary>
        public bool TdIsDelete { get; set; }
        public int RID { get; set; }
        public string RoleName { get; set; }
    }
}
