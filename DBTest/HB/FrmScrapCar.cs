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

namespace DBTest.HB
{
    public partial class FrmScrapCar : Form
    {
        DapperHelper dbHelper = DapperHelper.getInstance();
        public FrmScrapCar()
        {
            InitializeComponent();
            dbHelper.DbType = DbTypes.MsSQL;
            dbHelper.ConnString = ConfigurationManager.AppSettings["ConnString_hb"];//ConnString3
            lblInfo.Text = dbHelper.ConnString;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dt = WinUI.ReadExcelFile("报废，状态已拆解，拆解日期2016-3-28(1).xls");
            dgv.DataSource = dt;
        }
    }
}
