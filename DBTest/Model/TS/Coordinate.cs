using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace DBTest.Model.TS
{
    //坐标
    [Table("Coordinate")]
    public class Coordinate
    {
        public int ID { get; set; }
        public int CoordinateID { get; set; }//未定义的ID
        public string CustomID { get; set; }//自定义ID, 用户绑定界面上的控件
        public string FormName { get; set; }  //窗口名
        public string ItemType { get; set; }  //分类
        public int CoordinateOrder { get; set; }  //坐标顺序,手动调顺序

        public string AreaName { get; set; } //区域名,箱子名, 用于区分一个区域有多个坐标
        public string CoordinateName { get; set; } //坐标名
        public string CoordinateDesc { get; set; }
        public float XAxis { get; set; }
        public float YAxis { get; set; }
        public float ZAxis { get; set; }
        public float AAxis { get; set; }
        public float BAxis { get; set; }
        public float CAxis { get; set; }
        public int IsZero { get; set; }  //是否零点
        public int Settled { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
