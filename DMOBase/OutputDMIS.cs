using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMOBase
{
    public class OutputDMIS
    {
        ElementFDMIS elementf;
        List<ElementTDMIS> elementts;
        public ElementFDMIS ElementF
        {
            get
            {
                return elementf;
            }
            set
            {
                elementf = value;
            }
        }
        public List<ElementTDMIS> ElementsT
        {
            get
            {
                if (elementts == null)
                    elementts = new List<ElementTDMIS>();
                return elementts;
            }
            set
            {
                elementts = value;
            }
        }
        public List<ElementTDMIS> ElementsTSorted
        {
            get
            {
                return ElementsT.OrderBy(n => n.TolDirection).ToList();
            }
        }
        public string FeatureName
        {
            get
            {
                return elementf.Name;
            }
        }
        public override string ToString()
        {
            System.IO.StringWriter sw = new System.IO.StringWriter();
            sw.Write("OUTPUT/");
            sw.Write(ElementF.getName());
            foreach(var e in ElementsTSorted)
            {
                sw.Write(",{0}", e.getName());
            }
            sw.WriteLine();
            sw.WriteLine(ElementF.ToString());
            foreach(var e in ElementsTSorted)
            {
                sw.WriteLine(e.ToString());
            }
            return sw.ToString();
        }
        public OutputDMIS(List<string> data)
        {
            elementts = new List<ElementTDMIS>();
            int pos_splash = data[0].IndexOf('/');
            List<string> names = data[0].Substring(pos_splash + 1).Split(',').ToList();
            for (int i = 1; i < data.Count; ++i)
            {
                int pos_bracket = data[i].IndexOf('(');
                string type = data[i].Substring(0, pos_bracket);
                switch (type)
                {
                    case "F":
                    case "FA":
                        string feature_buf;
                        if (data[i].EndsWith("$"))
                        {
                            feature_buf = data[i].Substring(0, data[i].Count() - 1) + data[i + 1];
                            i++;
                        }
                        else
                        {
                            feature_buf = data[i];
                        }
                        elementf = new ElementFDMIS(feature_buf);
                        break;
                    case "T":
                    case "TA":
                        elementts.Add(new ElementTDMIS(data[i]));
                        break;
                }
            }
        }

        public OutputDMIS()
        {
        }
    }
}
