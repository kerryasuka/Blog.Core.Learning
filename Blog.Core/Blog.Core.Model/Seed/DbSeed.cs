using Blog.Core.Common;
using Blog.Core.Common.Helper;
using Blog.Core.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Model.Seed
{
    public class DbSeed
    {
        // 这是我的在线demo数据，比较多，且杂乱
        // 国内网络不好的，可以使用这个gitee上的地址：https://gitee.com/laozhangIsPhi/Blog.Data.Share/raw/master/BlogCore.Data.json/{0}.tsv
        private static string GitJsonFileFormat = "https://github.com/anjoy8/Blog.Data.Share/raw/master/BlogCore.Data.json/{0}.tsv";

        // 这里我把重要的权限数据提出来的精简版，默认一个Admin_Role + 一个管理员用户，
        // 然后就是菜单+接口+权限分配，注意没有其他博客信息了，下边seeddata的时候，删掉即可。
        // 国内网络不好的，可以使用这个gitee上的地址：https://gitee.com/laozhangIsPhi/Blog.Data.Share/tree/master/Student.Achieve.json/{0}.tsv
        private static string GitJsonFileFormat2 = "https://github.com/anjoy8/Blog.Data.Share/raw/master/Student.Achieve.json/{0}.tsv";

        /// <summary>
        /// 异步添加种子数据
        /// </summary>
        /// <param name="myContext"></param>
        /// <returns></returns>
        public static async Task SeedAsync(MyContext myContext)
        {
            try
            {
                //如果生成过了，第二期就不用再执行一遍了，注释掉该方法即可

                #region 自动创建数据库暂停服务
                //自动创建数据库，注意版本是sqlsugar 5.x版本

                /*
                 *注意：当前sqlsugar版本在SqlServer和Sqlite中没有创建问题；在MySql中创建空数据库时，字符串不是utf-8的，因此需要手动创建空数据库，然后设置数据库为utf-8
                 */
                myContext.Db.DbMaintenance.CreateDatabase();
                #endregion

                //创建表
                myContext.CreateTableByEntity(false,
                    typeof(Advertisement),
                    typeof(BlogArticle),
                    typeof(GuestBook),
                    typeof(Module),
                    typeof(ModulePermission),
                    typeof(RoleModulePermission),
                    typeof(Role),
                    typeof(OperateLog),
                    typeof(PasswordLib),
                    typeof(Permission),
                    typeof(SysUserInfo),
                    typeof(Topic),
                    typeof(TopicDetail),
                    typeof(UserRole)
                    );

                //后期单独处理某些表
                //myContext.Db.CodeFirst.InitTables(typeof(SysUserInfo));
                //myContext.Db.CodeFirst.InitTables(typeof(Permission));
                //myContext.Db.CodeFirst.InitTables(typeof(Advertisement));

                Console.WriteLine("Database: WMBlog created success!");
                Console.WriteLine();

                if (AppSettings.App(new string[] { "AppSettings", "SeedDbDataEnabled" }).ObjToBoolean())
                {
                    Console.WriteLine("Seeding database...");

                    #region BlogArticle
                    if (!await myContext.Db.Queryable<BlogArticle>().AnyAsync())
                    {
                        myContext.GetEntityDb<BlogArticle>().InsertRange(JsonHelper.FromJson<List<BlogArticle>>(NetDataHelper.Get(string.Format(GitJsonFileFormat, "BlogArticle"))));
                        Console.WriteLine("Table: BlogArticle created success!");
                    }
                    else
                    {
                        Console.WriteLine("Table: BlogArticle already exists...");
                    }
                    #endregion

                    #region Module
                    if (!await myContext.Db.Queryable<Module>().AnyAsync())
                    {
                        myContext.GetEntityDb<Module>().InsertRange(JsonHelper.FromJson<List<Module>>(NetDataHelper.Get(string.Format(GitJsonFileFormat, "Module"))));
                        Console.WriteLine("Table: Module created success!");
                    }
                    else
                    {
                        Console.WriteLine("Table: Module already exists...");
                    }
                    #endregion

                    #region Permission
                    if (!await myContext.Db.Queryable<Permission>().AnyAsync())
                    {
                        myContext.GetEntityDb<Permission>().InsertRange(JsonHelper.FromJson<List<Permission>>(NetDataHelper.Get(string.Format(GitJsonFileFormat, "Permission"))));
                        Console.WriteLine("Table: Permission created success!");
                    }
                    else
                    {
                        Console.WriteLine("Table: Permission already exists...");
                    }
                    #endregion

                    #region Role
                    if (!await myContext.Db.Queryable<Role>().AnyAsync())
                    {
                        myContext.GetEntityDb<Role>().InsertRange(JsonHelper.FromJson<List<Role>>(NetDataHelper.Get(string.Format(GitJsonFileFormat, "Role"))));
                        Console.WriteLine("Table: Role created success!");
                    }
                    else
                    {
                        Console.WriteLine("Table: Role already exists...");
                    }
                    #endregion

                    #region RoleModulePermission
                    if (!await myContext.Db.Queryable<RoleModulePermission>().AnyAsync())
                    {
                        myContext.GetEntityDb<RoleModulePermission>().InsertRange(JsonHelper.FromJson<List<RoleModulePermission>>(NetDataHelper.Get(string.Format(GitJsonFileFormat, "RoleModulePermission"))));
                        Console.WriteLine("Table: RoleModulePermission created success!");
                    }
                    else
                    {
                        Console.WriteLine("Table: RoleModulePermission already exists...");
                    }
                    #endregion

                    #region Topic
                    if (!await myContext.Db.Queryable<Topic>().AnyAsync())
                    {
                        myContext.GetEntityDb<Topic>().InsertRange(JsonHelper.FromJson<List<Topic>>(NetDataHelper.Get(string.Format(GitJsonFileFormat, "Topic"))));
                        Console.WriteLine("Table: Topic created success!");
                    }
                    else
                    {
                        Console.WriteLine("Table: Topic already exists...");
                    }
                    #endregion

                    #region TopicDetail
                    if (!await myContext.Db.Queryable<TopicDetail>().AnyAsync())
                    {
                        myContext.GetEntityDb<TopicDetail>().InsertRange(JsonHelper.FromJson<List<TopicDetail>>(NetDataHelper.Get(string.Format(GitJsonFileFormat, "TopicDetail"))));
                        Console.WriteLine("Table: TopicDetail created success!");
                    }
                    else
                    {
                        Console.WriteLine("Table: TopicDetail already exists...");
                    }
                    #endregion

                    #region UserRole
                    if (!await myContext.Db.Queryable<UserRole>().AnyAsync())
                    {
                        myContext.GetEntityDb<UserRole>().InsertRange(JsonHelper.FromJson<List<UserRole>>(NetDataHelper.Get(string.Format(GitJsonFileFormat, "UserRole"))));
                        Console.WriteLine("Table: UserRole created success!");
                    }
                    else
                    {
                        Console.WriteLine("Table: UserRole already exists...");
                    }
                    #endregion

                    #region SysUserInfo
                    if (!await myContext.Db.Queryable<SysUserInfo>().AnyAsync())
                    {
                        myContext.GetEntityDb<SysUserInfo>().InsertRange(JsonHelper.FromJson<List<SysUserInfo>>(NetDataHelper.Get(string.Format(GitJsonFileFormat, "SysUserInfo"))));
                        Console.WriteLine("Table: SysUserInfo created success!");
                    }
                    else
                    {
                        Console.WriteLine("Table: SysUserInfo already exists...");
                    }
                    #endregion

                    Console.WriteLine("Done seeding database.");
                }

                Console.WriteLine();
            }
            catch (Exception ex)
            {
                throw new Exception("1、注意先创建空的数据库；\n2、" + ex.Message);
            }
        }
    }
}
