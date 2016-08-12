using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMOBase;

namespace ZeissImporter
{
    public class CalypsoDPResult
    {
        #region 公共方法 public method

        #endregion

        #region 字段 variable
        string _FILNAM;
        DateTime DATE;
        DateTime TIME;
        string OP;
        string DI;
        string DS;
        string DV;
        string PL;
        string UNIT_LENGTH;
        string UNIT_ANGLE;
        string UNIT_TEMP;
        List<OutputDMIS> data;


        #endregion

        #region 属性 properties
        public List<OutputDMIS> Outputs
        {
            get
            {
                return data;
            }
        }
        public string DMESWI
        {
            get
            {
                return DS;
            }
            set
            {
                DS = value;
            }
        }
        public string DMESWV
        {
            get
            {
                return DV;
            }
            set
            {
                DV = value;
            }
        }
        public string PLANID
        {
            get
            {
                return PL;
            }
            set
            {
                PL = value;
            }
        }
        public string DMEID
        {
            get
            {
                return DI;
            }
            set
            {
                DI = value;
            }
        }
        public string Operator
        {
            get
            {
                return OP;
            }
            set
            {
                OP = value;
            }
        }
        public DateTime DATETIME
        {
            get
            {
                return new DateTime(DATE.Year, DATE.Month, DATE.Day, TIME.Hour, TIME.Minute, TIME.Second);
            }
        }
        public string FILNAM
        {
            get
            {
                return _FILNAM;
            }

            set
            {
                _FILNAM = value;
            }
        }
        #endregion

        #region 构造函数 construction
        public CalypsoDPResult(string[] rawdata)
        {
            var l = rawdata.ToList();
            int split_line_number = l.IndexOf(@"$$ ------------------------  RESULT-BLOCKS ---------------------- ");
            int end_line_number = l.IndexOf("ENDFIL");
            ParseHeader(l.GetRange(1, split_line_number - 1));
            ParseOutputs(l.GetRange(split_line_number + 1, end_line_number - split_line_number - 1));
        }


        #endregion

        #region 派生可见方法

        #endregion

        #region 私有方法 private mehod
        private void ParseOutputs(List<string> list)
        {
            data = new List<OutputDMIS>();
            int i = 0;
            for(;i<list.Count;)
            {
                int j;
                for(j=i+1;j<list.Count;++j)
                {
                    if(list[j].StartsWith("OUTPUT"))
                    {
                        data.Add(new OutputDMIS(list.GetRange(i, j - i)));
                        i = j;
                        break;
                    }
                }
                if(j==list.Count)
                {
                    data.Add(new OutputDMIS(list.GetRange(i, j - i)));
                    break;
                }
            }
            //throw new NotImplementedException();
        }

        private void ParseHeader(List<string> list)
        {
            foreach(string s in list)
            {
                
                string fus = s.Substring(0, 2);
                switch (fus)
                {
                    case "$$":
                        //means the line is a comment, ignore, no operation
                        break;
                    case "FI":
                        //filnam
                        int pos_filnam = s.IndexOf('/');
                        FILNAM = s.Substring(pos_filnam+ 2, s.Count() - (pos_filnam + 2) - 1);
                        break;
                    case "DA":
                        //DATE
                        int pos_date = s.IndexOf('=');
                        DATE = DateTime.Parse(s.Substring(pos_date+1));
                        break;
                    case "TI":
                        //TIME
                        int pos_time = s.IndexOf('=');
                        TIME = DateTime.Parse(s.Substring(pos_time+1));
                        break;
                    case "OP":
                        //operid from Calypso, operator
                        int pos_op = s.IndexOf('=');
                        OP = s.Substring(pos_op + 2,s.Count()-pos_op-3);
                        break;
                    case "DI":
                        //dmeid config name of CMM
                        int pos_di = s.IndexOf('=');
                        DI = s.Substring(pos_di + 2, s.Count() - pos_di - 3);
                        break;
                    case "DS":
                        //dmeswi software name, usually it's Calypso
                        int pos_ds = s.IndexOf('=');
                        DS = s.Substring(pos_ds + 2, s.Count() - pos_ds - 3);
                        break;
                    case "DV":
                        //dmeswv software version
                        int pos_dv = s.IndexOf('=');
                        DV = s.Substring(pos_dv + 2, s.Count() - pos_dv - 3);
                        break;
                    case "PL":
                        //planid inspection name
                        int pos_pl = s.IndexOf('=');
                        PL = s.Substring(pos_pl + 2, s.Count() - pos_pl - 3);
                        break;
                    case "UN":
                        string[] unit_strings = s.Substring(6).Split(',');
                        UNIT_LENGTH = unit_strings[0].Trim();
                        UNIT_ANGLE = unit_strings[1].Trim();
                        UNIT_TEMP = unit_strings[2].Trim();
                        //unit length angle temperature
                        break;
                }
            }
        }

        private void ParseData(string[] data)
        {
            //DP format by Calypso
            for (int i = 0; i < data.Count(); ++i)

            {
                string buf = data[i];
                string fus = buf.Substring(0, 2);
                switch (fus)
                {
                    case "$$":
                        //means the line is a comment, ignore, no operation
                        break;
                    case "DA":
                        //DATE
                        break;
                    case "TI":
                        //TIME
                        break;
                    case "OP":
                        //operid from Calypso, operator
                        break;
                    case "DI":
                        //dmeid config name of CMM
                        break;
                    case "DS":
                        //dmeswi software name, usually it's Calypso
                        break;
                    case "DV":
                        //dmeswv software version
                        break;
                    case "PL":
                        //planid inspection name
                        break;
                    case "UN":
                        //unit length angle temperature
                        break;
                    case "OU":
                        //output content
                        break;

                }
            }
        }
        #endregion

        #region 静态成员

        #endregion

    }
}
