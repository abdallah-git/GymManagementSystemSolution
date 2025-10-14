using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangementDAL.Entities
{
    public abstract class BaseEntity
    {


        public int Id { get; set; }

        public DateOnly CreatedAt { get; set; } 


        public DateTime UpdatedAt { get; set; } 


    }
}
