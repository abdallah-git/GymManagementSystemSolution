using GymMangementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangementDAL.Repositories.Interfaces
{
    internal interface IMembershipReposiotry
    {
        IEnumerable<Membership> GetAll();



        Membership? GetbyId(int id);



        int Add(Membership membership
            );


        int Update(Membership membership);



        int Delete(Membership membership);


    }
}
