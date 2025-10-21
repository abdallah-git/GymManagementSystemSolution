using GymMangementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangementDAL.Repositories.Interfaces
{
    public interface ISessionRepository:IGenareicReposiotry<Session> 
    {


        IEnumerable<Session> GetAllsesionswithtrainersandcategories();

        int GetCountofbookslots(int sessionid); 


        Session? GetSessionByIDwithtrainersandcategories(int id );





    }
}
