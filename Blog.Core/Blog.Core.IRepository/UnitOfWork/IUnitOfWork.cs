using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.IRepository.UnitOfWork
{
    public interface IUnitOfWork
    {
        ISqlSugarClient GetDbClient();

        void BeginTran();
        void CommitTran();
        void RollBackTran();
    }
}
