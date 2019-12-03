using Blog.Core.Common.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Blog.Core.Common.DB
{
    public class BaseDbConfig
    {
        private static string sqliteConnection = AppSettings.App(new string[] { "AppSettings", "Sqlite", "SqliteConnection" });
        private static bool isSqliteEnable = AppSettings.App(new string[] { "AppSettings", "Sqlite", "Enabled" }).ObjToBool();

        private static string sqlServerConnection = AppSettings.App(new string[] { "AppSettings", "SqlServer", "SqlServerConnection" });
        private static bool isSqlServerEnable = AppSettings.App(new string[] { "AppSettings", "SqlServer", "Enabled" }).ObjToBool();

        private static string mySqlConnection = AppSettings.App(new string[] { "AppSettings", "MySql", "MySqlConnection" });
        private static bool isMySqlEnable = AppSettings.App(new string[] { "AppSettings", "MySql", "Enabled" }).ObjToBool();

        private static string oracleConnection = AppSettings.App(new string[] { "AppSettings", "Oracle", "OracleConnection" });
        private static bool isOracleEnable = AppSettings.App(new string[] { "AppSettings", "Oracle", "Enabled" }).ObjToBool();

        public static string ConnectionString => InitConn();
        public static DataBaseType DbType = DataBaseType.SqlServer;

        private static string InitConn()
        {
            if (isSqliteEnable)
            {
                DbType = DataBaseType.Sqlite;
                return sqliteConnection;
            }
            else if (isSqlServerEnable)
            {
                DbType = DataBaseType.SqlServer;
                return sqlServerConnection;
            }
            else if (isMySqlEnable)
            {
                DbType = DataBaseType.MySql;
                return mySqlConnection;
            }
            else if (isOracleEnable)
            {
                DbType = DataBaseType.Oracle;
                return oracleConnection;
                //return DifDbConnOfSecurity(@"D:\Oracle_Conn.txt", @"D:\Oracle_Conn.txt", oracleConnection);
            }
            else
            {
                return sqliteConnection;
            }
        }

        private static string DifDbConnOfSecurity(params string[] conn)
        {
            foreach (var item in conn)
            {
                try
                {
                    if (File.Exists(item))
                    {
                        return File.ReadAllText(item).Trim();
                    }
                }
                catch (Exception) { }
            }

            return conn[conn.Length - 1];
        }
    }

    public enum DataBaseType
    {
        MySql = 0,
        SqlServer = 1,
        Sqlite = 2,
        Oracle = 3,
        PostgreSQL = 4,
    }
}
