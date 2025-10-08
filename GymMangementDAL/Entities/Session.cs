using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangementDAL.Entities
{
    public class Session:BaseEntity
    {

        public string Descreption { get; set; } = null!;

        public int Capacity { get; set; } 

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        #region relationships 
        #region session - category 

        public int CategoryId { get; set; } 

        public Category sessioncategory { get; set; } = null!;

        #endregion


        #region Trainer - session
        public int TrainerId { get; set; } 

        public Trainer SessionTrainer { get; set; } = null!;


        #endregion


        #region session - membersession 

        public ICollection<Membersession> Sessionmembers { get; set; } = null!;


        #endregion 



        #endregion


    }
}
