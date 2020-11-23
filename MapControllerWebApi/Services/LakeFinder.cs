using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapControllerWebApi.Models;
using MapControllerWebApi.ServiceHandeler;
namespace MapControllerWebApi.Services
{
    public class LakeFinder:ILakeFinder
    {
        public int FillLake(char[][] wb, int x, int y)
        {
            int ret = 1;
            wb[x][y] = '#';
            if (x > 0 && wb[x - 1][y] == 'O') ret += FillLake(wb, x - 1, y);
            if (x < wb.Length - 1 && wb[x + 1][y] == 'O') ret += FillLake(wb, x + 1, y);
            if (y > 0 && wb[x][y - 1] == 'O') ret += FillLake(wb, x, y - 1);
            if (y < wb[0].Length - 1 && wb[x][y + 1] == 'O') ret += FillLake(wb, x, y + 1);
            return ret;
        }

        public List<LakeDefinition> FindLake(char[][] surface)
        {
            List<LakeDefinition> lakeDefinitionList = new List<LakeDefinition>();
            for (int i = 0; i < surface.Length; i++)
            {
                for (int j = 0; j < surface[0].Length; j++)
                {
                    if (surface[i][j] == 'O')
                    {
                        lakeDefinitionList.Add(new LakeDefinition(i, j, FillLake(surface, i, j)));
                    }
                }
            }
            return lakeDefinitionList;
        }

        public ResponseMessage<List<LakeDefinition>> FindLakeAreas(string mapInput)
        {
            // split the data into two dimensional array . to process them with an algorithm
            // split the input by new line
            string[] splittedArrayRows = mapInput.Split('\n');

            // contrust response object which will be sent as result
            var response = new ResponseMessage<List<LakeDefinition>>();

            //Check if the input is valid and contains data as two dimensional arrays
            if (splittedArrayRows==null || splittedArrayRows.Length == 0 || splittedArrayRows[0] == null || Array.Exists(splittedArrayRows, element => element == "0"))
            {
                response.MessageType = EnumMessageType.Error;
                response.Message = "The input provided is not valid.Please Provide Valid input data";
            }
            else
            {
                var lakes = this.FindLake(Utils.ConvertToTwoDimArray(splittedArrayRows));

                if (lakes != null && lakes.Count > 0)
                {
                    // there is valid lake area data 
                    response.MessageType = EnumMessageType.Success;
                    response.Message = ApiConstants.LAKE_DATA_FOUND;
                    response.Result = lakes;
                }

                else
                {
                    response.MessageType = EnumMessageType.Warning;
                    response.Message = ApiConstants.NO_APPROPRIATE_LAKE_FOUND_MESSAGE;
                    response.Result = lakes;

                }
            }
            return response;
        }

    }
}
