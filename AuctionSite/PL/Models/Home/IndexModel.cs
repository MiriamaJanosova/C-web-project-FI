using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PL.Models.Home
{
    public class IndexModel
    {

        [Required(ErrorMessage = "Required")]
        [StringLength(10, ErrorMessage = "Too short")]
        public string Search { get; set; }
    }
}