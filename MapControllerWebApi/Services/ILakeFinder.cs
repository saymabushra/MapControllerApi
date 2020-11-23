using MapControllerWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapControllerWebApi.Services
{
    public interface ILakeFinder
    {
        public List<LakeDefinition> FindLake(char[][] surface);
        public int FillLake(char[][] wb, int x, int y);

        public ResponseMessage<List<LakeDefinition>> FindLakeAreas(string mapInput);


    }
}
