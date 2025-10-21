using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangementDAL.Entities
{
    // 1-1 relationship with member 
    public class Healthrecord : BaseEntity 
    {
        // id at haelth is forignkey that refer to pk at table member 
        public decimal Hieght { get; set; }
        public decimal Weight { get; set; }

        public string BloodType { get; set; } = null!; 

        public string Note { get; set; }

       
    }
}
