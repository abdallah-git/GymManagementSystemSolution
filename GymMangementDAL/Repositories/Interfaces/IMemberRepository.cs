using GymMangementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangementDAL.Repositories.Interfaces
{
    internal interface IMemberRepository
    {

        //Getall

        IEnumerable<Member> GetAll();


        //Getbyid
        Member? GetbyId(int id);


        //Add
        int Add(Member member);

        //Update
        int Update(Member member);

        //Delete

        int Delete(int Id);




    }
}
