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

namespace DBTest.Fscb
{
    public partial class FrmT_CD_EXECNode : Form
    {
        DapperHelper dbHelper = DapperHelper.getInstance();
        public FrmT_CD_EXECNode()
        {
            InitializeComponent();
            dbHelper.DbType = DbTypes.MsSQL;
            dbHelper.ConnString = ConfigurationManager.AppSettings["ConnString_fscb"];//ConnString3
            lblInfo.Text = dbHelper.ConnString;
        }

        private void FrmT_CD_EXECNode_Load(object sender, EventArgs e)
        {

        }
    }
}
