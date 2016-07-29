using System;
using System.Collections.Generic;
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
    }
}
