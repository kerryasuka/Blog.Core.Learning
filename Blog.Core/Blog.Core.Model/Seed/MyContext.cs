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

        #region 根据数据库表产生IServices层
        /// <summary>
        /// 根据数据库表产生IServices层
        /// </summary>
        /// <param name="strPath">实体类存放路径</param>
        /// <param name="strNameSpace">命名空间</param>
        /// <param name="lstTableNames">产生指定的表名称</param>
        /// <param name="strInterface">实现接口</param>
        public void CreateIServicesByDbTable(string strPath, string strNameSpace, string[] lstTableNames, string strInterface)
        {
            var IDbFirst = _db.DbFirst;
            if (lstTableNames != null && lstTableNames.Length > 0)
            {
                IDbFirst = IDbFirst.Where(lstTableNames);
            }

            IDbFirst.IsCreateDefaultValue().IsCreateAttribute().SettingClassTemplate(c => c =
                @"using Blog.Core.IServices.Base;
                  using Blog.Core.Model.Models;
                  
                  namespace " + strNameSpace + @"
                  {
                        /// <summary>
                        /// I{ClassName}Services
                        /// <summary>
                        public interface I{ClassName}Services : IBaseServices<{ClassName}>" + (string.IsNullOrEmpty(strInterface) ? "" : (" , " + strInterface)) + @"
                        {}
                  }
                "
            ).CreateClassFile(strPath, strNameSpace);
        }
        #endregion

        #region 根据数据库表产生Repository层
        /// <summary>
        /// 根据数据库表产生Repository层
        /// </summary>
        /// <param name="strPath">实体类存放路径</param>
        /// <param name="strNameSpace">命名空间</param>
        /// <param name="lstTableNames">产生指定的表名称</param>
        /// <param name="strInterface">实现接口</param>
        public void CreateRepositoryByDbTable(string strPath, string strNameSpace, string[] lstTableNames, string strInterface)
        {
            var IDbFirst = _db.DbFirst;
            if (lstTableNames != null && lstTableNames.Length > 0)
            {
                IDbFirst = IDbFirst.Where(lstTableNames);
            }

            IDbFirst.IsCreateDefaultValue().IsCreateAttribute().SettingClassTemplate(c => c =
                @"using Blog.Core.IRepository;
                  using Blog.Core.IRepository.UnitOfWork;
                  using Blog.Core.Model.Models;
                  using Blog.Core.Repository.Base;
                  
                  namespace " + strNameSpace + @"
                  {
                        /// <summary>
                        /// {ClassName}Repository
                        /// <summary>
                        public class {ClassName}Repository : BaseRepository<{ClassName}>, I{ClassName}Repository" + (string.IsNullOrEmpty(strInterface) ? "" : (" , " + strInterface)) + @"
                        {
                            public {ClassName}Repository(IUnitOfWork unitOfWork) : base(unitOfWork)
                            {}
                        }
                  }
                "
            ).CreateClassFile(strPath, strNameSpace);
        }
        #endregion

        #region 根据数据库表产生Services层
        /// <summary>
        /// 根据数据库表产生Services层
        /// </summary>
        /// <param name="strPath">实体类存放路径</param>
        /// <param name="strNameSpace">命名空间</param>
        /// <param name="lstTableNames">产生指定的表名称</param>
        /// <param name="strInterface">实现接口</param>
        public void CreateServicesByDbTable(string strPath, string strNameSpace, string[] lstTableNames, string strInterface)
        {
            var IDbFirst = _db.DbFirst;
            if (lstTableNames != null && lstTableNames.Length > 0)
            {
                IDbFirst = IDbFirst.Where(lstTableNames);
            }

            IDbFirst.IsCreateDefaultValue().IsCreateAttribute().SettingClassTemplate(c => c =
                @"using Blog.Core.IRepository;
                  using Blog.Core.IRepository.UnitOfWork;
                  using Blog.Core.IServices;
                  using Blog.Core.Model.Models;
                  using Blog.Core.Services.Base;
                                    
                  namespace " + strNameSpace + @"
                  {                        
                        public partial class {ClassName}Services : BaseServices<{ClassName}>, I{ClassName}Services" + (string.IsNullOrEmpty(strInterface) ? "" : (" , " + strInterface)) + @"
                        {
                            I{ClassName}Repository _dal;
                            public {ClassName}Services(I{ClassName}Repository dal)
                            {
                                this._dal = dal;
                                base.BaseDal = dal;
                            }
                        }
                  }
                "
            ).CreateClassFile(strPath, strNameSpace);
        }
        #endregion

        #region 实例方法
        /// <summary>
        /// 获取数据库处理对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public SimpleClient<T> GetEntityDb<T>() where T : class, new()
        {
            return new SimpleClient<T>(_db);
        }

        /// <summary>
        /// 获取数据库处理对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <returns></returns>
        public SimpleClient<T> GetEntityDb<T>(SqlSugarClient db) where T : class, new()
        {
            return new SimpleClient<T>(_db);
        }
        #endregion

        #region 根据实体类生成数据库表
        /// <summary>
        /// 根据实体生成数据库表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="blnBackupTable">是否备份表</param>
        /// <param name="lstEntitys">指定的实体</param>
        public void CreateTableByEntity<T>(bool blnBackupTable, params T[] lstEntitys) where T : class, new()
        {
            Type[] lstTypes = null;
            if (lstEntitys != null)
            {
                lstTypes = new Type[lstEntitys.Length];
                for (int i = 0; i < lstEntitys.Length; i++)
                {
                    T t = lstEntitys[i];
                    lstTypes[i] = typeof(T);

                }
            }
            CreateTableByEntity(blnBackupTable, lstTypes);
        }

        /// <summary>
        /// 根据实体类生成数据库表
        /// </summary>
        /// <param name="blnBackupTable">是否备份表</param>
        /// <param name="lstEntitys">指定的实体</param>
        public void CreateTableByEntity(bool blnBackupTable, params Type[] lstEntitys)
        {
            if (blnBackupTable)
            {
                _db.CodeFirst.BackupTable().InitTables(lstEntitys);
            }
            else
            {
                _db.CodeFirst.InitTables(lstEntitys);
            }
        }
        #endregion

        #region 静态方法
        /// <summary>
        /// 活得一个DbContext
        /// </summary>
        /// <returns></returns>
        public static MyContext GetDbContext()
        {
            return new MyContext();
        }

        /// <summary>
        /// 设置初始化参数
        /// </summary>
        /// <param name="strConnectionString">连接字符串</param>
        /// <param name="enumDbType">数据库类型</param>
        public static void Init(string strConnectionString, DbType enumDbType = SqlSugar.DbType.SqlServer)
        {
            _connectionString = strConnectionString;
            _dbType = enumDbType;
        }

        /// <summary>
        /// 创建一个链接配置
        /// </summary>
        /// <param name="blnIsAutoCloseConnection">是否自动关闭链接</param>
        /// <param name="blnIsSharedSameThread">是否跨类事务</param>
        /// <returns>ConnectionConfig</returns>
        public static ConnectionConfig GetConnectionConfig(bool blnIsAutoCloseConnection = true, bool blnIsSharedSameThread = false)
        {
            ConnectionConfig config = new ConnectionConfig()
            {
                ConnectionString = _connectionString,
                DbType = _dbType,
                IsAutoCloseConnection = blnIsAutoCloseConnection,
                ConfigureExternalServices = new ConfigureExternalServices()
                {
                    //DataInfoCacheService = new HttpRuntimeCache,
                },
                IsShardSameThread = blnIsSharedSameThread
            };
            return config;
        }

        /// <summary>
        /// 获取一个自定义的Db
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static SqlSugarClient GetCustomDb(ConnectionConfig config)
        {
            return new SqlSugarClient(config);
        }

        /// <summary>
        /// 获取一个自定义的数据库处理对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlSugarClient"></param>
        /// <returns></returns>
        public static SimpleClient<T> GetCustomEntityDb<T>(SqlSugarClient sqlSugarClient) where T : class, new()
        {
            return new SimpleClient<T>(sqlSugarClient);
        }

        /// <summary>
        /// 获取一个自定义的数据库处理对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config"></param>
        /// <returns></returns>
        public static SimpleClient<T> GetCustomEntityDb<T>(ConnectionConfig config) where T : class, new()
        {
            SqlSugarClient sqlSugarClient = GetCustomDb(config);
            return GetCustomEntityDb<T>(sqlSugarClient);
        }
        #endregion
    }
}
