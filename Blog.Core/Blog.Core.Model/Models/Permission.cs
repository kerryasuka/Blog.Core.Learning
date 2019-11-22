﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.Models
{
    /// <summary>
    /// 路由菜单表
    /// </summary>
    public class Permission : RootEntity
    {
        public Permission()
        {
            this.ModulePermission = new List<ModulePermission>();
            this.RoleModulePermission = new List<RoleModulePermission>();
        }

        /// <summary>
        /// 菜单执行Action名
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 菜单显示名(如用户名、编辑(按钮)、删除(按钮))
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否是按钮
        /// </summary>
        public bool IsButton { get; set; }
        /// <summary>
        /// 是否是隐藏菜单
        /// </summary>
        public bool? IsHide { get; set; }
        /// <summary>
        /// 按钮事件
        /// </summary>
        public string Func { get; set; }
        /// <summary>
        /// 上一级菜单(0表示上一级无菜单)
        /// </summary>
        public int Pid { get; set; }
        /// <summary>
        /// 接口API
        /// </summary>
        public int Mid { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderSort { get; set; }
        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 菜单描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 激活状态
        /// </summary>
        public bool Enable { get; set; }
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
        public DateTime? CreateTime { get; set; } = DateTime.Now;
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
        public DateTime? ModifyTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 获取或设置是否禁用，逻辑上的删除，非物理删除
        /// </summary>
        public bool? IsDeleted { get; set; }

        public List<int> PIdArr { get; set; }
        public List<string> PNameArr { get; set; }
        public List<string> PCodeArr { get; set; }
        public string MName { get; set; }

        public virtual ICollection<ModulePermission> ModulePermission { get; set; }
        public virtual ICollection<RoleModulePermission> RoleModulePermission { get; set; }
    }
}

