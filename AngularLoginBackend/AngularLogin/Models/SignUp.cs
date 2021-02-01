﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngularLogin.Models
{
    public class SignUp
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        [Key]
        public string userName { get; set; }
        public string password { get; set; }
    }
}