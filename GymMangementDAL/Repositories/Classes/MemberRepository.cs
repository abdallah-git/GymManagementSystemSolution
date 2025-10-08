using GymMangementDAL.Data.Contexts;
using GymMangementDAL.Entities;
using GymMangementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangementDAL.Repositories.Classes
{
    internal class MemberRepository : IMemberRepository

    {
        private readonly GymDbcontext dbcontext1 = new GymDbcontext(); 
        public MemberRepository(GymDbcontext dbcontext)
        {

            this.dbcontext1 = dbcontext; 
        }
        public int Add(Member member)
        {
            dbcontext1.Members.Add(member);
            return dbcontext1.SaveChanges();
        }

        public int Delete(int Id)
        {
            var Member = dbcontext1.Members.Find(Id);
            if (Member == null) return 0;
            dbcontext1.Members.Remove(Member);
             return dbcontext1.SaveChanges(); 
        }

        public IEnumerable<Member> GetAll()
        {
           return dbcontext1.Members.ToList(); 
        }

        public Member? GetbyId(int id)
        {
            return dbcontext1.Members.Find(id); 
        }

        public int Update(Member member)
        {
            dbcontext1.Members.Update(member);
            return dbcontext1.SaveChanges(); 



        }
    }
}
