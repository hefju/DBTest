using CcbaSystem.Model.BA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace DBTest.ccba.BA
{
    public partial class FrmT_BA_Annual_Leave : Form
    {
        DapperHelper dbHelper = DapperHelper.getInstance();
        public FrmT_BA_Annual_Leave()
        {
            InitializeComponent();
            dbHelper.DbType = DbTypes.MsSQL;
            dbHelper.ConnString = ConfigurationManager.AppSettings["ConnString3"];//
        }

        #region 初始化年假
        private void button1_Click(object sender, EventArgs e)
        {
            var sql = "SELECT  ID,姓名,入职时间,在职,年假情况 FROM T_BA_Employee where statu>-1 ";
            var list = dbHelper.GetList<T_BA_Employee>(sql);
            dgv.DataSource = list;
            WinUI.dgv_ShowColumns(dgv, "ID,姓名,入职时间,在职,年假情况");

        }

        public  int MonthDifference(DateTime lValue, DateTime rValue)
        {
            //return Math.Abs((lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year));
            var tmp1=lValue.Month - rValue.Month;
            var tmp2= 12 * (lValue.Year - rValue.Year);
            return Math.Abs((lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year));
        }

        private Dictionary<int, int> Annual = new Dictionary<int, int>();//EmpID,本月的年假.
        private Dictionary<int, DateTime> InTime = new Dictionary<int, DateTime>();//EmpID,入职时间
        private void button2_Click(object sender, EventArgs e)
        {
            var date = new DateTime(2016, 5, 1);
            List<T_BA_Employee> list = (List<T_BA_Employee>)dgv.DataSource;
            foreach (var x in list)
            {
               if(x.ID== 182)
                CalcAnnual_Leave(date,x);
            }
            MessageBox.Show("统计完毕.");
            this.Cursor = Cursors.WaitCursor;
            var t1 = DateTime.Now;
            List<string> sqls = new List<string>();
            foreach (var y in list)
            {
                var intime = GetInTime(y.ID);
                sqls.Add(string.Format("INSERT INTO T_BA_Annual_Leave(年, 月, EmpID, 姓名, 存年假, 休年假, 剩余年假, 备注, 在职,入职时间,CreateAt)" +
                    " VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',getdate())", date.Year, date.Month, y.ID, y.姓名, Annual[y.ID], 0, Annual[y.ID], y.年假情况, y.在职, intime));
            }
            dbHelper.GroupExec(sqls);
            this.Cursor = Cursors.Default;
            var span= DateTime.Now- t1;
            //dgv2.DataSource = list;
            //WinUI.dgv_ShowColumns(dgv, "ID,姓名,入职时间,在职,年假情况");
            MessageBox.Show("插入完毕.耗时:" + span.TotalMilliseconds+"毫秒.");
        }

        private string GetInTime(int ID)
        {
            if(InTime.ContainsKey(ID))
            return InTime[ID].ToString("yyyy-MM-dd");
            else
                return "";
        }

        private void CalcAnnual_Leave(DateTime date, T_BA_Employee x)
        {
            if (x.入职时间 == "")
            {
                Annual[x.ID] = 0;
                x.年假情况 = "错误,未填写入职时间";
                return;
            }
            var time = DateTime.Parse(x.入职时间);
            InTime[x.ID] = time;
            var months = MonthDifference(date, time);
            if(months>=24)
            {
          
                var span=date-time;
                var days = span.TotalDays;
                if (days >= 365&&days<365*10)
                {
                    Annual[x.ID] = 5;
                    x.年假情况 = "满两年,5天年假";
                }
                else if(days>=365*10&&days<365*20)
                {
                    Annual[x.ID] = 10;
                    x.年假情况 = "满两年,10天年假";
                }
                else if (days >= 365 * 20)
                {
                    Annual[x.ID] = 15;
                    x.年假情况 = "满两年,15天年假";
                }
            }
            else if (months < 12)
            {
                Annual[x.ID] = 0;
                x.年假情况 = "不满一年,无年假";
            }
            else
            {
                //计算出入职的月数
                var num = (date.Year - time.Year - 1) * 12 + date.Month - time.Month;
                if (num % 2 == 0)//求余为0,表示双数
                {
                    Annual[x.ID] = 1;
                    //var day = num / 2; //可激活的天数, 初始化才这么做, 实际每月计算是 var day=1;
                    var day = 1;
                    x.年假情况 = "入职1年:" + day.ToString() + "天年假," + num.ToString();
                }
                else
                {
                    Annual[x.ID] = 0;
                    x.年假情况 = "满1年非年假月份"; //"不满两个月无年假";
                }
            }
        }
        #endregion


        private Dictionary<int, int> Annual_Use = new Dictionary<int, int>();//EmpID,本月使用的年假
        private void button3_Click(object sender, EventArgs e)
        {   
         //   var date = new DateTime(2016, 6, 1);
            var date = new DateTime(int.Parse(txtYear.Text),int.Parse(txtMonth.Text),1);
            var sql = string.Format("sp_BA_GetYearLeave_T_BA_Duty {0},{1},{2}",date.Year,date.Month,1);
            var list = dbHelper.GetList<T_BA_Duty>(sql);
            CalcUse(list);
          //  MessageBox.Show("计算完毕.");

            dgv.DataSource = Annual_Use.ToList();
            dgv.Columns["key"].HeaderText = "EmpID";
            dgv.Columns["value"].HeaderText = "休年假";
          //  dgv.DataSource = Annual_Use;// list;
         //   WinUI.dgv_ShowColumns(dgv, "ID,姓名,入职时间,在职,年假情况");
        }

        //统计每个人当月使用的年假
        private void CalcUse(List<T_BA_Duty> list)
        {
            foreach (var item in list)
            {
                var count = 0;
                if (item.D1 == "年假" || item.D1 == "年加") count++;
                if (item.D2 == "年假" || item.D2 == "年加") count++;
                if (item.D3 == "年假" || item.D3 == "年加") count++;
                if (item.D4 == "年假" || item.D4 == "年加") count++;
                if (item.D5 == "年假" || item.D5 == "年加") count++;
                if (item.D6 == "年假" || item.D6 == "年加") count++;
                if (item.D7 == "年假" || item.D7 == "年加") count++;
                if (item.D8 == "年假" || item.D8 == "年加") count++;
                if (item.D9 == "年假" || item.D9 == "年加") count++;
                if (item.D10 == "年假" || item.D10 == "年加") count++;
                if (item.D11 == "年假" || item.D11 == "年加") count++;
                if (item.D12 == "年假" || item.D12 == "年加") count++;
                if (item.D13 == "年假" || item.D13 == "年加") count++;
                if (item.D14 == "年假" || item.D14 == "年加") count++;
                if (item.D15 == "年假" || item.D15 == "年加") count++;
                if (item.D16 == "年假" || item.D16 == "年加") count++;
                if (item.D17 == "年假" || item.D17 == "年加") count++;
                if (item.D18 == "年假" || item.D18 == "年加") count++;
                if (item.D19 == "年假" || item.D19 == "年加") count++;
                if (item.D20 == "年假" || item.D20 == "年加") count++;
                if (item.D21 == "年假" || item.D21 == "年加") count++;
                if (item.D22 == "年假" || item.D22 == "年加") count++;
                if (item.D23 == "年假" || item.D23 == "年加") count++;
                if (item.D24 == "年假" || item.D24 == "年加") count++;
                if (item.D25 == "年假" || item.D25 == "年加") count++;
                if (item.D26 == "年假" || item.D26 == "年加") count++;
                if (item.D27 == "年假" || item.D27 == "年加") count++;
                if (item.D28 == "年假" || item.D28 == "年加") count++;
                if (item.D29 == "年假" || item.D29 == "年加") count++;
                if (item.D30 == "年假" || item.D30 == "年加") count++;
                if (item.D31 == "年假" || item.D31 == "年加") count++;

                Annual_Use[item.EmpID] = count;
            }
        }


        Dictionary<int, int> lastMonth = new Dictionary<int, int>();//上月存年假
        //读取上月年假
        private void button4_Click(object sender, EventArgs e)
        {

            var date = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMonth.Text), 1);// var date = new DateTime(2016, 6, 1);
            date = date.AddMonths(-1);
            var sql =string.Format( "SELECT  * FROM T_BA_Annual_Leave where 年={0} and 月={1} ",date.Year,date.Month);
           // lastMonth.Clear();
            var list = dbHelper.GetList<T_BA_Annual_Leave>(sql);
            dgv.DataSource = list;
            //将上月年假存放到字典里面
            foreach (var item in list)
            {
                lastMonth[item.EmpID] = item.剩余年假;
            }
        }
        //生成下月年假
        private void button5_Click(object sender, EventArgs e)
        {
            var date = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMonth.Text), 1); //var date = new DateTime(2016, 6, 1);
            //1.获取人员名单
            var sql = "SELECT  ID,姓名,入职时间,在职,年假情况 FROM T_BA_Employee where statu>-1 ";
            var Emplist = dbHelper.GetList<T_BA_Employee>(sql);
            dgv.DataSource = Emplist;
            WinUI.dgv_ShowColumns(dgv, "ID,姓名,入职时间,在职,年假情况");
            //2.获取上月年假数据
            var mapLeaveLast = lastMonth;
            //3.获取本月休假天数
            var mapRest = Annual_Use;

            if (mapLeaveLast.Count < 1)
            {
                MessageBox.Show("上月年假数据未统计.");
                return;
            }
            if (mapRest.Count < 1)
            {
                MessageBox.Show("本月休假天数未统计.");
                return;
            }

            //4.生成本月年假数据
            List<T_BA_Annual_Leave> list = new List<T_BA_Annual_Leave>();
            foreach (var emp in Emplist)
            {
                T_BA_Annual_Leave one = new T_BA_Annual_Leave();
                one.年=date.Year;
                one.月=date.Month;
                one.EmpID=emp.ID;
                one.姓名=emp.姓名;
                one.存年假 = GetLeaveLast(mapLeaveLast, emp.ID); //mapLeaveLast[emp.ID];//上月剩余年假
                one.休年假 = GetRest(mapRest,emp.ID);// mapRest[emp.ID];//本月休年假
                one.剩余年假=  one.存年假-  one.休年假;
                one.备注="";
                one.入职时间=emp.入职时间;
                one.在职 = emp.在职;
                list.Add(one);
            }
          //  dgv.DataSource = list;
            this.Cursor = Cursors.WaitCursor;
            var t1 = DateTime.Now;
            List<string> sqls = new List<string>();
            foreach (var y in list)
            {
                var intime = GetInTime(y.ID);
                sqls.Add(string.Format("INSERT INTO T_BA_Annual_Leave(年, 月, EmpID, 姓名, 存年假, 休年假, 剩余年假, 备注, 在职,入职时间,CreateAt)" +
                    " VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',getdate())", y.年, y.月, y.EmpID, y.姓名, y.存年假, y.休年假, y.剩余年假, y.备注, y.在职, y.入职时间,DateTime.Now));
            }
            dbHelper.GroupExec(sqls);
            this.Cursor = Cursors.Default;
            var span = DateTime.Now - t1;
            MessageBox.Show("插入完毕.耗时:" + span.TotalMilliseconds + "毫秒.");


        }

        private int GetRest(Dictionary<int, int> mapRest, int empID)
        {
            int value=0;
            mapRest.TryGetValue(empID,out value);
            return value;           
        }

        private int GetLeaveLast(Dictionary<int, int> mapLeaveLast, int empID)
        {
            int value = 0;
            mapLeaveLast.TryGetValue(empID, out value);
            return value;      
        }

        private void FrmT_BA_Annual_Leave_Load(object sender, EventArgs e)
        {
            lblInfo.Text = "注意:当前连接是:"+dbHelper.ConnString;
            SetTime();

        }

        private void SetTime()
        {
            var time = DateTime.Now.AddMonths(-1);
            txtYear.Text = time.Year.ToString();
            txtMonth.Text = time.Month.ToString();

        }



    }
}
