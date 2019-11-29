using Blog.Core.Common.Helper;
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
    public class SysUserInfoServices : BaseServices<SysUserInfo>, ISysUserInfoServices
    {
        ISysUserInfoRepository m_Dal;
        IUserRoleServices m_UserRolesServices;
        IRoleRepository m_RoleRepository;

        public SysUserInfoServices(ISysUserInfoRepository dal, IUserRoleServices userRoleServices, IRoleRepository roleRepository)
        {
            this.m_Dal = dal;
            this.m_UserRolesServices = userRoleServices;
            this.m_RoleRepository = roleRepository;
            base.baseDal = dal;
        }

        public async Task<SysUserInfo> SaveUserInfo(string loginName, string loginPwd)
        {
            SysUserInfo sysUserInfo = new SysUserInfo(loginName, loginPwd);
            SysUserInfo model = new SysUserInfo();

            var userList = await base.Query(a => a.ULoginName == sysUserInfo.ULoginName && a.ULoginPwd == sysUserInfo.ULoginPwd);
            if (userList.Count > 0)
            {
                model = userList.FirstOrDefault();
            }
            else
            {
                var id = await base.Add(sysUserInfo);
                model = await base.QueryById(id);
            }

            return model;
        }

        public async Task<string> GetUserRoleNameStr(string loginName, string loginPwd)
        {
            string roleName = "";
            var user = (await base.Query(a => a.ULoginName == loginName && a.ULoginPwd == loginPwd)).FirstOrDefault();
            var roleList = await m_RoleRepository.Query(a => a.IsDeleted == false);
            if (user != null)
            {
                var userRole = await m_UserRolesServices.Query(q => q.UserId == user.UId);
                if(userRole.Count > 0)
                {
                    var arr = userRole.Select(s => s.RoleId.ObjToString()).ToList();
                    var roles = roleList.Where(w => arr.Contains(w.Id.ObjToString()));

                    roleName = string.Join(',', roles.Select(s => s.Name).ToArray());
                }
            }

            return roleName;
        }
    }
}
