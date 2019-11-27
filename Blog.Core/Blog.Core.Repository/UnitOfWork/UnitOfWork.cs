using Blog.Core.Common;
using Blog.Core.Common.Helper;
using Blog.Core.IRepository.UnitOfWork;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISqlSugarClient _sqlSugarClient;

        public UnitOfWork(ISqlSugarClient sqlSugarClient)
        {
            _sqlSugarClient = sqlSugarClient;

            if (AppSettings.App(new string[] { "AppSetting", "SqlAOP", "Enable" }).ObjToBool())
            {
                sqlSugarClient.Aop.OnLogExecuting = (sql, pars) =>
                {
                    Parallel.ForEach(0, 1, e =>
                    {
                        MiniProfiler.Current.CustomTiming("SQL: ", GetParas(pars) + "【SQL语句】: " + sql);
                        LogLock.OutSql2Log("SqlLog", new string[] { GetParas(pars), "【SQL语句】: " + sql });
                    });
                };
            }
        }

        private string GetParas(SugarParameter[] pars)
        {
            string key = "【SQL参数】";
            foreach (var param in pars)
            {
                key = $"{param.ParameterName}:{param.Value}\n";
            }
            return key;
        }

        public ISqlSugarClient GetDbClient()
        {
            throw new NotImplementedException();
        }

        public void BeginTran()
        {
            throw new NotImplementedException();
        }

        public void CommitTran()
        {
            throw new NotImplementedException();
        }

        public void RollBackTran()
        {
            throw new NotImplementedException();
        }
    }
}
