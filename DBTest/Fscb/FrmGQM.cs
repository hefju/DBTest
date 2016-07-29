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
using CcbaSystem.Model.JF;

namespace DBTest.Fscb
{
    public partial class FrmGQM : Form
    {
        DapperHelper dbHelper = DapperHelper.getInstance();
        public FrmGQM()
        {
            InitializeComponent();
            dbHelper.DbType = DbTypes.MsSQL;
            dbHelper.ConnString = ConfigurationManager.AppSettings["ConnString_fscb"];//ConnString3
            lblInfo.Text = dbHelper.ConnString;
        }

        private void FrmGQM_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var sql = "select * from GQM";
            var list = dbHelper.GetList<GQM>(sql);
            dgv.DataSource = list;
            WinUI.dgv_ShowColumns(dgv,"编号,名称, 型号, 单位, 单价, 产地, 旧编号, 供应商");
            UpdateNumber(list);
        }

        Dictionary<string, int> mapNum = new Dictionary<string, int>();//6位编号字典,如果不在字典, 就+0001, 并增加到字典里面
        //6位编号升级到10位编号
        private void UpdateNumber(List<GQM> list)
        {
            int count = 0;
            foreach (var item in list)
            {
                var bh = item.旧编号;
                if (bh.Length != 6)
                    continue;
                int tmp = 1;
                if (!mapNum.TryGetValue(bh, out tmp))
                {
                    mapNum[bh] = 1;
                }
                else
                {
                    mapNum[bh]++;
                }

                tmp++;
                item.编号 = item.编号 + tmp.ToString().PadLeft(4, '0');// item.编号 + '-' + tmp.ToString().PadLeft(4, '0');
                count++;
                //if (count > 50)
                //    return;
            }
            dgv.DataSource = list;
         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            List<string> sqls = new List<string>();
            List<GQM> list = (List<GQM>)dgv.DataSource;
            var t1 = DateTime.Now;

            int count = 0;
            foreach (var item in list)
            {
                var sql = string.Format("update GQM set 编号='{0}' where ID={1}",item.编号,item.ID);
                sqls.Add(sql);
                count++;
                //if (count > 50)
                //    break;
            }
            dbHelper.GroupExec(sqls);
            this.Cursor = Cursors.Default;
            var span = DateTime.Now - t1;
            MessageBox.Show("更新完毕.耗时:" + span.TotalMilliseconds + "毫秒.");
           // MessageBox.Show("更新完毕");
        }
    }
}
