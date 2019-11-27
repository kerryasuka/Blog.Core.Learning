using Blog.Core.IRepository.Base;
using Blog.Core.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.IRepository
{
    public interface IRoleModulePermissionRepository : IBaseRepository<RoleModulePermission>
    {
        Task<List<RoleModulePermission>> WithChildrenModel();
        Task<List<TestMuchTableResult>> QueryMuchTable();
    }
}
