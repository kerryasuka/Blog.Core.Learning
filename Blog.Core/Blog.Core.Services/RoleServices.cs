using Blog.Core.IRepository;
using Blog.Core.IServices;
using Blog.Core.Model.Models;
using Blog.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Services
{
    public class RoleServices : BaseServices<Role>, IRoleServices
    {
        IRoleRepository m_Dal;

        public RoleServices(IRoleRepository dal)
        {
            this.m_Dal = dal;
            base.baseDal = dal;
        }

        public async Task<Role> SaveRole(string roleName)
        {
            Role role = new Role(roleName);
            Role model = new Role();
            var userList = await base.Query(a => a.Name == role.Name && a.Enable);
            if(userList.Count > 0)
            {
                model = userList.FirstOrDefault();
            }
            else
            {
                var id = await base.Add(role);
                model = await base.QueryById(id);
            }

            return model;
        }

        public async Task<string> GetRoleNameByRid(int rid)
        {
            return ((await base.QueryById(rid))?.Name); 
        }        
    }
}
