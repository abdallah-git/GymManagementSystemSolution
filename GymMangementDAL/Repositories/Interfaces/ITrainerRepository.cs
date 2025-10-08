using GymMangementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangementDAL.Repositories.Interfaces
{
    internal interface ITrainerRepository
    {

        IEnumerable<Trainer> GetAll();

        Trainer GetTrainerbyId(int id); 


        int Add(Trainer trainer);

        int Update(Trainer trainer);


        int Delete(Trainer trainer ); 






    }
}
