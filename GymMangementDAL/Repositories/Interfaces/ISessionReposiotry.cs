using GymMangementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangementDAL.Repositories.Interfaces
{
    internal interface ISessionReposiotry
    {


        IEnumerable<Session> GetAll();


    
        Session? GetbyId(int id);


     
        int Add(Session session);

   
        int Update(Session session);

    

        int Delete(Session session);

    }
}
