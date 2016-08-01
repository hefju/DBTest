using System;
using System.Xml.Serialization;

namespace CcbaSystem.Model.BA
{
    /// <summary>
    /// T_BA_Employee
    /// 2016-03-24 17:17
    /// </summary>	
    [Serializable]
    public partial class T_BA_Employee
    {
        #region Field Members
		public int ID { get; set; }
		public int 序号 { get; set; }
		public string 档案号 { get; set; }
		public string 姓名 { get; set; }
		public string 真实姓名 { get; set; }
		public string 拼音码 { get; set; }
		public string 曾用名 { get; set; }
		public string 公司 { get; set; }
		public string 部门 { get; set; }
		public string 岗位 { get; set; }
		public string 派驻单位 { get; set; }
		public string 负责队长 { get; set; }
		public string 资格证号 { get; set; }
		public string 省厅资格证号 { get; set; }
		public string 上岗证 { get; set; }
		public string 电子照 { get; set; }
		public string 籍贯 { get; set; }
		public string 银行 { get; set; }
		public string 银行账号 { get; set; }
		public string 性别 { get; set; }
		public string 身份证号 { get; set; }
		public int 年龄 { get; set; }
		public string 民族 { get; set; }
		public string 文化程度 { get; set; }
		public int 身高 { get; set; }
		public string 入职时间 { get; set; }
		public string 户籍地址 { get; set; }
		public string 佛山暂住地址 { get; set; }
		public string 电话号码 { get; set; }
		public decimal 岗位补贴 { get; set; }
		public decimal 电话费补助 { get; set; }
		public decimal 班长费 { get; set; }
		public decimal 房租补贴 { get; set; }
		public decimal 扣社保金额 { get; set; }
		public string 购社保地 { get; set; }
		public string 参保情况 { get; set; }
		public string 停保时间 { get; set; }
		public string 是否服役 { get; set; }
		public string 合同期日 { get; set; }
		public string 合同止日 { get; set; }
		public string 年假情况 { get; set; }
		public bool 在职 { get; set; }
		public string 离职时间 { get; set; }
		public string 离职原因 { get; set; }
		public string 保安类型 { get; set; }
		public string 员工类型 { get; set; }
		public string 工资标准 { get; set; }
		public bool 在用 { get; set; }
		public bool 工作证 { get; set; }
		public string 备注 { get; set; }
		public string GlobalID { get; set; }
		public int statu { get; set; }
		public bool 已发工资 { get; set; }
		public string 政治面貌 { get; set; }

        #endregion
    }
}
