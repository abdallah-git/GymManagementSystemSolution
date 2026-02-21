using GymMangementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangementDAL.Repositories.Interfaces
{
    public interface IBookingRepository : IGenareicReposiotry<Membersession> 
    {

        IEnumerable<Membersession> GetSessionById(int sessionid); 




    }
}
