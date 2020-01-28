﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Authentication.Models
{
    public class LoggedInUserModel : ILoggedInUserModel
    {
        public int Id { get; set; }
        public string Token { get; set; }
    }
}
