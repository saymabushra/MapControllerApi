using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using MapControllerWebApi.Models;
using MapControllerWebApi.Services;
using Microsoft.AspNetCore.Cors;
using System.Web.Http.Description;
using MapControllerWebApi.Filters;
using Swashbuckle.Swagger.Annotations;


namespace MapControllerWebApi.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class MapController : ControllerBase
    {
        private ILakeFinder _lakeFinder;
       

        public MapController(ILakeFinder lakeFinder)
        {
            this._lakeFinder = lakeFinder;
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapRequest"> request object encapsulating map data string</param>
        /// <returns></returns>
        [Route("api/v{version:apiVersion}/LakeArea")]
        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage<List<LakeDefinition>>), 200)]
        [ProducesResponseType(typeof(ResponseMessage<List<string>>), 500)]
        [ProducesResponseType(typeof(ResponseMessage<List<string>>), 400)]
        [EnableCors("AllowAll")]
        [ValidateModel]
        public IActionResult GetLakeArea(MapRequest mapRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(mapRequest.MapQueryString))
                {
                    var responesMessage = new ResponseMessage<string>();
                    responesMessage.Message = ApiConstants.BAD_INPUT_MESSAGE;
                    return BadRequest(responesMessage);
                }
                return Ok(this._lakeFinder.FindLakeAreas(mapRequest.MapQueryString));
            }
            catch (NullReferenceException exception)
            {
                var nullpointerException = new ResponseMessage<string>();
                nullpointerException.Message = ApiConstants.NULL_POINTER_EXCEPTION_OCCURED;
                nullpointerException.Code = 500;
                nullpointerException.Result = exception.Message;
                return BadRequest(nullpointerException);
            }

            catch (Exception exception)
            {
                var generalException = new ResponseMessage<string>();
                generalException.Message = ApiConstants.UNHANDELED_EXCEPTION_MESSAGE;
                generalException.Code = 500;
                generalException.Result = exception.Message;
                return BadRequest(generalException);
            }

        }
    }


    
}
