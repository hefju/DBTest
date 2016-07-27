﻿using DBTest.ccba.BA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

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
            string attDbPath = ConfigurationManager.AppSettings["ConnString2"];
            DapperHelper dbHelper = DapperHelper.getInstance();
            dbHelper.ConnString = attDbPath;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmT_BA_Annual_Leave());//Form1

           
        }
    }
}
