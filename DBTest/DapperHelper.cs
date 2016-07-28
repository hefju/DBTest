using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

namespace DBTest
{
    public enum DbTypes
    {
        Sqlite,
        MsSQL,
        MySQL,
        PostgreSQL,
        None

    }
    class DapperHelper
    {
           private static DapperHelper singleton;
           private DapperHelper() { }
           public static DapperHelper getInstance()
        {
            if (singleton == null)
                singleton = new DapperHelper();
            return singleton;
        }

           public string ConnString = "";
           public DbTypes DbType = DbTypes.None ;
    

        public List<T> GetList<T>(string sql)
        {
            using (var conn = GetConn())
            {
                conn.Open();
                var a = conn.Query<T>(sql, null);
                conn.Close();
                return a.ToList();
            }
        }

        
        public T ExecuteScalar<T>(string sql)
        {
            using (var conn = GetConn())
            {
                conn.Open();
                var a = conn.ExecuteScalar<T>(sql, null);
                conn.Close();
                return a;
            }
        }
        public int Execute(string sql)
        {
            using (var conn = GetConn())
            {
                conn.Open();
                var a = conn.Execute(sql, null);
                conn.Close();
                return a;
            }
        }

        public void GroupExec(List<string> sqls)
        {
            using (var conn = GetConn())
            {
                conn.Open();
                //开户事务
                var trans = conn.BeginTransaction();
                try
                {
                    foreach (var sql in sqls)
                    {
                        var rows = conn.Execute(sql, null, trans);
                    }
                    trans.Commit();
                }
                catch(Exception e)
                {
                    trans.Rollback();
                    throw e;
                }

            }
        }

        private System.Data.IDbConnection GetConn()
        {
            IDbConnection conn = null;
            switch (DbType)
            {
                case DbTypes.Sqlite:
                    conn = new System.Data.SQLite.SQLiteConnection(ConnString);
                    break;
                case DbTypes.MsSQL:
                    conn = new System.Data.SqlClient.SqlConnection(ConnString);
                    break;
                case DbTypes.MySQL:
                //    conn = new MySql.Data.MySqlClient.MySqlConnection(ConnString);
                    break;
                default:
                    break;
            }
            //  var conn = new System.Data.SQLite.SQLiteConnection(ConnString);
           // var conn = new System.Data.SqlClient.SqlConnection(ConnString);
           // var conn = new MySql.Data.MySqlClient.MySqlConnection(ConnString);
            return conn;
        }
    }
}
