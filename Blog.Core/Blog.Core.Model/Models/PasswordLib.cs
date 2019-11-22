using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.Models
{
    /// <summary>
    /// 密码库表
    /// </summary>
    public class PasswordLib
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int PLId { get; set; }
        /// <summary>
        /// 获取或设置是否禁用，逻辑上的删除，非物理删除
        /// </summary>
        public bool? IsDeleted { get; set; }
        public string PLUrl { get; set; }
        public string PLPwd { get; set; }
        public string PLAccountName { get; set; }
        public int? PLStatus { get; set; }
        public int? PLErrorCount { get; set; }
        public string PlHintPwd { get; set; }
        public string PLHintQuestion { get; set; }
        public DateTime? PLCreateTime { get; set; }
        public DateTime? PLUpdateTime { get; set; }
        public DateTime? PLLastErrTime { get; set; }
        public string Test { get; set; }
    }
}
