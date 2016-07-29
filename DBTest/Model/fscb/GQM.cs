using System;
using System.Xml.Serialization;

namespace CcbaSystem.Model.JF
{
    /// <summary>
    /// GQM
    /// 2016-07-29 09:52
    /// </summary>	
    [Serializable]
    public partial class GQM
    {
        #region Field Members
		public int ID { get; set; }
		public string 编号 { get; set; }
		public string 名称 { get; set; }
		public string 型号 { get; set; }
		public string 单位 { get; set; }
		public string 货架号 { get; set; }
		public string 类别 { get; set; }
		public string 收料 { get; set; }
		public string 一级编码 { get; set; }
		public string 二级编码 { get; set; }
		public decimal 单价 { get; set; }
		public string 产地 { get; set; }
		public string 旧编号 { get; set; }
		public string 供应商 { get; set; }

        #endregion
    }
}
