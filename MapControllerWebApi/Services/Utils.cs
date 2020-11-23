using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapControllerWebApi.ServiceHandeler
{
    public class Utils
    {
        public static char[][] ConvertToTwoDimArray(string[] strArr)
        {
            char[][] ret = new char[strArr.Length][];
            for (int i = 0; i < strArr.Length; i++)
            {
                ret[i] = strArr[i].ToCharArray();
            }
            return ret;
        }
    }
}
