using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangementDAL.Entities
{
    public class Membership : BaseEntity
    {
        // start date inhert from base i will changename by fluent api 
       public DateTime EndDate { get; set; } 



        public string Statues
        {
            get
            {
                if (EndDate >= DateTime.Now)

                    return "expired";
                else
                    return "active"; 
                    
                
            }
        }

        public int MemberId { get; set; } 
        public Member Member { get; set; } = null!;


        public int PlanId  { get; set; }
        public Plan Plan { get; set; } = null!;






    }
}
