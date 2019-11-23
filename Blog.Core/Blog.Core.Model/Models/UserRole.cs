using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.Models
{
    /// <summary>
    /// 用户角色关联表
    /// </summary>
    public class UserRole : RootEntity
    {
        public UserRole()
        {

        }

        public UserRole(int uid, int rid)
        {
            this.UserId = uid;
            this.RoleId = rid;
            this.IsDeleted = false;
            this.CreateId = uid;
            this.CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 获取或设置是否禁用，逻辑上的删除，非物理删除
        /// </summary>
        public bool? IsDeleted { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 创建Id
        /// </summary>
        public int? CreateId { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 修改Id
        /// </summary>
        public int? ModifyId { get; set; }
        /// <summary>
        /// 修改者
        /// </summary>
        public string ModifyBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }
    }
}
