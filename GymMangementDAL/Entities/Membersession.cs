﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangementDAL.Entities
{
    public class Membersession : BaseEntity
    {
        public bool IsAttend { get; set; }
        public int MemberId { get; set; } 
        public Member Member { get; set; } = null!; 

        public int SessionId { get; set; } 
        public Session Session { get; set; } = null!; 
    }
}
