using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMOBase;
using ZeissImporter;
using System.IO;

namespace VolvoDMOOutput
{
    public class VolvoDMOResult
    {
        #region 公共方法 public method
        static public VolvoDMOResult ParseCalypsoDP(CalypsoDPResult dp)
        {
            VolvoDMOResult res = new VolvoDMOResult();
            res.FILNAM = dp.FILNAM;
            res.DATETIME = dp.DATETIME;
            res.partName = dp.PLANID;
            res.SW_name_version = dp.DMESWI + dp.DMESWV;
            res.contactor = dp.Operator;
            res.note = "note";

            res.data = dp.MergedOutputs;

            return res;
        }

        string getHeader()
        {
            StringWriter sw = new StringWriter();
            sw.WriteLine("FILNAME/'{0}'", FILNAM);
            sw.WriteLine("TEXT / OUTFIL, '{0}'", partName);
            sw.WriteLine("TEXT / OUTFIL, '{0}'", SW_name_version);
            sw.WriteLine("TEXT / OUTFIL, '{0}'", contactor);
            sw.WriteLine("TEXT / OUTFIL, '{0}'", note);
            sw.WriteLine();
            sw.WriteLine("DATE={0}", DATE.ToString("yyyy/MM/dd"));
            sw.WriteLine("TIME={0}", TIME.ToString("hh:mm:ss"));
            sw.WriteLine("PR(1)='{0}'", PR);
            sw.WriteLine("LI(2)='{0}'", LI);
            sw.WriteLine("PL(3)='{0}'", PL);
            sw.WriteLine("PN(4)='{0}'", PN);
            sw.WriteLine("PS(5)='{0}'", PS);
            sw.WriteLine("Q(6)='{0}'", Q);

            return sw.ToString();
        }
        string getOutputs()
        {
            StringWriter sw = new StringWriter();
            foreach (var o in data)
            {
                sw.WriteLine(o.ToString());
            }
            return sw.ToString();
        }

        string getEnd()
        {
            return "ENDFIL";
        }
        #endregion
        public new string ToString()
        {
            return getHeader() + getOutputs() + getEnd();
        }
        #region 字段 variable
        string _FILNAM;
        DateTime DATE;
        DateTime TIME;
        string partName;    //for text
        string SW_name_version;
        string contactor;
        string note;

        List<OutputDMIS> data;


        #endregion

        #region 属性 properties
        public string PR { get; set; }
        public string LI { get; set; }
        public string PL { get; set; }
        public string PN { get; set; }
        public string PS { get; set; }
        public string Q { get; set; }
        public DateTime DATETIME
        {
            set
            {
                DATE = value;
                TIME = value;
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

        #endregion

        #region 派生可见方法

        #endregion

        #region 私有方法 private mehod

        #endregion

        #region 静态成员

        #endregion
    }
}
