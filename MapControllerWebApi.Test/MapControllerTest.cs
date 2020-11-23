using Microsoft.VisualStudio.TestTools.UnitTesting;
using MapControllerWebApi;
using MapControllerWebApi.ServiceHandeler;
using System.Collections.Generic;
using System;
using MapControllerWebApi.Controllers;
using MapControllerWebApi.Services;
using MapControllerWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Reflection;

namespace MapControllerWebApi.Test
{
    [TestClass]
    public class MapControllerTest
    {
        MapController mapController = new MapController(new LakeFinder());

        [TestCategory("MapArea")]
        [TestMethod]
        public void Test_MapAreaWithValidInput() 
        {
            var mapAreaRequest = new MapRequest();
            mapAreaRequest.MapQueryString = "#######\n##O##O#\n#OO#OO#\n###O#O#\n#OOOO##\nO#O#OO#\nOO#O###";
            var lakeInfoResult = mapController.GetLakeArea(mapAreaRequest);
            var okResult = lakeInfoResult as OkObjectResult;
            var value = okResult.Value;
            PropertyInfo pi = value.GetType().GetProperty("Result");
            var result = (pi.GetValue(value, null));
            IList mapCollection = (IList)result;

            List<LakeDefinition> expected = new List<LakeDefinition>();
            expected.Add(new LakeDefinition(1, 2, 3));
            expected.Add(new LakeDefinition(1, 5, 4));
            expected.Add(new LakeDefinition(3, 3, 8));
            expected.Add(new LakeDefinition(5, 0, 3));
            expected.Add(new LakeDefinition(6, 3, 1));

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200,okResult.StatusCode);
            Assert.AreEqual(5,mapCollection.Count);
        }

        [TestCategory("MapArea")]
        [TestMethod]
        public void Test_NoSurfaceLakeAreaWhileInputOnlyLand() 
        {
            var mapAreaRequest = new MapRequest();
            mapAreaRequest.MapQueryString = "#######\n#######\n#######";
            var lakeInfoResult = mapController.GetLakeArea(mapAreaRequest);
            var okResult = lakeInfoResult as OkObjectResult;
            var value = okResult.Value;
            PropertyInfo pi = value.GetType().GetProperty("Message");
            string message = (string)(pi.GetValue(value, null));
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual("No Appropriate lake data found in the given input", message);
        }

        [TestCategory("MapArea")]
        [TestMethod]
        public void Test_MapAreaWithEmptyInput()  
        {
            var mapAreaRequest = new MapRequest();
            mapAreaRequest.MapQueryString = "";
            var lakeInfoResult = mapController.GetLakeArea(mapAreaRequest);
            var okResult = lakeInfoResult as ObjectResult;
            var value = okResult.Value;
            PropertyInfo pi = value.GetType().GetProperty("Message");
            string message = (string)(pi.GetValue(value, null));
            Assert.IsNotNull(okResult);
            Assert.AreEqual(400, okResult.StatusCode);
            Assert.AreEqual("Provide valid input", message);

        }

        [TestCategory("MapArea")]
        [TestMethod]
        public void Test_MapAreaWithNullInput()
        {
            var mapAreaRequest = new MapRequest();
            mapAreaRequest.MapQueryString = null;
            var lakeInfoResult = mapController.GetLakeArea(mapAreaRequest);
            var Result = lakeInfoResult as ObjectResult;
            var value = Result.Value;
            PropertyInfo pi = value.GetType().GetProperty("Message");
            string message = (string)(pi.GetValue(value, null));
            Assert.IsNotNull(Result);
            Assert.AreEqual(400, Result.StatusCode);
            Assert.AreEqual("Provide valid input", message);
        }

        [TestCategory("MapArea")]
        [TestMethod]
        public void Test_MapAreaWithWrongInput_NoLake_NoLand()   
        {
            var mapAreaRequest = new MapRequest();
            mapAreaRequest.MapQueryString = "asdertgh";
            var lakeInfoResult = mapController.GetLakeArea(mapAreaRequest);
            var Result = lakeInfoResult as OkObjectResult;
            var value = Result.Value;
            PropertyInfo pi = value.GetType().GetProperty("Message");
            string message = (string)(pi.GetValue(value, null));
            Assert.IsNotNull(Result);
            Assert.AreEqual(200,Result.StatusCode);
            Assert.AreEqual("No Appropriate lake data found in the given input", message);

        }

        [TestCategory("MapArea")]
        [TestMethod]
        public void Test_MapAreaWithInvalidInput_IncludingLakeAndLand()
        {
            List<LakeDefinition> lakeInfo = new List<LakeDefinition>();
            var mapAreaRequest = new MapRequest();
            mapAreaRequest.MapQueryString = "asdfgg#O";
            var lakeInfoResult = mapController.GetLakeArea(mapAreaRequest);
            var okResult = lakeInfoResult as OkObjectResult;
            var value = okResult.Value;
            PropertyInfo pi = value.GetType().GetProperty("Message");
            string message = (string)(pi.GetValue(value, null));
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual("No Appropriate lake data found in the given input", message);
        }

    }
}
