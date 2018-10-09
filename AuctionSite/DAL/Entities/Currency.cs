﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Currency : IEntity
    {
        public int ID { get; set; }
        [MaxLength(5)]
        public string Code  { get; set; }
        public double ExchangeRate { get; set; }
    }
}
