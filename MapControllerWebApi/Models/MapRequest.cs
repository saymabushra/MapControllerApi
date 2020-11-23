using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MapControllerWebApi.Models
{
    public class MapRequest
    {
        [Required(ErrorMessage = "Map String is required")]
        [DataType(DataType.Text)]
        public string MapQueryString { get; set; }
    }
}
