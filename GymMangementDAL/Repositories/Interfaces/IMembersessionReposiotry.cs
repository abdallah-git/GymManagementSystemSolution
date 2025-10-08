using GymMangementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangementDAL.Repositories.Interfaces
{
    internal interface IMembersessionReposiotry
    {
        IEnumerable<Membersession> GetAll();



        Membersession? GetbyId(int id);



        int Add(Membersession membersession
            );


        int Update(Membersession membersession);



        int Delete(Membersession membersession);

    }
}
