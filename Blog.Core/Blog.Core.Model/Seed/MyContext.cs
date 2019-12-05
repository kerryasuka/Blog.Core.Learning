using Blog.Core.Common.DB;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.Seed
{
    public class MyContext
    {
        private static string _connectionString = BaseDbConfig.ConnectionString;
        private static DbType _dbType = (DbType)BaseDbConfig.DbType;
        private SqlSugarClient _db;

        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public static DbType DbType
        {
            get { return _dbType; }
            set { _dbType = value; }
        }

        /// <summary>
        /// 数据连接对象
        /// </summary>
        public SqlSugarClient Db
        {
            get { return _db; }
            set { _db = value; }
        }

        /// <summary>
        /// 数据库上下文实例(自动关闭连接)
        /// </summary>
        public static MyContext Context
        {
            get { return new MyContext(); }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MyContext()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new ArgumentException("数据库连接字符串为空。");
            }

            _db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = _connectionString,
                DbType = _dbType,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute,
                ConfigureExternalServices = new ConfigureExternalServices()
                {
                    //DataInfoCacheService = new HttpRuntimeCache,
                },
                MoreSettings = new ConnMoreSettings()
                {
                    //IsWithNoLockQuery = true,
                    IsAutoRemoveDataCache = true,
                }
            });
        }

        #region 根据数据库表产生Model层
        /// <summary>
        /// 根据数据库表产生Model层
        /// </summary>
        /// <param name="strPath">实体类存放路径</param>
        /// <param name="strNameSpace">命名空间</param>
        /// <param name="lstTableNames">产生指定的表名称</param>
        /// <param name="strInterFace">实现接口</param>
        /// <param name="blnSerializable">是否序列化</param>
        public void CreateModelByDbTable(string strPath, string strNameSpace, string[] lstTableNames, string strInterFace, bool blnSerializable = false)
        {
            var IDbFirst = _db.DbFirst;
            if (lstTableNames != null && lstTableNames.Length > 0)
            {
                IDbFirst = IDbFirst.Where(lstTableNames);
            }

            IDbFirst.IsCreateDefaultValue().IsCreateAttribute().SettingClassTemplate(c => c =
                @"{using}
                namespace " + strNameSpace +
                @"{
                    {ClassDescription}{SugarTable}" + (blnSerializable ? "[Serializable]" : "") +
                    @"public class {ClassName}" + (string.IsNullOrEmpty(strInterFace) ? "" : (" : " + strInterFace)) + @"
                    {
                        public {ClassName}()
                        {}
                        {PropertyName}
                    }
                }"
            ).SettingPropertyTemplate(p => p =
                @"{SugarColumn}
                public {PropertyType} {PropertyName} { get;set; }
                "
            ).CreateClassFile(strPath, strNameSpace);
        }
        #endregion

        #region 根据数据库表产生IRepository层
        /// <summary>
        /// 根据数据库表产生IRepository层
        /// </summary>
        /// <param name="strPath">实体类存放路径</param>
        /// <param name="strNameSpace">命名空间</param>
        /// <param name="lstTableNames">产生指定的表名称</param>
        /// <param name="strInterface">实现接口</param>
        public void CreateIRepositoryByDbTable(string strPath, string strNameSpace, string[] lstTableNames, string strInterface)
        {
            var IDbFirst = _db.DbFirst;
            if (lstTableNames != null && lstTableNames.Length > 0)
            {
                IDbFirst = IDbFirst.Where(lstTableNames);
            }

            IDbFirst.IsCreateDefaultValue().IsCreateAttribute().SettingClassTemplate(c => c =
                @"using Blog.Core.IRepository.Base;
                  using Blog.Core.Model.Models;
                  
                  namespace " + strNameSpace + @"
                  {
                        /// <summary>
                        /// I{ClassName}Repository
                        /// <summary>
                        public interface I{ClassName}Repository : IBaseRepository<{ClassName}>" + (string.IsNullOrEmpty(strInterface) ? "" : (" , " + strInterface)) + @"
                        {}
                  }
                "
            ).CreateClassFile(strPath, strNameSpace);

        }
        #endregion
    }
}
