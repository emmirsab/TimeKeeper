﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models
{
    public class MemberModel
    {
        public string Role { get; set; }
        public string Employee { get; set; }
        public decimal Hours { get; set; }
    }
}