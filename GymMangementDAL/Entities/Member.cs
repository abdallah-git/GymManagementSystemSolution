using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangementDAL.Entities
{
    public class Member : Gymuser
    {

        // joindate == createdat of baseentity 

        public string? photo { get; set; }

        #region Relationships 

        #region member - Healthrecord 

        public Healthrecord Healthrecord { get; set; } = null!;



        #endregion

        #region member - membership 

        public ICollection<Membership> Memberships { get; set; } = null!;

        #endregion


        #region member - membersession

        public ICollection<Membersession> Membersessions { get; set; } = null!; 



        #endregion





        #endregion




    }
}



