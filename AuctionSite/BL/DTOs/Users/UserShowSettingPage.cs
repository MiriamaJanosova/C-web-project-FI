using System.ComponentModel.DataAnnotations;
using BL.DTOs.Common;
using DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BL.DTOs.Users
/////////////////////////////////////////
/// Kdyz user vleze do nastaveni uctu ///
/////////////////////////////////////////

{
    public class UserShowSettingPage : DtoBase
    {
        
        public string UserName { get; set; }
        
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}