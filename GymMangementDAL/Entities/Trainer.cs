using GymMangementDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangementDAL.Entities
{
    public class Trainer : Gymuser
    {


        // Hiringdate is createdat but change name of column by fluent api 


        public Specialties Specialties { get; set; }


        public ICollection<Session> TrainerSessions { get; set; } = null!; 


    }
}
