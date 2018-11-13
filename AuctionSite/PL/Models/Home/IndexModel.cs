using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PL.Models.Home
{
    public class IndexModel
    {

        [Required(ErrorMessage = "Bullshit")]
        [StringLength(10, ErrorMessage = "Kill yourself")]
        public string Search { get; set; }
    }
}