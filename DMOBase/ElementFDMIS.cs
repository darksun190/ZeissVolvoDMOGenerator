using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMOBase
{
    public class ElementFDMIS : IElementDMIS
    {
        #region 公共方法 public method

        #endregion

        #region 字段 variable
        DMISFeatureType FeatureType = DMISFeatureType.POINT;
        private string rawstring;
        public DMISFType Ftype
        {
            get;
            set;
        }
        string name;
        double x, y, z, i, j, k;


        #endregion

        #region 属性 properties
        public string Name
        {
            get
            {
                return name;
            }
        }
        #endregion

        #region 构造函数 construction
        public ElementFDMIS(string feature_buf)
        {
            rawstring = feature_buf;
            if (feature_buf.Substring(0, 2) == "FA")
            {
                Ftype = DMISFType.FA;
            }
            else if (feature_buf.Substring(0, 1) == "F")
            {
                Ftype = DMISFType.F;
            }
            else
            {
                throw new ArgumentNullException();
            }
            int pos_bracket = feature_buf.IndexOf('(');
            int pos_anti_bracket = feature_buf.IndexOf(')');
            name = feature_buf.Substring(pos_bracket + 1, pos_anti_bracket - pos_bracket - 1);
            int pos_cart = feature_buf.IndexOf("CART,");
            string[] coordinates = feature_buf.Substring(pos_cart + 5).Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            x = double.Parse(coordinates[0]);
            y = double.Parse(coordinates[1]);
            z = double.Parse(coordinates[2]);
            i = double.Parse(coordinates[3]);
            j = double.Parse(coordinates[4]);
            k = double.Parse(coordinates[5]);
        }
        #endregion

        #region 派生可见方法
        public string getName()
        {
            return string.Format("{0}<{1}>", Ftype.ToString(), name);
        }
        public string getType()
        {
            return string.Format("FEAT/POINT,CART");
        }
        public string ToString()
        {
            string ftype = Ftype.ToString();

            return string.Format(@"{0}({1})={2},{3},{4},{5},${6}    {7},{8},{9}",
                new object[] {
                    ftype,          //0
                    name,           //1
                    getType(),      //2
                    x,              //3
                    y,              //4
                    z,              //5
                    Environment.NewLine,
                    i,              //7
                    j,              //8
                    k               //9
                });
        }
        #endregion

        #region 私有方法 private mehod

        #endregion

        #region 静态成员

        #endregion

    }
}
