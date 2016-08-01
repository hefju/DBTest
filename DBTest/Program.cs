using DBTest.ccba.BA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using DBTest.TSAutomation;
using DBTest.Fscb;
using DBTest.HB;

namespace DBTest
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //string attDbPath = ConfigurationManager.AppSettings["ConnString_sqlite"];//ConnString3
            //DapperHelper dbHelper = DapperHelper.getInstance();
            //dbHelper.ConnString = attDbPath;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FrmT_BA_Annual_Leave());//Form1
           // Application.Run(new FrmCoordinate());//Form1 FrmGQM
           // Application.Run(new FrmGQM()); 
            Application.Run(new FrmScrapCar()); 
            

           
        }
    }
}
