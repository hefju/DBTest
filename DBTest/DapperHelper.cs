using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

namespace DBTest
{
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
            //  var conn = new System.Data.SQLite.SQLiteConnection(ConnString);
            var conn = new System.Data.SqlClient.SqlConnection(ConnString);
           // var conn = new MySql.Data.MySqlClient.MySqlConnection(ConnString);
            return conn;
        }
    }
}
