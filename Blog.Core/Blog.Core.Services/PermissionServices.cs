using Blog.Core.IRepository;
using Blog.Core.IServices;
using Blog.Core.Model.Models;
using Blog.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Services
{
    public class PermissionServices : BaseServices<Permission>, IPermissionServices
    {
        IPermissionRepository m_Dal;

        public PermissionServices(IPermissionRepository dal)
        {
            this.m_Dal = dal;
            base.baseDal = dal;
        }
    }
}
