using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapControllerWebApi.Models
{
    public class LakeDefinition
    {
        public int xCoord { get; set; }
        public int yCoord { get; set; }
        public int area { get; set; }
        public LakeDefinition(int x, int y, int a)
        {
            this.xCoord = x;
            this.yCoord = y;
            this.area = a;
        }
        
        public override string ToString()
        {
            return "Lake at - (" + xCoord + ", " + yCoord + "), area - " + area + "\n";
        }


    }
}
