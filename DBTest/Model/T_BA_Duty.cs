using System;
using System.Xml.Serialization;

namespace CcbaSystem.Model.BA
{
    /// <summary>
    /// T_BA_Duty
    /// 2016-01-28 09:35
    /// </summary>	
    [Serializable]
    public partial class T_BA_Duty
    {
        #region Field Members
		public int ID { get; set; }
		public int 年 { get; set; }
		public int 月 { get; set; }
		public int 序号 { get; set; }
		public string 派驻单位 { get; set; }
		public int EmpID { get; set; }
		public string 姓名 { get; set; }
		public string 岗位 { get; set; }
		public int 年假 { get; set; }
		public int ClientID { get; set; }
		public string D1 { get; set; }
		public string D2 { get; set; }
		public string D3 { get; set; }
		public string D4 { get; set; }
		public string D5 { get; set; }
		public string D6 { get; set; }
		public string D7 { get; set; }
		public string D8 { get; set; }
		public string D9 { get; set; }
		public string D10 { get; set; }
		public string D11 { get; set; }
		public string D12 { get; set; }
		public string D13 { get; set; }
		public string D14 { get; set; }
		public string D15 { get; set; }
		public string D16 { get; set; }
		public string D17 { get; set; }
		public string D18 { get; set; }
		public string D19 { get; set; }
		public string D20 { get; set; }
		public string D21 { get; set; }
		public string D22 { get; set; }
		public string D23 { get; set; }
		public string D24 { get; set; }
		public string D25 { get; set; }
		public string D26 { get; set; }
		public string D27 { get; set; }
		public string D28 { get; set; }
		public string D29 { get; set; }
		public string D30 { get; set; }
		public string D31 { get; set; }
		public decimal 上班合计 { get; set; }
		public decimal 加班合计 { get; set; }
		public decimal 替班合计 { get; set; }
		public decimal 休息合计 { get; set; }
		public int 休年假 { get; set; }
		public string 备注 { get; set; }
		public int UserID { get; set; }
		public int statu { get; set; }
		public int 修改 { get; set; }
		public string 试用期 { get; set; }
		public int 停发工资 { get; set; }

        #endregion
    }
}
