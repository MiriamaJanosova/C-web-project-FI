using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BL.DTOs.Base;

namespace PL.Models.Users
{
    public class UserListModel
    {
        public UserListModel(IEnumerable<UserDto> users)
        {
            Users = users;
        }

        public IEnumerable<UserDto> Users { get; }

        public string GetAverageReviewString(UserDto dto)
        {
            var c = dto.Reviews.Count;
            if (c == 0)
                return "no reviews";
            return $"{dto.ReviewAvg} from {c} reviews";
        }
    }
}