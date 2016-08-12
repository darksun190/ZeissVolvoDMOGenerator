using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMOBase
{
    public class ElementTDMIS : IElementDMIS
    {
        #region 公共方法 public method

        #endregion

        #region 字段 variable
        string name;
        DMISTolDirection tolDirection;
        ToleranceValue tol;
        double actual;
        bool status;
        private string rawstring;


        #endregion

        #region 属性 properties
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public DMISTType TType
        {
            get;
            set;
        }
        public DMISTolDirection TolDirection
        {
            get
            {
                return tolDirection;
            }
        }
        #endregion

        #region 构造函数 construction
        public ElementTDMIS(string tolerance_buf)
        {
            rawstring = tolerance_buf;
            if (tolerance_buf.Substring(0, 2) == "TA")
            {
                TType = DMISTType.TA;
            }
            else if (tolerance_buf.Substring(0, 1) == "T")
            {
                TType = DMISTType.T;
            }
            else
            {
                throw new ArgumentNullException();
            }

            int pos_bracket = tolerance_buf.IndexOf('(');
            int pos_anti_bracket = tolerance_buf.IndexOf(')');
            name = tolerance_buf.Substring(pos_bracket + 1, pos_anti_bracket - pos_bracket - 1);
            int pos_TOL = tolerance_buf.IndexOf("TOL/");
            int pos_comma = tolerance_buf.IndexOf(",");
            int pos_comma_2 = pos_comma;
            string tol_dire_buf = tolerance_buf.Substring(pos_TOL + 4, pos_comma - pos_TOL - 4);
            if (tol_dire_buf == "PROFP")
            {
                tolDirection = DMISTolDirection.PROFS;
            }
            else
            {
                pos_comma_2 = tolerance_buf.IndexOf(",", pos_comma + 1);
                string axis_buf = tolerance_buf.Substring(pos_comma + 1, pos_comma_2 - pos_comma - 1);
                switch (axis_buf.Trim())
                {
                    case "XAXIS":
                        tolDirection = DMISTolDirection.XAXIS;
                        break;
                    case "YAXIS":
                        tolDirection = DMISTolDirection.YAXIS;
                        break;
                    case "ZAXIS":
                        tolDirection = DMISTolDirection.ZAXIS;
                        break;
                    default:
                        throw new ArgumentNullException("unknown tolerance direction");
                }
            }
            string[] value_buf = tolerance_buf.Substring(pos_comma_2 + 1).Split(',');
            if (TType == DMISTType.T)
            {
                tol = new ToleranceValue()
                {
                    LowerTol = double.Parse(value_buf[0]),
                    UpperTol = double.Parse(value_buf[1])
                };
            }
            else
            {

                status = value_buf[1].Trim().CompareTo("INTOL") == 0;
                actual = double.Parse(value_buf[0]);

            }
        }
        #endregion

        #region 派生可见方法
        public string getName()
        {
            return string.Format("{0}<{1}>", TType.ToString(), name);
        }
        public string getType()
        {
            switch (tolDirection)
            {
                case DMISTolDirection.XAXIS:
                    return "TOL/CORTOL,XAXIS";
                case DMISTolDirection.YAXIS:
                    return "TOL/CORTOL,YAXIS";
                case DMISTolDirection.ZAXIS:
                    return "TOL/CORTOL,ZAXIS";
                case DMISTolDirection.PROFS:
                    return "TOL/PROFS";
                default:
                    throw new NotImplementedException("unknown tolerance direction");
            }
        }
        public override string ToString()
        {
            if (TType == DMISTType.T)
            {
                return string.Format("T({0})={1},{2},{3}", name, getType(), tol.LowerTol, tol.UpperTol);
            }
            else

            {
                if (tolDirection == DMISTolDirection.PROFS)
                {
                    return string.Format("TA({0})={1},{2},{3},{4}",
                        name,
                        getType(),
                        actual,
                        actual,
                        status ? "INTOL" : "OUTTOL");
                }
                else
                {
                    return string.Format("TA({0})={1},{2},{3}",
                        name,
                        getType(),
                        actual,
                        status ? "INTOL" : "OUTTOL");
                }
            }
        }
        #endregion

        #region 私有方法 private mehod

        #endregion

        #region 静态成员

        #endregion
    }
}
