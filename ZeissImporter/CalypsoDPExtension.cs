using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMOBase;

namespace ZeissImporter
{
    public static class CalypsoDPExtension
    {
        static public List<OutputDMIS> MergeDPOutputs(this List<OutputDMIS> datas)
        {
            List<string> names = (from u in datas
                                  group u by u.FeatureName into g
                                  select g.Key).ToList();
            List<OutputDMIS> res = new List<OutputDMIS>();
            foreach(var s in names)
            {
                var query_F = from u in datas
                            where u.FeatureName == s && u.ElementF.Ftype == DMOBase.DMISFType.F
                            select u;
                OutputDMIS entry_F = new OutputDMIS();
                entry_F.ElementF = query_F.First().ElementF;
                foreach(var e in query_F)
                {
                    entry_F.ElementsT.AddRange(e.ElementsT);
                }
                res.Add(entry_F);

                var query_FA = from u in datas
                              where u.FeatureName == s && u.ElementF.Ftype == DMOBase.DMISFType.FA
                              select u;
                OutputDMIS entry_FA = new OutputDMIS();
                entry_FA.ElementF = query_FA.First().ElementF;
                foreach (var e in query_FA)
                {
                    entry_FA.ElementsT.AddRange(e.ElementsT);
                }
                res.Add(entry_FA);

            }
            return res;
        }
    }
}
