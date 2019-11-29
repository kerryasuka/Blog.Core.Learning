using Blog.Core.IRepository;
using Blog.Core.IServices;
using Blog.Core.Model.Models;
using Blog.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Services
{
    public class ModulePermissionServices : BaseServices<ModulePermission>, IModulePermissionServices
    {
        IModulePermissionRepository m_Dal;

        public ModulePermissionServices(IModulePermissionRepository dal)
        {
            this.m_Dal = dal;
            base.baseDal = dal;
        }
    }
}
