using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapControllerWebApi.Models
{
    public class ApiConstants
    {
        public static string API_MODEL_ERROR = "Required parameter can not be null or empty";
        public static string ALLOW_ALL_CORS = "AllowAll";


        #region Response Messages
        public static string UNHANDELED_EXCEPTION_MESSAGE = "Unhandled Exception Occoured.Please review.";
        public static string NULL_POINTER_EXCEPTION_OCCURED = "Null pointer exception happened.Please check your input and algorithm.";
        public static string BAD_INPUT_MESSAGE = "Provide valid input";
        public static string NO_APPROPRIATE_LAKE_FOUND_MESSAGE = "No Appropriate lake data found in the given input";
        public static string LAKE_DATA_FOUND = "Lake data found";

        #endregion
    }
}
