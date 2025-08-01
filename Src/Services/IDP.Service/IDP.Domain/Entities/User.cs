﻿using IDP.Domain.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Domain.Entities
{
    public class User : BaseEntity
    {
        public  string? FullName { get; set; }
        public  string? MobileNumber { get; set; }
        public  string UserName { get; set; }
        public  string? Password { get; set; }
        public  string? Salt { get; set; }
    }
}
