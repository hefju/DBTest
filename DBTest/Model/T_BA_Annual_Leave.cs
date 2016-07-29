using System;
using System.Xml.Serialization;

namespace CcbaSystem.Model.BA
{
    /// <summary>
    /// T_BA_Annual_Leave
    /// 2016-07-26 17:17
    /// </summary>	
    [Serializable]
    public partial class T_BA_Annual_Leave
    {
        #region Field Members
		public int ID { get; set; }
		public int 年 { get; set; }
		public int 月 { get; set; }
		public int EmpID { get; set; }
		public string 姓名 { get; set; }
		public int 存年假 { get; set; }
		public int 休年假 { get; set; }
		public int 剩余年假 { get; set; }
		public string 备注 { get; set; }
		public string 入职时间 { get; set; }
		public bool 在职 { get; set; }

        #endregion
    }
}
