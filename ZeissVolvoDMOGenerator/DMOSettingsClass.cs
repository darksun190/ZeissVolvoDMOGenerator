using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace ZeissVolvoDMOGenerator
{
    public class dic_data_class : List<DMOSettingsClass>
    {
        public List<string> Keys
        {
            get
            {
                return this.Select(n => n.PLANID).ToList();
            }
        }
        public void Remove(string s)
        {
            if (this.Keys.Contains(s))
            {
                var query = from u in this
                            where u.PLANID == s
                            select u;
                this.Remove(query.First());
            }
            else

            {
                return;
            }
        }
        
        public DMOSettingsClass this[string s]
        {
            get
            {
                if (this.Keys.Contains(s))
                {
                    return this.Where(n => n.PLANID == s).First();
                }
                return null;
            }
        }
    }
    public class DMOSettingsClass
    {
        public string PLANID
        {
            get;
            set;
        }
        public string PR
        {
            get;
            set;
        }
        public string LI
        {
            get;
            set;
        }
        public string PL
        {
            get;
            set;
        }
        public string PN
        {
            get;
            set;
        }
        public string PS
        {
            get;
            set;
        }
        public string Q
        {
            get;
            set;
        }
    }
}
