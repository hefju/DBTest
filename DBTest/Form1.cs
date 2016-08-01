using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CcbaSystem.Model.BA;

namespace DBTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DapperHelper dbHelper = DapperHelper.getInstance();
        private void button1_Click(object sender, EventArgs e)
        {
            var sql = "SELECT  * FROM T_BA_Employee where statu>-1 ";
            var list = dbHelper.GetList<T_BA_Employee>(sql);
            dgv.DataSource = list;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var date = new DateTime(2016,5,1);
            List<T_BA_Employee> list =( List < T_BA_Employee >) dgv.DataSource;
            foreach (var x in list)
            {
                var time =DateTime.Parse( x.入职时间);
             
                var months = MonthDifference(date, time);
                Console.WriteLine(x.姓名 + "-" + x.入职时间 + "  " + months.ToString());
                //if (months > 24)
                //{
                //    Console.WriteLine(x.姓名+"-"+x.入职时间+"  "+months.ToString());
                //}
            }
        }

        public static int MonthDifference(DateTime lValue, DateTime rValue)
        {
            return Math.Abs((lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year));
        }
    }
}
