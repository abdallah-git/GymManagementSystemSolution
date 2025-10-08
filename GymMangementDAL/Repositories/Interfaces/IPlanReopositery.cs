using GymMangementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangementDAL.Repositories.Interfaces
{
    internal interface IPlanReopositery
    {

        IEnumerable<Plan> GetAll();


       
        Plan? GetbyId(int id);


     
        int Add(Plan plan
            );

      
        int Update(Plan plan);

       

        int Delete(Plan plan);
    }
}
