using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using DMOBase;

namespace ZeissImporter
{

    public class DPImporter : IImporter
    {
        #region 公共方法 public method

        #endregion

        #region 字段 variable
        string rawdata;

        #endregion

        #region 属性 properties
        public CalypsoDPResult DPResult
        {
            get;
            set;
        }
        #endregion

        #region 构造函数 construction
        public DPImporter(string path)
        {
            rawdata = System.IO.File.ReadAllText(path);
            string[] data = rawdata.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            DPResult = new CalypsoDPResult(FormatDP(data));
        }

        #endregion

        #region 派生可见方法

        #endregion

        #region 私有方法 private mehod
        private string[] FormatDP(string[] data)
        {
            List<string> res = new List<string>();
            foreach (var s in data)
            {
                if (!isNullEmptyComment(s))
                {
                    res.Add(s);
                }
            }
            return res.ToArray();
        }

        private bool isNullEmptyComment(string s)
        {
            if (string.IsNullOrEmpty(s))
                return true;
            //if (s.StartsWith("$$"))
            //    return true;
            Regex re = new Regex(@"^\s*$");
            if (re.IsMatch(s))
                return true;
            return false;
        }


        #endregion

        #region 静态成员

        #endregion


    }
}
