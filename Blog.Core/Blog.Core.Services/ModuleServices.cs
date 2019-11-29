using Blog.Core.IRepository;
using Blog.Core.IServices;
using Blog.Core.Model.Models;
using Blog.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Services
{
    public class ModuleServices : BaseServices<Module>, IModuleServices
    {
        IModuleRepository m_Dal;

        public ModuleServices(IModuleRepository dal)
        {
            this.m_Dal = dal;
            base.baseDal = dal;
        }
    }
}
