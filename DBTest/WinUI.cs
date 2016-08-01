using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBTest
{
    class WinUI
    {
        public static void dgv_ShowColumns(DataGridView dgv,string columns)
        {
            var splits = columns.Split(',');
            List<string> cols = new List<string>();
            foreach (var col in splits)
            {
                if (col != "")
                    cols.Add(col.Trim());
            }
           // Dictionary<string,string>=new 
            
                 foreach (DataGridViewColumn c in dgv.Columns)
            {
                var colname = c.DataPropertyName;
                c.Visible = cols.Contains(colname);

            }
        }


        //通过指定文件路径读取excel的数据
        public static DataTable ReadExcelFile(string path)
        {
            return ReadExcelFile("Sheet1", path);
        }
        public static DataTable ReadExcelFile(string sheetName, string path)
        {
            using (OleDbConnection conn = new OleDbConnection())
            {
                DataTable dt = new DataTable();
                string Import_FileName = path;
                string fileExtension = Path.GetExtension(Import_FileName);
                if (fileExtension == ".xls")
                    conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 8.0;HDR=YES;'";
                if (fileExtension == ".xlsx")
                    conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                using (OleDbCommand comm = new OleDbCommand())
                {
                    comm.CommandText = "Select * from [" + sheetName + "$]";

                    comm.Connection = conn;

                    using (OleDbDataAdapter da = new OleDbDataAdapter())
                    {
                        da.SelectCommand = comm;
                        da.Fill(dt);
                        return dt;
                    }

                }
            }
        }


    }
}
