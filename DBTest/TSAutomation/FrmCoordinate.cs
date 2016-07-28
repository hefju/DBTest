using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DBTest.Model.TS;

namespace DBTest.TSAutomation
{
    public partial class FrmCoordinate : Form
    {
        DapperHelper dbHelper = DapperHelper.getInstance();
        public FrmCoordinate()
        {
            InitializeComponent();
            dbHelper.DbType = DbTypes.Sqlite;
            dbHelper.ConnString=ConfigurationManager.AppSettings["ConnString_sqlite"];//ConnString3
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var sql = "select * from Coordinate where formname<>'Frm05_2' order by formname,customID";
            var list = dbHelper.GetList<Coordinate>(sql);
            dgv.DataSource = list;
        }

        private void FrmCoordinate_Load(object sender, EventArgs e)
        {

        }

        private bool SysChange = false;
        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (SysChange) return;
            var colName = dgv.Columns[e.ColumnIndex].DataPropertyName;
            var ID = dgv["ID", e.RowIndex].Value.ToString();
            if (colName == "CustomID")
            {
                var customID = dgv["CustomID", e.RowIndex].Value.ToString();
                var sql = string.Format("select count(*) from Coordinate where CustomID='{0}'", customID);
                var count = dbHelper.ExecuteScalar<int>(sql);
             //   MessageBox.Show(count.ToString());
                if (count < 1)
                {
                    sql = string.Format("update Coordinate set CustomID='{0}' where ID={1}", customID, ID);
                    count = (int)dbHelper.Execute(sql);
                    if (count < 1)
                        MessageBox.Show("更新失败.");
                }
                else
                {
                    SysChange = true;
                    dgv["CustomID", e.RowIndex].Value = "";
               
                    SysChange = false;
                    MessageBox.Show(customID + " 已经存在不能设置.");
                }
            }
        }
    }
}
