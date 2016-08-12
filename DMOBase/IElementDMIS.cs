using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMOBase
{
    interface IElementDMIS
    {

        string ToString();
        string getType();
        string getName();

    }

    public enum DMISFeatureType
    {
        POINT
    }
    
    public enum DMISFType
    {
        F,
        FA
    }
    public enum DMISTType
    {
        T,
        TA
    }
    public enum DMISTolDirection : int
    {
        XAXIS=0,
        YAXIS=1,
        ZAXIS=2,
        PROFS=4
    }
}
