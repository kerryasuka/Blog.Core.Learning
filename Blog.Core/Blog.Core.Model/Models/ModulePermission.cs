using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.Models
{
    /// <summary>
    /// 菜单与按钮关系表
    /// </summary>
    public class ModulePermission : RootEntity
    {
        /// <summary>
        /// 获取或设置是否禁用，逻辑上的删除，非物理删除
        /// </summary>
        public bool? IsDeleted { get; set; }
        /// <summary>
        /// 菜单Id
        /// </summary>
        public int ModuleId { get; set; }
        /// <summary>
        /// 按钮Id
        /// </summary>
        public int PermissionId { get; set; }
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

        //public virtual Module Module { get; set; }
        //public virtual Permission Permission { get; set; }
    }
}
