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
    public class RoleModulePermissionServices: BaseServices<RoleModulePermission>, IRoleModulePermissionServices
    {
        readonly IRoleModulePermissionRepository m_Dal;
        readonly IModuleRepository m_ModuleRepository;
        readonly IRoleRepository m_RoleRepository;        

        public RoleModulePermissionServices(IRoleModulePermissionRepository dal, IModuleRepository moduleRepository, IRoleRepository roleRepository)
        {
            this.m_Dal = dal;
            this.m_ModuleRepository = moduleRepository;
            this.m_RoleRepository = roleRepository;
            base.baseDal = dal;
        }

        /// <summary>
        /// 获取全部角色接口(按钮)关系数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<RoleModulePermission>> GetRoleModule()
        {
            var roleModulePermission = await base.Query(a => a.IsDeleted == false);
            var roles = await m_RoleRepository.Query(a => a.IsDeleted == false);
            var modules = await m_ModuleRepository.Query(a => a.IsDeleted == false);

            if(roleModulePermission.Count > 0)
            {
                foreach (var item in roleModulePermission)
                {
                    item.Role = roles.FirstOrDefault(d => d.Id == item.RoleId);
                    item.Module = modules.FirstOrDefault(d => d.Id == item.ModuleId);
                }
            }

            return roleModulePermission;
        }

        public async Task<List<RoleModulePermission>> TestModelWithChildren()
        {
            return await m_Dal.WithChildrenModel();
        }

        public async Task<List<TestMuchTableResult>> QueryMuchTable()
        {
            return await m_Dal.QueryMuchTable();
        }
    }
}
